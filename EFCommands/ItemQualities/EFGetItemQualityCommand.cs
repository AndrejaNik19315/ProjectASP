using Application.Commands.ItemQualities;
using Application.Dto;
using Application.Exceptions;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCommands.ItemQualities
{
    public class EFGetItemQualityCommand : BaseEFCommand, IGetItemQualityCommand
    {
        public EFGetItemQualityCommand(ProjectContext context) : base(context)
        {
        }

        public ItemQualityDto Execute(int request)
        {
            var itemQuality = Context.ItemQualities.Find(request);

            if (itemQuality == null)
                throw new EntityNotFoundException("ItemQuality not found.");

            return new ItemQualityDto
            {
                Id = itemQuality.Id,
                Name = itemQuality.Name,
                CreatedAt = itemQuality.CreatedAt,
                UpdatedAt = itemQuality.UpdatedAt
            };
        }

        public ItemQualityDto Execute(int request, int id)
        {
            throw new NotImplementedException();
        }
    }
}
