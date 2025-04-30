//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using MarsXApps.Models;
//using MarsXApps.Models.Authorized;
//using MarsXApps.Models.Constants;
//using MarsXApps.Models.ThirdParty.SMSMKT.OtpValidate;
//using MarsXApps.Module.SMS.Repositories.SmsMtkRepositories;
//using MarsXApps.Service.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

//namespace MarsXApps.Module.Authorized.Repositories
//{
//    public interface IAuthorizedRepositories : IDisposable
//    {
//        public Task<Response> SignIn(Login Param);

//        public Task<Response> SignInWithToken(LoginWithToken Param);

//        public Task<Response> SignInWithMobileOTP(string Param);

//        public Task<Response> SignInVerifyOTP(OtpValidateRequest param);


//        public Task<Response> RegisterWithMobilePhone(string param);

//        public Task<Response> RegisterVerifyOTP(OtpValidateRequest param);


//        public Task<Response> RevokeTokenAll();

//        public Task<Response> RevokeTokenById(string Uid);


//        public Task<Response> ForgotPassword(string phone);

//        public Task<Response> ForgotPasswordOtp(ForgotPassword param);

//        public Task<Response> ChangePassword(ChangePassword param);


//    }

//    public class AuthorizedRepositories : IAuthorizedRepositories
//    {
//        DateTime ServerTime;

//        IConfigurationRoot configuration = new ConfigurationBuilder()
//                   .SetBasePath(Directory.GetCurrentDirectory())
//                   .AddJsonFile("appsettings.json")
//                   .Build();

//        private readonly IJWTManagerRepositories _JWTManagerRepositories;

//        private readonly ILogAccessTokenRepositories _LogAccessTokenRepositories;


//        private readonly MarscommuContext _context;

//        private readonly SmsMtkRepositories _smsMtkRepositories;

//        public AuthorizedRepositories()
//        {
//            _context = new MarscommuContext();

//            _smsMtkRepositories = new SmsMtkRepositories();

//            _JWTManagerRepositories = new JWTManagerRepositories();

//            _LogAccessTokenRepositories = new LogAccessTokenRepositories();

//            ServerTime = Helper.GetDateTimeByGMT(Convert.ToInt32(configuration["APP:GMT"]));
//        }


//        #region Sign In


//        public async Task<Response> SignIn(Login param)
//        {
//            Response resp = new Response();

//            SysCustomer sysCustomer = new SysCustomer();

//            LoginRespone loginResp = new LoginRespone();

//            try
//            {
//                var queryable = _context.SysCustomers.AsQueryable();


//                queryable = queryable.Where(x =>
//                            x.Username.ToLower() == param.Username.ToLower() ||
//                            x.MobilePhone.ToLower() == param.Username.ToLower());

//                sysCustomer = await queryable.FirstOrDefaultAsync();

//                if (sysCustomer != null)
//                {
//                    string storedHashedPassword = sysCustomer.Password;

//                    byte[] storedSaltBytes = sysCustomer.PasswordSalt;

//                    string enteredPassword = param.Password;

//                    byte[] enteredPasswordBytes = Encoding.UTF8.GetBytes(enteredPassword);

//                    byte[] saltedPassword = new byte[enteredPasswordBytes.Length + storedSaltBytes.Length];

//                    Buffer.BlockCopy(enteredPasswordBytes, 0, saltedPassword, 0, enteredPasswordBytes.Length);

//                    Buffer.BlockCopy(storedSaltBytes, 0, saltedPassword, enteredPasswordBytes.Length, storedSaltBytes.Length);

//                    string enteredPasswordHash = Helper.HashPassword(enteredPassword, storedSaltBytes);

//                    if (enteredPasswordHash == storedHashedPassword)
//                    {
//                        if (string.IsNullOrEmpty(sysCustomer.AccessToken) || sysCustomer.AccessTokenExpire < ServerTime)
//                        {
//                            loginResp.Uid = sysCustomer.Id;
//                            loginResp.Username = sysCustomer.Username;
//                            loginResp.FirstName = sysCustomer.FirstName == null ? string.Empty : sysCustomer.FirstName;
//                            loginResp.LastName = sysCustomer.LastName == null ? string.Empty : sysCustomer.LastName;
//                            loginResp.Email = sysCustomer.Email == null ? string.Empty : sysCustomer.Email;
//                            loginResp.MobilePhone = sysCustomer.MobilePhone == null ? string.Empty : sysCustomer.MobilePhone;

