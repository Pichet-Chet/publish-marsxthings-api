using System;
using MarsXApps.Models;
using MarsXApps.Models.Constants;
using MarsXApps.Models.Customs;
using MarsXApps.Models.Customs.MethodType;
using MarsXApps.Models.Filter.MasterData;
using MarsXApps.Service;
using MarsXApps.Service.Models;
using Microsoft.Extensions.Configuration;

namespace MarsXApps.Module.Environment.MasterData
{
    public interface ISysCustomerAddressRepositories
    {
        public Task<Response> GetAll(SysCustomerAddressFilter param);

        public Task<Response> GetByCustomerId(Guid id, Pagination param);

        public Task<Response> GetById(int id);

        public Task<Response> Create(SysCustomersAddressModel param);

        public Task<Response> Update(SysCustomersAddressModel param);

        public Task<Response> Delete(Guid customerId, int id);
    }
    public class SysCustomerAddressRepositories : ISysCustomerAddressRepositories
    {
        DateTime ServerTime;

        IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();

        private readonly Mapper _mapper;

        private readonly ISysCustomerAddressService _service;


        public SysCustomerAddressRepositories(ISysCustomerAddressService service)
        {
            _mapper = new Mapper();

            _service = service;

            ServerTime = DateTime.Now.MarsX();
        }

        public async Task<Response> GetAll(SysCustomerAddressFilter param)
        {
            Response resp = new Response();

            List<SysCustomersAddressModel> Outbound = new List<SysCustomersAddressModel>();

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

        public async Task<Response> GetByCustomerId(Guid id, Pagination param)
        {
            Response resp = new Response();

            List<SysCustomersAddressModel> Outbound = new List<SysCustomersAddressModel>();

            try
            {
                Outbound = await _service.GetByCustomerId(id, param);

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

            SysCustomersAddressModel Outbound = new SysCustomersAddressModel();

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

        public async Task<Response> Create(SysCustomersAddressModel param)
        {
            Response resp = new Response();

            Service.Models.SysCustomerAddress Inbound = new Service.Models.SysCustomerAddress();

            SysCustomersAddressModel Outbound = new SysCustomersAddressModel();

            try
            {
                Inbound = _mapper.Map(param);

                Inbound.CreatedDate = ServerTime;
                Inbound.UpdatedDate = ServerTime;

                Outbound = await _service.Create(Inbound);

                if (param.IsMain == true)
                {
                    var RevokeIsMain = _service.RevokeIsMain(Outbound.Id, param.SysCustomerId);
                }

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

        public async Task<Response> Update(SysCustomersAddressModel param)
        {
            Response resp = new Response();

            Service.Models.SysCustomerAddress Inbound = new Service.Models.SysCustomerAddress();

            SysCustomersAddressModel Outbound = new SysCustomersAddressModel();

            try
            {
                Inbound = _mapper.Map(param);

                var objUpdate = await _service.GetById(Inbound.Id);

                if (objUpdate != null)
                {
                    Inbound.CreatedBy = objUpdate.CreatedBy;
                    Inbound.CreatedDate = objUpdate.CreatedDate == null ? DateTime.Now.MarsX() : objUpdate.CreatedDate;
                    Inbound.UpdatedDate = ServerTime;

                    Outbound = await _service.Update(Inbound);

                    if (param.IsMain == true)
                    {
                        var RevokeIsMain = _service.RevokeIsMain(Outbound.Id, param.SysCustomerId);
                    }

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

        public async Task<Response> Delete(Guid customerId, int id)
        {
            Response resp = new Response();
            try
            {
                var Outbound = await _service.Delete(customerId, id);

                if (Outbound == true)
                {
                    resp.Status = Constants.StatusSuccess;
                    resp.HttpCode = Constants.HttpCode200;
                    resp.HttpMessage = Constants.HttpCode200Message;
                    resp.Message = Constants.DeleteModelMachineSuccess;
                    resp.Output = Outbound;
                }
                else
                {
                    resp.Status = Constants.StatusError;
                    resp.HttpCode = Constants.HttpCode400;
                    resp.HttpMessage = Constants.HttpCode204Message;
                    resp.Message = Constants.DeleteModelMachineFailed;
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

