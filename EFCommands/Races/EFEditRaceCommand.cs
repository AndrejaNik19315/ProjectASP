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
    public class EFEditRaceCommand : BaseEFCommand, IEditRaceCommand
    {
        public EFEditRaceCommand(ProjectContext context) : base(context)
        {
        }

        public void Execute(RaceDto request, int id)
        {
            var race = Context.Races.Find(id);

            if (race == null)
                throw new EntityNotFoundException("Race with that Id doesn't exist");

            if (race.Name.ToLower() != request.Name.ToLower())
                if (Context.Races.Any(r => r.Name.ToLower().Equals(request.Name.ToLower())))
                    throw new EntityAlreadyExistsException("Race with that name already exists.");

            race.Name = request.Name;
            race.UpdatedAt = DateTime.Now;

            Context.SaveChanges();
        }
    }
}
