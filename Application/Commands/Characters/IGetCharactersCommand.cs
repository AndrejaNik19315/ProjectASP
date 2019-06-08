using Application.Dto;
using Application.Dto.Characters;
using Application.Interfaces;
using Application.Responses;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Characters
{
    public interface IGetCharactersCommand : ICommand<CharacterSearch, Paged<FullCharacterDto>>
    {
    }
}
