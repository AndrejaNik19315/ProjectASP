using Application.Dto.Items;
using Application.Interfaces;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Items.WebApp
{
    public interface IGetItemsWebCommand : ICommand<ItemSearchWeb, IEnumerable<FullItemDto>>
    {

    }
}
