using System;
using System.Collections.Generic;
using System.Text;

namespace Application.HelperClasses
{
    public class LoggedUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Role { get; set; }
        public bool IsLogged { get; set; }
    }
}
