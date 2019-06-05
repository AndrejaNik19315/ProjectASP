using Application.Commands.Characters;
using Application.Dto;
using Application.Dto.Characters;
using Application.Dto.Inventories;
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

        public IEnumerable<FullCharacterDto> Execute(CharacterSearch request)
        {
            var query = Context.Characters
                .Include(c => c.GameClass)
                .Include(c => c.Race)
                .Include(c => c.Gender)
                .Include(c => c.Inventory)
                .AsQueryable();

            if (request.Name != null) {
                query = query.Where(c => c.Name.ToLower().Contains(request.Name.ToLower()));
            }

            if (request.Level != null) {
                query = query.Where(c => c.Level.Equals(request.Level));
            }

            if (request.MinFunds != null && request.MaxFunds != null) {
                query = query.Where(c => c.Funds >= request.MinFunds && c.Funds <= request.MaxFunds);
            }

            return query.Select(u => new FullCharacterDto
            {
                Id = u.Id,
                Name = u.Name,
                Level = (int)u.Level,
                Funds = (int)u.Funds,
                GameClass = u.GameClass.Name,
                Gender = u.Gender.Sex,
                Race = u.Race.Name,
                Invetory = new InventoryDto {
                    Id = u.Inventory.Id,
                    
                },
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt
            });
        }

        public IEnumerable<FullCharacterDto> Execute(CharacterSearch request, int id)
        {
            throw new NotImplementedException();
        }
    }
}
