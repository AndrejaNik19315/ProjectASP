using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto.Items
{
    public class FullItemDto : BaseEntityDto
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public bool isCovert { get; set; }
        public bool inStock {
            get {
                return (Quantity > 0) ? true : false;
            }
        }
        public string ItemType { get; set; }
        public string ItemQuality { get; set; }
        public int? Quantity { get; set; }
    }
}
