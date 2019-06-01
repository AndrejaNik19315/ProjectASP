using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Items
{
    public interface IDeleteItemCommand : ICommand<int>
    {
    }
}
