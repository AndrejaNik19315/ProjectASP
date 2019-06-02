using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using EFDataAccess;
using Application.Commands.ItemQualities;
using Application.Searches;
using Application.Exceptions;
using Application.Dto;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemQualitiesController : ControllerBase
    {
        private readonly IGetItemQualitiesCommand _getItemQualities;
        private readonly IGetItemQualityCommand _getItemQuality;
        private readonly IAddItemQualityCommand _addItemQuality;
        private readonly IEditItemQualityCommand _editItemQuality;
        private readonly IDeleteItemQualityCommand _deleteItemQuality;

        private string genericErrorMsg = "Somehting went wrong on the server.";

        public ItemQualitiesController(IGetItemQualitiesCommand getItemQualities, IGetItemQualityCommand getItemQuality, IAddItemQualityCommand addItemQuality, IEditItemQualityCommand editItemQuality, IDeleteItemQualityCommand deleteItemQuality)
        {
            _getItemQualities = getItemQualities;
            _getItemQuality = getItemQuality;
            _addItemQuality = addItemQuality;
            _editItemQuality = editItemQuality;
            _deleteItemQuality = deleteItemQuality;
        }

        // GET: api/ItemQualities
        [HttpGet]
        public IActionResult Get([FromQuery] ItemQualitySearch query)
        {
            return Ok(_getItemQualities.Execute(query));
        }

        // GET: api/ItemQualities/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try {
                var itemQuality = _getItemQuality.Execute(id);
                return Ok(itemQuality);
            }
            catch (EntityNotFoundException ex) {
                return NotFound(ex.Message);
            }
            catch (Exception) {
                return StatusCode(500, genericErrorMsg);
            }
            
        }

        // PUT: api/ItemQualities/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ItemQualityDto dto)
        {
            try {
                _editItemQuality.Execute(dto, id);
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

        // POST: api/ItemQualities
        [HttpPost]
        public IActionResult Post([FromBody] ItemQualityDto dto)
        {
            try
            {
                _addItemQuality.Execute(dto);
                return Created("api/itemqualities/" + dto.Id, new ItemQualityDto
                {
                    Id = dto.Id,
                    Name = dto.Name
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
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

        // DELETE: api/ItemQualities/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _deleteItemQuality.Execute(id);
                return NoContent();
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
    }
}
