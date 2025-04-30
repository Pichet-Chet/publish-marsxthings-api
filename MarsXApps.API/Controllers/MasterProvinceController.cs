using System.Diagnostics;
using MarsXApps.API.Helper;
using MarsXApps.Models;
using MarsXApps.Models.Constants;
using MarsXApps.Models.Customs;
using MarsXApps.Models.Filter.MasterData;
using MarsXApps.Module.Environment.MasterData;
using MarsXApps.Service.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarsXApps.API.Controllers
{
    [Route("api/[controller]")]
    public class MasterProvinceController : Controller
    {
        private readonly IMasterProvinceRepositories _MasterProvinceRepositories;

        public MasterProvinceController()
        {
            this._MasterProvinceRepositories = new MasterProvinceRepositories();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] MasterProvinceFilter param)
        {
            Response resp = new Response();

            try
            {
                

                

                resp = await _MasterProvinceRepositories.GetAll(param);

                

                
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
                

                

                resp = await _MasterProvinceRepositories.GetById(Id);

                

                
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
        public async Task<IActionResult> Create([FromBody] MasterProvinceModel param)
        {
            Response resp = new Response();

            try
            {
                

                

                resp = await _MasterProvinceRepositories.Create(param);

                

                
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
        public async Task<IActionResult> Update([FromBody] MasterProvinceModel param)
        {
            Response resp = new Response();

            try
            {
                

                

                resp = await _MasterProvinceRepositories.Update(param);

                

                
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

