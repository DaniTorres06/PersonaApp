using Microsoft.AspNetCore.Mvc;
using PersonaBusiness.Interfaces;
using PersonaModel;
using PersonaModel.Response;

namespace PersonaApp.Controllers
{
    public class PeopleController : Controller
    {
        private readonly ILogger<PeopleController> _logger;
        private readonly IPeopleBusiness _service;

        public PeopleController(ILogger<PeopleController> logger, IPeopleBusiness service)
        {
            _logger = logger;
            _service = service;
        }   

        

        [HttpGet("PeopleGetAsync")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RspPerson))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RspPerson>> PeopleGetAsync()
        {
            try
            {
                var response = await _service.PeopleGetAsync();

                if (response is null)
                    return BadRequest();
                if (!response.Response.Status)
                    return BadRequest(response);
                else
                    return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }

        [HttpPost("PeopleAddAsync")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response>> PeopleAddAsync([FromBody] People vPeople)
        {
            try
            {
                var response = await _service.PeopleAddAsync(vPeople);

                if (response is null)
                    return BadRequest();
                if (!response.Status)
                    return BadRequest(response);
                else
                    return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }
    }
}
