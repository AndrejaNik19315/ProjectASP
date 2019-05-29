using Application.Dto;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.GameClasses
{
    public interface IGetGameClassCommand : ICommand<int, GameClassDto>
    {
    }
}
