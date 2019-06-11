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

        /// <summary>
        /// Returns all game classes
        /// </summary>
        //GET: api/GameClasses
       [HttpGet]
        public ActionResult<IEnumerable<GameClassDto>> Get([FromQuery] GameClassSearch query)
        {
            return Ok(_getGameClasses.Execute(query));
        }

        /// <response code="404">Game class not found</response>
        /// <response code="500">Server error.</response>
        /// <summary>
        /// Returns single game class by id
        /// </summary>
        // GET: api/GameClasses/5
        [HttpGet("{id}")]
        public ActionResult<GameClassDto> Get(int id)
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

        /// <response code="400">Bad Format</response>
        /// <response code="404">Game class not found</response>
        /// <response code="409">Conflict, game class with that name exists.</response>
        /// <response code="500">Server error.</response>
        /// <summary>
        /// Update game class
        /// </summary>
        /// <remarks>
        /// PUT / Example
        /// {
        ///     "Name" : "Warrior"
        /// }
        /// </remarks>
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

        /// <response code="201">Created</response>
        /// <response code="400">Bad Format</response>
        /// <response code="409">Conflict, game class with that name exists.</response>
        /// <response code="500">Server error.</response>
        /// <summary>
        /// Create game class
        /// </summary>
        /// <remarks>
        /// POST / Example
        /// {
        ///     "Name" : "Paladin"
        /// }
        /// </remarks>
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

        /// <response code="404">Game class doesn't exist.</response>
        /// <response code="500">Server error.</response>
        /// <summary>
        /// Remove game class by id
        /// </summary>
        /// <remarks>
        /// NOTE: Game classes that are in use cannot be removed and will return code 500
        /// </remarks>
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
