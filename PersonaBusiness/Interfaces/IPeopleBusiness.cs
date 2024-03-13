using PersonaModel;
using PersonaModel.Response;

namespace PersonaBusiness.Interfaces
{
    public interface IPeopleBusiness
    {
        public Task<RspPerson> PeopleGetAsync();
        public Task<Response> PeopleAddAsync(People vPeople);
    }
}