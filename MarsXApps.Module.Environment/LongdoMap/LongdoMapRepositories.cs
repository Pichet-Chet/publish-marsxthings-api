using System;
using MarsXApps.Models;
using MarsXApps.Models.Constants;
using MarsXApps.Models.Customs;
using MarsXApps.Models.Filter.LongdoMap;
using MarsXApps.Models.Filter.MasterData;
using MarsXApps.Service;

namespace MarsXApps.Module.Environment.LongdoMap
{
    public interface ILongdoMapRepositories
    {
        public Task<Response> rerverseGeocoding(ReverseGeocodingFilter param);
    }


    public class LongdoMapRepositories : ILongdoMapRepositories
    {
        DateTime ServerTime;

        private readonly Mapper _mapper;

        private readonly ILongdoMapService _service;

        public LongdoMapRepositories(ILongdoMapService service)
        {
            _mapper = new Mapper();

            _service = service;

            ServerTime = DateTime.Now.MarsX();

        }

        public async Task<Response> rerverseGeocoding(ReverseGeocodingFilter param)
        {
            Response resp = new Response();

            string Outbound = string.Empty;

            try
            {
                Outbound = await _service.rerverseGeocoding(param);

                if (!string.IsNullOrEmpty(Outbound))
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
    }
}

