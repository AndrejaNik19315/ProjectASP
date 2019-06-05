using Application.Dto.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto.Users
{
    public class FullUserDto : BaseEntityDto
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }
        public List<FullCharacterDto> Characters { get; set; }
    }
}
