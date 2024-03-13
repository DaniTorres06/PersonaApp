using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonaModel
{
    public class Users
    {
        public string? User { get; set; }
        public string? Password { get; set; }

        public Users()
        {
            User = string.Empty;
            Password = string.Empty;
        }
    }
}
