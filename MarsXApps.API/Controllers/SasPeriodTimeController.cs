using System.Diagnostics;
using MarsXApps.API.Helper;
using MarsXApps.Models;
using MarsXApps.Models.Constants;
using MarsXApps.Models.Customs;
using MarsXApps.Models.Filter.ServiceAfterSale;
using MarsXApps.Module.ServiceAfterSale.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MarsXApps.API.Controllers
{
    [Route("api/[controller]")]
    public class SasPeriodTimeController : Controller
    {
        private readonly ISasPeriodTimeRepositories _repositories;

        public SasPeriodTimeController(ISasPeriodTimeRepositories repositories)
        {
            _repositories = repositories;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] SasPeriodTimeFilter param)
        {
            Response resp = new Response();

            try
            {
                

                

                resp = await _repositories.GetAll(param);

                

                
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
                

                

                resp = await _repositories.GetById(Id);

                

                
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
        public async Task<IActionResult> Create([FromBody] SasPeriodTimeModel param)
        {
            Response resp = new Response();

            try
            {
                

                

                resp = await _repositories.Create(param);

                

                
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
        public async Task<IActionResult> Update([FromBody] SasPeriodTimeModel param)
        {
            Response resp = new Response();

            try
            {
                

                

                resp = await _repositories.Update(param);

                

                
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

