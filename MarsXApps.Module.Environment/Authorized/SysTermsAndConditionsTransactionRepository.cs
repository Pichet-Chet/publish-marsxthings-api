using MarsXApps.Models;
using MarsXApps.Models.Constants;
using MarsXApps.Models.Customs;
using MarsXApps.Models.Filter.Authorized;
using MarsXApps.Service;
using MarsXApps.Service.Models;

namespace MarsXApps.Module.Environment.Authorized
{
    public interface ISysTermsAndConditionsTransactionRepository
    {
        public Task<Response> GetAll(SysTermsAndConditionsTransactionFilter param);

        public Task<Response> GetById(int id);

        public Task<Response> GetLastEventByCustomer(Guid CustomerId);

        public Task<Response> GetLastEventByPhone(string Phone);

        public Task<Response> GetLastEventByUsername(string Username);

        public Task<Response> Create(SysTermsAndConditionsTransactionModel Param);
    }

    public class SysTermsAndConditionsTransactionRepository : ISysTermsAndConditionsTransactionRepository
    {
        DateTime ServerTime;

        private readonly Mapper _mapper;

        private readonly ISysTermsAndConditionService _sysTermsService;

        private readonly ISysTermsAndConditionsTransactionService _service;

        private readonly ISysCustomerService _sysCustomer;

        public SysTermsAndConditionsTransactionRepository(
            ISysTermsAndConditionService sysTermsService,
            ISysTermsAndConditionsTransactionService service,
            ISysCustomerService sysCustomer
            )
        {
            _mapper = new Mapper();

            _service = service;

            _sysTermsService = sysTermsService;

            _sysCustomer = sysCustomer;

            ServerTime = DateTime.Now.MarsX();
        }


        public async Task<Response> GetAll(SysTermsAndConditionsTransactionFilter param)
        {
            Response resp = new Response();

            List<SysTermsAndConditionsTransactionModel> Outbound = new List<SysTermsAndConditionsTransactionModel>();

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

            SysTermsAndConditionsTransactionModel Outbound = new SysTermsAndConditionsTransactionModel();

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

        public async Task<Response> GetLastEventByCustomer(Guid CustomerId)
        {
            Response resp = new Response();

            SysTermsAndConditionsTransactionModel Outbound = new SysTermsAndConditionsTransactionModel();

            try
            {
                Outbound = await _service.GetLastEventByCustomer(CustomerId);

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

        public async Task<Response> GetLastEventByPhone(string Phone)
        {
            Response resp = new Response();

            SysTermsAndConditionsTransactionModel Outbound = new SysTermsAndConditionsTransactionModel();

            SysCustomerModel sysCustomer = new SysCustomerModel();

            try
            {
                sysCustomer = await _sysCustomer.GetByPhone(Phone);

                if (sysCustomer != null)
                {
                    Outbound = await _service.GetLastEventByCustomer(sysCustomer.Id);

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

        public async Task<Response> GetLastEventByUsername(string Username)
        {
            Response resp = new Response();

            SysTermsAndConditionsTransactionModel Outbound = new SysTermsAndConditionsTransactionModel();

            SysCustomerModel sysCustomer = new SysCustomerModel();

            try
            {
                sysCustomer = await _sysCustomer.GetByUserName(Username);

                if (sysCustomer != null)
                {
                    Outbound = await _service.GetLastEventByCustomer(sysCustomer.Id);

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

        public async Task<Response> Create(SysTermsAndConditionsTransactionModel param)
        {
            Response resp = new Response();

            SysTermsAndConditionsTransaction Inbound = new SysTermsAndConditionsTransaction();

            SysTermsAndConditionsTransactionModel Outbound = new SysTermsAndConditionsTransactionModel();

            SysTermsAndConditionModel sysTermsAndCondition = new SysTermsAndConditionModel();

            SysCustomerModel sysCustomer = new SysCustomerModel();

            try
            {
                sysCustomer = await _sysCustomer.GetById(param.SysCustomersId.ToString());

                if (sysCustomer != null)
                {
                    sysTermsAndCondition = await _sysTermsService.GetLastVersionActive();

                    param.ConsiderationDate = ServerTime;
                    param.SysTermsAndConditionsId = sysTermsAndCondition.Id;

                    Inbound = _mapper.Map(param);

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
                    resp.Message = Constants.CustomerNotFound;
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

