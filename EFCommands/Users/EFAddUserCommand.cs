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
                throw new EntityAlreadyExistsException();

            Context.Users.Add(new Domain.User {
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                Username = request.Username,
                Email = request.Email,
                IsActive = request.IsActive
            });

            Context.SaveChanges();
        }

        public void Execute(UserDto request, int id)
        {
            throw new NotImplementedException();
        }
    }
}