//                            var authClaims = new List<Claim>
//                                {
//                                    new Claim(ClaimTypes.Name, sysCustomer.Username),
//                                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//                                };

//                            var token = _JWTManagerRepositories.CreateToken(authClaims);

//                            _ = int.TryParse(configuration["JWT:TokenValidityInDays"], out int TokenValidityInDays);

//                            var TokenCreate = new JwtSecurityTokenHandler().WriteToken(token);

//                            #region Adding Log Access

//                            LogAccessToken logAccessToken = new LogAccessToken();

//                            logAccessToken.SysCustomersId = sysCustomer.Id;
//                            logAccessToken.AccessToken = TokenCreate;
//                            logAccessToken.ExpireDate = ServerTime.AddDays(TokenValidityInDays);
//                            logAccessToken.RequestDate = ServerTime;

//                            Task.Run(() => _LogAccessTokenRepositories.Create(logAccessToken));

//                            #endregion

//                            sysCustomer.AccessToken = logAccessToken.AccessToken;
//                            sysCustomer.AccessTokenExpire = logAccessToken.ExpireDate;
//                            sysCustomer.LastLoginDate = ServerTime;

//                            _context.SysCustomers.Update(sysCustomer);

//                            await _context.SaveChangesAsync();

//                            loginResp.AccessToken = TokenCreate;
//                            loginResp.AccessTokenExpire = logAccessToken.ExpireDate;
//                            loginResp.LastLogin = ServerTime;

//                            resp.Status = Constants.StatusSuccess;
//                            resp.HttpCode = Constants.HttpCode200;
//                            resp.HttpMessage = Constants.HttpCode200Message;
//                            resp.Output = loginResp;
//                        }
//                        else
//                        {
//                            sysCustomer.LastLoginDate = ServerTime;

//                            _context.SysCustomers.Update(sysCustomer);

//                            await _context.SaveChangesAsync();


//                            loginResp.Uid = sysCustomer.Id;
//                            loginResp.Username = sysCustomer.Username;
//                            loginResp.FirstName = sysCustomer.FirstName == null ? string.Empty : sysCustomer.FirstName;
//                            loginResp.LastName = sysCustomer.LastName == null ? string.Empty : sysCustomer.LastName;
//                            loginResp.Email = sysCustomer.Email == null ? string.Empty : sysCustomer.Email;
//                            loginResp.MobilePhone = sysCustomer.MobilePhone == null ? string.Empty : sysCustomer.MobilePhone;
//                            loginResp.AccessToken = sysCustomer.AccessToken;
//                            loginResp.AccessTokenExpire = sysCustomer.AccessTokenExpire == null ? null : sysCustomer.AccessTokenExpire;
//                            loginResp.LastLogin = ServerTime;


//                            resp.Status = Constants.StatusSuccess;
//                            resp.HttpCode = Constants.HttpCode200;
//                            resp.HttpMessage = Constants.HttpCode200Message;
//                            resp.Output = loginResp;

//                        }
//                    }
//                    else
//                    {
//                        resp.Status = Constants.StatusError;
//                        resp.HttpCode = Constants.HttpCode400;
//                        resp.HttpMessage = Constants.HttpCode400Message;
//                        resp.Message = Constants.AuthenticationInvalidPassword;
//                    }
//                }
//                else
//                {
//                    resp.Status = Constants.StatusError;
//                    resp.HttpCode = Constants.HttpCode400;
//                    resp.HttpMessage = Constants.HttpCode400Message;
//                    resp.Message = Constants.AuthenticationUsernameNotFound;
//                }

//            }
//            catch (Exception ex)
//            {
//                resp.Status = Constants.StatusError;
//                resp.HttpCode = Constants.HttpCode500;
//                resp.Message = Constants.HttpCode500Message;
//                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
//            }

//            return resp;
//        }

//        public async Task<Response> SignInWithToken(LoginWithToken Param)
//        {
//            Response resp = new Response();

//            SysCustomer sysCustomer = new SysCustomer();

//            LoginRespone loginResp = new LoginRespone();

//            try
//            {
//                var queryable = _context.SysCustomers.AsQueryable();

//                Guid guidId = Guid.Parse(Param.Uid);

//                queryable = queryable.Where(x => x.Id == guidId);

//                sysCustomer = await queryable.FirstOrDefaultAsync();

