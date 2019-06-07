using Application.Commands.Items;
using Application.Dto;
using Application.Dto.Items;
using Application.Exceptions;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCommands.Items
{
    public class EFGetItemCommand : BaseEFCommand, IGetItemCommand
    {
        public EFGetItemCommand(ProjectContext context) : base(context)
        {
        }

        public FullItemDto Execute(int request)
        {
            var item = Context.Items.Find(request);

            if (item == null)
                throw new EntityNotFoundException("Item not found.");

            return new FullItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Cost = item.Cost,
                isCovert = item.isCovert,
                isForSale = item.isForSale,
                ItemType = item.ItemType.Name,
                ItemQuality = item.ItemQuality.Name,
                Quantity = item.Quantity,
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt
            };
        }

        public FullItemDto Execute(int request, int id)
        {
            throw new NotImplementedException();
        }
    }
}
