﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dto
{
    public class UserDto : BaseEntityDto
    {
        [MinLength(3)]
        [MaxLength(16)]
        public string Firstname { get; set; }
        [MinLength(3)]
        [MaxLength(24)]
        public string Lastname { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(24)]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
