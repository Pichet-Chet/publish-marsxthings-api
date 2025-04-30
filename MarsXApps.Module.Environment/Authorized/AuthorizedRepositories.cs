using MarsXApps.Models;
using MarsXApps.Models.Authorized;
using MarsXApps.Models.Constants;
using MarsXApps.Models.Customs;
using MarsXApps.Models.ThirdParty.SMSMKT.OtpValidate;
using MarsXApps.Service;
using MarsXApps.Service.Models;
using Microsoft.Extensions.Configuration;

namespace MarsXApps.Module.Environment.Authorized
{
    public interface IAuthorizedRepositories
    {
        public Task<Response> SignIn(Login Param);

        public Task<Response> SignInWithToken(LoginWithToken Param);

        public Task<Response> SignInWithMobileOTP(string Param);

        public Task<Response> SignInVerifyOTP(OtpValidateRequest param);


        public Task<Response> RegisterWithMobilePhone(string param);

        public Task<Response> RegisterVerifyOTP(OtpValidateRequest param);


        public Task<Response> RevokeTokenAll();

        public Task<Response> RevokeTokenById(string Uid);


        public Task<Response> ForgotPassword(string phone);

        public Task<Response> ForgotPasswordOtp(ForgotPassword param);

        public Task<Response> ChangePassword(ChangePassword param);


    }

    public class AuthorizedRepositories : IAuthorizedRepositories
    {
        DateTime ServerTime;

        IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();

        private readonly AuthorizedService _service;

        private readonly ISysTermsAndConditionService _sysTermsAndConditionService;

        private readonly ISysTermsAndConditionsTransactionService _sysTermsAndConditionsTransactionService;

        private readonly SysCustomerService _SysCustomerService;


        public AuthorizedRepositories(
            ISysTermsAndConditionService SysTermsAndConditionService,
            ISysTermsAndConditionsTransactionService sysTermsAndConditionsTransactionService
            )
        {
            _service = new AuthorizedService();

            _sysTermsAndConditionService = SysTermsAndConditionService;

            _sysTermsAndConditionsTransactionService = sysTermsAndConditionsTransactionService;

            _SysCustomerService = new SysCustomerService();

            ServerTime = DateTime.Now.MarsX();
        }


        #region Sign In


        public async Task<Response> SignIn(Login param)
        {
            Response resp = new Response();

            try
            {
                resp = await _service.SignIn(param);
            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.Message = Constants.HttpCode500Message;
                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return resp;
        }

        public async Task<Response> SignInWithToken(LoginWithToken Param)
        {
            Response resp = new Response();

            try
            {
                resp = await _service.SignInWithToken(Param);
            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.Message = Constants.HttpCode500Message;
                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return resp;
        }

        public async Task<Response> SignInWithMobileOTP(string phone)
        {
            Response resp = new Response();

            try
            {
                resp = await _service.SignInWithMobileOTP(phone);
            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.Message = Constants.HttpCode500Message;
                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return resp;
        }

        public async Task<Response> SignInVerifyOTP(OtpValidateRequest param)
        {
            Response resp = new Response();

            try
            {
                resp = await _service.SignInVerifyOTP(param);
            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.Message = Constants.HttpCode500Message;
                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return resp;
        }


        #endregion


        #region Sign Up

        public async Task<Response> RegisterWithMobilePhone(string phone)
        {
            Response resp = new Response();

            try
            {
                resp = await _service.RegisterWithMobilePhone(phone);
            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.Message = Constants.HttpCode500Message;
                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return resp;
        }

        public async Task<Response> RegisterVerifyOTP(OtpValidateRequest param)
        {
            Response resp = new Response();

            SysTermsAndConditionModel sysTermsAndConditionModel = new SysTermsAndConditionModel();

            SysCustomerModel sysCustomerModel = new SysCustomerModel();

            SysTermsAndConditionsTransactionModel sysTermsAndConditionsTransactionModel = new SysTermsAndConditionsTransactionModel();

            try
            {
                resp = await _service.RegisterVerifyOTP(param);

                if (resp.Status == true)
                {
                    sysTermsAndConditionModel = await _sysTermsAndConditionService.GetLastVersionActive();

                    sysCustomerModel = await _SysCustomerService.GetByPhone(param.phone);

                    if (sysTermsAndConditionModel != null && sysCustomerModel != null)
                    {
                        SysTermsAndConditionsTransaction ObjTerm = new SysTermsAndConditionsTransaction();

                        ObjTerm.SysTermsAndConditionsId = sysTermsAndConditionModel.Id;
                        ObjTerm.SysCustomersId = sysCustomerModel.Id;
                        ObjTerm.Consideration = true;
                        ObjTerm.ConsiderationDate = ServerTime;

                        sysTermsAndConditionsTransactionModel = await _sysTermsAndConditionsTransactionService.Create(ObjTerm);
                    }
                }
            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.Message = Constants.HttpCode500Message;
                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return resp;
        }

        #endregion



        #region Revoke

        public async Task<Response> RevokeTokenAll()
        {
            Response resp = new Response();

            try
            {
                resp = await _service.RevokeTokenAll();
            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.Message = Constants.HttpCode500Message;
                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return resp;
        }

        public async Task<Response> RevokeTokenById(string Uid)
        {
            Response resp = new Response();

            try
            {
                resp = await _service.RevokeTokenById(Uid);
            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.Message = Constants.HttpCode500Message;
                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return resp;
        }

        #endregion


        #region Forgot and change password

        public async Task<Response> ForgotPassword(string phone)
        {
            Response resp = new Response();

            try
            {
                resp = await _service.ForgotPassword(phone);
            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.Message = Constants.HttpCode500Message;
                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return resp;
        }

        public async Task<Response> ForgotPasswordOtp(ForgotPassword param)
        {
            Response resp = new Response();

            try
            {
                resp = await _service.ForgotPasswordOtp(param);
            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.Message = Constants.HttpCode500Message;
                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return resp;
        }

        public async Task<Response> ChangePassword(ChangePassword param)
        {
            Response resp = new Response();

            try
            {
                resp = await _service.ChangePassword(param);
            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.Message = Constants.HttpCode500Message;
                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return resp;
        }

        #endregion

    }
}

