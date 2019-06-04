using Application.Dto;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Roles
{
    public interface IGetRoleCommand : ICommand<int, RoleDto>
    {
    }
}
