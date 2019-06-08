using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dto
{
    public class OrderDto : BaseEntityDto
    {
        [Required]
        public int ItemId { get; set; }
        [Required]
        public int CharacterId { get; set; }
    }
}
