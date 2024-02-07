using homeSchool.core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.model.Response;
using Subsciber.CORE.interfaceBL;
using Subscriber.Data.DTO;

namespace Subscriber.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SubscriberCardController : Controller
    {
        ICardSubscriberService _css;

        public SubscriberCardController(ICardSubscriberService css)
        {
            _css = css;
        }
        [HttpPost]
        [Route("subscriber")]
        public async Task<ActionResult<BaseResponseGeneral<bool>>> Register([FromBody] SubscriberDTO subscriberDTO)
        {
            BaseResponseGeneral<bool> response = new BaseResponseGeneral<bool>();
            response = await _css.Register(subscriberDTO);
            if (response.Success == false)
                return NotFound(response);
            return Ok(response);
        }
       
        [HttpGet]
        [Route("sub")]
        public async Task<ActionResult<BaseResponseGeneral<SubscriberResponse>>> GetSubscriberById(int id)
        {
            BaseResponseGeneral<SubscriberResponse> res = new BaseResponseGeneral<SubscriberResponse>();
            res = await _css.GetSubscriberById(id);
            if (res.Success == false)
            {
                return NotFound(res);
            }
            return Ok(res);
        }
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<BaseResponseGeneral<int>>> Login(string email, string password)
        {
            BaseResponseGeneral<int> res = new BaseResponseGeneral<int>();
            res = await _css.Login(email, password);
            if(res.Success == false)
                return Unauthorized(res);
            return Ok(res); 
        }
    }
}
