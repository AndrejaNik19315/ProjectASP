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
        private readonly ProjectContext _context;

        private IGetUserCommand _getOneCommand;
        private IGetUsersCommand _getCommand;

        public UsersController(IGetUserCommand getOneCommand, IGetUsersCommand getCommand)
        {
            _getCommand = getCommand;
            _getOneCommand = getOneCommand;
        }

        // GET: api/users
        [HttpGet]
        public IActionResult Get([FromQuery]UserSearch query)
        {
            var users = _getCommand.Execute(query);
            return Ok(users);
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try {
                var user = _getOneCommand.Execute(id);
                return Ok(user);
            }
            catch (EntityNotFoundException) {
                return NotFound();
            }
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public IActionResult PutUser(int id, [FromBody] UserDto dto)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            if (_context.Users.Any(u => u.Username == dto.Username || u.Email == dto.Email)) {
                return Conflict("User with that username or email already exists");
            }

            try
            {
                _context.SaveChanges();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Something went wrong.");
            }
        }

        // POST: api/Users
        [HttpPost]
        public IActionResult PostUser([FromBody] UserDto dto)
        {
            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                Firstname = dto.Firstname,
                Lastname = dto.Lastname,
                IsActive = dto.IsActive,
            };

            _context.Users.Add(user);

            try
            {
                _context.SaveChanges();

                return Created("/api/users/" + user.Id, new UserDto {
                    Id = user.Id,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Username = user.Username, 
                    Email = user.Email,
                    IsActive = user.IsActive
                });
            }
            catch {
                return StatusCode(500, "Something went wrong.");
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            try
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }

    }
}
