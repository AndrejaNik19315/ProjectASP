using Application.Commands.Items;
using Application.Dto;
using Application.Searches;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCommands.Items
{
    public class EFGetItemsCommand : BaseEFCommand, IGetItemsCommand
    {
        public EFGetItemsCommand(ProjectContext context) : base(context)
        {
        }

        public IEnumerable<ItemDto> Execute(ItemSearch request)
        {
            var query = Context.Items.AsQueryable();

            if(request.Name != null)
                query = query.Where(i => i.Name.ToLower().Contains(request.Name.ToLower()));

            if (request.isCovert)
                query = query.Where(i => i.isCovert == request.isCovert);

            if (request.isForSale)
                query = query.Where(i => i.isForSale == request.isForSale);

            if (request.MinPrice != null && request.MaxPrice != null)
                query = query.Where(i => i.Cost >= request.MinPrice && i.Cost <= request.MaxPrice);

            return query.Select(i => new ItemDto
            {
                Id = i.Id,
                Name = i.Name,
                Cost = i.Cost,
                isCovert = i.isCovert,
                isForSale = i.isForSale,
                ItemTypeId = i.ItemTypeId,
                ItemQualityId = i.ItemQualityId
            }).OrderBy(i => i.Id);
        }

        public IEnumerable<ItemDto> Execute(ItemSearch request, int id)
        {
            throw new NotImplementedException();
        }
    }
}