//                if (sysCustomer != null)
//                {
//                    if (sysCustomer.AccessToken == Param.Token)
//                    {
//                        if (sysCustomer.AccessTokenExpire < ServerTime)
//                        {
//                            loginResp.Uid = sysCustomer.Id;
//                            loginResp.Username = sysCustomer.Username;
//                            loginResp.FirstName = sysCustomer.FirstName == null ? string.Empty : sysCustomer.FirstName;
//                            loginResp.LastName = sysCustomer.LastName == null ? string.Empty : sysCustomer.LastName;
//                            loginResp.Email = sysCustomer.Email == null ? string.Empty : sysCustomer.Email;
//                            loginResp.MobilePhone = sysCustomer.MobilePhone == null ? string.Empty : sysCustomer.MobilePhone;
//                            loginResp.AccessToken = sysCustomer.AccessToken;
//                            loginResp.AccessTokenExpire = sysCustomer.AccessTokenExpire.Value;
//                            loginResp.LastLogin = ServerTime;


//                            resp.Status = Constants.StatusSuccess;
//                            resp.HttpCode = Constants.HttpCode200;
//                            resp.HttpMessage = Constants.HttpCode200Message;
//                            resp.Output = loginResp;

//                        }
//                        else
//                        {
//                            resp.Status = Constants.StatusError;
//                            resp.HttpCode = Constants.HttpCode400;
//                            resp.HttpMessage = Constants.HttpCode400Message;
//                            resp.Message = Constants.AuthenticationTokenExpire;
//                        }
//                    }
//                    else
//                    {
//                        resp.Status = Constants.StatusError;
//                        resp.HttpCode = Constants.HttpCode400;
//                        resp.HttpMessage = Constants.HttpCode400Message;
//                        resp.Message = Constants.AuthenticationTokenNotFound;
//                    }
//                }
//                else
//                {
//                    resp.Status = Constants.StatusError;
//                    resp.HttpCode = Constants.HttpCode400;
//                    resp.HttpMessage = Constants.HttpCode400Message;
//                    resp.Message = Constants.AuthenticationUsernameNotFound;
//                }
//            }
//            catch (Exception ex)
//            {
//                resp.Status = Constants.StatusError;
//                resp.HttpCode = Constants.HttpCode500;
//                resp.Message = Constants.HttpCode500Message;
//                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
//            }

//            return resp;
//        }

//        public async Task<Response> SignInWithMobileOTP(string phone)
//        {
//            Response resp = new Response();

//            SysCustomer sysCustomer = new SysCustomer();

//            try
//            {
//                var queryable = _context.SysCustomers.AsQueryable();

//                queryable = queryable.Where(x => x.MobilePhone.ToLower() == phone.ToLower());

//                sysCustomer = await queryable.FirstOrDefaultAsync();

//                if (sysCustomer != null)
//                {
//                    var otpSendReturn = await _smsMtkRepositories.OTPSend(phone);

//                    if (otpSendReturn.code == "000" && otpSendReturn.detail == "OK.")
//                    {
//                        resp.Status = Constants.StatusSuccess;
//                        resp.HttpCode = Constants.HttpCode200;
//                        resp.HttpMessage = Constants.HttpCode200Message;
//                        resp.Output = otpSendReturn.result;
//                    }
//                    else
//                    {
//                        resp.Status = Constants.StatusError;
//                        resp.HttpCode = Constants.HttpCode400;
//                        resp.HttpMessage = Constants.HttpCode400Message;
//                        resp.Message = Constants.RegisterMobilePhoneOTPFaild;
//                    }
//                }
//                else
//                {
//                    resp.Status = Constants.StatusError;
//                    resp.HttpCode = Constants.HttpCode400;
//                    resp.HttpMessage = Constants.HttpCode400Message;
//                    resp.Message = Constants.AuthenticationUsernameNotFound;
//                }

//            }
//            catch (Exception ex)
//            {
//                resp.Status = Constants.StatusError;
//                resp.HttpCode = Constants.HttpCode500;
//                resp.Message = Constants.HttpCode500Message;
//                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
//            }

//            return resp;
//        }

//        public async Task<Response> SignInVerifyOTP(OtpValidateRequest param)
//        {
//            Response resp = new Response();

//            SysCustomer sysCustomer = new SysCustomer();

//            LoginRespone loginResp = new LoginRespone();

