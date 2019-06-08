using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto.Items
{
    public class PartialItemDto
    {
        public string Name { get; set; }
        public bool isCovert { get; set; }
        public string ItemType { get; set; }
        public string ItemQuality { get; set; }
    }
}
