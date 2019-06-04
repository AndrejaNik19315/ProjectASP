using Application.Commands.ItemTypes;
using Application.Dto;
using Application.Exceptions;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCommands.ItemTypes
{
    public class EFAddItemTypeCommand : BaseEFCommand, IAddItemTypeCommand
    {
        public EFAddItemTypeCommand(ProjectContext context) : base(context)
        {
        }

        public void Execute(ItemTypeDto request)
        {
            if (Context.ItemTypes.Any(it => it.Name.ToLower() == request.Name.ToLower()))
                throw new EntityAlreadyExistsException("ItemType with this name already exists.");

            Context.ItemTypes.Add(new Domain.ItemType
            {
                Name = request.Name
            });

            Context.SaveChanges();

        }

        public void Execute(ItemTypeDto request, int id)
        {
            throw new NotImplementedException();
        }
    }
}
