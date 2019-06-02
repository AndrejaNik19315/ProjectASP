using Application.Commands.ItemQualities;
using Application.Exceptions;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCommands.ItemQualities
{
    public class EFDeleteItemQualityCommand : BaseEFCommand, IDeleteItemQualityCommand
    {
        public EFDeleteItemQualityCommand(ProjectContext context) : base(context)
        {
        }

        public void Execute(int request)
        {
            var itemQuality = Context.ItemQualities.Find(request);

            if (itemQuality == null)
                throw new EntityNotFoundException("ItemQuality not found.");

            Context.ItemQualities.Remove(itemQuality);
            Context.SaveChanges();
        }

        public void Execute(int request, int id)
        {
            throw new NotImplementedException();
        }
    }
}
