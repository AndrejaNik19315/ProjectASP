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
        [Range(1.00,1000.00)]
        public decimal Cost { get; set; }
        public bool isCovert { get; set; }
        public bool inStock { get; set; }
        [Required]
        public int ItemTypeId { get; set; }
        [Required]
        public int ItemQualityId { get; set; }
        [RegularExpression("^[0-9][0-9]?$|^100$", ErrorMessage ="Quantity available range is from 0 to 100")]
        public int? Quantity { get; set; }  
    }
}
