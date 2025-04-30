using MarsXApps.API.Helper;
using MarsXApps.Models;
using MarsXApps.Models.Constants;
using MarsXApps.Models.Customs;
using MarsXApps.Models.Filter.MasterData;
using MarsXApps.Module.Environment.MasterData;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarsXApps.API.Controllers
{
    [Route("api/[controller]")]
    public class MasterSubDistrictController : Controller
    {
        private readonly IMasterSubDistrictRepositories _MasterSubDistrictRepositories;

        public MasterSubDistrictController()
        {
            this._MasterSubDistrictRepositories = new MasterSubDistrictRepositories();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] MasterSubDistrictFilter param)
        {
            Response resp = new Response();

            try
            {
                
                resp = await _MasterSubDistrictRepositories.GetAll(param);
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

                resp = await _MasterSubDistrictRepositories.GetById(Id);

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
        public async Task<IActionResult> Create([FromBody] MasterSubdistrictModel param)
        {
            Response resp = new Response();

            try
            {

                resp = await _MasterSubDistrictRepositories.Create(param);

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
        public async Task<IActionResult> Update([FromBody] MasterSubdistrictModel param)
        {
            Response resp = new Response();

            try
            {

                resp = await _MasterSubDistrictRepositories.Update(param);

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
        [Route("postCode/{postCode}")]
        public async Task<IActionResult> PostCode(int postCode)
        {
            Response resp = new Response();

            try
            {
                resp = await _MasterSubDistrictRepositories.GetByPostCode(postCode);
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

