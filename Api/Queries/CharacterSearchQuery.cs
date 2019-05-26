using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Queries
{
    public class CharacterSearchQuery
    {
        public string Name { get; set; }
        public int? Level { get; set; }
        public decimal? MinFunds { get; set; }
        public decimal? MaxFunds { get; set; }
    }
}
