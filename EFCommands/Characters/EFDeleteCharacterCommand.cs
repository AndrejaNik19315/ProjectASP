using Application.Commands.Characters;
using Application.Exceptions;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCommands.Characters
{
    public class EFDeleteCharacterCommand : BaseEFCommand, IDeleteCharacterCommand
    {
        public EFDeleteCharacterCommand(ProjectContext context) : base(context)
        {
        }

        public void Execute(int request)
        {
            var character = Context.Characters.Find(request);

            if (character == null) {
                throw new EntityNotFoundException("Character not found.");
            }

            Context.Characters.Remove(character);

            Context.SaveChanges();
        }

        public void Execute(int request, int id)
        {
            throw new NotImplementedException();
        }
    }
}
