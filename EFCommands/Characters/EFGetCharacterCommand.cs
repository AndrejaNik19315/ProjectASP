using Application.Commands.Characters;
using Application.Dto;
using Application.Exceptions;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCommands.Characters
{
    public class EFGetCharacterCommand : BaseEFCommand, IGetCharacterCommand
    {
        public EFGetCharacterCommand(ProjectContext context) : base(context)
        {
        }

        public CharacterDto Execute(int request)
        {
            var character = Context.Characters.Find(request);

            if (character == null)
                throw new EntityNotFoundException("Character not found.");

            return new CharacterDto
            {
                Id = character.Id,
                Name = character.Name,
                Level = character.Level,
                Funds = character.Funds,
                GameClassId = character.GameClassId,
                GenderId = character.GenderId,
                RaceId = character.RaceId,
                UserId = character.UserId,
                CreatedAt = character.CreatedAt,
                UpdatedAt = character.UpdatedAt
            };
        }

        public CharacterDto Execute(int request, int id)
        {
            throw new NotImplementedException();
        }
    }
}
