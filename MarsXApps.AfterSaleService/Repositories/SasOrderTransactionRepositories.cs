using MarsXApps.Models;
using MarsXApps.Models.Constants;
using MarsXApps.Models.Customs;
using MarsXApps.Models.Filter.ServiceAfterSale;
using MarsXApps.Service;
using MarsXApps.Service.Models;
using MarsXApps.Service.Validate;
using Microsoft.Extensions.Configuration;

namespace MarsXApps.Module.ServiceAfterSale.Repositories
{
    public interface ISasOrderTransactionRepositories
    {
        public Task<Response> GetAll(SasOrderTransactionFilter param);

        public Task<Response> GetById(int id);

        public Task<Response> Create(SasOrderTransactionModel param);

        public Task<Response> Update(SasOrderTransactionModel param);
    }

    public class SasOrderTransactionRepositories : ISasOrderTransactionRepositories
    {
        DateTime ServerTime;

        IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();

        private readonly Mapper _mapper;

        private readonly IMasterDataValidationService _validate;

        private readonly ISasOrderTransactionService _service;

        public SasOrderTransactionRepositories(ISasOrderTransactionService service, IMasterDataValidationService validate)
        {
            _mapper = new Mapper();

            _service = service;

            _validate = validate;

            ServerTime = DateTime.Now.MarsX();
        }

        public async Task<Response> GetAll(SasOrderTransactionFilter param)
        {
            Response resp = new Response();

            List<SasOrderTransactionModel> Outbound = new List<SasOrderTransactionModel>();

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

            SasOrderTransactionModel Outbound = new SasOrderTransactionModel();

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


        public async Task<Response> Create(SasOrderTransactionModel param)
        {
            Response resp = new Response();

            SasOrderTransaction Inbound = new SasOrderTransaction();

            SasOrderTransactionModel Outbound = new SasOrderTransactionModel();

            try
            {
                Inbound = _mapper.Map(param);

                #region Valiedation Zone

                Func<Task<Response>>[] validationFunctions = new Func<Task<Response>>[]
                {
                    async () => await _validate.SasOrder(Inbound.SasOrderId),
                    async () => await _validate.SasStatus(Inbound.SasStatusId),
                };

                foreach (var func in validationFunctions)
                {
                    var validate = await func();

                    if (validate != null)
                    {
                        if (validate.Status == false)
                        {
                            resp.Status = Constants.StatusError;
                            resp.HttpCode = Constants.HttpCode400;
                            resp.HttpMessage = Constants.HttpCode204Message;
                            resp.Message = validate.Message;

                            return resp;
                        }
                    }
                }

                #endregion

                Inbound.CreatedDate = ServerTime;
                Inbound.UpdatedDate = ServerTime;

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

        public async Task<Response> Update(SasOrderTransactionModel param)
        {
            Response resp = new Response();

            SasOrderTransaction Inbound = new SasOrderTransaction();

            SasOrderTransactionModel Outbound = new SasOrderTransactionModel();

            try
            {
                Inbound = _mapper.Map(param);

                #region Valiedation Zone

                Func<Task<Response>>[] validationFunctions = new Func<Task<Response>>[]
                {
                    async () => await _validate.SasOrder(Inbound.SasOrderId),
                    async () => await _validate.SasStatus(Inbound.SasStatusId),
                };

                foreach (var func in validationFunctions)
                {
                    var validate = await func();

                    if (validate != null)
                    {
                        if (validate.Status == false)
                        {
                            resp.Status = Constants.StatusError;
                            resp.HttpCode = Constants.HttpCode400;
                            resp.HttpMessage = Constants.HttpCode204Message;
                            resp.Message = validate.Message;

                            return resp;
                        }
                    }
                }

                #endregion

                var objUpdate = await _service.GetById(Inbound.Id);

                if (objUpdate != null)
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

