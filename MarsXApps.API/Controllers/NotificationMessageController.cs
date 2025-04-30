using MarsXApps.API.Helper;
using MarsXApps.Models;
using MarsXApps.Models.Constants;
using MarsXApps.Models.Customs;
using MarsXApps.Models.Filter.NotificationMessageFilter;
using MarsXApps.Module.NotificationMessage.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MarsXApps.API.Controllers
{
    [Route("api/[controller]")]
    public class NotificationMessageController : Controller
    {
        private readonly INotificationMessageRepository _notificationMessageRepository;

        public NotificationMessageController(INotificationMessageRepository notificationMessageRepository)
        {
            _notificationMessageRepository = notificationMessageRepository;
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> GetAll([FromQuery] NotificationMessageFilter param)
        {
            Response resp = new Response();

            try
            {
                resp = await _notificationMessageRepository.GetAll(param);
            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.HttpMessage = Constants.HttpCode500Message;
                resp.Message = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return StatusCode(resp.HttpCode, AppHelper.GetResponseController(resp));
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            Response resp = new Response();

            try
            {
                //resp = await _notificationMessageRepository.GetById(Id);
            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.HttpMessage = Constants.HttpCode500Message;
                resp.Message = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return StatusCode(resp.HttpCode, AppHelper.GetResponseController(resp));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PromotionModel param)
        {
            Response resp = new Response();

            try
            {
               // resp = await _notificationMessageRepository.Create(param);

            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.HttpMessage = Constants.HttpCode500Message;
                resp.Message = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return StatusCode(resp.HttpCode, AppHelper.GetResponseController(resp));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PromotionModel param)
        {
            Response resp = new Response();

            try
            {

               // resp = await _notificationMessageRepository.Update(param);

            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.HttpMessage = Constants.HttpCode500Message;
                resp.Message = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return StatusCode(resp.HttpCode, AppHelper.GetResponseController(resp));
        }

        [HttpGet]
        [Route("GetByCode/{Code}")]
        public async Task<IActionResult> GetByCode(string Code)
        {
            Response resp = new Response();

            try
            {
               // resp = await _notificationMessageRepository.GetByCode(Code);
            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.HttpMessage = Constants.HttpCode500Message;
                resp.Message = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return StatusCode(resp.HttpCode, AppHelper.GetResponseController(resp));
        }
    }

}

