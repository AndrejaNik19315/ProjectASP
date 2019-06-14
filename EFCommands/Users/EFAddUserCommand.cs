using Application.Commands.Users;
using Application.Dto;
using Application.Exceptions;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCommands.Users
{
    public class EFAddUserCommand : BaseEFCommand, IAddUserCommand
    {
        public EFAddUserCommand(ProjectContext context) : base(context)
        {
        }

        public void Execute(UserDto request)
        {
            if (Context.Users.Any(u => u.Username == request.Username || u.Email == request.Email))
                throw new EntityAlreadyExistsException("Username or Email already exist.");

            if (!(Context.Roles.Any(r => r.Id == request.RoleId)))
                throw new EntityUnprocessableException("No such role.");

            Context.Users.Add(new Domain.User {
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
                IsActive = request.IsActive,
                RoleId = request.RoleId
            });

            Context.SaveChanges();
        }
    }
}
