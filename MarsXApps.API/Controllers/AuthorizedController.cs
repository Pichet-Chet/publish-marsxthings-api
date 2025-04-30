using System.Diagnostics;
using MarsXApps.API.Auth;
using MarsXApps.API.Helper;
using MarsXApps.Models;
using MarsXApps.Models.Authorized;
using MarsXApps.Models.Constants;
using MarsXApps.Models.ThirdParty.SMSMKT.OtpValidate;
using MarsXApps.Module.Environment.Authorized;
using MarsXApps.Module.Environment.Connection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarsXApps.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class AuthorizedController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _Configuration;

        private readonly IAuthorizedRepositories _AuthorizedRepositories;
        private readonly IApplicationTokenRepositories _ApplicationTokenRepositories;
        private readonly IConnectionRepositories _ConnectionRepositories;


        public AuthorizedController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IAuthorizedRepositories AuthorizedRepositories,
            IConnectionRepositories ConnectionRepositories
            )
        {
            _ApplicationTokenRepositories = new ApplicationTokenRepositories();

            _AuthorizedRepositories = AuthorizedRepositories;

            _ConnectionRepositories = ConnectionRepositories;

            _userManager = userManager;

            _roleManager = roleManager;

            _Configuration = configuration;
        }

        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
        public class MultiGroupApiExplorerSettingsAttribute : Attribute
        {
            public string[] GroupNames { get; }

            public MultiGroupApiExplorerSettingsAttribute(params string[] groupNames)
            {
                GroupNames = groupNames;
            }
        }

        #region Connection

        [MultiGroupApiExplorerSettings("internal", "outsource")]
        [HttpPost]
        [AllowAnonymous]
        [Route("ConnectionMarsX")]
        public async Task<IActionResult> ConnectionMarsX()
        {
            Response resp = new Response();

            try
            {
                resp = await _ConnectionRepositories.MarsXDatabase();
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


        [MultiGroupApiExplorerSettings("internal", "outsource")]
        [HttpPost]
        [Route("TokenCheck")]
        public async Task<IActionResult> TokenCheck()
        {
            Response resp = new Response();

            try
            {
                resp.Status = Constants.StatusSuccess;
                resp.HttpCode = Constants.HttpCode200;
                resp.HttpMessage = Constants.HttpCode200Message;
                resp.Message = Constants.MessageSuccess;
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

        #endregion


        #region Customer 

        [ApiExplorerSettings(GroupName = "internal")]
        [HttpPost]
        [AllowAnonymous]
        [Route("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] Login param)
        {
            Response resp = new Response();

            try
            {
                resp = await _AuthorizedRepositories.SignIn(param);
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

        [ApiExplorerSettings(GroupName = "internal")]
        [HttpPost]
        [AllowAnonymous]
        [Route("SignIn/Token")]
        public async Task<IActionResult> SignInWithToken([FromBody] LoginWithToken param)
        {
            Response resp = new Response();

            try
            {
                resp = await _AuthorizedRepositories.SignInWithToken(param);
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

        [ApiExplorerSettings(GroupName = "internal")]
        [HttpPost]
        [AllowAnonymous]
        [Route("SignIn/{phone}")]
        public async Task<IActionResult> SignInWithMobileOTP(string phone)
        {
            Response resp = new Response();

            try
            {
                resp = await _AuthorizedRepositories.SignInWithMobileOTP(phone);
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


        [ApiExplorerSettings(GroupName = "internal")]
        [HttpPost]
        [AllowAnonymous]
        [Route("SignIn/OTP")]
        public async Task<IActionResult> SignInVerifyOTP([FromBody] OtpValidateRequest param)
        {
            Response resp = new Response();

            try
            {
                resp = await _AuthorizedRepositories.SignInVerifyOTP(param);
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

        [ApiExplorerSettings(GroupName = "internal")]
        [HttpPost]
        [AllowAnonymous]
        [Route("Register/{phone}")]
        public async Task<IActionResult> RegisterVerifyPhone(string phone)
        {
            Response resp = new Response();

            try
            {
                resp = await _AuthorizedRepositories.RegisterWithMobilePhone(phone);
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

        [ApiExplorerSettings(GroupName = "internal")]
        [HttpPost]
        [AllowAnonymous]
        [Route("Register/OTP")]
        public async Task<IActionResult> RegisterVerifyOTP([FromBody] OtpValidateRequest param)
        {
            Response resp = new Response();

            try
            {
                resp = await _AuthorizedRepositories.RegisterVerifyOTP(param);
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

        [ApiExplorerSettings(GroupName = "internal")]
        [HttpPut]
        [Route("Revoke/All")]
        public async Task<IActionResult> RevokeTokenAll()
        {
            Response resp = new Response();

            try
            {
                resp = await _AuthorizedRepositories.RevokeTokenAll();
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


        [ApiExplorerSettings(GroupName = "internal")]
        [HttpPut]
        [Route("Revoke/{Id}")]
        public async Task<IActionResult> RevokeTokenById(string Uid)
        {
            Response resp = new Response();

            try
            {
                resp = await _AuthorizedRepositories.RevokeTokenById(Uid);
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

        [ApiExplorerSettings(GroupName = "internal")]
        [HttpPost]
        [AllowAnonymous]
        [Route("ForgotPassword/{phone}")]
        public async Task<IActionResult> ForgotPassword(string phone)
        {
            Response resp = new Response();

            try
            {
                resp = await _AuthorizedRepositories.ForgotPassword(phone);
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

        [ApiExplorerSettings(GroupName = "internal")]
        [HttpPost]
        [AllowAnonymous]
        [Route("ForgotPasswordOtp")]
        public async Task<IActionResult> ForgotPasswordOtp([FromBody] ForgotPassword param)
        {
            Response resp = new Response();

            try
            {
                resp = await _AuthorizedRepositories.ForgotPasswordOtp(param);
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

        [ApiExplorerSettings(GroupName = "internal")]
        [HttpPost]
        [AllowAnonymous]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword param)
        {
            Response resp = new Response();

            try
            {
                resp = await _AuthorizedRepositories.ChangePassword(param);
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


        #endregion

        #region Application

        [MultiGroupApiExplorerSettings("internal", "outsource")]
        [HttpPost]
        [AllowAnonymous]
        [Route("Application")]
        public async Task<IActionResult> ApplicationAccess([FromBody] ApplicationAccessModel param)
        {
            Response resp = new Response();

            try
            {
                resp = await _ApplicationTokenRepositories.AccessToken(param.Key);
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

        [MultiGroupApiExplorerSettings("internal", "outsource")]
        [HttpPut]
        [Route("Application/Revoke")]
        public async Task<IActionResult> RevokeApplicationTokenAll()
        {
            Response resp = new Response();

            try
            {
                resp = await _ApplicationTokenRepositories.RevokeTokenAll();
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

        [MultiGroupApiExplorerSettings("internal", "outsource")]
        [HttpPut]
        [Route("Application/Revoke/{Id}")]
        public async Task<IActionResult> RevokeApplicationTokenById(string Uid)
        {
            Response resp = new Response();

            try
            {
                resp = await _ApplicationTokenRepositories.RevokeTokenById(Uid);
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

        #endregion

    }
}

