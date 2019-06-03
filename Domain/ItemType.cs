using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ItemType : BaseEntity
    {
        public string Name { get; set; }
        public IEnumerable<Item> Items { get; set; }
        public int ItemId { get; set; }
    }
}
