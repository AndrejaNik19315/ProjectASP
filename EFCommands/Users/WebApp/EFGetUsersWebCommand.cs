using Application.Commands.Users.WebApp;
using Application.Dto.Characters;
using Application.Dto.Users;
using Application.Searches;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCommands.Users.WebApp
{
    public class EFGetUsersWebCommand : BaseEFCommand, IGetUsersWebCommand
    {
        public EFGetUsersWebCommand(ProjectContext context) : base(context)
        {
        }

        public IEnumerable<PartialUserDto> Execute(UserSearchWeb request)
        {
            var query = Context.Users.AsQueryable();

            if (request.Username != null)
            {
                query = query.Where(u => u.Username.ToLower().Contains(request.Username.ToLower()));
            }

            if (request.IsActive.HasValue)
            {
                query = query.Where(u => u.IsActive == request.IsActive);
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
                Characters = u.Characters.Select(c => new PartialCharacterDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Level = (int)c.Level,
                    GameClass = c.GameClass.Name,
                    Gender = c.Gender.Sex,
                    Race = c.Race.Name,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt
                }).ToList()
            }).OrderBy(u => u.Id);
        }
    }
}
