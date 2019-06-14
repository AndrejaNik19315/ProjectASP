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
    public class EFAddItemQualityCommand : BaseEFCommand, IAddItemQualityCommand
    {
        public EFAddItemQualityCommand(ProjectContext context) : base(context)
        {
        }

        public void Execute(ItemQualityDto request)
        {
            if (Context.ItemQualities.Any(iq => iq.Name.Trim().ToLower() == request.Name.Trim().ToLower()))
                throw new EntityAlreadyExistsException("ItemQuality with this name already exists.");

            Context.ItemQualities.Add(new Domain.ItemQuality
            {
                Name = request.Name
            });

            Context.SaveChanges();
        }
    }
}
