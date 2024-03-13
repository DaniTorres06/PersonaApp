using Microsoft.AspNetCore.Mvc;
using PersonaBusiness.Interfaces;
using PersonaModel;
using PersonaModel.Response;

namespace PersonaApp.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserBusiness _service;

        public UserController(ILogger<UserController> logger, IUserBusiness service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost("UserValidateAsync")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response>> UserValidateAsync([FromBody] Users vUsers)
        {
            try
            {
                var response = await _service.UserValidateAsync(vUsers);

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

        [HttpPost("UserAddAsync")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response>> UserAddAsync([FromBody] Users vUsers)
        {
            try
            {
                var response = await _service.UserAddAsync(vUsers);

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

        [HttpGet("UserGetAsync")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RspUser))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RspUser>> UserGetAsync()
        {
            try
            {
                var response = await _service.UserGetAsync();

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


    }
}
