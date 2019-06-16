using Application.Commands.Characters;
using Application.Dto;
using Application.Dto.Characters;
using Application.Dto.Inventories;
using Application.Interfaces;
using Application.Responses;
using Application.Searches;
using EFDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCommands.Characters
{
    public class EFGetCharactersCommand : BaseEFCommand, IGetCharactersCommand
    {
        public EFGetCharactersCommand(ProjectContext context) : base(context)
        {
        }

        public Paged<FullCharacterDto> Execute(CharacterSearch request)
        {
            var query = Context.Characters.AsQueryable();

            if (request.Name != null)
            {
                query = query.Where(c => c.Name.ToLower().Contains(request.Name.ToLower()));
            }

            if (request.Level != null)
            {
                query = query.Where(c => c.Level.Equals(request.Level));
            }

            if (request.MinFunds != null && request.MaxFunds != null)
            {
                query = query.Where(c => c.Funds >= request.MinFunds && c.Funds <= request.MaxFunds);
            }

            var totalCount = query.Count();

            var pagesCount = (int)Math.Ceiling((double)totalCount / request.PerPage);

            query = query
               .Include(c => c.GameClass)
               .Include(c => c.Race)
               .Include(c => c.Gender)
               .Include(c => c.Inventory)
               .Skip((request.PageNumber - 1) * request.PerPage)
               .Take(request.PerPage);

            return new Paged<FullCharacterDto> {
                CurrentPage = request.PageNumber,
                PagesCount = pagesCount,
                TotalCount = totalCount,
                Data = query.Select(c => new FullCharacterDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Level = (int)c.Level,
                    Funds = (int)c.Funds,
                    GameClass = c.GameClass.Name,
                    Gender = c.Gender.Sex,
                    Race = c.Race.Name,
                    UserId = c.UserId,
                    Invetory = new PartialInventoryDto
                    {
                        MaxSlots = c.Inventory.MaxSlots,
                        SlotsFilled = c.Inventory.SlotsFilled
                    },
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt
                })
            };
        }
    }
}
