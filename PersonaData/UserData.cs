using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PersonaData.Interfaces;
using PersonaModel;
using PersonaModel.DTO;
using PersonaModel.Response;
using System.Data;

namespace PersonaData
{
    public class UserData : IUserData
    {
        private readonly ILogger<UserData> _logger;
        private readonly IConfiguration _config;

        public UserData(ILogger<UserData> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;
        }

        private const string UsersValidate = "Users_validate";
        private const string UsersAdd = "Users_add";
        private const string UsersGetAll = "users_get";


        public async Task<Response> UserValidateAsync(Users vUsers)
        {
            Response vObjRsp = new();

            SqlConnection conn = new(_config["ConnectionStrings:SqlServer"]);
            try
            {
                SqlCommand StoreProc_enc = new(UsersValidate, conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                StoreProc_enc.Parameters.Add("@UserApp", SqlDbType.VarChar, 50).Value = vUsers.User;
                StoreProc_enc.Parameters.Add("@Pass", SqlDbType.VarChar, 50).Value = vUsers.Password;

                conn.Open();
                using SqlDataReader reader = await StoreProc_enc.ExecuteReaderAsync();
                while (reader.Read())
                {
                    if (Convert.ToInt32(reader["HasErrors"]) == 0)
                    {
                        string? UserApp = reader["UserApp"] != DBNull.Value ? reader["UserApp"].ToString() : string.Empty;

                        vObjRsp.Status = true;
                        vObjRsp.Message = "Usuario logueado exitosamente";
                        vObjRsp.Object = UserApp;
                    }
                    else
                    {
                        vObjRsp.Status = false;
                        vObjRsp.Message = "Usuario o contraseña incorrectas";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                vObjRsp.Status = false;
                vObjRsp.Message = "Problemas al buscar el usuario " + ex.Message;
                return vObjRsp;
            }

            finally
            {
                conn.Close();
            }

            return vObjRsp;
        }

        public async Task<Response> UserAddAsync(Users vUsers)
        {
            Response vObjRsp = new();

            SqlConnection conn = new(_config["ConnectionStrings:SqlServer"]);
            try
            {
                SqlCommand StoreProc_enc = new(UsersAdd, conn);
                StoreProc_enc.CommandType = CommandType.StoredProcedure;                

                StoreProc_enc.Parameters.Add("@UserApp", SqlDbType.VarChar, 50).Value = vUsers.User;
                StoreProc_enc.Parameters.Add("@Pass", SqlDbType.VarChar, 50).Value = vUsers.Password;

                conn.Open();
                using SqlDataReader reader = await StoreProc_enc.ExecuteReaderAsync();
                while (reader.Read())
                {
                    if (Convert.ToInt32(reader["HasErrors"]) == 0)
                    {                       

                        vObjRsp.Status = true;
                        vObjRsp.Message = "Usuario guardado exitosamente";                        
                    }
                    else
                    {
                        vObjRsp.Status = false;
                        vObjRsp.Message = "El usuario " + vUsers.User + " ya esta registrado";
                    } 
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                vObjRsp.Status = false;
                vObjRsp.Message = "Problemas al guardar el usuario " + ex.Message;
                return vObjRsp;
            }

            finally
            {
                conn.Close();
            }

            return vObjRsp;
        }

        public async Task<RspUser> UserGetAsync()
        {
            RspUser vRspUsers = new();

            SqlConnection conn = new(_config["ConnectionStrings:SqlServer"]);
            try
            {
                SqlCommand StoreProc_enc = new(UsersGetAll, conn);
                StoreProc_enc.CommandType = CommandType.StoredProcedure;

                conn.Open();
                using SqlDataReader reader = await StoreProc_enc.ExecuteReaderAsync();
                while (reader.Read())
                {
                    if (Convert.ToInt32(reader["HasErrors"]) == 0)
                    {
                        UserDTO vObjUsers = new();
                        vObjUsers.UserName = reader["UserApp"] != DBNull.Value ? reader["UserApp"].ToString() : string.Empty;
                        vObjUsers.CreationDate = reader["CreationDate"] != DBNull.Value ? reader["CreationDate"].ToString() : string.Empty;

                        vRspUsers.LstUsers.Add(vObjUsers);
                        vRspUsers.Response.Status = true;
                        vRspUsers.Response.Message = "Usuario encontrados";

                    }
                    else
                    {
                        vRspUsers.Response.Status = false;
                        vRspUsers.Response.Message = "No se encontraron registros";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                vRspUsers.Response.Status = false;
                vRspUsers.Response.Message = "Problemas al buscar el usuario " + ex.Message;
                return vRspUsers;
            }

            finally
            {
                conn.Close();
            }

            return vRspUsers;
        }
    }
}