//            try
//            {
//                if (!string.IsNullOrEmpty(param.phone) && !string.IsNullOrEmpty(param.token) && !string.IsNullOrEmpty(param.otp_code) && !string.IsNullOrEmpty(param.ref_code))
//                {
//                    var otpSendReturn = _smsMtkRepositories.OTPValidate(param);

//                    if (otpSendReturn.result != null && otpSendReturn.result.status == true)
//                    {
//                        sysCustomer = await _context.SysCustomers.Where(x => x.MobilePhone == param.phone).AsNoTracking().FirstOrDefaultAsync();

//                        if (string.IsNullOrEmpty(sysCustomer.AccessToken) || sysCustomer.AccessTokenExpire < ServerTime)
//                        {
//                            loginResp.Uid = sysCustomer.Id;
//                            loginResp.Username = sysCustomer.Username;
//                            loginResp.FirstName = sysCustomer.FirstName == null ? string.Empty : sysCustomer.FirstName;
//                            loginResp.LastName = sysCustomer.LastName == null ? string.Empty : sysCustomer.LastName;
//                            loginResp.Email = sysCustomer.Email == null ? string.Empty : sysCustomer.Email;
//                            loginResp.MobilePhone = sysCustomer.MobilePhone == null ? string.Empty : sysCustomer.MobilePhone;

//                            var authClaims = new List<Claim>
//                                {
//                                    new Claim(ClaimTypes.Name, sysCustomer.Username),
//                                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//                                };

//                            var token = _JWTManagerRepositories.CreateToken(authClaims);

//                            _ = int.TryParse(configuration["JWT:TokenValidityInDays"], out int TokenValidityInDays);

//                            var TokenCreate = new JwtSecurityTokenHandler().WriteToken(token);

//                            #region Adding Log Access

//                            LogAccessToken logAccessToken = new LogAccessToken();

//                            logAccessToken.SysCustomersId = sysCustomer.Id;
//                            logAccessToken.AccessToken = TokenCreate;
//                            logAccessToken.ExpireDate = ServerTime.AddDays(TokenValidityInDays);
//                            logAccessToken.RequestDate = ServerTime;

//                            Task.Run(() => _LogAccessTokenRepositories.Create(logAccessToken));

//                            #endregion

//                            sysCustomer.AccessToken = logAccessToken.AccessToken;
//                            sysCustomer.AccessTokenExpire = logAccessToken.ExpireDate;
//                            sysCustomer.LastLoginDate = ServerTime;

//                            _context.SysCustomers.Update(sysCustomer);

//                            await _context.SaveChangesAsync();

//                            loginResp.AccessToken = TokenCreate;
//                            loginResp.AccessTokenExpire = logAccessToken.ExpireDate;
//                            loginResp.LastLogin = ServerTime;

//                            resp.Status = Constants.StatusSuccess;
//                            resp.HttpCode = Constants.HttpCode200;
//                            resp.HttpMessage = Constants.HttpCode200Message;
//                            resp.Output = loginResp;
//                        }
//                        else
//                        {
//                            sysCustomer.LastLoginDate = ServerTime;

//                            _context.SysCustomers.Update(sysCustomer);

//                            await _context.SaveChangesAsync();


//                            loginResp.Uid = sysCustomer.Id;
//                            loginResp.Username = sysCustomer.Username;
//                            loginResp.FirstName = sysCustomer.FirstName == null ? string.Empty : sysCustomer.FirstName;
//                            loginResp.LastName = sysCustomer.LastName == null ? string.Empty : sysCustomer.LastName;
//                            loginResp.Email = sysCustomer.Email == null ? string.Empty : sysCustomer.Email;
//                            loginResp.MobilePhone = sysCustomer.MobilePhone == null ? string.Empty : sysCustomer.MobilePhone;
//                            loginResp.AccessToken = sysCustomer.AccessToken;
//                            loginResp.AccessTokenExpire = sysCustomer.AccessTokenExpire == null ? null : sysCustomer.AccessTokenExpire;
//                            loginResp.LastLogin = ServerTime;


//                            resp.Status = Constants.StatusSuccess;
//                            resp.HttpCode = Constants.HttpCode200;
//                            resp.HttpMessage = Constants.HttpCode200Message;
//                            resp.Output = loginResp;

//                        }
//                    }

