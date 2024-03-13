using PersonaModel;
using PersonaModel.Response;

namespace PersonaData.Interfaces
{
    public interface IUserData
    {
        public Task<Response> UserValidateAsync(Users vUsers);
        public Task<Response> UserAddAsync(Users vUsers);
        public Task<RspUser> UserGetAsync();
    }
}