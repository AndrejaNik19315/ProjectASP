using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DataTransfer
{
    public class CharacterDto : BaseDto
    {
        public string Name { get; set; }
        public int? Level { get; set; }
        public decimal? Funds { get; set; }
        public int GameClassId { get; set; }
        public int GenderId { get; set; }
        public int RaceId { get; set; }
        public int UserId { get; set; }
        public int? InventoryId { get; set; }
    }
}
