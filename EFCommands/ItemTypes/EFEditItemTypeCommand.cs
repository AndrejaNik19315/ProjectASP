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
    public class EFEditItemTypeCommand : BaseEFCommand, IEditItemTypeCommand
    {
        public EFEditItemTypeCommand(ProjectContext context) : base(context)
        {
        }

        public void Execute(ItemTypeDto request, int id)
        {
            var itemType = Context.ItemTypes.Find(id);

            if (itemType == null)
                throw new EntityNotFoundException("EntityType not found.");

            if (request.Name.ToLower() != itemType.Name.ToLower())
                if (Context.ItemTypes.Any(it => it.Name.ToLower() == request.Name.ToLower()))
                    throw new EntityAlreadyExistsException("ItemType with this name already exists.");

            itemType.Name = request.Name;
            itemType.UpdatedAt = DateTime.Now;

            Context.SaveChanges();
        }

        public void Execute(ItemTypeDto request)
        {
            throw new NotImplementedException();
        }
    }
}
