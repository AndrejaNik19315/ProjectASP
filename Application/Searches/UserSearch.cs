using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
    public class UserSearch
    {
        public string Username { get; set; }
        public bool? IsActive { get; set; }
        public int PerPage { get; set; } = 3;
        public int PageNumber { get; set; } = 1;
    }
}
