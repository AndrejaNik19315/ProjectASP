using Application.Commands.Characters;
using Application.Dto;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCommands.Characters
{
    public class EFEditCharacterCommand : BaseEFCommand, IEditCharacterCommand
    {
        public EFEditCharacterCommand(ProjectContext context) : base(context)
        {
        }

        public CharacterDto Execute(int request)
        {
            throw new NotImplementedException();
        }
    }
}
