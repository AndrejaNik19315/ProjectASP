using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Gender : BaseEntity
    {
        public string Sex { get; set; }
        public ICollection<Character> Characters { get; set; }
    }
}
