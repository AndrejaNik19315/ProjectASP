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
            catch (EntityNotFoundException ex) {
                return NotFound(ex.Message);
            }
            catch (Exception) {
                return StatusCode(500, genericErrorMsg);
            }
        }

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

        // PUT: api/Characters/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CharacterDto dto)
        {
            try {
                _editCharacter.Execute(dto, id);
                return NoContent();
            }
            catch (EntityNotFoundException ex) {
                return NotFound(ex.Message);
            }
            catch (EntityNotActiveException ex) {
                return UnprocessableEntity(ex.Message);
            }
            catch (EntityAlreadyExistsException ex) {
                return Conflict(ex.Message);
            }
            catch(Exception) {
                return StatusCode(500, genericErrorMsg);
            }
        }

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
                return UnprocessableEntity(ex.Message);
            }
            catch (Exception e) {
                return StatusCode(500, genericErrorMsg);
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
