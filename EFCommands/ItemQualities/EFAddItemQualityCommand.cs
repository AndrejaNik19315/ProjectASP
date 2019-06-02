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
            if(Context.ItemQualities.Any(itm => itm.Name.ToLower() == request.Name.ToLower()))
                throw new EntityAlreadyExistsException("ItemQuality already exists.");

            Context.ItemQualities.Add(new Domain.ItemQuality {
                Id = request.Id,
                Name = request.Name,
                CreatedAt = request.CreatedAt,
                UpdatedAt = request.UpdatedAt
            });

            Context.SaveChanges();
        }

        public void Execute(ItemQualityDto request, int id)
        {
            throw new NotImplementedException();
        }
    }
}
