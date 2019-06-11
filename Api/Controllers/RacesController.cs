using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using EFDataAccess;
using Application.Commands.Races;
using Application.Searches;
using Application.Exceptions;
using Application.Dto;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RacesController : ControllerBase
    {

        private readonly IGetRacesCommand _getRaces;
        private readonly IGetRaceCommand _getRace;
        private readonly IAddRaceCommand _addRace;
        private readonly IEditRaceCommand _editRace;
        private readonly IDeleteRaceCommand _deleteRace;

        private string genericErrorMsg = "Something went wrong on the server.";

        public RacesController(IGetRacesCommand getRaces, IGetRaceCommand getRace, IAddRaceCommand addRace, IEditRaceCommand editRace, IDeleteRaceCommand deleteRace)
        {
            _getRaces = getRaces;
            _getRace = getRace;
            _addRace = addRace;
            _editRace = editRace;
            _deleteRace = deleteRace;
        }

        /// <summary>
        /// Returns all races
        /// </summary>
        // GET: api/Races
        [HttpGet]
        public ActionResult<IEnumerable<RaceDto>> Get([FromQuery] RaceSearch query)
        {
            return Ok(_getRaces.Execute(query));
        }

        /// <response code="404">Race not found</response>
        /// <response code="500">Server error.</response>
        /// <summary>
        /// Returns single race by id
        /// </summary>
        // GET: api/Races/5
        [HttpGet("{id}")]
        public ActionResult<RaceDto> Get(int id)
        {
            try
            {
                var race = _getRace.Execute(id);
                return Ok(race);
            }
            catch (EntityNotFoundException ex) {
                return NotFound(ex.Message);
            }
            catch(Exception) {
                return StatusCode(500, genericErrorMsg);
            }
        }

        /// <response code="400">Bad Format</response>
        /// <response code="404">Race not found</response>
        /// <response code="409">Conflict, race with that name exists.</response>
        /// <response code="500">Server error.</response>
        /// <summary>
        /// Update race
        /// </summary>
        /// <remarks>
        /// PUT / Example
        /// {
        ///     "Name" : "Orc"
        /// }
        /// </remarks>
        // PUT: api/Races/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] RaceDto dto)
        {
            try {
                _editRace.Execute(dto, id);
                return NoContent();
            }
            catch (EntityNotFoundException ex) {
                return NotFound(ex.Message);
            }
            catch (EntityAlreadyExistsException ex) {
                return Conflict(ex.Message);
            }
            catch (Exception) {
                return StatusCode(500, genericErrorMsg);
            }
        }

        /// <response code="201">Created</response>
        /// <response code="400">Bad Format</response>
        /// <response code="409">Conflict, race with that name exists.</response>
        /// <response code="500">Server error.</response>
        /// <summary>
        /// Create race
        /// </summary>
        /// <remarks>
        /// POST / Example
        /// {
        ///     "Name" : "Human"
        /// }
        /// </remarks>
        // POST: api/Races
        [HttpPost]
        public IActionResult Post([FromBody] RaceDto dto)
        {
            try {
                _addRace.Execute(dto);
                return Created("api/races" + dto.Id, new RaceDto
                {
                    Id = dto.Id,
                    Name = dto.Name
                });
            }
            catch (EntityAlreadyExistsException ex) {
                return Conflict(ex.Message);
            }
            catch (Exception) {
                return StatusCode(500, genericErrorMsg);
            }
        }

        /// <response code="404">Race doesn't exist.</response>
        /// <response code="500">Server error.</response>
        /// <summary>
        /// Remove race by id
        /// </summary>
        /// <remarks>
        /// NOTE: Races that are in use cannot be removed and will return code 500.
        /// </remarks>
        // DELETE: api/Races/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _deleteRace.Execute(id);
                return NoContent();
            }
            catch(EntityNotFoundException ex) {
                return NotFound(ex.Message);
            }
            catch (Exception) {
                return StatusCode(500, genericErrorMsg);
            }
        }
    }
}
