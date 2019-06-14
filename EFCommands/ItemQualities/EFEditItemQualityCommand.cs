using Application.Commands.ItemQualities;
using Application.Dto;
using Application.Exceptions;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCommands.ItemQualities
{
    public class EFEditItemQualityCommand : BaseEFCommand, IEditItemQualityCommand
    {
        public EFEditItemQualityCommand(ProjectContext context) : base(context)
        {
        }

        public void Execute(ItemQualityDto request, int id)
        {
            var itemQuality = Context.ItemQualities.Find(id);

            if (itemQuality == null)
                throw new EntityNotFoundException("ItemQuality not found.");

            if (itemQuality.Name != request.Name)
                if (Context.ItemQualities.Any(iq => iq.Name.Trim().ToLower() == request.Name.Trim().ToLower()))
                    throw new EntityAlreadyExistsException("ItemQuality with this name already exists.");

            itemQuality.Name = request.Name;
            itemQuality.UpdatedAt = DateTime.Now;

            Context.SaveChanges();
        }
    }
}
