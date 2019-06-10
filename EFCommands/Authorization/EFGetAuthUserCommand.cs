using Application.Commands.Authorization;
using Application.Dto;
using Application.Exceptions;
using Application.HelperClasses;
using Application.Interfaces;
using EFDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCommands.Authorization
{
    public class EFGetAuthUserCommand : BaseEFCommand, IGetAuthUserCommand
    {
        public EFGetAuthUserCommand(ProjectContext context) : base(context)
        {
        }

        public LoggedUser Execute(LoginDto request)
        {
            var user = Context.Users
                .Include(u => u.Role)
                .Where(u => u.Username.Equals(request.Username) && u.Password.Equals(request.Password))
                .SingleOrDefault();

            if (user == null)
                throw new EntityNotFoundException("Invalid Username or password.");

            if (!user.IsActive)
                throw new EntityNotActiveException("This user account is not active.");

            return new LoggedUser
            {
                Id = user.Id,
                Username = user.Username,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Role = user.Role.Name
            };
        }

        public LoggedUser Execute(LoginDto request, int id)
        {
            throw new NotImplementedException();
        }

    }
}
