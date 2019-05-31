using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dto
{
    public class GenderDto : BaseEntityDto
    {
        [Required]
        public string Sex { get; set; }
    }
}
