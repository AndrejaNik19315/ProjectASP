using Application.Commands.Races;
using Application.Dto;
using Application.Exceptions;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCommands.Races
{
    public class EFAddRaceCommand : BaseEFCommand, IAddRaceCommand
    {
        public EFAddRaceCommand(ProjectContext context) : base(context)
        {
        }

        public void Execute(RaceDto request)
        {
            if (Context.Races.Any(r => r.Name.ToLower().Equals(request.Name.ToLower())))
                throw new EntityAlreadyExistsException("Name already exists.");

            Context.Races.Add(new Domain.Race
            {
                Name = request.Name
            });

            Context.SaveChanges();
        }

        public void Execute(RaceDto request, int id)
        {
            throw new NotImplementedException();
        }
    }
}
