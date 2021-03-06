﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto.Items
{
    public class FullItemDto : BaseEntityDto
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public bool isCovert { get; set; }
        public bool inStock { get; set; }
        public string ItemType { get; set; }
        public string ItemQuality { get; set; }
        public int? Quantity { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
    }
}
