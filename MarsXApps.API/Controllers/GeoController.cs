using System.Diagnostics;
using MarsXApps.API.Helper;
using MarsXApps.Models;
using MarsXApps.Models.Constants;
using MarsXApps.Models.Customs;
using MarsXApps.Models.Filter.LongdoMap;
using MarsXApps.Models.Filter.MasterData;
using MarsXApps.Module.Environment.LongdoMap;
using MarsXApps.Module.Environment.MasterData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarsXApps.API.Controllers
{
    [Route("api/[controller]")]
    public class GeoController : Controller
    {
        private readonly ILongdoMapRepositories _repositories;


        public GeoController(ILongdoMapRepositories repositories)
        {
            _repositories = repositories;
        }


        [HttpGet]
        [Route("rerverseGeocoding")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] ReverseGeocodingFilter param)
        {
            Response resp = new Response();

            try
            {
                resp = await _repositories.rerverseGeocoding(param);
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

