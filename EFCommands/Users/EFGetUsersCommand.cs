﻿using Application.Commands.Users;
using Application.Dto;
using Application.Searches;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCommands.Users
{
    public class EFGetUsersCommand : BaseEFCommand, IGetUsersCommand
    {
        public EFGetUsersCommand(ProjectContext context) : base(context)
        {
        }

        public IEnumerable<UserDto> Execute(UserSearch request)
        {
            var query = Context.Users.AsQueryable();

            if (request.Username != null) {
                query = query.Where(u => u.Username.ToLower().Contains(request.Username.ToLower()));
            }

            if (request.IsActive.HasValue) {
                query = query.Where(u => u.IsActive);
            }

            return query.Select(u => new UserDto
            {
                Id = u.Id,
                Firstname = u.Firstname,
                Lastname = u.Lastname,
                Username = u.Username,
                Email = u.Email,
                IsActive = u.IsActive
            });
        }
    }
}