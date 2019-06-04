using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dto
{
    public class ItemQualityDto : BaseEntityDto
    {
        [Required]
        [MaxLength(16)]
        public string Name { get; set; }
    }
}
