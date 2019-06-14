using Application.Commands.ItemTypes;
using Application.Exceptions;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCommands.ItemTypes
{
    public class EFDeleteItemTypeCommand : BaseEFCommand, IDeleteItemTypeCommand
    {
        public EFDeleteItemTypeCommand(ProjectContext context) : base(context)
        {
        }

        public void Execute(int request)
        {
            var itemType = Context.ItemTypes.Find(request);

            if (itemType == null)
                throw new EntityNotFoundException("ItemType not found.");

            Context.ItemTypes.Remove(itemType);
            Context.SaveChanges();
        }
    }
}
