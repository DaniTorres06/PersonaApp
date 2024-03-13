using PersonaModel.DTO;

namespace PersonaModel.Response
{
    public class RspUser
    {
        public List<UserDTO>? LstUsers { get; set; }
        public Response? Response { get; set; }

        public RspUser()
        {
            LstUsers = new List<UserDTO>();
            Response = new Response();
        }

    }
}
