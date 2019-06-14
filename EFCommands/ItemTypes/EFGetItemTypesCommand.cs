using Application.Commands.ItemTypes;
using Application.Dto;
using Application.Searches;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCommands.ItemTypes
{
    public class EFGetItemTypesCommand : BaseEFCommand, IGetItemTypesCommand
    {
        public EFGetItemTypesCommand(ProjectContext context) : base(context)
        {
        }

        public IEnumerable<ItemTypeDto> Execute(ItemTypeSearch request)
        {
            var query = Context.ItemTypes.AsQueryable();

            if (request.Name != null)
                query = Context.ItemTypes.Where(it => it.Name.ToLower().Contains(request.Name.ToLower()));

            return query.Select(it => new ItemTypeDto
            {
                Id = it.Id,
                Name = it.Name,
                CreatedAt = it.CreatedAt,
                UpdatedAt = it.UpdatedAt
            });
        }
    }
}
