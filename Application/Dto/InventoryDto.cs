using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto
{
    public class InventoryDto : BaseEntityDto
    {
        public int SlotsFilled { get; set; }
        public int? MaxSlots { get; set; }
    }
}
