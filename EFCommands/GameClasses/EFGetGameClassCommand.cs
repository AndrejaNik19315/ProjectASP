using Application.Commands.GameClasses;
using Application.Dto;
using Application.Exceptions;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCommands.GameClasses
{
    public class EFGetGameClassCommand : BaseEFCommand, IGetGameClassCommand
    {
        public EFGetGameClassCommand(ProjectContext context) : base(context)
        {
        }

        public GameClassDto Execute(int request)
        {
            var gameClass = Context.GameClasses.Find(request);

            if (gameClass == null)
                throw new EntityNotFoundException();

            return new GameClassDto
            {
                Id = gameClass.Id,
                Name = gameClass.Name,
                CreatedAt = gameClass.CreatedAt,
                UpdatedAt = gameClass.UpdatedAt
            };
        }

        public GameClassDto Execute(int request, int id)
        {
            throw new NotImplementedException();
        }
    }
}
