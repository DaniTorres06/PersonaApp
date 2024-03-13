using PersonaModel.DTO;

namespace PersonaModel.Response
{
    public class RspPerson
    {
        public List<PeopleDTO>? LstPeple { get; set; }
        public Response? Response { get; set; }

        public RspPerson()
        {
            LstPeple = new List<PeopleDTO>();
            Response = new Response();
        }
    }
}
