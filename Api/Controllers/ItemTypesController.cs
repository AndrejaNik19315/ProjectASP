using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using EFDataAccess;
using Application.Dto;
using Application.Commands.ItemTypes;
using Application.Exceptions;
using Application.Searches;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemTypesController : ControllerBase
    {

        private readonly IGetItemTypesCommand _getItemTypes;
        private readonly IGetItemTypeCommand _getItemType;
        private readonly IAddItemTypeCommand _addItemType;
        private readonly IEditItemTypeCommand _editItemType;
        private readonly IDeleteItemTypeCommand _deleteItemType;

        private string genericErrorMsg = "Something went wrong on the server.";

        public ItemTypesController(IGetItemTypesCommand getItemTypes, IGetItemTypeCommand getItemType, IAddItemTypeCommand addItemType, IEditItemTypeCommand editItemType, IDeleteItemTypeCommand deleteItemType)
        {
            _getItemTypes = getItemTypes;
            _getItemType = getItemType;
            _addItemType = addItemType;
            _editItemType = editItemType;
            _deleteItemType = deleteItemType;
        }

        // GET: api/ItemTypes
        [HttpGet]
        public IActionResult Get([FromQuery] ItemTypeSearch query)
        {
            return Ok(_getItemTypes.Execute(query));
        }

        // GET: api/ItemTypes/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var itemType = _getItemType.Execute(id);
                return Ok(itemType);
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

        // PUT: api/ItemTypes/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, ItemTypeDto dto)
        {
            try
            {
                _editItemType.Execute(dto, id);
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

        // POST: api/ItemTypes
        [HttpPost]
        public IActionResult Post(ItemTypeDto dto)
        {
            try
            {
                _addItemType.Execute(dto);
                return Created("api/itemtypes/" + dto.Id, new ItemTypeDto
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

        // DELETE: api/ItemTypes/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _deleteItemType.Execute(id);
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
