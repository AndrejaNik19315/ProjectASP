using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using EFDataAccess;
using Application.Commands.GameClasses;
using Application.Searches;
using Application.Exceptions;
using Application.Dto;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameClassesController : ControllerBase
    {
        private readonly IGetGameClassCommand _getGameClass;
        private readonly IGetGameClassesCommand _getGameClasses;
        private readonly IDeleteGameClassCommand _deleteGameClass;
        private readonly IEditGameClassCommand _editGameClass;
        private readonly IAddGameClassCommand _addGameClass;

        private string genericErrorMsg = "Something went wrong on the server";

        public GameClassesController(IGetGameClassCommand getGameClass, IGetGameClassesCommand getGameClasses, IDeleteGameClassCommand deleteGameClass, IEditGameClassCommand editGameClass, IAddGameClassCommand addGameClass)
        {
            _getGameClass = getGameClass;
            _getGameClasses = getGameClasses;
            _deleteGameClass = deleteGameClass;
            _editGameClass = editGameClass;
            _addGameClass = addGameClass;
        }

        //GET: api/GameClasses
       [HttpGet]
        public IActionResult Get([FromQuery] GameClassSearch query)
        {
            return Ok(_getGameClasses.Execute(query));
        }

        // GET: api/GameClasses/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var gameClass = _getGameClass.Execute(id);
                return Ok(gameClass);
            }
            catch (EntityNotFoundException ex) {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, genericErrorMsg);
            }

        }

        // PUT: api/GameClasses/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] GameClassDto dto)
        {
            try {
                _editGameClass.Execute(dto, id);
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

        // POST: api/GameClasses
        [HttpPost]
        public IActionResult Post([FromBody] GameClassDto dto)
        {
            try {
                _addGameClass.Execute(dto);
                return Created("api/gameclasses/" + dto.Id, new GameClassDto {
                    Id = dto.Id,
                    Name = dto.Name
                });
            }
            catch (EntityAlreadyExistsException ex) {
                return Conflict(ex.Message);
            }
            catch {
                return StatusCode(500, genericErrorMsg);
            }
        }

        // DELETE: api/GameClasses/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _deleteGameClass.Execute(id);
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
