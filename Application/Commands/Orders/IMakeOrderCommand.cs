using Application.Dto;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Orders
{
    public interface IMakeOrderCommand : ICommand<OrderDto>
    {
    }
}
