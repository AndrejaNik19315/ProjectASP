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
using Api.DataTransfer;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly ProjectContext _context = new ProjectContext();

        private readonly IGetCharacterCommand _getCharacter;
        private readonly IGetCharactersCommand _getCharacters;
        private readonly IDeleteCharacterCommand _deleteCharacter;
        private readonly IEditCharacterCommand _editCharacter;
        private readonly IAddCharacterCommand _addCharacter;

        public CharactersController(IGetCharacterCommand getCharacter, IGetCharactersCommand getCharacters, IDeleteCharacterCommand deleteCharacter, IEditCharacterCommand editCharacter, IAddCharacterCommand addCharacter)
        {
            _getCharacter = getCharacter;
            _getCharacters = getCharacters;
            _deleteCharacter = deleteCharacter;
            _editCharacter = editCharacter;
            _addCharacter = addCharacter;
        }

        // GET: api/Characters
        [HttpGet]
        public IActionResult Get([FromQuery] CharacterSearch query)
        {
            var characters = _getCharacters.Execute(query);
            return Ok(characters);
        }

        // GET: api/Characters/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try {
                var character = _getCharacter.Execute(id);
                return Ok(character);
            }
            catch (EntityNotFoundException) {
                return NotFound();
            }
            catch {
                return StatusCode(500, "Something went wrong on the server.");
            } 
        }

        // PUT: api/Characters/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Application.Dto.CharacterDto dto)
        {
            try {
                _editCharacter.Execute(dto, id);
                return NoContent();
            }
            catch (EntityNotFoundException ex) {
                return NotFound(ex.Message);
            }
            catch (EntityNotActiveException) {
                return BadRequest("User must be active.");
            }
            catch (EntityAlreadyExistsException) {
                return Conflict("Character with this name already exists.");
            }
            catch (EntityBadFormatException) {
                return BadRequest("Bad format, level and funds cannot be below or equal to 0.");
            }
            catch {
                return StatusCode(500, "Something went wrong on the server");
            }
        }

        // POST: api/Characters
        [HttpPost]
        public IActionResult Post([FromBody] Application.Dto.CharacterDto dto)
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
                    InventoryId = dto.InventoryId,
                    UserId = dto.UserId
                });
            }
            catch (EntityAlreadyExistsException) {
                return Conflict("Character with that Name already exists.");
            }
            catch (EntityBadFormatException) {
                return BadRequest("Bad format, level and funds cannot be below or equal to 0.");
            }
            catch (EntityNotFoundException ex) {
                return NotFound(ex.Message);
            }
            catch (EntityNotActiveException) {
                return BadRequest("User must be active in order to add character.");
            }
            catch (Exception) {
                return StatusCode(500, "Oh noes!");
            }
        }

        // DELETE: api/Characters/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _deleteCharacter.Execute(id);
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                return NotFound("Character with this id, doesn't exist.");
            }
            catch {
                return StatusCode(500, "Something went wrong on the server.");
            }
        }
    }
}
