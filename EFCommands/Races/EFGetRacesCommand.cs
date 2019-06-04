using Application.Commands.Races;
using Application.Dto;
using Application.Searches;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCommands.Races
{
    public class EFGetRacesCommand : BaseEFCommand, IGetRacesCommand
    {
        public EFGetRacesCommand(ProjectContext context) : base(context)
        {
        }

        public IEnumerable<RaceDto> Execute(RaceSearch request)
        {
            var query = Context.Races.AsQueryable();

            if (request.Name != null)
                query = query.Where(r => r.Name.ToLower().Contains(request.Name.ToLower()));

            return query.Select(r => new RaceDto
            {
                Id = r.Id,
                Name = r.Name
            });
        }

        public IEnumerable<RaceDto> Execute(RaceSearch request, int id)
        {
            throw new NotImplementedException();
        }
    }
}
