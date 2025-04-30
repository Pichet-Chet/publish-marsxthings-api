using System;
using System.Net;
using MarsXApps.Models;
using MarsXApps.Models.Constants;
using MarsXApps.Models.Customs;
using MarsXApps.Models.Customs.CAEC;
using MarsXApps.Models.Filter.MasterData;
using MarsXApps.Service;
using MarsXApps.Service.Models;
using Microsoft.Extensions.Configuration;
using static MarsXApps.Service.SysCustomerMachineService;

namespace MarsXApps.Module.Environment.MasterData
{

    public interface ISysCustomerMachineRepositories
    {
        public Task<Response> GetAll(SysCustomerMachineFilter param);

        public Task<Response> VerifyMachineCaec(SysCustomerMachineModelVerify param);

        public Task<Response> GetAllMachineModelCaec();

        public Task<Response> GetByCustomerId(Guid id, Pagination param);

        public Task<Response> GetById(int id);

        public Task<Response> Create(SysCustomerMachineModelCreate param);

        public Task<Response> Update(SysCustomerMachineModel param);

        public Task<Response> Delete(Guid customerId, int id);
    }

    public class SysCustomerMachineRepositories : ISysCustomerMachineRepositories
    {
        DateTime ServerTime;

        IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();

        private readonly Mapper _mapper;

        private readonly ISysCustomerMachineService _service;

        private readonly ISysCustomerService _customer;



        public SysCustomerMachineRepositories(ISysCustomerMachineService service)
        {
            _mapper = new Mapper();

            _service = service;

            _customer = new SysCustomerService();

            ServerTime = DateTime.Now.MarsX();
        }

