using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonaModel.DTO
{
    public class PeopleDTO
    {
        public string? FullName { get; set; }
        public string? TypeId { get; set; }
        public Int64? Identification { get; set; }
        public string? Email { get; set; }
        public string? CreationDate { get; set; }

        public PeopleDTO()
        {
            FullName = string.Empty;
            TypeId = string.Empty;
            Identification = 0;
            Email = string.Empty;
            CreationDate = string.Empty;
        }
    }
}
