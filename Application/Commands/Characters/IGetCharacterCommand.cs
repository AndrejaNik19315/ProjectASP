using Application.Dto;
using Application.Dto.Characters;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Characters
{
    public interface IGetCharacterCommand : ICommand<int, FullCharacterDto>
    {
    }
}
