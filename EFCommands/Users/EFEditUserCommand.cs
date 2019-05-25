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

        public void Execute(UserDto request)
        {
            //User user = new User
            //{
            //    Firstname = request.Firstname,
            //    Lastname = request.Lastname,
            //    Username = request.Username,
            //    Email = request.Email,
            //    IsActive = request.IsActive,
            //    UpdatedAt = new DateTime()
            //};

            //Context.Users.Attach(user);
            //var entry = Context.Entry(user);

            //if (user.Firstname != null)
            //    entry.Property(u => u.Firstname).IsModified = true;
            //if (user.Lastname != null)
            //    entry.Property(u => u.Lastname).IsModified = true;
            //if (user.Username != null)
            //    entry.Property(u => u.Username).IsModified = true;
            //if (user.Email != null)
            //    entry.Property(u => u.Email).IsModified = true;

            //var user = Context.Users.Find(request.Id);

            //if (user == null)
            //    throw new EntityNotFoundException();


            //if (Context.Users.Any(u => u.Username == request.Username || u.Email == request.Email))
            //    throw new EntityAlreadyExistsException();

            Context.SaveChanges();
        }
    }
}
