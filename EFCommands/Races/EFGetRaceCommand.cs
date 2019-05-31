using Application.Commands.Races;
using Application.Dto;
using Application.Exceptions;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCommands.Races
{
    public class EFGetRaceCommand : BaseEFCommand, IGetRaceCommand
    {
        public EFGetRaceCommand(ProjectContext context) : base(context)
        {
        }

        public RaceDto Execute(int request)
        {
            var race = Context.Races.Find(request);

            if (race == null)
                throw new EntityNotFoundException("Race not found.");

            return new RaceDto
            {
                Id = race.Id,
                Name = race.Name,
                CreatedAt = race.CreatedAt,
                UpdatedAt = race.UpdatedAt
            };
        }

        public RaceDto Execute(int request, int id)
        {
            throw new NotImplementedException();
        }
    }
}
