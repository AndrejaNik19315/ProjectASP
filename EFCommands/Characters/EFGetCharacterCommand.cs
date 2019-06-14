using Application.Commands.Characters;
using Application.Dto;
using Application.Dto.Characters;
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
    public class EFGetCharacterCommand : BaseEFCommand, IGetCharacterCommand
    {
        public EFGetCharacterCommand(ProjectContext context) : base(context)
        {
        }

        public FullCharacterDto Execute(int request)
        {
            var character = Context.Characters
                .Include(c => c.GameClass)
                .Include(c => c.GameClass)
                .Include(c => c.Race)
                .Include(c => c.Gender)
                .Include(c => c.Inventory)
                .FirstOrDefault(c => c.Id == request);

            if (character == null)
                throw new EntityNotFoundException("Character not found.");

            return new FullCharacterDto
            {
                Id = character.Id,
                UserId = character.UserId,
                Name = character.Name,
                Level = (int)character.Level,
                Funds = (int)character.Funds,
                GameClass = character.GameClass.Name,
                Gender = character.Gender.Sex,
                Race = character.Race.Name,
                Invetory = new PartialInventoryDto
                {
                    MaxSlots = character.Inventory.MaxSlots,
                    SlotsFilled = character.Inventory.SlotsFilled
                },
                CreatedAt = character.CreatedAt,
                UpdatedAt = character.UpdatedAt
            };
        }
    }
}
