using Application.Commands.Items;
using Application.Dto;
using Application.Exceptions;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCommands.Items
{
    public class EFAddItemCommand : BaseEFCommand, IAddItemCommand
    {
        public EFAddItemCommand(ProjectContext context) : base(context)
        {
        }

        public void Execute(ItemDto request)
        {
            if (Context.Items.Any(i => i.Name == request.Name))
                throw new EntityAlreadyExistsException("Item with that name already exists.");

            if (!(Context.ItemTypes.Any(it => it.Id == request.ItemTypeId)))
                throw new EntityUnprocessableException("Type doesn't exist.");

            if (!(Context.ItemQualities.Any(iq => iq.Id == request.ItemQualityId)))
                throw new EntityUnprocessableException("Quality doesn't exist.");

            Context.Items.Add(new Domain.Item
            {
                Name = request.Name,
                Cost = request.Cost,
                isCovert = request.isCovert,
                isForSale = request.isForSale,
                ItemTypeId = request.ItemTypeId,
                ItemQualityId = request.ItemQualityId
            });

            Context.SaveChanges();
        }

        public void Execute(ItemDto request, int id)
        {
            throw new NotImplementedException();
        }
    }
}
