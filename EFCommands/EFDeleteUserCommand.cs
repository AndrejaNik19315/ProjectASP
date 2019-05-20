﻿using Application.Commands.Users;
using Application.Exceptions;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCommands
{
    public class EFDeleteUserCommand : BaseEFCommand, IDeleteUserCommand
    {
        public EFDeleteUserCommand(ProjectContext context) : base(context)
        {
        }

        public void Execute(int request)
        {
            var user = Context.Users.Find(request);

            if (user == null)
                throw new EntityNotFoundException();

            Context.Users.Remove(user);

            Context.SaveChanges();
        }
    }
}