//                    else
//                    {
//                        resp.Status = Constants.StatusError;
//                        resp.HttpCode = Constants.HttpCode400;
//                        resp.HttpMessage = Constants.HttpCode400Message;
//                        resp.Message = Constants.RegisterMobilePhoneOTPVerifyFaild;
//                    }
//                }

//                else
//                {
//                    resp.Status = Constants.StatusError;
//                    resp.HttpCode = Constants.HttpCode400;
//                    resp.HttpMessage = Constants.HttpCode400Message;
//                    resp.Message = Constants.Required;
//                }
//            }
//            catch (Exception ex)
//            {
//                resp.Status = Constants.StatusError;
//                resp.HttpCode = Constants.HttpCode500;
//                resp.Message = Constants.HttpCode500Message;
//                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
//            }

//            return resp;
//        }


//        #endregion


//        #region Sign Up

//        public async Task<Response> RegisterWithMobilePhone(string phone)
//        {
//            Response resp = new Response();

//            SysCustomer sysCustomer = new SysCustomer();

//            try
//            {
//                var queryable = _context.SysCustomers.AsQueryable();

//                queryable = queryable.Where(x => x.MobilePhone.ToLower() == phone.ToLower());

//                sysCustomer = await queryable.FirstOrDefaultAsync();

//                if (sysCustomer == null)
//                {
//                    var otpSendReturn = await _smsMtkRepositories.OTPSend(phone);

//                    if (otpSendReturn.code == "000" && otpSendReturn.detail == "OK.")
//                    {
//                        resp.Status = Constants.StatusSuccess;
//                        resp.HttpCode = Constants.HttpCode200;
//                        resp.HttpMessage = Constants.HttpCode200Message;
//                        resp.Output = otpSendReturn.result;
//                    }
//                    else
//                    {
//                        resp.Status = Constants.StatusError;
//                        resp.HttpCode = Constants.HttpCode400;
//                        resp.HttpMessage = Constants.HttpCode400Message;
//                        resp.Message = Constants.RegisterMobilePhoneOTPFaild;
//                    }
//                }
//                else
//                {
//                    resp.Status = Constants.StatusError;
//                    resp.HttpCode = Constants.HttpCode400;
//                    resp.HttpMessage = Constants.HttpCode400Message;
//                    resp.Message = Constants.RegisterMobilePhoneHasAlready;
//                }

//            }
//            catch (Exception ex)
//            {
//                resp.Status = Constants.StatusError;
//                resp.HttpCode = Constants.HttpCode500;
//                resp.Message = Constants.HttpCode500Message;
//                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
//            }

//            return resp;
//        }

//        public async Task<Response> RegisterVerifyOTP(OtpValidateRequest param)
//        {
//            Response resp = new Response();

//            try
//            {
//                if (!string.IsNullOrEmpty(param.phone) && !string.IsNullOrEmpty(param.token) && !string.IsNullOrEmpty(param.otp_code) && !string.IsNullOrEmpty(param.ref_code))
//                {
//                    var otpSendReturn = _smsMtkRepositories.OTPValidate(param);

//                    if (otpSendReturn.result != null && otpSendReturn.result.status == true)
//                    {
//                        SysCustomer sysCustomer = new SysCustomer();

//                        sysCustomer.Id = Helper.GenerateGuid();
//                        sysCustomer.MobilePhone = param.phone;
//                        sysCustomer.Username = param.phone;
//                        sysCustomer.IsActive = true;
//                        sysCustomer.RegisterDate = ServerTime;
//                        sysCustomer.LastLoginDate = ServerTime;

//                        _context.SysCustomers.Add(sysCustomer);

//                        await _context.SaveChangesAsync();

//                        resp.Status = Constants.StatusSuccess;
//                        resp.HttpCode = Constants.HttpCode200;
//                        resp.HttpMessage = Constants.HttpCode200Message;
//                        resp.Output = sysCustomer;
//                    }

//                    else
//                    {
//                        resp.Status = Constants.StatusError;
//                        resp.HttpCode = Constants.HttpCode400;
//                        resp.HttpMessage = Constants.HttpCode400Message;
//                        resp.Message = Constants.RegisterMobilePhoneOTPVerifyFaild;
//                    }
//                }

//                else
//                {
//                    resp.Status = Constants.StatusError;
//                    resp.HttpCode = Constants.HttpCode400;
//                    resp.HttpMessage = Constants.HttpCode400Message;
//                    resp.Message = Constants.Required;
//                }
//            }
//            catch (Exception ex)
//            {
//                resp.Status = Constants.StatusError;
//                resp.HttpCode = Constants.HttpCode500;
//                resp.Message = Constants.HttpCode500Message;
//                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
//            }

