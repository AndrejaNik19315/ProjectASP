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
using Api.DataTransfer;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameClassesController : ControllerBase
    {
        private IGetGameClassCommand _getGameClass;
        private IGetGameClassesCommand _getGameClasses;
        private IDeleteGameClassCommand _deleteGameClass;
        private IEditGameClassCommand _editGameClass;
        private IAddGameClassCommand _addGameClass;

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
            catch (EntityNotFoundException) {
                return NotFound("Game class not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong on the server");
            }

        }

        // PUT: api/GameClasses/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Application.Dto.GameClassDto dto)
        {
            try {
                _editGameClass.Execute(dto, id);
                return NoContent();
            }
            catch (EntityNotFoundException) {
                return NotFound("Game class not found");
            }
            catch (EntityAlreadyExistsException) {
                return Conflict("Game class with thsi name already exists");
            }
            catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        // POST: api/GameClasses
        [HttpPost]
        public IActionResult Post([FromBody] Application.Dto.GameClassDto dto)
        {
            try {
                _addGameClass.Execute(dto);
                return Created("api/gameclasses/" + dto.Id, new GameClassDto {
                    Id = dto.Id,
                    Name = dto.Name
                });
            }
            catch (EntityAlreadyExistsException) {
                return Conflict("Class with that name already exists");
            }
            catch {
                return StatusCode(500, "Something went wrong on the server.");
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
                return StatusCode(500, "Something went wrong on the server.");
            }
        }
    }
}
