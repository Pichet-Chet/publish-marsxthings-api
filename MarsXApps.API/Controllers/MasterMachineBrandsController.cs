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
    public class MasterMachineBrandsController : Controller
    {
        private readonly IMasterMachineBrandRepositories _MasterMachineBrandRepositories;

        public MasterMachineBrandsController()
        {
            this._MasterMachineBrandRepositories = new MasterMachineBrandRepositories();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] MasterMachineBrandFilter param)
        {
            Response resp = new Response();

            try
            {
                

                

                resp = await _MasterMachineBrandRepositories.GetAll(param);

                

                
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
                

                

                resp = await _MasterMachineBrandRepositories.GetById(Id);

                

                
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
        public async Task<IActionResult> Create([FromBody] MasterMachineBrandModel param)
        {
            Response resp = new Response();

            try
            {
                

                

                resp = await _MasterMachineBrandRepositories.Create(param);

                

                
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
        public async Task<IActionResult> Update([FromBody] MasterMachineBrandModel param)
        {
            Response resp = new Response();

            try
            {
                

                

                resp = await _MasterMachineBrandRepositories.Update(param);

                

                
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
        [Route("Image")]
        public async Task<IActionResult> UpdateImage(int id, IFormFile? FileUpload)
        {
            Response resp = new Response();

            try
            {
                

                

                resp = await _MasterMachineBrandRepositories.UpdateImage(id, FileUpload);

                

                
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

