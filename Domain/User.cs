using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class User : BaseEntity
    {
        public User()
        {
            Characters = new List<Character>();
        }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
        public IEnumerable<Character> Characters { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
