using Application.Commands.Items;
using Application.Dto;
using Application.Dto.Items;
using Application.Responses;
using Application.Searches;
using EFDataAccess;
using Microsoft.EntityFrameworkCore;
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

        public Paged<FullItemDto> Execute(ItemSearch request)
        {
            var query = Context.Items.AsQueryable();

            if(request.Name != null)
                query = query.Where(i => i.Name.ToLower().Contains(request.Name.ToLower()));

            if (request.isCovert.HasValue)
                query = query.Where(i => i.isCovert == request.isCovert);

            if (request.isForSale.HasValue)
                query = query.Where(i => i.isForSale == request.isForSale);

            if (request.MinPrice != null && request.MaxPrice != null)
                query = query.Where(i => i.Cost >= request.MinPrice && i.Cost <= request.MaxPrice);

            var totalCount = query.Count();

            var pagesCount = (int) Math.Ceiling((double)totalCount / request.PerPage);

            query = query
                .Include(i => i.ItemQuality)
                .Include(i => i.ItemType)
                .Skip((request.PageNumber - 1) * request.PerPage).Take(request.PerPage);


            return new Paged<FullItemDto>
            {
                CurrentPage = request.PageNumber,
                PagesCount = pagesCount,
                TotalCount = totalCount,
                Data = query.Select(i => new FullItemDto {
                    Id = i.Id,
                    Name = i.Name,
                    Cost = i.Cost,
                    isCovert = i.isCovert,
                    isForSale = i.isForSale,
                    ItemQuality= i.ItemQuality.Name,
                    ItemType = i.ItemType.Name,
                    Quantity = i.Quantity,
                    CreatedAt = i.CreatedAt,
                    UpdatedAt = i.UpdatedAt
                })  
            };
        }

        public Paged<FullItemDto> Execute(ItemSearch request, int id)
        {
            throw new NotImplementedException();
        }
    }
}
