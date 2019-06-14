using Application.Dto;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.ItemTypes
{
    public interface IEditItemTypeCommand : ICommand<ItemTypeDto,int,int>
    {
    }
}
