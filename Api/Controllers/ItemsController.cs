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
using Application.Dto.Items;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IGetItemsCommand _getItems;
        private readonly IGetItemCommand _getItem;
        private readonly IAddItemCommand _addItem;
        private readonly IEditItemCommand _editItem;
        private readonly IDeleteItemCommand _deleteItem;

        public readonly string genericErrorMsg = "Something went wrong on the server.";

        public ItemsController(IGetItemsCommand getItems, IGetItemCommand getItem, IAddItemCommand addItem, IEditItemCommand editItem, IDeleteItemCommand deleteItem)
        {
            _getItems = getItems;
            _getItem = getItem;
            _addItem = addItem;
            _editItem = editItem;
            _deleteItem = deleteItem;
        }

        /// <summary>
        /// Returns all items by search parameters
        /// </summary>
        // GET: api/Items
        [HttpGet]
        public ActionResult<IEnumerable<FullItemDto>> Get([FromQuery] ItemSearch query)
        {
            return Ok(_getItems.Execute(query));
        }

        /// <response code="404">Item not found</response>
        /// <response code="500">Server error.</response>
        /// <summary>
        /// Returns single item by id
        /// </summary>
        // GET: api/Items/5
        [HttpGet("{id}")]
        public ActionResult<FullItemDto> Get(int id)
        {
            try {
                var item = _getItem.Execute(id);
                return Ok(item);
            }
            catch (EntityNotFoundException ex) {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, genericErrorMsg);
            }
        }

        /// <response code="204">No content</response>
        /// <response code="400">Bad Format</response>
        /// <response code="404">Item not found</response>
        /// <response code="409">Conflict, item with that name exists.</response>
        /// <response code="422">Format of the request is good but one more properties are not valid.</response>
        /// <response code="500">Server error.</response>
        /// <summary>
        /// Update item
        /// </summary>
        /// <remarks>
        /// PUT / Example
        /// {
        ///    	"Name" : "nestodrugo",
	    ///     "Cost" : "150",
	    ///     "ItemTypeId" : 2,
	    ///     "ItemQualityId" : 2,
	    ///     "quantity" : 5,
	    ///     "isCovert" : true
        /// }
        /// </remarks>
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
            catch (EntityAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception) {
                return StatusCode(500, genericErrorMsg);
            }
        }

        /// <response code="201">Created</response>
        /// <resposne code="400">Bad format.</resposne>
        /// <response code="409">Conflict, item name is already taken.</response>
        /// <response code="500">Server error.</response>
        /// <summary>
        /// Creates new item 
        /// </summary>
        /// <remarks>
        /// POST / Example
        /// {
        ///     "Name" : "nesto",
	    ///     "Cost" : "15",
	    ///     "ItemTypeId" : 1,
	    ///     "ItemQualityId" : 2,
	    ///     "quantity" : 2,
	    ///     "isCovert" : false  
        /// }
        /// </remarks>
        // POST: api/Items
        [HttpPost]
        public IActionResult Post([FromBody] ItemDto order66)
        {
            try
            {
                _addItem.Execute(order66);

                return Created("api/items/" + order66.Id, new ItemDto
                {
                    Id = order66.Id,
                    Name = order66.Name,
                    Cost = order66.Cost,
                    isCovert = order66.isCovert,
                    ItemQualityId = order66.ItemQualityId,
                    ItemTypeId = order66.ItemTypeId,
                    Quantity = order66.Quantity
                });
            }
            catch (EntityUnprocessableException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
            catch (EntityAlreadyExistsException ex) {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, genericErrorMsg);
            }
        }

        /// <response code="204">No content</response>
        /// <response code="404">Item doesn't exist.</response>
        /// <response code="500">Server error.</response>
        /// <summary>
        /// Remove item by id
        /// </summary>
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
