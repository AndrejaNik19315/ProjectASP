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
        private readonly IGetItemQualitiesCommand _getQualities;
        private readonly IGetItemQualityCommand _getQuality;
        private readonly IAddItemQualityCommand _addQuality;
        private readonly IEditItemQualityCommand _editQuality;
        private readonly IDeleteItemQualityCommand _deleteQuality;

        private string genericErrorMsg = "Something went wrong on the server";

        public ItemQualitiesController(IGetItemQualitiesCommand getQualities, IGetItemQualityCommand getQuality, IAddItemQualityCommand addQuality, IEditItemQualityCommand editQuality, IDeleteItemQualityCommand deleteQuality)
        {
            _getQualities = getQualities;
            _getQuality = getQuality;
            _addQuality = addQuality;
            _editQuality = editQuality;
            _deleteQuality = deleteQuality;
        }

        /// <summary>
        /// Returns all item qulities
        /// </summary>
        // GET: api/ItemQualities
        [HttpGet]
        public ActionResult<IEnumerable<ItemQualityDto>> Get([FromQuery] ItemQualitySearch query)
        {
            return Ok(_getQualities.Execute(query));
        }


        /// <response code="404">Item quality not found</response>
        /// <response code="500">Server error.</response>
        /// <summary>
        /// Returns single item quality by id
        /// </summary>
        // GET: api/ItemQualities/5
        [HttpGet("{id}")]
        public ActionResult<ItemQualityDto> Get(int id)
        {
            try {
                var itemQuality = _getQuality.Execute(id);
                return Ok(itemQuality);
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

        /// <response code="400">Bad Format</response>
        /// <response code="404">Item quality not found</response>
        /// <response code="409">Conflict, quality with that name exists.</response>
        /// <response code="500">Server error.</response>
        /// <summary>
        /// Update quality
        /// </summary>
        /// <remarks>
        /// PUT / Example
        /// {
        ///     "Name" : "Uncommon"
        /// }
        /// </remarks>
        // PUT: api/ItemQualities/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ItemQualityDto dto)
        {
            try {
                _editQuality.Execute(dto, id);
                return NoContent();
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

        /// <response code="201">Created</response>
        /// <response code="400">Bad Format</response>
        /// <response code="409">Conflict, quality with that name exists.</response>
        /// <response code="500">Server error.</response>
        /// <summary>
        /// Create item quality
        /// </summary>
        /// <remarks>
        /// POST / Example
        /// {
        ///     "Name" : "Common"
        /// }
        /// </remarks>
        // POST: api/ItemQualities
        [HttpPost]
        public ActionResult Post([FromBody] ItemQualityDto dto)
        {
            try {
                _addQuality.Execute(dto);
                return Created("api/itemquality/" + dto.Id, new ItemQualityDto
                {
                    Id = dto.Id,
                    Name = dto.Name
                });
            }
            catch (EntityAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch(Exception)
            {
                return StatusCode(500, genericErrorMsg);
            }
        }

        /// <response code="404">Item quality doesn't exist.</response>
        /// <response code="500">Server error.</response>
        /// <summary>
        /// Remove quality by id
        /// </summary>
        /// <remarks>
        /// NOTE: Qualities that are in use cannot be removed and will return code 500
        /// </remarks>
        // DELETE: api/ItemQualities/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try {
                _deleteQuality.Execute(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception)
            {
                return StatusCode(500, genericErrorMsg);
            }
        }
    }
}
