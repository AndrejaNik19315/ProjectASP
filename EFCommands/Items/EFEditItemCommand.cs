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
    public class EFEditItemCommand : BaseEFCommand, IEditItemCommand
    {
        public EFEditItemCommand(ProjectContext context) : base(context)
        {
        }

        public void Execute(ItemDto request, int id)
        {
            var item = Context.Items.Find(id);

            if (item == null)
                throw new EntityNotFoundException("Item not found.");

            if (request.Name != item.Name)
                if (Context.Items.Any(i => i.Name == request.Name))
                    throw new EntityAlreadyExistsException("Item with that name already exists.");

            if (request.ItemTypeId != item.ItemTypeId)
                if (!(Context.ItemTypes.Any(t => t.Id == request.ItemTypeId)))
                    throw new EntityUnprocessableException("Type doesn't exist.");

            if (request.ItemQualityId != item.ItemQualityId)
                if (!(Context.ItemQualities.Any(q => q.Id == request.ItemQualityId)))
                    throw new EntityUnprocessableException("Quality doesn't exist.");

            if(request.Quantity != item.Quantity)

            item.Name = request.Name;
            item.Cost = request.Cost;
            item.isCovert = request.isCovert;
            item.isForSale = request.isForSale;
            item.ItemQualityId = request.ItemQualityId;
            item.ItemTypeId = request.ItemTypeId;
            item.Quantity = (item.Quantity != request.Quantity && request.Quantity >= 0)? request.Quantity : item.Quantity;
            item.UpdatedAt = DateTime.Now;

            Context.SaveChanges();
        }

        public void Execute(ItemDto request)
        {
            throw new NotImplementedException();
        }
    }
}