//            return resp;
//        }

//        #endregion



//        #region Revoke

//        public async Task<Response> RevokeTokenAll()
//        {
//            Response resp = new Response();

//            List<SysCustomer> sysCustomers = new List<SysCustomer>();


//            try
//            {
//                var queryable = _context.SysCustomers.AsQueryable();

//                sysCustomers = queryable.AsNoTracking().ToList();

//                if (sysCustomers != null && sysCustomers.Count > 0)
//                {
//                    foreach (var item in sysCustomers)
//                    {
//                        item.AccessToken = string.Empty;
//                        item.AccessTokenExpire = null;
//                    }

//                    _context.SysCustomers.UpdateRange(sysCustomers);

//                    await _context.SaveChangesAsync();

//                    resp.Status = Constants.StatusSuccess;
//                    resp.HttpCode = Constants.HttpCode200;
//                    resp.HttpMessage = Constants.HttpCode200Message;
//                }
//                else
//                {
//                    resp.Status = Constants.StatusError;
//                    resp.HttpCode = Constants.HttpCode400;
//                    resp.HttpMessage = Constants.HttpCode400Message;
//                    resp.Message = Constants.AuthenticationUsernameNotFound;
//                }

//            }
//            catch (Exception ex)
//            {
//                resp.Status = Constants.StatusError;
//                resp.HttpCode = Constants.HttpCode500;
//                resp.Message = Constants.HttpCode500Message;
//                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
//            }

//            return resp;
//        }

//        public async Task<Response> RevokeTokenById(string Uid)
//        {
//            Response resp = new Response();

//            SysCustomer sysCustomer = new SysCustomer();

//            try
//            {
//                var queryable = _context.SysCustomers.AsQueryable();

//                Guid guidId = Guid.Parse(Uid);

//                queryable = queryable.Where(x => x.Id == guidId);

//                sysCustomer = await queryable.FirstOrDefaultAsync();

//                if (sysCustomer != null)
//                {
//                    sysCustomer.AccessToken = string.Empty;
//                    sysCustomer.AccessTokenExpire = null;

//                    _context.SysCustomers.Update(sysCustomer);

//                    await _context.SaveChangesAsync();

//                    resp.Status = Constants.StatusSuccess;
//                    resp.HttpCode = Constants.HttpCode200;
//                    resp.HttpMessage = Constants.HttpCode200Message;
//                }
//                else
//                {
//                    resp.Status = Constants.StatusError;
//                    resp.HttpCode = Constants.HttpCode400;
//                    resp.HttpMessage = Constants.HttpCode400Message;
//                    resp.Message = Constants.AuthenticationUsernameNotFound;
//                }
//            }
//            catch (Exception ex)
//            {
//                resp.Status = Constants.StatusError;
//                resp.HttpCode = Constants.HttpCode500;
//                resp.Message = Constants.HttpCode500Message;
//                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
//            }

//            return resp;
//        }

//        #endregion


//        #region Forgot and change password

//        public async Task<Response> ForgotPassword(string phone)
//        {
//            Response resp = new Response();

//            SysCustomer sysCustomer = new SysCustomer();

//            try
//            {
//                var queryable = _context.SysCustomers.AsQueryable();

//                queryable = queryable.Where(x => x.MobilePhone.ToLower() == phone.ToLower());

//                sysCustomer = await queryable.FirstOrDefaultAsync();

//                if (sysCustomer != null)
//                {
//                    var otpSendReturn = await _smsMtkRepositories.OTPSend(phone);

//                    if (otpSendReturn.code == "000" && otpSendReturn.detail == "OK.")
//                    {
//                        resp.Status = Constants.StatusSuccess;
//                        resp.HttpCode = Constants.HttpCode200;
//                        resp.HttpMessage = Constants.HttpCode200Message;
//                        resp.Output = otpSendReturn.result;
//                    }
//                    else
//                    {
//                        resp.Status = Constants.StatusError;
//                        resp.HttpCode = Constants.HttpCode400;
//                        resp.HttpMessage = Constants.HttpCode400Message;
//                        resp.Message = Constants.RegisterMobilePhoneOTPFaild;
//                    }
//                }
//                else
//                {
//                    resp.Status = Constants.StatusError;
//                    resp.HttpCode = Constants.HttpCode400;
//                    resp.HttpMessage = Constants.HttpCode400Message;
//                    resp.Message = Constants.AuthenticationUsernameNotFound;
//                }

