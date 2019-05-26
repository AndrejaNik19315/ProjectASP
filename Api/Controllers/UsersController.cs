using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using EFDataAccess;
using Application.Commands.Users;
using Application.Searches;
using Application.Exceptions;
using Api.DataTransfer;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ProjectContext _context = new ProjectContext();

        private readonly IGetUserCommand _getUser;
        private readonly IGetUsersCommand _getUsers;
        private readonly IEditUserCommand _editUser;
        private readonly IAddUserCommand _addUser;
        private readonly IDeleteUserCommand _deleteUser;

        public UsersController(IGetUserCommand getUser, IGetUsersCommand getUsers, IEditUserCommand editUser, IAddUserCommand addUser, IDeleteUserCommand deleteUser)
        {
            _getUser = getUser;
            _getUsers = getUsers;
            _editUser = editUser;
            _addUser = addUser;
            _deleteUser = deleteUser;
        }

        // GET: api/users
        [HttpGet]
        public IActionResult Get([FromQuery]UserSearch query)
        {
            var users = _getUsers.Execute(query);
            return Ok(users);
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try {
                var user = _getUser.Execute(id);
                return Ok(user);
            }
            catch (EntityNotFoundException) {
                return NotFound();
            }
            catch {
                return StatusCode(500, "Something went wrong on the server.");
            }
        }

        // PUT: api/Users/5
        //TODO Figure out how to send changes only
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Application.Dto.UserDto dto)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound("User with specified Id, does not exist.");
            }

            if (_context.Users.Any(u => u.Username == dto.Username || u.Email == dto.Email)) {
                return Conflict("User with that username or email already exists");
            }

            try
            {
                _editUser.Execute(dto);
                return NoContent();
            }
            catch
            {
                return StatusCode(500, "Something went wrong.");
            }
        }

        // POST: api/Users
        [HttpPost]
        public IActionResult Post([FromBody] Application.Dto.UserDto dto)
        {
            try
            {
                _addUser.Execute(dto);

                return Created("/api/users/" + dto.Id, new UserDto
                {
                    Id = dto.Id,
                    Firstname = dto.Firstname,
                    Lastname = dto.Lastname,
                    Username = dto.Username,
                    Email = dto.Email,
                    IsActive = dto.IsActive
                });
            }
            catch (EntityAlreadyExistsException)
            {
                return Conflict("User with that username or email already exists.");
            }
            catch (EntityBadFormatException) {
                return BadRequest("User out of format.");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _deleteUser.Execute(id);
                return NoContent();
            }
            catch (EntityNotFoundException) {
                return NotFound("User not found");
            }
            catch
            {
                return StatusCode(500, "Something went wrong on the server.");
            }
        }

    }
}
