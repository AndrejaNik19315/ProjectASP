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
                query = Context.ItemTypes.Where(itm => itm.Name.ToLower().Contains(request.Name.ToLower()));

            return query.Select(itm => new ItemTypeDto
            {
                Id = itm.Id,
                Name = itm.Name
            }).OrderBy(itm => itm.Id);
        }

        public IEnumerable<ItemTypeDto> Execute(ItemTypeSearch request, int id)
        {
            throw new NotImplementedException();
        }
    }
}
