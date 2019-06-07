using Application.Dto;
using Application.Interfaces;
using Application.Responses;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Items
{
    public interface IGetItemsCommand : ICommand<ItemSearch, Paged<ItemDto>>
    {
    }
}
