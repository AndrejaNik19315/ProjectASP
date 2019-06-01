using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dto
{
    public class InventoryDto : BaseEntityDto
    {
        public int SlotsFilled { get; set; }
        [Range(1,100)]
        public int? MaxSlots { get; set; }
    }
}
