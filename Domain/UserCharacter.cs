using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class UserCharacter
    {
        public int UserId { get; set; }
        public int CharacterId { get; set; }
        public User User { get; set; }
        public Character Character { get; set; }
    }
}
