using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Item : BaseEntity
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public bool isCovert { get; set; }
        public bool inStock { get; set; }
        public int ItemTypeId { get; set; }
        public ItemType ItemType { get; set; }
        public int ItemQualityId { get; set; }
        public ItemQuality ItemQuality { get; set; }
        public ICollection<InventoryItem> InventoryItems { get; set; }
        public int? Quantity { get; set; }
        public ICollection<Order> Orders { get; set; }
        public string ImagePath { get; set; }
        public string ImageAlt { get; set; }
    }
}
