using Application.Commands.GameClasses;
using Application.Exceptions;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCommands.GameClasses
{
    public class EFDeleteGameClassCommand : BaseEFCommand, IDeleteGameClassCommand
    {
        public EFDeleteGameClassCommand(ProjectContext context) : base(context)
        {
        }

        public void Execute(int request)
        {
            var gameClass = Context.GameClasses.Find(request);

            if (gameClass == null)
                throw new EntityNotFoundException("Game class with that id doesn't exist.");

            Context.GameClasses.Remove(gameClass);
            Context.SaveChanges();
        }
    }
}