        public async Task<Response> GetAll(SysCustomerMachineFilter param)
        {
            Response resp = new Response();

            List<SysCustomerMachineModel> Outbound = new List<SysCustomerMachineModel>();

            try
            {
                Outbound = await _service.GetAll(param);

                if (Outbound.Count > 0)
                {
                    var random = new Random();

                    foreach (var item in Outbound)
                    {
                        // Latitude between 5.600 and 20.500
                        item.Latitude = (random.NextDouble() * (20.500 - 5.600) + 5.600).ToString("F6");

                        // Longitude between 97.400 and 105.800
                        item.Longitude = (random.NextDouble() * (105.800 - 97.400) + 97.400).ToString("F6");
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

        public async Task<Response> VerifyMachineCaec(SysCustomerMachineModelVerify param)
        {
            Response resp = new Response();

            MachinesMasterModel Outbound = new MachinesMasterModel();

            MachinesMasterModel caecResult = new MachinesMasterModel();

            GetMachinesMasterModel caec = new GetMachinesMasterModel();

            try
            {
                caec.model = param.Model;

                caec.textSearch = string.IsNullOrEmpty(param.SerialNo) ? string.Empty : param.SerialNo;

                caec.textSearch = string.IsNullOrEmpty(param.Chassic) ? string.Empty : param.Chassic;

                caecResult = await _service.VerifyMachineCaec(caec);

                if (caecResult != null)
                {
                    Outbound.mcModel = caecResult.mcModel;
                    Outbound.mcSerialNo = caecResult.mcSerialNo;
                    Outbound.mcChassis = caecResult.mcChassis;

                    resp.Status = Constants.StatusSuccess;
                    resp.HttpCode = Constants.HttpCode200;
                    resp.HttpMessage = Constants.HttpCode200Message;
                    resp.Output = caecResult;
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

        public async Task<Response> GetAllMachineModelCaec()
        {
            Response resp = new Response();

            List<SysCustomerMachineModelAll> Outbound = new List<SysCustomerMachineModelAll>();

            try
            {

                Outbound = await _service.GetMachineModelCaec();

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

            List<SysCustomerMachineModel> Outbound = new List<SysCustomerMachineModel>();

            try
            {
                Outbound = await _service.GetByCustomerId(id, param);

                if (Outbound.Count > 0)
                {
                    var random = new Random();

                    foreach (var item in Outbound)
                    {
                        // Latitude between 13.000 and 15.800
                        item.Latitude = (random.NextDouble() * (15.800 - 13.000) + 13.000).ToString("F6");

                        // Longitude between 99.000 and 101.200
                        item.Longitude = (random.NextDouble() * (101.200 - 99.000) + 99.000).ToString("F6");
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

            SysCustomerMachineModel Outbound = new SysCustomerMachineModel();

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

        public async Task<Response> Create(SysCustomerMachineModelCreate param)
        {
            Response resp = new Response();

            SysCustomerMachine Inbound = new SysCustomerMachine();

            SysCustomerMachineModel Outbound = new SysCustomerMachineModel();

            try
            {
                MachinesMasterModel caecResult = new MachinesMasterModel();

                GetMachinesMasterModel caec = new GetMachinesMasterModel();

                caec.model = param.MachineModel;

                caec.searchMachineIdentity = !string.IsNullOrEmpty(param.MachineSerial) ? "serialno" : string.Empty;

                caec.searchMachineIdentity = string.IsNullOrEmpty(caec.searchMachineIdentity) ? "chassis" : "serialno";

                caec.textSearch = string.IsNullOrEmpty(param.MachineSerial) ? param.MachineChassic : param.MachineSerial;

                caecResult = await _service.VerifyMachineCaec(caec);

                if (caecResult != null)
                {
                    Helper.TransferData_ClassA_to_ClassB<SysCustomerMachineModelCreate, SysCustomerMachineModel>(param, ref Outbound);

                    Inbound = _mapper.Map(Outbound);

                    Inbound.UpdatedBy = param.CreatedBy;
                    Inbound.CreatedDate = ServerTime;
                    Inbound.UpdatedDate = ServerTime;
                    Inbound.MachineSerial = Inbound.MachineSerial == null ? null : Inbound.MachineSerial.ToUpper();
                    Inbound.MachineChassic = Inbound.MachineChassic == null ? null : Inbound.MachineChassic.ToUpper();
                    Inbound.AliasName = string.IsNullOrEmpty(param.AliasName) ? Inbound.MachineModel.ToUpper() : Inbound.AliasName.ToUpper();


                    var customer = await _customer.GetById(param.SysCustomerId.ToString());

                    if (customer != null)
                    {
                        var checkAlreadyExists = await _service.VerifyMachineCreate(Inbound);
                        if (checkAlreadyExists.Status)
                        {
                            Outbound = await _service.Create(Inbound);

                            resp.Status = Constants.StatusSuccess;
                            resp.HttpCode = Constants.HttpCode200;
                            resp.Message = Constants.HttpCode200Message;
                            resp.Output = Outbound;
                        }
                        else
                        {
                            resp.Status = Constants.StatusError;
                            resp.HttpCode = Constants.HttpCode400;
                            resp.Message = checkAlreadyExists.Message;
                            resp.Output = null;
                        }
                       
                    }
                    else
                    {
                        resp.Status = Constants.StatusError;
                        resp.HttpCode = Constants.HttpCode400;
                        resp.Message = Constants.CustomerNotFound;
                        resp.Output = null;
                    }
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

        public async Task<Response> Update(SysCustomerMachineModel param)
        {
            Response resp = new Response();

            Service.Models.SysCustomerMachine Inbound = new Service.Models.SysCustomerMachine();

            SysCustomerMachineModel Outbound = new SysCustomerMachineModel();

            try
            {
                Inbound = _mapper.Map(param);

                var objUpdate = await _service.GetById(Inbound.Id);

                if (objUpdate != null)
                {
                    Inbound.CreatedBy = objUpdate.CreatedBy;
                    Inbound.CreatedDate = objUpdate.CreatedDate == null ? DateTime.Now.MarsX() : objUpdate.CreatedDate;
                    Inbound.UpdatedDate = ServerTime;
                    Inbound.AliasName = string.IsNullOrEmpty(param.AliasName) ? Inbound.MachineModel.ToUpper() : Inbound.AliasName.ToUpper();


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

