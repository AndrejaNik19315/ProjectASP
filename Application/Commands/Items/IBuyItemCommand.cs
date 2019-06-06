using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Application.Commands.Items
{
    public interface IBuyItemCommand : ICommand<int>
    {
    }
}
