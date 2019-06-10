using Application.Dto;
using Application.HelperClasses;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Authorization
{
    public interface IGetAuthUserCommand : ICommand<LoginDto, LoggedUser>
    {

    }
}
