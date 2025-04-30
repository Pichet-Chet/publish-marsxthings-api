using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MarsXApps.API.Helper;
using MarsXApps.Models;
using MarsXApps.Models.Constants;
using MarsXApps.Models.Customs;
using MarsXApps.Module.Environment.Authorized;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarsXApps.API.Controllers
{
    [Route("api/[controller]")]
    public class SysTermsAndConditionsTransactionController : Controller
    {
        private readonly ISysTermsAndConditionsTransactionRepository _repository;

        public SysTermsAndConditionsTransactionController(ISysTermsAndConditionsTransactionRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetLastEventByCustomer")]
        public async Task<IActionResult> GetLastEventByCustomer(Guid customerId)
        {
            Response resp = new Response();

            try
            {
                resp = await _repository.GetLastEventByCustomer(customerId);
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
        [Route("GetLastEventByPhone")]
        public async Task<IActionResult> GetLastEventByPhone(string Phone)
        {
            Response resp = new Response();

            try
            {
                resp = await _repository.GetLastEventByPhone(Phone);
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
        [Route("GetLastEventByUsername")]
        public async Task<IActionResult> GetLastEventByUsername(string Username)
        {
            Response resp = new Response();

            try
            {
                resp = await _repository.GetLastEventByUsername(Username);
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
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] SysTermsAndConditionsTransactionModel param)
        {
            Response resp = new Response();

            try
            {
                resp = await _repository.Create(param);
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

