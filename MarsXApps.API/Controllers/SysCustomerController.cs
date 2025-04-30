using System.Diagnostics;
using MarsXApps.API.Helper;
using MarsXApps.Models;
using MarsXApps.Models.Constants;
using MarsXApps.Models.Customs;
using MarsXApps.Models.Customs.Request.SysCustomer;
using MarsXApps.Models.Filter.Authorized;
using MarsXApps.Module.ServiceAfterSale.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarsXApps.API.Controllers
{
    [Route("api/[controller]")]
    public class SysCustomerController : ControllerBase
    {
        private readonly ISysCustomerRepositories _repositories;

        public SysCustomerController(ISysCustomerRepositories repositories)
        {
            _repositories = repositories;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] CustomerFilter param)
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
        public async Task<IActionResult> GetById(string Id)
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
        [Route("delete/{Id}")]
        public async Task<IActionResult> DeleteById(string Id)
        {
            Response resp = new Response();

            try
            {
                resp = await _repositories.DeleteById(Id);
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
        [Route("username/{UserName}")]
        public async Task<IActionResult> GetByUserName(string username)
        {
            Response resp = new Response();

            try
            {
                resp = await _repositories.GetByUserName(username);
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
        [AllowAnonymous]
        [Route("phone/{phone}")]
        public async Task<IActionResult> GetByPhone(string phone)
        {
            Response resp = new Response();

            try
            {
                resp = await _repositories.GetByPhone(phone);
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
        [Route("Profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] SysCustomerModel param)
        {
            Response resp = new Response();

            try
            {
                resp = await _repositories.UpdateProfile(param);
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

        [HttpPatch]
        [Route("Image")]
        public async Task<IActionResult> UpdateImage(Guid id, IFormFile? FileUpload)
        {
            Response resp = new Response();

            try
            {
                resp = await _repositories.UpdateImage(id, FileUpload);
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

