using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto
{
    public partial class BaseEntityDto
    {
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
