using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto
{
    public class UserDto : BaseEntityDto
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
