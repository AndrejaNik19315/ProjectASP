using Application.Dto;
using Application.Interfaces;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Characters
{
    public interface IGetCharactersCommand : ICommand<CharacterSearch, IEnumerable<CharacterDto>>
    {
    }
}
