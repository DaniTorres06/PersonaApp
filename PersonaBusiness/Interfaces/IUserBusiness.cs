using PersonaModel;
using PersonaModel.Response;

namespace PersonaBusiness.Interfaces
{
    public interface IUserBusiness
    {
        public Task<Response> UserValidateAsync(Users vUsers);
        public Task<Response> UserAddAsync(Users vUsers);
        public Task<RspUser> UserGetAsync();
    }
}