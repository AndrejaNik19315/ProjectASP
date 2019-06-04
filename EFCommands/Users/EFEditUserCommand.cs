using Application.Commands.Users;
using Application.Dto;
using Application.Exceptions;
using Domain;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCommands.Users
{
    public class EFEditUserCommand : BaseEFCommand, IEditUserCommand
    {
        public EFEditUserCommand(ProjectContext context) : base(context)
        {
        }

        public void Execute(UserDto request, int id)
        {
            var user = Context.Users.Find(id);

            if (user == null)
                throw new EntityNotFoundException("User not found.");

            if(request.Username != user.Username || request.Email != user.Email)
                if (Context.Users.Any(u => u.Username.ToLower() == request.Username.ToLower() && u.Id != id || u.Email == request.Email && u.Id != id))
                    throw new EntityAlreadyExistsException("Username or Email already exist.");

            if (request.RoleId != user.RoleId)
                if (!(Context.Roles.Any(r => r.Id == user.RoleId)))
                    throw new EntityUnprocessableException("No such role.");

            user.Username = request.Username;
            user.Email = request.Email;
            user.Firstname = request.Firstname;
            user.Lastname = request.Lastname;
            if (request.Password != user.Password)
                user.Password = request.Password;
            if(request.IsActive != user.IsActive)
                user.IsActive = request.IsActive;

            user.UpdatedAt = DateTime.Now;

            Context.SaveChanges();
        }

        public void Execute(UserDto request)
        {
            throw new NotImplementedException();
        }
    }
}
