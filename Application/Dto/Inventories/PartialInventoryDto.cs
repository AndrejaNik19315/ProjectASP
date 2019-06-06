using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto.Inventories
{
    public class PartialInventoryDto
    {
        public int SlotsFilled { get; set; }
        public int? MaxSlots { get; set; }
    }
}
