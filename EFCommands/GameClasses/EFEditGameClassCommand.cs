using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Commands.GameClasses;
using Application.Dto;
using Application.Exceptions;
using EFDataAccess;

namespace EFCommands.GameClasses
{
    public class EFEditGameClassCommand : BaseEFCommand, IEditGameClassCommand
    {
        public EFEditGameClassCommand(ProjectContext context) : base(context)
        {
        }

        public void Execute(GameClassDto request, int id)
        {
            var gameClass = Context.GameClasses.Find(id);

            if (gameClass == null)
                throw new EntityNotFoundException("GameClass not found.");

            if(request.Name.ToLower() != gameClass.Name.ToLower())
                if (Context.GameClasses.Any(gc => gc.Name.ToLower() == request.Name.ToLower()))
                    throw new EntityAlreadyExistsException("GameClass with this name already exists.");

            gameClass.Name = request.Name;
            gameClass.UpdatedAt = DateTime.Now;

            Context.SaveChanges();
        }
    }
}
