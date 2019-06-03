using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Inventory : BaseEntity
    {
        public int SlotsFilled { get; }
        public int? MaxSlots { get; set; }
        //public IEnumerable<InventoryItem> InventoryItems { get; set; }
        public int CharacterId { get; set; }
        public Character Character { get; set; }
    }
}
