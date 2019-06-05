using Application.Commands.Users;
using Application.Dto;
using Application.Dto.Characters;
using Application.Dto.Users;
using Application.Exceptions;
using Application.Interfaces;
using EFDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCommands.Users
{
    public class EFGetUserCommand : BaseEFCommand, IGetUserCommand
    {
        public EFGetUserCommand(ProjectContext context) : base(context)
        {
        }

        public FullUserDto Execute(int request)
        {
            var user = Context.Users
                .Include(u => u.Characters)
                .ThenInclude(c => c.GameClass)
                .Include(u => u.Characters)
                .ThenInclude(c => c.Gender)
                .Include(u => u.Characters)
                .ThenInclude(c => c.Race)
                .Include(u => u.Characters)
                .ThenInclude(c => c.Inventory)
                .Include(u => u.Role)
                .AsQueryable()
                .SingleOrDefault(u => u.Id == request);

            if (user == null)
                throw new EntityNotFoundException("User not found.");

            return new FullUserDto
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Username = user.Username,
                Email = user.Email,
                IsActive = user.IsActive,
                Role = user.Role.Name,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                Characters = user.Characters.Select(c => new FullCharacterDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Level = (int)c.Level,
                    Funds = (int)c.Funds,
                    GameClass = c.GameClass.Name,
                    Gender = c.Gender.Sex,
                    Race = c.Race.Name,
                    Invetory = new InventoryDto
                    {
                        Id = c.Inventory.Id,
                        MaxSlots = c.Inventory.MaxSlots,
                        SlotsFilled = c.Inventory.SlotsFilled,
                        CharacterId = c.Inventory.CharacterId,
                        CreatedAt = c.Inventory.CreatedAt,
                        UpdatedAt = c.Inventory.UpdatedAt
                    },
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                }).ToList()
            };
        }

        public FullUserDto Execute(int request, int id)
        {
            throw new NotImplementedException();
        }
    }
}
