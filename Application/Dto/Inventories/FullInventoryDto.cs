using Application.Dto.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto.Inventories
{
    public class FullInventoryDto : BaseEntityDto
    {
        public int SlotsFilled { get; set; }
        public int? MaxSlots { get; set; }
        public int SlotsLeft {
            get {
                return (int) (MaxSlots - SlotsFilled);
            }
        }
        public IEnumerable<PartialItemDto> InventoryItems { get; set; }
    }
}
