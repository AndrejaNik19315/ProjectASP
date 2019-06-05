using Application.Dto;
using Application.Dto.Users;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Users
{
    public interface IGetUserCommand : ICommand<int, FullUserDto>
    {

    }
}
