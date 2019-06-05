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
using Application.Dto;

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

        private string genericErrorMsg = "Something went wrong on the server.";

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
            catch (EntityNotFoundException ex) {
                return NotFound(ex.Message);
            }
            catch(Exception) {
                return StatusCode(500, genericErrorMsg);
            }
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserDto dto)
        {
            try
            {
                _editUser.Execute(dto, id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (EntityAlreadyExistsException ex) {
                return Conflict(ex.Message);
            }
            catch
            {
                return StatusCode(500, genericErrorMsg);
            }
        }

        // POST: api/Users
        [HttpPost]
        public IActionResult Post([FromBody] UserDto dto)
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
            catch (EntityAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (EntityBadFormatException ex) {
                return BadRequest(ex.Message);
            }
            catch(Exception)
            {
                return StatusCode(500, genericErrorMsg);
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
            catch (EntityNotFoundException ex) {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(500, genericErrorMsg);
            }
        }

    }
}
