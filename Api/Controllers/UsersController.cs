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
using Application.Dto.Users;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
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

        /// <summary>
        /// Returns all Users according to search parameters
        /// </summary>
        // GET: api/users
        [HttpGet]
        public ActionResult<IEnumerable<PartialUserDto>> Get([FromQuery]UserSearch query)
        {
            var users = _getUsers.Execute(query);
            return Ok(users);
        }

        /// <response code="404">User doesn't exist</response>   
        /// <response code="500">Server error.</response>
        /// <summary>
        /// Returns single User by id
        /// </summary>

        // GET: api/users/5
        [HttpGet("{id}")]
        public ActionResult<FullUserDto> Get(int id)
        {
            try {
                var user = _getUser.Execute(id);
                return Ok(user);
            }
            catch (EntityNotFoundException ex) {
                return NotFound(ex.Message);
            }
            catch (Exception) {
                return StatusCode(500, genericErrorMsg);
            }
        }

        /// <response code="204">No content</response>
        /// <response code="400">Bad format of user.</response>
        /// <response code="404">User deosn't exist.</response>
        /// <response code="409">Conflict, User with that Email or Username already exists.</response>
        /// <response code="422">Format of the request is good but there are one or more invalid values.</response>
        /// <response code="500">Server error.</response>
        /// <summary>
        /// Updates user
        /// </summary>
        /// <remarks>
        /// PUT Example
        /// {
        ///     "firstname" : "Jane",
        ///     "lastname" : "Doe",
        ///     "username" : "Jenny",
        ///     "email" : "jane@gmail.com",
        ///     "password" : "jane1",
        ///     "isActive" : true ,
        ///     "roleId" : 2
        /// }
        /// </remarks>

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
            catch (EntityAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (EntityUnprocessableException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
            catch
            {
                return StatusCode(500, genericErrorMsg);
            }
        }

        /// <response code="201">Created</response>
        /// <response code="400">Bad format of user.</response>
        /// <response code="409">User with that Email or Username already exists.</response>
        /// <response code="422">Format of the request is good but there are one or more invalid values.</response>
        /// <response code="500">Server error.</response>
        /// <summary>
        /// Create user
        /// </summary>
        ///<remarks>
        /// POST Example
        /// {
        ///     "firstname" : "Jane",
	    ///     "lastname" : "Doe",
	    ///     "username" : "Jane",
	    ///     "email" : "jane@gmail.com",
	    ///     "password" : "jane1",
	    ///     "isActive" : true ,
	    ///     "roleId" : 1
        /// }
        /// </remarks>

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
            catch (Exception)
            {
                return StatusCode(500, genericErrorMsg);
            }
        }

        /// <response code="204">No content</response>
        /// <response code="404">User doesn't exist.</response>
        /// <response code="500">Server error.</response>
        /// <summary>
        /// Removes user by id
        /// </summary>
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

        //LOGIN Method
        //[HttpPost("login")]
        //public IActionResult Login([FromBody] LoginDto dto) {
        //    //
        //}
    }
}
