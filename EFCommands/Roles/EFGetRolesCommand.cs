using Application.Commands.Roles;
using Application.Dto;
using Application.Searches;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCommands.Roles
{
    public class EFGetRolesCommand : BaseEFCommand, IGetRolesCommand
    {
        public EFGetRolesCommand(ProjectContext context) : base(context)
        {
        }

        public IEnumerable<RoleDto> Execute(RoleSearch request)
        {
            var query = Context.Roles.AsQueryable();

            if (request.Name != null)
                query = Context.Roles.Where(r => r.Name.Trim().ToLower().Contains(request.Name.Trim().ToLower()));

            return query.Select(r => new RoleDto
            {
                Id = r.Id,
                Name = r.Name,
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt
            });
        }

        public IEnumerable<RoleDto> Execute(RoleSearch request, int id)
        {
            throw new NotImplementedException();
        }
    }
}
