using System;
using MarsXApps.Models;
using MarsXApps.Models.Constants;
using MarsXApps.Models.Customs;
using MarsXApps.Models.Filter.Authorized;
using MarsXApps.Service;
using MarsXApps.Service;
using MarsXApps.Service.Models;
using MarsXApps.Service.Validate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MarsXApps.Module.Environment.Authorized
{
    public interface ILogAccessApplicationTokenRepositories
    {
        public Task<Response> GetAll(LogAccessApplicationTokenFilter param);

        public Task<Response> GetById(int id);

        public Task<Response> Create(LogAccessApplicationTokenModel param);
    }

    public class LogAccessApplicationTokenRepositories : ILogAccessApplicationTokenRepositories
    {
        DateTime ServerTime;

        IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();

        private readonly Mapper _mapper;

        private readonly ILogAccessApplicationTokenService _service;

        public LogAccessApplicationTokenRepositories()
        {
            _mapper = new Mapper();

            _service = new LogAccessApplicationTokenService();

            ServerTime = DateTime.Now.MarsX();
        }


        public async Task<Response> GetAll(LogAccessApplicationTokenFilter param)
        {
            Response resp = new Response();

            List<LogAccessApplicationTokenModel> Outbound = new List<LogAccessApplicationTokenModel>();

            try
            {
                Outbound = await _service.GetAll(param);

                if (Outbound.Count > 0)
                {
                    resp.Status = Constants.StatusSuccess;
                    resp.HttpCode = Constants.HttpCode200;
                    resp.HttpMessage = Constants.HttpCode200Message;
                    resp.Output = Outbound;
                }
                else
                {
                    resp.Status = Constants.StatusError;
                    resp.HttpCode = Constants.HttpCode400;
                    resp.HttpMessage = Constants.HttpCode204Message;
                    resp.Message = Constants.RecordDataNotFound;
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

        public async Task<Response> GetById(int id)
        {
            Response resp = new Response();

            LogAccessApplicationTokenModel Outbound = new LogAccessApplicationTokenModel();

            try
            {
                Outbound = await _service.GetById(id);

                if (Outbound != null)
                {
                    resp.Status = Constants.StatusSuccess;
                    resp.HttpCode = Constants.HttpCode200;
                    resp.HttpMessage = Constants.HttpCode200Message;
                    resp.Output = Outbound;
                }
                else
                {
                    resp.Status = Constants.StatusError;
                    resp.HttpCode = Constants.HttpCode400;
                    resp.HttpMessage = Constants.HttpCode204Message;
                    resp.Message = Constants.RecordDataNotFound;
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

        public async Task<Response> Create(LogAccessApplicationTokenModel param)
        {
            Response resp = new Response();

            LogAccessApplicationToken Inbound = new LogAccessApplicationToken();

            LogAccessApplicationTokenModel Outbound = new LogAccessApplicationTokenModel();

            try
            {
                Inbound = _mapper.Map(param);

                Outbound = await _service.Create(Inbound);

                resp.Status = Constants.StatusSuccess;
                resp.HttpCode = Constants.HttpCode200;
                resp.HttpMessage = Constants.HttpCode200Message;
                resp.Output = Outbound;
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

