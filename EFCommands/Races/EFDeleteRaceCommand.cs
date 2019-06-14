using Application.Commands.Races;
using Application.Exceptions;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCommands.Races
{
    public class EFDeleteRaceCommand : BaseEFCommand, IDeleteRaceCommand
    {
        public EFDeleteRaceCommand(ProjectContext context) : base(context)
        {
        }

        public void Execute(int request)
        {
            var race = Context.Races.Find(request);

            if (race == null)
                throw new EntityNotFoundException("Race with that Id doesn't exist.");

            Context.Races.Remove(race);
            Context.SaveChanges();
        }
    }
}
