using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class InventoryItem : BaseEntity
    {
        public Item Item { get; set; }
        public int ItemId { get; set; }
        public Inventory Inventory { get; set; }
        public int InventoryId { get; set; }
    }
}
