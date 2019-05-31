using Application.Commands.GameClasses;
using Application.Dto;
using Application.Exceptions;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCommands.GameClasses
{
    public class EFAddGameClassCommandCommand : BaseEFCommand, IAddGameClassCommand
    {
        public EFAddGameClassCommandCommand(ProjectContext context) : base(context)
        {
        }

        public void Execute(GameClassDto request)
        {
            if (Context.GameClasses.Any(gc => gc.Name.ToLower() == request.Name.ToLower()))
                throw new EntityAlreadyExistsException();

            Context.GameClasses.Add(new Domain.GameClass
            {
                Name = request.Name
            });

            Context.SaveChanges();
        }

        public void Execute(GameClassDto request, int id)
        {
            throw new NotImplementedException();
        }
    }
}
