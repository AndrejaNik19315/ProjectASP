using Application.Commands.Items;
using Application.Exceptions;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCommands.Items
{
    public class EFDeleteItemCommand : BaseEFCommand, IDeleteItemCommand
    {
        public EFDeleteItemCommand(ProjectContext context) : base(context)
        {
        }

        public void Execute(int request)
        {
            var item = Context.Items.Find(request);

            if (item == null)
                throw new EntityNotFoundException();

            Context.Items.Remove(item);

            Context.SaveChanges();
        }
    }
}
