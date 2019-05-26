using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Characters
{
    public interface IDeleteCharacterCommand : ICommand<int>
    {
    }
}
