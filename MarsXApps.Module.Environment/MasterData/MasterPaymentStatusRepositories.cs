using System;
using MarsXApps.Models;
using MarsXApps.Models.Constants;
using MarsXApps.Models.Customs;
using MarsXApps.Models.Customs.MethodType;
using MarsXApps.Models.Filter.MasterData;
using MarsXApps.Models.Filter.ServiceAfterSale;
using MarsXApps.Service;
using MarsXApps.Service;
using MarsXApps.Service.Models;
using MarsXApps.Service.Validate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MarsXApps.Module.Environment.MasterData
{
    public interface IMasterPaymentStatusRepositories
    {
        public Task<Response> GetAll(MasterPaymentStatusFilter param);

        public Task<Response> GetById(int id);

        public Task<Response> Create(MasterPaymentStatusModel param);

        public Task<Response> Update(MasterPaymentStatusModel param);
    }

    public class MasterPaymentStatusRepositories : IMasterPaymentStatusRepositories
    {
        DateTime ServerTime;

        IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();

        private readonly Mapper _mapper;

        private readonly IMasterPaymentStatusService _service;

        public MasterPaymentStatusRepositories()
        {
            _mapper = new Mapper();

            _service = new MasterPaymentStatusService();

            ServerTime = DateTime.Now.MarsX();
        }



        public async Task<Response> GetAll(MasterPaymentStatusFilter param)
        {
            Response resp = new Response();

            List<MasterPaymentStatusModel> Outbound = new List<MasterPaymentStatusModel>();

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

            MasterPaymentStatusModel Outbound = new MasterPaymentStatusModel();

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

        public async Task<Response> Create(MasterPaymentStatusModel param)
        {
            Response resp = new Response();

            MasterPaymentStatus Inbound = new MasterPaymentStatus();

            MasterPaymentStatusModel Outbound = new MasterPaymentStatusModel();

            try
            {
                Inbound = _mapper.Map(param);

                Inbound.CreatedDate = ServerTime;
                Inbound.UpdatedDate = ServerTime;

                bool duplicate = await _service.DuplicateKey(Inbound, MethodType.CREATE);

                if (duplicate == false)
                {
                    Outbound = await _service.Create(Inbound);

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
                    resp.Output = Constants.InvalidDataDuplicate;
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

        public async Task<Response> Update(MasterPaymentStatusModel param)
        {
            Response resp = new Response();

            MasterPaymentStatus Inbound = new MasterPaymentStatus();

            MasterPaymentStatusModel Outbound = new MasterPaymentStatusModel();

            try
            {
                Inbound = _mapper.Map(param);

                var objUpdate = await _service.GetById(Inbound.Id);

                if (objUpdate != null)
                {
                    bool duplicate = await _service.DuplicateKey(Inbound, MethodType.UPDATE);

                    if (duplicate == false)
                    {
                        Inbound.UpdatedDate = ServerTime;

                        Outbound = await _service.Update(Inbound);

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
                        resp.Output = Constants.InvalidDataDuplicate;
                    }
                }
                else
                {
                    resp.Status = Constants.StatusError;
                    resp.HttpCode = Constants.HttpCode400;
                    resp.HttpMessage = Constants.HttpCode204Message;
                    resp.Message = Constants.UpdateDataNotFound;
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

