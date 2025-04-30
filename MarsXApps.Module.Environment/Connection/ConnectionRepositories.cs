using System;
using MarsXApps.Models;
using MarsXApps.Models.Constants;
using MarsXApps.Models.Customs;
using MarsXApps.Models.Customs.MethodType;
using MarsXApps.Models.Filter.MasterData;
using MarsXApps.Service;
using Microsoft.Extensions.Configuration;

namespace MarsXApps.Module.Environment.Connection
{
    public interface IConnectionRepositories
    {
        public Task<Response> MarsXDatabase();

    }

    public class ConnectionRepositories : IConnectionRepositories
    {
        DateTime ServerTime;

        private readonly IConnectionService _service;

        public ConnectionRepositories(IConnectionService service)
        {
            _service = service;

            ServerTime = DateTime.Now.MarsX();
        }



        public async Task<Response> MarsXDatabase()
        {
            Response resp = new Response();

            bool Outbound = false;

            try
            {
                Outbound = await _service.OpenConnection();

                if (Outbound == true)
                {
                    await _service.CloseConnection();

                    resp.Status = Constants.StatusSuccess;
                    resp.HttpCode = Constants.HttpCode200;
                    resp.HttpMessage = Constants.HttpCode200Message;
                    resp.Message = Constants.HttpCode200Message;
                    resp.Output = "";
                }
                else
                {
                    resp.Status = Constants.StatusError;
                    resp.HttpCode = Constants.HttpCode400;
                    resp.HttpMessage = Constants.HttpCode204Message;
                    resp.Message = Constants.HttpCode500Message;
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


    }
}

