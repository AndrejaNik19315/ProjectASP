using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
    public class CharacterSearch
    {
        public string Name { get; set; }
        public int? Level { get; set; }
        public decimal? MinFunds { get; set; }
        public decimal? MaxFunds { get; set; }
        public int PerPage { get; set; } = 3;
        public int PageNumber { get; set; } = 1;
    }
}
