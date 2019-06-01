using Application.Commands.Items;
using Application.Dto;
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

        public ItemDto Execute(int request)
        {
            var item = Context.Items.Find(request);

            if (item == null)
                throw new EntityNotFoundException("Item not found.");

            return new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Cost = item.Cost,
                isCovert = item.isCovert,
                isForSale = item.isForSale,
                ItemTypeId = item.ItemTypeId,
                ItemQualityId = item.ItemQualityId,
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt
            };
        }

        public ItemDto Execute(int request, int id)
        {
            throw new NotImplementedException();
        }
    }
}
