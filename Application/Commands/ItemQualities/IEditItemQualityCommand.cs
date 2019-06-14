using Application.Dto;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.ItemQualities
{
    public interface IEditItemQualityCommand : ICommand<ItemQualityDto, int ,int>
    {
    }
}
