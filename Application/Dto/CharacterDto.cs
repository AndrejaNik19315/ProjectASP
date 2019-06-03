using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dto
{
    public class CharacterDto : BaseEntityDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(16)]
        public string Name { get; set; }
        [Range(1, 60)]
        public int? Level { get; set; }
        [Range(0, 1000)]
        public decimal? Funds { get; set; }
        [Required]
        public int GameClassId { get; set; }
        [Required]
        public int GenderId { get; set; }
        [Required]
        public int RaceId { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
