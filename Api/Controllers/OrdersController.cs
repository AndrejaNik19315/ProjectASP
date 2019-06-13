using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using EFDataAccess;
using Application.Commands.Orders;
using Application.Dto;
using Application.Exceptions;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMakeOrderCommand _makeOrder;

        private readonly string genericErrorMsg = "Something went wrong on the server.";

        public OrdersController(IMakeOrderCommand makeOrder)
        {
            _makeOrder = makeOrder;
        }

        /// <response code="201">Created, item was bought by the character.</response>
        /// <resposne code="400">Bad format.</resposne>
        /// <response code="404">Character or Item doesn't exist.</response> 
        /// <response code="409">User not active.</response>
        /// <response code="422">Format of the request is good but one more properties are not valid.</response>
        /// <response code="500">Server error.</response>
        /// <summary>
        /// Creates order for a character that is buying an item.
        /// </summary>
        /// <remarks>
        /// POST Example
        /// {
        ///   "itemId" : 1,
        ///   "characterId" : 2
        /// }
        /// </remarks>

        // POST: api/Orders
        [HttpPost]
        public IActionResult Post([FromBody] OrderDto dto) {
            try
            {
                _makeOrder.Execute(dto);
                return Created("api/orders", new OrderDto
                {
                    Id = dto.Id,
                    ItemId = dto.ItemId,
                    CharacterId = dto.CharacterId
                });
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
            catch (Exception ex)
            {
                return StatusCode(500, genericErrorMsg);
            }
        }
    }
}
