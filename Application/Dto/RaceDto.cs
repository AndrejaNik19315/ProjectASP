using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dto
{
    public class RaceDto : BaseEntityDto
    {
        [Required]
        public string Name { get; set; }
    }
}
