using Application.Dto.Users;
using Application.Interfaces;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Users.WebApp
{
    public interface IGetUsersWebCommand : ICommand<UserSearchWeb, IEnumerable<PartialUserDto>>
    {
    }
}
