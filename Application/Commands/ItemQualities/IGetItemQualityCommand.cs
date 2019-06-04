using Application.Dto;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.ItemQualities
{
    public interface IGetItemQualityCommand : ICommand<int, ItemQualityDto>
    {
    }
}
