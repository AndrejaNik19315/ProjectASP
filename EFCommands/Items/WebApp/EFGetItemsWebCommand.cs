using Application.Commands.Items.WebApp;
using Application.Dto.Items;
using Application.Searches;
using EFDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCommands.Items.WebApp
{
    public class EFGetItemsWebCommand : BaseEFCommand, IGetItemsWebCommand
    {
        public EFGetItemsWebCommand(ProjectContext context) : base(context)
        {
        }

        public IEnumerable<FullItemDto> Execute(ItemSearchWeb request)
        {
            var query = Context.Items
                .Include(i => i.ItemQuality)
                .Include(i => i.ItemType)
                .AsQueryable();

            if (request.Name != null)
                query = query.Where(i => i.Name.ToLower().Contains(request.Name.ToLower()));

            if (request.isCovert.HasValue)
                query = query.Where(i => i.isCovert == request.isCovert);

            if (request.MinPrice != null && request.MaxPrice != null)
                query = query.Where(i => i.Cost >= request.MinPrice && i.Cost <= request.MaxPrice);

            return query.Select(i => new FullItemDto
            {
                Id = i.Id,
                Name = i.Name,
                Cost = i.Cost,
                isCovert = i.isCovert,
                ItemQuality = i.ItemQuality.Name,
                ItemType = i.ItemType.Name,
                inStock = i.inStock,
                Quantity = i.Quantity,
                ImageName = i.ImageAlt,
                ImagePath = i.ImagePath,
                CreatedAt = i.CreatedAt,
                UpdatedAt = i.UpdatedAt
            });
        }
    }
}
