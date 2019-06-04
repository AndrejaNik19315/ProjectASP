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

namespace Api.Controllers
{
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

        // GET: api/Roles
        [HttpGet]
        public IActionResult Get([FromQuery] RoleSearch query)
        {
            return Ok(_getRoles.Execute(query));
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
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
