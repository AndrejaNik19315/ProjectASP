using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dto
{
    public class GameClassDto : BaseEntityDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(16)]
        public string Name { get; set; }
    }
}
