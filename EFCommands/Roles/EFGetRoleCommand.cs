using Application.Commands.Roles;
using Application.Dto;
using Application.Exceptions;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCommands.Roles
{
    public class EFGetRoleCommand : BaseEFCommand, IGetRoleCommand
    {
        public EFGetRoleCommand(ProjectContext context) : base(context)
        {
        }

        public RoleDto Execute(int request)
        {
            var role = Context.Roles.Find(request);

            if (role == null)
                throw new EntityNotFoundException("Role not found.");

            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
                CreatedAt = role.CreatedAt,
                UpdatedAt = role.UpdatedAt
            };
        }

        public RoleDto Execute(int request, int id)
        {
            throw new NotImplementedException();
        }
    }
}
