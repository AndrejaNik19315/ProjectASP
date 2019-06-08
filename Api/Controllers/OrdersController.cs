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
                return UnprocessableEntity(ex.Message);
            }
            catch (EntityUnprocessableException ex) {
                return UnprocessableEntity(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
