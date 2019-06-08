using Application.Commands.Orders;
using Application.Dto;
using Application.Exceptions;
using EFDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCommands.Orders
{
    public class EFMakeOrderCommand : BaseEFCommand, IMakeOrderCommand
    {
        public EFMakeOrderCommand(ProjectContext context) : base(context)
        {
        }

        public void Execute(OrderDto request)
        {
            var character = Context.Characters
                .Include(c => c.User)
                .Include(c => c.Inventory)
                    .ThenInclude(i => i.InventoryItems)
                .FirstOrDefault(c => c.Id == request.CharacterId);

            if (character == null)
                throw new EntityNotFoundException("Character not found.");

            if (!character.User.IsActive) //greska user nije ucitan
                throw new EntityNotActiveException("User is not active.");

            var item = Context.Items.Find(request.ItemId);

            if (item == null)
                throw new EntityNotFoundException("Item not found.");

            if (!item.inStock)
                throw new EntityUnprocessableException("Item not in stock.");

            var cost = (item.isCovert == true) ? ((item.Cost / 100) * 25 + item.Cost) : item.Cost;

            if (cost > 1000)
                cost = 1000;

            if (character.Funds < cost)
                throw new EntityUnprocessableException("Need more gold!");

            if (!(character.Inventory.MaxSlots - character.Inventory.SlotsFilled > 0))
                throw new EntityUnprocessableException("Not enought place in inventory.");

            //Transaction

            //Make new order
            Context.Orders.Add(new Domain.Order
            {
                CharacterId = character.Id,
                ItemId = item.Id,
                Cost = cost
            });

            //AddToInventory
            character.Inventory.InventoryItems.Add(new Domain.InventoryItem
            {
                InventoryId = character.Inventory.Id,
                ItemId = item.Id
            });

            //Update character&inventory
            character.Name = character.Name;
            character.Level = character.Level;
            character.RaceId = character.RaceId;
            character.UserId = character.UserId;
            character.GameClassId = character.GameClassId;
            character.GenderId = character.GenderId;
            character.Funds = character.Funds - cost;
            character.Inventory.SlotsFilled = character.Inventory.SlotsFilled + 1;

            Context.SaveChanges();
        }

        public void Execute(OrderDto request, int id)
        {
            throw new NotImplementedException();
        }
    }
}
