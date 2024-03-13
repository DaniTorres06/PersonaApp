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
    public class PeopleData : IPeopleData
    {
        private readonly ILogger<PeopleData> _logger;
        private readonly IConfiguration _config;

        public PeopleData(ILogger<PeopleData> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;
        }

        private const string PeopleGetAll = "people_get";
        private const string PeopleAdd = "people_add";

        public async Task<RspPerson> PeopleGetAsync()
        {
            RspPerson vRspPerson = new();

            SqlConnection conn = new(_config["ConnectionStrings:SqlServer"]);
            try
            {
                SqlCommand StoreProc_enc = new(PeopleGetAll, conn);
                StoreProc_enc.CommandType = CommandType.StoredProcedure;

                conn.Open();
                using SqlDataReader reader = await StoreProc_enc.ExecuteReaderAsync();
                while (reader.Read())
                {
                    if (Convert.ToInt32(reader["HasErrors"]) == 0)
                    {
                        PeopleDTO vObjPeople = new();
                        vObjPeople.FullName = reader["FullName"] != DBNull.Value ? reader["FullName"].ToString() : string.Empty;
                        vObjPeople.TypeId = reader["TypeId"] != DBNull.Value ? reader["TypeId"].ToString() : string.Empty;
                        vObjPeople.Identification = reader["Identification"] != DBNull.Value ? Int64.Parse(reader["Identification"].ToString()) : 0;
                        vObjPeople.Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : string.Empty;
                        vObjPeople.CreationDate = reader["CreationDate"] != DBNull.Value ? reader["CreationDate"].ToString() : string.Empty;

                        vRspPerson.LstPeple.Add(vObjPeople);
                        vRspPerson.Response.Status = true;
                        vRspPerson.Response.Message = "Registros encontrados";

                    }
                    else
                    {
                        vRspPerson.Response.Status = false;
                        vRspPerson.Response.Message = "No se encontraron registros";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                vRspPerson.Response.Status = false;
                vRspPerson.Response.Message = "Problemas al buscar el usuario " + ex.Message;
                return vRspPerson;
            }

            finally
            {
                conn.Close();
            }

            return vRspPerson;
        }

        public async Task<Response> PeopleAddAsync(People vPeople)
        {
            Response vRsp = new();

            SqlConnection conn = new(_config["ConnectionStrings:SqlServer"]);
            try
            {
                SqlCommand StoreProc_enc = new(PeopleAdd, conn);
                StoreProc_enc.CommandType = CommandType.StoredProcedure;

                StoreProc_enc.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = vPeople.FirstName;
                StoreProc_enc.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = vPeople.LastName;
                StoreProc_enc.Parameters.Add("@Identification", SqlDbType.Int).Value = vPeople.Identification;
                StoreProc_enc.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = vPeople.Email;
                StoreProc_enc.Parameters.Add("@TypeId", SqlDbType.Int).Value = vPeople.TypeId;

                conn.Open();
                using SqlDataReader reader = await StoreProc_enc.ExecuteReaderAsync();
                while (reader.Read())
                {
                    if (Convert.ToInt32(reader["HasErrors"]) == 0)
                    {
                        vRsp.Status = true;
                        vRsp.Message = "Usuario guardado exitosamente";
                        vRsp.Object = vPeople;
                    }
                    else
                    {
                        vRsp.Status = false;
                        vRsp.Message = "El usuario con la identificacion " + vPeople.Identification + " ya existe";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                vRsp.Status = false;
                vRsp.Message = "Problemas al buscar el usuario " + ex.Message;
                return vRsp;
            }

            finally
            {
                conn.Close();
            }

            return vRsp;
        }

    }
}
