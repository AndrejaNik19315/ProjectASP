using Application.Commands.Items;
using Application.Dto;
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

        public Paged<ItemDto> Execute(ItemSearch request)
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

            var pagesCount = (int)Math.Ceiling((double)totalCount / request.PerPage);

            query = query.Skip((request.PageNumber - 1) * request.PerPage).Take(request.PerPage);


            return new Paged<ItemDto>
            {
                CurrentPage = request.PageNumber,
                PagesCount = pagesCount,
                TotalCount = totalCount,
                Data = query.Select(i => new ItemDto {
                    Id = i.Id,
                    Name = i.Name,
                    Cost = i.Cost,
                    isCovert = i.isCovert,
                    isForSale = i.isForSale,
                    ItemQualityId = i.ItemQualityId,
                    ItemTypeId = i.ItemTypeId,
                    CreatedAt = i.CreatedAt,
                    UpdatedAt = i.UpdatedAt
                })  
            };

            //return query.Select(i => new ItemDto
            //{
            //    Id = i.Id,
            //    Name = i.Name,
            //    Cost = i.Cost,
            //    isCovert = i.isCovert,
            //    isForSale = i.isForSale,
            //    ItemTypeId = i.ItemTypeId,
            //    ItemQualityId = i.ItemQualityId,
            //    CreatedAt = i.CreatedAt,
            //    UpdatedAt = i.UpdatedAt
            //}).OrderBy(i => i.Id);
        }

        public Paged<ItemDto> Execute(ItemSearch request, int id)
        {
            throw new NotImplementedException();
        }
    }
}
