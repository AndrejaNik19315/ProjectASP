using Application.Commands.Characters;
using Application.Dto;
using Application.Dto.Inventories;
using Application.Dto.Items;
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

            var inventory = Context.Inventories
                .Include(i => i.InventoryItems)
                    .ThenInclude(it => it.Item)
                        .ThenInclude(i => i.ItemQuality)
                    .ThenInclude(iq => iq.Items)
                        .ThenInclude(i => i.ItemType)
                    .Where(i => i.CharacterId == request)
                    .FirstOrDefault();

            return new FullInventoryDto
            {
                Id = inventory.Id,
                MaxSlots = inventory.MaxSlots,
                SlotsFilled = inventory.SlotsFilled,
                InventoryItems = inventory.InventoryItems.Select(i => new PartialItemDto
                {
                    Name = i.Item.Name,
                    isCovert = i.Item.isCovert,
                    ItemQuality = i.Item.ItemQuality.Name,
                    ItemType = i.Item.ItemType.Name
                }),
                CreatedAt = inventory.CreatedAt,
                UpdatedAt = inventory.UpdatedAt
            };
        }
    }
}
