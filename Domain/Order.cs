using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Order : BaseEntity
    {
        public int? ItemId { get; set; }
        public Item Item { get; set; }
        public int? CharacterId { get; set; }
        public Character Character { get; set; }
        public decimal Cost { get; set; }
    }
}
