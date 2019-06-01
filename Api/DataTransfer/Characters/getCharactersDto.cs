using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DataTransfer.Characters
{
    public class getCharactersDto : BaseDto
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public string GameClass { get; set; }
        public string Gender { get; set; }
        public string Race { get; set; }
        public string User { get; set; }
    }
}
