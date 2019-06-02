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

            if (!(itemQuality.Name.ToLower().Equals(request.Name.ToLower())))
                if (Context.ItemQualities.Any(itm => itm.Name.ToLower() == request.Name.ToLower()))
                    throw new EntityAlreadyExistsException("ItemQuality already exists.");

            itemQuality.Name = request.Name;
            itemQuality.UpdatedAt = DateTime.Now;

            Context.SaveChanges();
        }

        public void Execute(ItemQualityDto request)
        {
            throw new NotImplementedException();
        }
    }
}
