﻿using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto
{
    public class CharacterDto : BaseEntityDto
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public decimal? Funds { get; set; }
        public int GameClassId { get; set; }
        public int GenderId { get; set; }
        public int RaceId { get; set; }
        public int? InventoryId { get; set; }
    }
}
