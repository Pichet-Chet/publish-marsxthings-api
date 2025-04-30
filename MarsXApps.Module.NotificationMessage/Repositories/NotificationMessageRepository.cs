using MarsXApps.Models;
using MarsXApps.Models.Constants;
using MarsXApps.Models.Customs;
using MarsXApps.Models.Customs.MethodType;
using MarsXApps.Models.Filter.NotificationMessageFilter;
using MarsXApps.Models.Filter.Promotion;
using MarsXApps.Service;
using MarsXApps.Service.Models;
namespace MarsXApps.Module.NotificationMessage.Repositories
{
    public interface INotificationMessageRepository
    {
         Task<Response> GetAll(NotificationMessageFilter param);
         //Task<Response> GetById(int id);
         //Task<Response> GetByCustomerId(string customerId, bool? isActive, bool? isRead, bool? isBroadcast);
         //Task<Response> Create(NotificationMessageModel model);
         //Task<Response> Update(NotificationMessageModel model);
         //Task<Response> Delete(int id);
       
    }
    public class NotificationMessageRepository : INotificationMessageRepository
    {
        DateTime ServerTime;

        private readonly Mapper _mapper;

        private readonly INotificationMessageService _service;

        public NotificationMessageRepository(INotificationMessageService service)
        {
            _mapper = new Mapper();
            _service = service;

        }

        public async Task<Response> GetAll(NotificationMessageFilter param)
        {
            Response resp = new Response();
            try
            {

                List<NotificationMessageModel> Outbound = await _service.GetAll(param);

                if (Outbound != null)
                {
                    resp.Status = Constants.StatusSuccess;
                    resp.HttpCode = Constants.HttpCode200;
                    resp.HttpMessage = Constants.HttpCode200Message;
                    resp.Output = Outbound;

                    resp.PageNumber = param.PageNumber;
                    resp.PageSize = param.PageSize;
                    resp.EffectRow = await _service.CountAsync();
                }
                else
                {
                    resp.Status = Constants.StatusError;
                    resp.HttpCode = Constants.HttpCode204;
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
            try
            {
                NotificationMessageModel Outbound = await _service.GetById(id);
             
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
                    resp.HttpCode = Constants.HttpCode204;
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

        public async Task<Response> Create(NotificationMessageModel model)
        {
            Response resp = new();
            NotificationMessageModel Outbound = new();
            try
            {
                model.CreatedDate = ServerTime;

                bool duplicate = await _service.DuplicateKey(model);

                if (duplicate == false)
                {
                    Outbound = await _service.Create(_mapper.Map(model));

                    resp.Status = Constants.StatusSuccess;
                    resp.HttpCode = Constants.HttpCode200;
                    resp.HttpMessage = Constants.HttpCode200Message;
                    resp.Output = Outbound;
                }
                else
                {
                    resp.Status = Constants.StatusError;
                    resp.HttpCode = Constants.HttpCode204;
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

        public async Task<Response> Update(NotificationMessageModel param)
        {
            Response resp = new Response();
            //NotificationMessageModel Inbound = new();
            NotificationMessageModel Outbound = new();

            try
            {
               //Inbound = _mapper.Map(param);

                if (param != null)
                {
                    //param.u = ServerTime;

                    Outbound = await _service.Update(_mapper.Map(param));

                    resp.Status = Constants.StatusSuccess;
                    resp.HttpCode = Constants.HttpCode200;
                    resp.HttpMessage = Constants.HttpCode200Message;
                    resp.Output = Outbound;
                }
                else
                {
                    resp.Status = Constants.StatusError;
                    resp.HttpCode = Constants.HttpCode204;
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
