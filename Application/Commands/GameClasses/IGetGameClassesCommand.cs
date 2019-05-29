using Application.Dto;
using Application.Interfaces;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.GameClasses
{
    public interface IGetGameClassesCommand : ICommand<GameClassSearch, IEnumerable<GameClassDto>>
    {
    }
}
