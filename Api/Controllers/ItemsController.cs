using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using EFDataAccess;
using Application.Commands.Items;
using Application.Searches;
using Application.Exceptions;
using Application.Dto;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IGetItemsCommand _getItems;
        private readonly IGetItemCommand _getItem;
        private readonly IAddItemCommand _addItem;
        private readonly IEditItemCommand _editItem;
        private readonly IDeleteItemCommand _deleteItem;

        public string genericErrorMsg = "Somehting went wrong on the server.";

        public ItemsController(IGetItemsCommand getItems, IGetItemCommand getItem, IAddItemCommand addItem, IEditItemCommand editItem, IDeleteItemCommand deleteItem)
        {
            _getItems = getItems;
            _getItem = getItem;
            _addItem = addItem;
            _editItem = editItem;
            _deleteItem = deleteItem;
        }

        // GET: api/Items
        [HttpGet]
        public IActionResult Get([FromBody] ItemSearch query)
        {
            return Ok(_getItems.Execute(query));
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try {
                var item = _getItem.Execute(id);
                return Ok(item);
            }
            catch (EntityNotFoundException ex) {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, genericErrorMsg);
            }
        }

        // PUT: api/Items/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ItemDto dto)
        {
            try {
                _editItem.Execute(dto, id);
                return NoContent();
            }
            catch (EntityNotFoundException ex) {
                return NotFound(ex.Message);
            }
            catch (EntityUnprocessableException ex) {
                return UnprocessableEntity(ex.Message);
            }
            catch (Exception) {
                return StatusCode(500, genericErrorMsg);
            }
        }

        // POST: api/Items
        [HttpPost]
        public IActionResult Post([FromBody] ItemDto order66)
        {
            try {
                _addItem.Execute(order66);

                return Created("api/items/" + order66.Id, new ItemDto
                {
                    Id = order66.Id,
                    Name = order66.Name,
                    Cost = order66.Cost,
                    isCovert = order66.isCovert,
                    isForSale = order66.isForSale,
                    ItemQualityId = order66.ItemQualityId,
                    ItemTypeId = order66.ItemTypeId
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (EntityUnprocessableException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, genericErrorMsg);
            }
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _deleteItem.Execute(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(500, genericErrorMsg);
            }
        }
    }
}
