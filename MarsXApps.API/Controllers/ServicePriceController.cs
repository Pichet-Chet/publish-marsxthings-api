using MarsXApps.API.Helper;
using MarsXApps.Models;
using MarsXApps.Models.Constants;
using MarsXApps.Module.ServicePrice.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MarsXApps.API.Controllers
{
    [Route("api/[controller]")]
    public class ServicePriceController : Controller
    {
        private readonly IServicePriceRepository _repository;

        public ServicePriceController(IServicePriceRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("Models")]
        public async Task<IActionResult> MachinePMPriceModels()
        {
            Response resp = new Response();

            try
            {
                resp = await _repository.GetPMPriceModels();
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
        [Route("Model/{mcModel}")]
        public async Task<IActionResult> MachinePMPrice(string mcModel)
        {
            Response resp = new Response();

            try
            {
                resp = await _repository.GetPMPrice(mcModel);
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
        [Route("Model/{mcModel}/pmHour/{pmHour}")]
        public async Task<IActionResult> MachinePMPriceHour(string mcModel, int pmHour)
        {
            Response resp = new Response();

            try
            {
                resp = await _repository.GetPMPriceHour(mcModel,pmHour);
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
