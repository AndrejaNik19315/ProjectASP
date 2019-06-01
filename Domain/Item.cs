﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Item : BaseEntity
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public bool isCovert { get; set; }
        public bool isForSale { get; set; }
        public int ItemTypeId { get; set; }
        public int ItemQualityId { get; set; }
        public IEnumerable<InventoryItem> Inventories { get; set; }
    }
}