using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
    public class ItemSearchWeb
    {
        public string Name { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public bool? isCovert { get; set; }
        public bool? inStock { get; set; }
    }
}
