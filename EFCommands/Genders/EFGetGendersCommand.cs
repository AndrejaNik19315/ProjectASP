using Application.Commands.Genders;
using Application.Dto;
using Application.Searches;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCommands.Genders
{
    public class EFGetGendersCommand : BaseEFCommand, IGetGendersCommand
    {
        public EFGetGendersCommand(ProjectContext context) : base(context)
        {
        }

        public IEnumerable<GenderDto> Execute(GenderSearch request)
        {
            var query = Context.Genders.AsQueryable();

            if (request.Sex != null)
                query = query.Where(g => g.Sex.ToLower().Contains(request.Sex.ToLower()));

            return query.Select(g => new GenderDto
            {
                Id = g.Id,
                Sex = g.Sex
            });

        }

        public IEnumerable<GenderDto> Execute(GenderSearch request, int id)
        {
            throw new NotImplementedException();
        }
    }
}
