using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto.Characters
{
    public class FullCharacterDto : BaseEntityDto
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public decimal Funds { get; set; }
        public string GameClass { get; set; }
        public string Race { get; set; }
        public string Gender { get; set; }
        public InventoryDto Invetory { get; set; }
    }
}
