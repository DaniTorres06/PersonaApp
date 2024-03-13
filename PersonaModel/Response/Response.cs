using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonaModel.Response
{
    public class Response
    {
        public string? Message { get; set; }        
        public bool Status { get; set; }
        public object Object {  get; set; }

        public Response()
        {
            Message = string.Empty;            
            Status = false;
            Object = new();
        }
    }
}
