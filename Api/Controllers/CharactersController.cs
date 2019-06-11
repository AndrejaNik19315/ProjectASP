using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using EFDataAccess;
using Application.Commands.Characters;
using Application.Searches;
using Application.Exceptions;
using Application.Dto;
using Application.Dto.Characters;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly IGetCharacterCommand _getCharacter;
        private readonly IGetCharactersCommand _getCharacters;
        private readonly IDeleteCharacterCommand _deleteCharacter;
        private readonly IEditCharacterCommand _editCharacter;
        private readonly IAddCharacterCommand _addCharacter;
        private readonly IGetCharacterInventoryCommand _getInventory;

        private readonly string genericErrorMsg = "Something went wrong on the server.";

        public CharactersController(IGetCharacterCommand getCharacter, IGetCharactersCommand getCharacters, IDeleteCharacterCommand deleteCharacter, IEditCharacterCommand editCharacter, IAddCharacterCommand addCharacter, IGetCharacterInventoryCommand getInventory)
        {
            _getCharacter = getCharacter;
            _getCharacters = getCharacters;
            _deleteCharacter = deleteCharacter;
            _editCharacter = editCharacter;
            _addCharacter = addCharacter;
            _getInventory = getInventory;
        }

        /// <summary>
        /// Returns all characters according to search parameters 
        /// </summary>
        // GET: api/Characters
        [HttpGet]
        public ActionResult<IEnumerable<FullCharacterDto>> Get([FromQuery] CharacterSearch query)
        {
            var characters = _getCharacters.Execute(query);
            return Ok(characters);
        }

        /// <response code="404">Character doesn't exist</response>   
        /// <response code="500">Server error.</response>
        /// <summary>
        /// Returns single character by id
        /// </summary>
        // GET: api/Characters/5
        [HttpGet("{id}")]
        public ActionResult<FullCharacterDto> Get(int id)
        {
            try {
                var character = _getCharacter.Execute(id);
                return Ok(character);
            }
            catch (EntityNotFoundException ex) {
                return NotFound(ex.Message);
            }
            catch (Exception) {
                return StatusCode(500, genericErrorMsg);
            }
        }

        /// <response code="404">Character doesn't exist</response>   
        /// <response code="500">Server error.</response>
        /// <summary>
        /// Returns contents of inventory of a character
        /// </summary>
        //GET: api/Characters/5/inventory
        [HttpGet("{id}/inventory")]
        public IActionResult GetInventory(int id){
            try {
                return Ok(_getInventory.Execute(id));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, genericErrorMsg);
            }
        }

        /// <resposne code="400">Bad format.</resposne>
        /// <response code="404">Character doesn't exist</response> 
        /// <response code="409">Conflict, character name is already taken or user is not active.</response>
        /// <response code="422">Format of the request is good but one more properties are not valid.</response>
        /// <response code="500">Server error.</response>
        /// <summary>
        /// Updates character
        /// </summary>
        /// <remarks>
        /// PUT Example
        /// {
        ///   	"Name": "Rogar",
        ///     "Level": 44,
        ///     "Funds": 592,
        ///     "GameClassId": 1,
        ///     "RaceId": 2,
        ///     "GenderId": 1,
        /// }
        /// </remarks>
        // PUT: api/Characters/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CharacterDto dto)
        {
            try
            {
                _editCharacter.Execute(dto, id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (EntityNotActiveException ex)
            {
                return Conflict(ex.Message);
            }
            catch (EntityUnprocessableException ex) {
                return UnprocessableEntity(ex.Message);
            }
            catch (EntityAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, genericErrorMsg);
            }
        }

        /// <response code="201">Created</response>
        /// <resposne code="400">Bad format.</resposne>
        /// <response code="404">Character or User doesn't exist</response> 
        /// <response code="409">Conflict, character name is already taken.</response>
        /// <response code="500">Server error.</response>
        /// <summary>
        /// Creates new character for a user
        /// </summary>
        /// <remarks>
        /// POST Example
        /// {
        ///   	"Name": "Ragnar",
        ///     "Level": 43,
        ///     "Funds": 77,
        ///     "GameClassId": 1,
        ///     "RaceId": 2,
        ///     "GenderId": 1,
        ///     "UserId": 3
        /// }
        /// </remarks>
        // POST: api/Characters
        [HttpPost]
        public IActionResult Post([FromBody] CharacterDto dto)
        {
            try
            {
                _addCharacter.Execute(dto);

                return Created("/api/characters/" + dto.Id, new CharacterDto
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Level = dto.Level,
                    Funds = dto.Funds,
                    GameClassId = dto.GameClassId,
                    GenderId = dto.GenderId,
                    RaceId = dto.RaceId,
                    UserId = dto.UserId
                });
            }
            catch (EntityAlreadyExistsException ex) {
                return Conflict(ex.Message);
            }
            catch (EntityNotFoundException ex) {
                return NotFound(ex.Message);
            }
            catch (EntityNotActiveException ex) {
                return Conflict(ex.Message);
            }
            catch (Exception) {
                return StatusCode(500, genericErrorMsg);
            }
        }

        /// <response code="404">Character doesn't exist.</response>
        /// <response code="500">Server error.</response>
        /// <summary>
        /// Removes character by id
        /// </summary>
        // DELETE: api/Characters/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _deleteCharacter.Execute(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch {
                return StatusCode(500, genericErrorMsg);
            }
        }
    }
}
