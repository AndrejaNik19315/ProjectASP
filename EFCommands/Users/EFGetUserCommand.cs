using Application.Commands.Users;
using Application.Dto;
using Application.Exceptions;
using Application.Interfaces;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCommands.Users
{
    public class EFGetUserCommand : BaseEFCommand, IGetUserCommand
    {
        public EFGetUserCommand(ProjectContext context) : base(context)
        {
        }

        public UserDto Execute(int request)
        {
            var user = Context.Users.Find(request);

            if (user == null)
                throw new EntityNotFoundException("User not found.");

            return new UserDto
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Username = user.Username,
                Email = user.Email,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }

        public UserDto Execute(int request, int id)
        {
            throw new NotImplementedException();
        }
    }
}
