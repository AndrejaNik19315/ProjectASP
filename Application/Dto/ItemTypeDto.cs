using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dto
{
    public class ItemTypeDto : BaseEntityDto
    {
        [Required]
        [MaxLength(24)]
        public string Name { get; set; }
    }
}
