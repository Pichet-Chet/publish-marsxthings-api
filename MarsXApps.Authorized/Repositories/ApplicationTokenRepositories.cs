//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using MarsXApps.Models;
//using MarsXApps.Models.Authorized;
//using MarsXApps.Models.Constants;
//using MarsXApps.Service.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;

//namespace MarsXApps.Module.Authorized.Repositories
//{
//    public interface IApplicationTokenRepositories : IDisposable
//    {
//        public Task<Response> AccessToken(string Key);

//        public Task<Response> RevokeTokenAll();

//        public Task<Response> RevokeTokenById(string Uid);

//    }

//    public class ApplicationTokenRepositories : IApplicationTokenRepositories
//    {
//        DateTime ServerTime;

//        IConfigurationRoot configuration = new ConfigurationBuilder()
//                   .SetBasePath(Directory.GetCurrentDirectory())
//                   .AddJsonFile("appsettings.json")
//                   .Build();

//        private readonly IJWTManagerRepositories _JWTManagerRepositories;

//        private readonly ILogAccessApplicationTokenRepositories _LogAccessApplicationTokenRepositories;

//        private readonly MarscommuContext _context;

//        public ApplicationTokenRepositories()
//        {
//            _context = new MarscommuContext();

//            _JWTManagerRepositories = new JWTManagerRepositories();

//            _LogAccessApplicationTokenRepositories = new LogAccessApplicationTokenRepositories();

//            ServerTime = Helper.GetDateTimeByGMT(Convert.ToInt32(configuration["APP:GMT"]));

//        }


//        public async Task<Response> AccessToken(string Key)
//        {
//            Response resp = new Response();

//            SysApplicationToken sysApplicationToken = new SysApplicationToken();

//            ApplicationTokenReponse tokenReponse = new ApplicationTokenReponse();

//            try
//            {
//                var queryable = _context.SysApplicationTokens.AsQueryable();

//                queryable = queryable.Where(x => x.Key == Key);

//                sysApplicationToken = await queryable.FirstOrDefaultAsync();

//                if (sysApplicationToken != null)
//                {
//                    if (string.IsNullOrEmpty(sysApplicationToken.AccessToken) || sysApplicationToken.AccessTokenExpire < ServerTime)
//                    {
//                        tokenReponse.Uid = sysApplicationToken.Uid.ToString();

//                        var authClaims = new List<Claim>
//                                {
//                                    new Claim(ClaimTypes.Name, tokenReponse.Uid),
//                                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//                                };

//                        var token = _JWTManagerRepositories.CreateToken(authClaims);

//                        _ = int.TryParse(configuration["JWT:TokenValidityInDays"], out int TokenValidityInDays);

//                        var TokenCreate = new JwtSecurityTokenHandler().WriteToken(token);


//                        #region Adding Log Access

//                        LogAccessApplicationToken logAccessToken = new LogAccessApplicationToken();

//                        logAccessToken.SysApplicationTokenId = sysApplicationToken.Id;
//                        logAccessToken.AccessToken = TokenCreate;
//                        logAccessToken.ExpireDate = ServerTime.AddDays(TokenValidityInDays);
//                        logAccessToken.RequestDate = ServerTime;

//                        Task.Run(() => _LogAccessApplicationTokenRepositories.Create(logAccessToken));

//                        #endregion


//                        sysApplicationToken.AccessToken = logAccessToken.AccessToken;
//                        sysApplicationToken.AccessTokenExpire = logAccessToken.ExpireDate;
//                        sysApplicationToken.LastRequest = ServerTime;

//                        _context.SysApplicationTokens.Update(sysApplicationToken);

//                        await _context.SaveChangesAsync();

//                        tokenReponse.AccessToken = logAccessToken.AccessToken;
//                        tokenReponse.AccessTokenExpire = logAccessToken.ExpireDate;
//                        tokenReponse.LastRequest = ServerTime;

//                        resp.Status = Constants.StatusSuccess;
//                        resp.HttpCode = Constants.HttpCode200;
//                        resp.HttpMessage = Constants.HttpCode200Message;
//                        resp.Output = tokenReponse;
//                    }
//                    else
//                    {
//                        sysApplicationToken.LastRequest = ServerTime;

//                        _context.SysApplicationTokens.Update(sysApplicationToken);

//                        await _context.SaveChangesAsync();


//                        tokenReponse.Uid = sysApplicationToken.Uid.ToString();
//                        tokenReponse.AccessToken = sysApplicationToken.AccessToken;
//                        tokenReponse.AccessTokenExpire = sysApplicationToken.AccessTokenExpire;
//                        tokenReponse.LastRequest = ServerTime;


//                        resp.Status = Constants.StatusSuccess;
//                        resp.HttpCode = Constants.HttpCode200;
//                        resp.HttpMessage = Constants.HttpCode200Message;
//                        resp.Output = tokenReponse;

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

//        public async Task<Response> RevokeTokenAll()
//        {
//            Response resp = new Response();

//            List<SysApplicationToken> sysApplications = new List<SysApplicationToken>();


//            try
//            {
//                var queryable = _context.SysApplicationTokens.AsQueryable();

//                sysApplications = queryable.AsNoTracking().ToList();

//                if (sysApplications != null && sysApplications.Count > 0)
//                {
//                    foreach (var item in sysApplications)
//                    {
//                        item.AccessToken = string.Empty;
//                        item.AccessTokenExpire = null;
//                    }

//                    _context.SysApplicationTokens.UpdateRange(sysApplications);

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

//            SysApplicationToken sysApplication = new SysApplicationToken();

//            try
//            {
//                var queryable = _context.SysApplicationTokens.AsQueryable();

//                Guid guidId = Guid.Parse(Uid);

//                queryable = queryable.Where(x => x.Uid == guidId);

//                sysApplication = await queryable.FirstOrDefaultAsync();

//                if (sysApplication != null)
//                {
//                    sysApplication.AccessToken = string.Empty;
//                    sysApplication.AccessTokenExpire = null;

//                    _context.SysApplicationTokens.Update(sysApplication);

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

