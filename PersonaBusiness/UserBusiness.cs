using Microsoft.Extensions.Logging;
using PersonaBusiness.Interfaces;
using PersonaData.Interfaces;
using PersonaModel;
using PersonaModel.Response;

namespace PersonaBusiness
{
    public class UserBusiness : IUserBusiness
    {
        private readonly ILogger<UserBusiness> _logger;
        private readonly IUserData _data;

        public UserBusiness(ILogger<UserBusiness> logger, IUserData data)
        {
            _logger = logger;
            _data = data;
        }

        public async Task<Response> UserValidateAsync(Users vUsers)
        {
            Response vObjRsp = new Response();

            try
            {
                vObjRsp = await _data.UserValidateAsync(vUsers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                vObjRsp.Status = false;
                vObjRsp.Message = "Problemas en capa de negocio";
                return vObjRsp;
            }

            return vObjRsp;
        }

        public async Task<Response> UserAddAsync(Users vUsers)
        {
            Response vObjRsp = new Response();

            try
            {
                vObjRsp = await _data.UserAddAsync(vUsers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                vObjRsp.Status = false;
                vObjRsp.Message = "Problemas en capa de negocio";
                return vObjRsp;
            }

            return vObjRsp;
        }

        public async Task<RspUser> UserGetAsync()
        {
            RspUser vObjRsp = new RspUser();

            try
            {
                vObjRsp = await _data.UserGetAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                vObjRsp.Response.Status = false;
                vObjRsp.Response.Message = "Problemas en capa de negocio";
                return vObjRsp;
            }

            return vObjRsp;
        }


    }
}
