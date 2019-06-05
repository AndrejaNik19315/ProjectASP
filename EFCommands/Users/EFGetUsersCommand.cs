using Application.Commands.Users;
using Application.Dto;
using Application.Dto.Characters;
using Application.Dto.Users;
using Application.Searches;
using EFDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCommands.Users
{
    public class EFGetUsersCommand : BaseEFCommand, IGetUsersCommand
    {
        public EFGetUsersCommand(ProjectContext context) : base(context)
        {
        }

        public IEnumerable<PartialUserDto> Execute(UserSearch request)
        {
            var query = Context.Users.AsQueryable();

            if (request.Username != null) {
                query = query.Where(u => u.Username.ToLower().Contains(request.Username.ToLower()));
            }

            if (request.IsActive.HasValue) {
                query = query.Where(u => u.IsActive);
            }

            return query.Select(u => new PartialUserDto
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                IsActive = u.IsActive,
                Role = u.Role.Name,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt,
                Characters = u.Characters.Select(c => new PartialCharacterDto {
                    Id = c.Id,
                    Name = c.Name,
                    Level = (int)c.Level,
                    GameClass = c.GameClass.Name,
                    Gender = c.Gender.Sex,
                    Race = c.Race.Name,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt
                }).ToList()
            }).ToList();
        }

        public IEnumerable<PartialUserDto> Execute(UserSearch request, int id)
        {
            throw new NotImplementedException();
        }
    }
}
