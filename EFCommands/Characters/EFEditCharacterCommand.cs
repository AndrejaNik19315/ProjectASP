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
                throw new EntityNotActiveException();

            if (Context.Characters.Any(c => c.Name == request.Name))
                throw new EntityAlreadyExistsException();

            if (request.Level <= 0 || request.Funds <= 0)
                throw new EntityBadFormatException();

            if (Context.GameClasses.Find(request.GameClassId) == null)
                throw new EntityNotFoundException("GameClass Id doesn't exist");

            if (request.Name != character.Name)
                character.Name = request.Name;
            if (request.Level != character.Level)
                character.Level = request.Level;
            if (request.GameClassId != character.GameClassId)
                character.GameClassId = request.GameClassId;

            character.UpdatedAt = DateTime.Now;

            Context.SaveChanges();
        }


        public void Execute(CharacterDto request)
        {
            throw new NotImplementedException();
        }
    }
}
