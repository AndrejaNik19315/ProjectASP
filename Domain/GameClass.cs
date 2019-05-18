using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class GameClass : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Character> Characters { get; set; }
    }
}