//            }
//            catch (Exception ex)
//            {
//                resp.Status = Constants.StatusError;
//                resp.HttpCode = Constants.HttpCode500;
//                resp.Message = Constants.HttpCode500Message;
//                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
//            }

//            return resp;
//        }

//        public async Task<Response> ForgotPasswordOtp(ForgotPassword param)
//        {
//            Response resp = new Response();

//            SysCustomer sysCustomer = new SysCustomer();

//            LoginRespone loginResp = new LoginRespone();

//            try
//            {
//                if (param.Password == param.ConfirmPassword)
//                {
//                    if (!string.IsNullOrEmpty(param.phone) && !string.IsNullOrEmpty(param.token) && !string.IsNullOrEmpty(param.otp_code) && !string.IsNullOrEmpty(param.ref_code))
//                    {
//                        var otpSendReturn = _smsMtkRepositories.OTPValidate(param);

//                        if (otpSendReturn.result != null && otpSendReturn.result.status == true)
//                        {
//                            sysCustomer = await _context.SysCustomers.Where(x => x.MobilePhone == param.phone).AsNoTracking().FirstOrDefaultAsync();

//                            loginResp.Uid = sysCustomer.Id;
//                            loginResp.Username = sysCustomer.Username;
//                            loginResp.FirstName = sysCustomer.FirstName == null ? string.Empty : sysCustomer.FirstName;
//                            loginResp.LastName = sysCustomer.LastName == null ? string.Empty : sysCustomer.LastName;
//                            loginResp.Email = sysCustomer.Email == null ? string.Empty : sysCustomer.Email;
//                            loginResp.MobilePhone = sysCustomer.MobilePhone == null ? string.Empty : sysCustomer.MobilePhone;

//                            var authClaims = new List<Claim>
//                                {
//                                    new Claim(ClaimTypes.Name, sysCustomer.Username),
//                                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//                                };

//                            var token = _JWTManagerRepositories.CreateToken(authClaims);

//                            _ = int.TryParse(configuration["JWT:TokenValidityInDays"], out int TokenValidityInDays);

//                            var TokenCreate = new JwtSecurityTokenHandler().WriteToken(token);

//                            #region Adding Log Access

//                            LogAccessToken logAccessToken = new LogAccessToken();

//                            logAccessToken.SysCustomersId = sysCustomer.Id;
//                            logAccessToken.AccessToken = TokenCreate;
//                            logAccessToken.ExpireDate = ServerTime.AddDays(TokenValidityInDays);
//                            logAccessToken.RequestDate = ServerTime;

//                            Task.Run(() => _LogAccessTokenRepositories.Create(logAccessToken));

//                            #endregion


//                            byte[] saltBytes = Helper.GenerateSalt();
//                            string hashedPassword = Helper.HashPassword(param.Password, saltBytes);
//                            string base64Salt = Convert.ToBase64String(saltBytes);
//                            byte[] retrievedSaltBytes = Convert.FromBase64String(base64Salt);

//                            sysCustomer.Password = hashedPassword;
//                            sysCustomer.PasswordSalt = retrievedSaltBytes;

//                            sysCustomer.AccessToken = logAccessToken.AccessToken;
//                            sysCustomer.AccessTokenExpire = logAccessToken.ExpireDate;
//                            sysCustomer.LastLoginDate = ServerTime;

//                            _context.SysCustomers.Update(sysCustomer);

//                            await _context.SaveChangesAsync();

//                            loginResp.AccessToken = TokenCreate;
//                            loginResp.AccessTokenExpire = logAccessToken.ExpireDate;
//                            loginResp.LastLogin = ServerTime;

//                            resp.Status = Constants.StatusSuccess;
//                            resp.HttpCode = Constants.HttpCode200;
//                            resp.HttpMessage = Constants.HttpCode200Message;
//                            resp.Output = loginResp;

//                        }

