using Application.Commands.Characters;
using Application.Dto;
using Application.Exceptions;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCommands.Characters
{
    public class EFEditCharacterCommand : BaseEFCommand, IEditCharacterCommand
    {
        public EFEditCharacterCommand(ProjectContext context) : base(context)
        {
        }

        public void Execute(CharacterDto request, int id)
        {
            var character = Context.Characters.Find(id);

            if (character == null)
                throw new EntityNotFoundException("Character with that Id doesn't exist");

            if (Context.Users.Find(request.UserId).IsActive == false)
                throw new EntityNotActiveException("User must be active.");

            if (request.Name != character.Name)
                if (Context.Characters.Any(c => c.Name == request.Name))
                    throw new EntityAlreadyExistsException("Character with this name already exists.");

            if (request.GenderId != character.GenderId)
                if (!(Context.Genders.Any(gen => gen.Id == request.GenderId)))
                    throw new EntityNotFoundException("There is no gender with that Id.");

            if (request.GameClassId != character.GameClassId)
                if (!(Context.GameClasses.Any(gc => gc.Id == request.GameClassId)))
                    throw new EntityNotFoundException("There is no gameclass with that Id.");

            if(request.RaceId != character.RaceId)
                if (!(Context.Races.Any(r => r.Id == request.RaceId)))
                    throw new EntityNotFoundException("There is no race with that Id.");

            character.Name = request.Name;
            character.Level = request.Level;
            character.GameClassId = request.GameClassId;
            character.GenderId = request.GenderId;
            character.RaceId = request.RaceId;
            character.UpdatedAt = DateTime.Now;

            Context.SaveChanges();
        }


        public void Execute(CharacterDto request)
        {
            throw new NotImplementedException();
        }
    }
}
