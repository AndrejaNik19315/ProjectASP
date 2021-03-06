﻿using Application.Dto;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Users
{
    public interface IEditUserCommand : ICommand<UserDto, int, int>
    {

    }
}
