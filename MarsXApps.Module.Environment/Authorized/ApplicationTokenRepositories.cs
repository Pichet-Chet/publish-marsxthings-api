using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MarsXApps.Models;
using MarsXApps.Models.Authorized;
using MarsXApps.Models.Constants;
using MarsXApps.Service;
using MarsXApps.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

namespace MarsXApps.Module.Environment.Authorized
{
    public interface IApplicationTokenRepositories
    {
        public Task<Response> AccessToken(string Key);

        public Task<Response> RevokeTokenAll();

        public Task<Response> RevokeTokenById(string Uid);
    }

    public class ApplicationTokenRepositories : IApplicationTokenRepositories
    {
        DateTime ServerTime;

        IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();

        private readonly IApplicationTokenService _service;


        public ApplicationTokenRepositories()
        {
            _service = new ApplicationTokenService();

            ServerTime = DateTime.Now.MarsX();

        }


        public async Task<Response> AccessToken(string Key)
        {
            Response resp = new Response();

            try
            {
                resp = await _service.AccessToken(Key);
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

    }
}

