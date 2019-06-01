using Application.Dto;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.ItemTypes
{
    public interface IGetItemTypeCommand : ICommand<int, ItemTypeDto>
    {
    }
}
