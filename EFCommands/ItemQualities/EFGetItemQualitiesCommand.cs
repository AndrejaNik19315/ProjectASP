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
                query = Context.ItemQualities.Where(iq => iq.Name.ToLower().Contains(request.Name.ToLower()));

            return query.Select(iq => new ItemQualityDto
            {
                Id = iq.Id,
                Name = iq.Name,
                CreatedAt = iq.CreatedAt,
                UpdatedAt = iq.UpdatedAt
            });
        }
    }
}
