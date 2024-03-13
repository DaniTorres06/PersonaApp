using PersonaModel;
using PersonaModel.Response;

namespace PersonaData.Interfaces
{
    public interface IPeopleData
    {
        public Task<RspPerson> PeopleGetAsync();
        public Task<Response> PeopleAddAsync(People vPeople);
    }
}