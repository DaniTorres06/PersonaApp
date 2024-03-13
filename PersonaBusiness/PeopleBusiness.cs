using Microsoft.Extensions.Logging;
using PersonaData.Interfaces;
using PersonaModel.Response;
using PersonaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using PersonaBusiness.Interfaces;

namespace PersonaBusiness
{
    public class PeopleBusiness : IPeopleBusiness
    {
        private readonly ILogger<UserBusiness> _logger;
        private readonly IPeopleData _peopleData;

        public PeopleBusiness(ILogger<UserBusiness> logger, IPeopleData peopleData)
        {
            _logger = logger;
            _peopleData = peopleData;

        }

        public async Task<RspPerson> PeopleGetAsync()
        {
            RspPerson vObjRsp = new RspPerson();
            try
            {
                vObjRsp = await _peopleData.PeopleGetAsync();
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

        public async Task<Response> PeopleAddAsync(People vPeople)
        {
            Response vObjRsp = new Response();
            try
            {
                vObjRsp = await _peopleData.PeopleAddAsync(vPeople);
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
    }
}
