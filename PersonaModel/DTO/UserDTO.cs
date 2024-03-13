using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonaModel.DTO
{
    public class UserDTO
    {
        public string? UserName { get; set; }
        public string? CreationDate { get; set; }

        public UserDTO()
        {
            UserName = string.Empty;
            CreationDate = string.Empty;
        }
    }
}
