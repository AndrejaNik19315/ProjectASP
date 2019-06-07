using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dto
{
    public class ItemDto : BaseEntityDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(64)]
        public string Name { get; set; }
        [Required]
        [Range(1,1000)]
        public decimal Cost { get; set; }
        public bool isCovert { get; set; }
        public bool isForSale { get; set; }
        [Required]
        public int ItemTypeId { get; set; }
        [Required]
        public int ItemQualityId { get; set; }
        [RegularExpression("^[1-9][0-9]?$|^100$")]
        public int? Quantity { get; set; }  
    }
}
