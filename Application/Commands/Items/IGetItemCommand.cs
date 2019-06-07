using Application.Dto;
using Application.Dto.Items;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Items
{
    public interface IGetItemCommand : ICommand<int, FullItemDto>
    {
    }
}
