using Application.Commands.ItemQualities;
using Application.Dto;
using Application.Searches;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCommands.ItemQualities
{
    public class EFGetItemQualitiesCommand : BaseEFCommand, IGetItemQualitiesCommand
    {
        public EFGetItemQualitiesCommand(ProjectContext context) : base(context)
        {
        }

        public IEnumerable<ItemQualityDto> Execute(ItemQualitySearch request)
        {
            var query = Context.ItemQualities.AsQueryable();

            if (request.Name != null)
                query = query.Where(itm => itm.Name.ToLower().Contains(request.Name.ToLower()));

            return query.Select(itm => new ItemQualityDto
            {
                Id = itm.Id,
                Name = itm.Name
            }).OrderBy(itm => itm.Id);
        }

        public IEnumerable<ItemQualityDto> Execute(ItemQualitySearch request, int id)
        {
            throw new NotImplementedException();
        }
    }
}