//                        else
//                        {
//                            resp.Status = Constants.StatusError;
//                            resp.HttpCode = Constants.HttpCode400;
//                            resp.HttpMessage = Constants.HttpCode400Message;
//                            resp.Message = Constants.RegisterMobilePhoneOTPVerifyFaild;
//                        }
//                    }
//                    else
//                    {
//                        resp.Status = Constants.StatusError;
//                        resp.HttpCode = Constants.HttpCode400;
//                        resp.HttpMessage = Constants.HttpCode400Message;
//                        resp.Message = Constants.Required;
//                    }
//                }
//                else
//                {
//                    resp.Status = Constants.StatusError;
//                    resp.HttpCode = Constants.HttpCode400;
//                    resp.HttpMessage = Constants.HttpCode400Message;
//                    resp.Message = Constants.PasswordNotMatch;
//                }
//            }
//            catch (Exception ex)
//            {
//                resp.Status = Constants.StatusError;
//                resp.HttpCode = Constants.HttpCode500;
//                resp.Message = Constants.HttpCode500Message;
//                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
//            }

//            return resp;
//        }

//        public async Task<Response> ChangePassword(ChangePassword param)
//        {
//            Response resp = new Response();
//            try
//            {
//                Guid guidId = Guid.Parse(param.Id);

//                var objUpdate = await _context.SysCustomers.Where(x => x.Id == guidId).FirstOrDefaultAsync();

//                if (objUpdate != null)
//                {
//                    if (param.Password == param.ConfirmPassword)
//                    {
//                        byte[] saltBytes = Helper.GenerateSalt();
//                        string hashedPassword = Helper.HashPassword(param.Password, saltBytes);
//                        string base64Salt = Convert.ToBase64String(saltBytes);
//                        byte[] retrievedSaltBytes = Convert.FromBase64String(base64Salt);

//                        objUpdate.Password = hashedPassword;
//                        objUpdate.PasswordSalt = retrievedSaltBytes;

//                        var authClaims = new List<Claim>
//                                {
//                                    new Claim(ClaimTypes.Name, objUpdate.Username),
//                                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//                                };

//                        var token = _JWTManagerRepositories.CreateToken(authClaims);

//                        _ = int.TryParse(configuration["JWT:TokenValidityInDays"], out int TokenValidityInDays);

//                        var TokenCreate = new JwtSecurityTokenHandler().WriteToken(token);

//                        #region Adding Log Access

//                        LogAccessToken logAccessToken = new LogAccessToken();

//                        logAccessToken.SysCustomersId = objUpdate.Id;
//                        logAccessToken.AccessToken = TokenCreate;
//                        logAccessToken.ExpireDate = ServerTime.AddDays(TokenValidityInDays);
//                        logAccessToken.RequestDate = ServerTime;

//                        Task.Run(() => _LogAccessTokenRepositories.Create(logAccessToken));

//                        #endregion


//                        objUpdate.AccessToken = logAccessToken.AccessToken;
//                        objUpdate.AccessTokenExpire = logAccessToken.ExpireDate;

//                        _context.SysCustomers.Update(objUpdate);

//                        await _context.SaveChangesAsync();

//                        resp.Status = Constants.StatusSuccess;
//                        resp.HttpCode = Constants.HttpCode200;
//                        resp.HttpMessage = Constants.HttpCode200Message;
//                        resp.Output = objUpdate;
//                    }
//                    else
//                    {
//                        resp.Status = Constants.StatusError;
//                        resp.HttpCode = Constants.HttpCode400;
//                        resp.HttpMessage = Constants.HttpCode400Message;
//                        resp.Message = Constants.PasswordNotMatch;
//                    }
//                }
//                else
//                {
//                    resp.Status = Constants.StatusError;
//                    resp.HttpCode = Constants.HttpCode400;
//                    resp.HttpMessage = Constants.HttpCode400Message;
//                    resp.Message = Constants.AuthenticationUsernameNotFound;
//                }
//            }
//            catch (Exception ex)
//            {
//                resp.Status = Constants.StatusError;
//                resp.HttpCode = Constants.HttpCode500;
//                resp.Message = Constants.HttpCode500Message;
//                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
//            }

//            return resp;
//        }

//        #endregion


//        #region IDispose Zone

//        private bool DisposedValue;

//        protected virtual void Dispose(bool disposing)
//        {
//            if (!DisposedValue)
//            {
//                if (disposing)
//                {
//                    _context.Dispose();
//                }

//                DisposedValue = true;
//            }
//        }

//        public void Dispose()
//        {
//            Dispose(disposing: true);
//            GC.SuppressFinalize(this);
//        }

//        #endregion

//    }
//}

