using Application.Commands.ItemTypes;
using Application.Dto;
using Application.Exceptions;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCommands.ItemTypes
{
    public class EFGetItemTypeCommand : BaseEFCommand, IGetItemTypeCommand
    {
        public EFGetItemTypeCommand(ProjectContext context) : base(context)
        {
        }

        public ItemTypeDto Execute(int request)
        {
            var itemType = Context.ItemTypes.Find(request);

            if (itemType == null)
                throw new EntityNotFoundException("ItemType not found.");

            return new ItemTypeDto
            {
                Id = itemType.Id,
                Name = itemType.Name,
                CreatedAt = itemType.CreatedAt,
                UpdatedAt = itemType.UpdatedAt
            };
        }
    }
}
