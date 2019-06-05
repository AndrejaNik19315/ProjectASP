using Application.Commands.Characters;
using Application.Dto;
using Application.Dto.Inventories;
using Application.Exceptions;
using EFDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCommands.Characters
{
    public class EFGetCharacterInventoryCommand : BaseEFCommand, IGetCharacterInventoryCommand
    {
        public EFGetCharacterInventoryCommand(ProjectContext context) : base(context)
        {
        }

        public FullInventoryDto Execute(int request)
        {
            if (Context.Characters.Find(request) == null)
                throw new EntityNotFoundException("Character not found.");

            //TODO fix.
            //var inventory = Context.Inventories
            //    .Include(i => i.InventoryItems)
            //    .ThenInclude(ii => ii.)
            //    .AsQueryable()
            //    .SingleOrDefault(i => i.CharacterId == request);

            return new FullInventoryDto
            {
                Id = inventory.Id,
                MaxSlots = inventory.MaxSlots,
                SlotsFilled = inventory.SlotsFilled,
                InventoryItems = inventory.InventoryItems.Select(i => new ItemDto {
                    Id = i.ItemId,
                    Name = i.Item.Name,
                    Cost = i.Item.Cost,
                    isForSale = i.Item.isForSale,
                    isCovert = i.Item.isCovert,
                    ItemQualityId = i.Item.ItemQualityId,
                    ItemTypeId = i.Item.ItemTypeId,
                    CreatedAt = i.Item.CreatedAt,
                    UpdatedAt = i.Item.UpdatedAt
                }),
                CreatedAt = inventory.CreatedAt,
                UpdatedAt = inventory.UpdatedAt
            };
        }

        public FullInventoryDto Execute(int request, int id)
        {
            throw new NotImplementedException();
        }
    }
}
