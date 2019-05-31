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

        private IGetRacesCommand _getRaces;
        private IGetRaceCommand _getRace;
        private IAddRaceCommand _addRace;
        private IEditRaceCommand _editRace;
        private IDeleteRaceCommand _deleteRace;

        private string genericErrorMsg = "Something went wrong on the server.";

        public RacesController(IGetRacesCommand getRaces, IGetRaceCommand getRace, IAddRaceCommand addRace, IEditRaceCommand editRace, IDeleteRaceCommand deleteRace)
        {
            _getRaces = getRaces;
            _getRace = getRace;
            _addRace = addRace;
            _editRace = editRace;
            _deleteRace = deleteRace;
        }

        // GET: api/Races
        [HttpGet]
        public IActionResult Get([FromQuery] RaceSearch query)
        {
            return Ok(_getRaces.Execute(query));
        }

        // GET: api/Races/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
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
