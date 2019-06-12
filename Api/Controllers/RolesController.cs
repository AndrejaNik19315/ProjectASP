using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using EFDataAccess;
using Application.Commands.Roles;
using Application.Searches;
using Application.Exceptions;
using Application.Dto;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IGetRolesCommand _getRoles;
        private readonly IGetRoleCommand _getRole;

        private readonly string genericErrorMsg = "Something went wrong on the server.";

        public RolesController(IGetRolesCommand getRoles, IGetRoleCommand getRole)
        {
            _getRoles = getRoles;
            _getRole = getRole;
        }

        /// <summary>
        /// Returns all roles
        /// </summary>
        // GET: api/Roles
        [HttpGet]
        public ActionResult<IEnumerable<RoleDto>> Get([FromQuery] RoleSearch query)
        {
            return Ok(_getRoles.Execute(query));
        }

        /// <response code="404">Role not found</response>
        /// <response code="500">Server error.</response>
        /// <summary>
        /// Returns single role by id
        /// </summary>
        // GET: api/Roles/5
        [HttpGet("{id}")]
        public ActionResult<RoleDto> Get(int id)
        {
            try {
                var role = _getRole.Execute(id);
                return Ok(role);
            }
            catch (EntityNotFoundException ex) {
                return NotFound(ex.Message);
            }
            catch (Exception) {
                return StatusCode(500, genericErrorMsg);
            }
        }
    }
}
