using MarsXApps.Models;
using MarsXApps.Models.Constants;
using MarsXApps.Models.Customs;
using MarsXApps.Models.Customs.MethodType;
using MarsXApps.Models.Filter.Promotion;
using MarsXApps.Service;
using MarsXApps.Service.Models;
namespace MarsXApps.Module.Promotion.Repositories
{
    public interface IPromotionRepository
    {
        Task<Response> GetAll(PromotionFilter param);
        Task<Response> GetById(int id);
        Task<Response> Create(PromotionModel param);
        Task<Response> Update(PromotionModel param);
        Task<Response> GetByCode(string Code);
        Task<Response> CreatePromotionWithWidget(PromotionModel param);
        Task<Response> UpdatePromotionWithWidget(PromotionModel param);
    }
    public class PromotionRepository : IPromotionRepository
    {
        DateTime ServerTime;

        private readonly Mapper _mapper;

        private readonly IPromotionService _service;

        public PromotionRepository(IPromotionService service)
        {
            _mapper = new Mapper();
            _service = service;

        }

        public async Task<Response> GetAll(PromotionFilter param)
        {
            Response resp = new Response();
            try
            {
              
                List<PromotionModel> Outbound = await _service.GetAll(param);
               

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
            try
            {
                PromotionModel Outbound = await _service.GetById(id);
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

        public async Task<Response> Create(PromotionModel param)
        {
            Response resp = new();
            MarsXApps.Service.Models.Promotion Inbound = new();
            PromotionModel Outbound = new();
            try
            {
                Inbound = _mapper.Map(param);
                Inbound.CreateDate = ServerTime;
                Inbound.UpdateDate = ServerTime;

                bool duplicate = await _service.DuplicateKey(Inbound);

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

        public async Task<Response> Update(PromotionModel param)
        {
            Response resp = new Response();
            MarsXApps.Service.Models.Promotion Inbound = new();
            PromotionModel Outbound = new();

            try
            {
                Inbound = _mapper.Map(param);

                if (Inbound != null)
                {
                    Inbound.UpdateDate = ServerTime;

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

        public async Task<Response> GetByCode(string Code)
        {
            Response resp = new Response();
            try
            {
                PromotionModel Outbound = await _service.GetByCode(Code);
                Outbound.CmsWidgetDisplayPath = Outbound.CmsWidgetModel.DisplayPath;
                Outbound.CmsWidgetModel = null;
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

        public async Task<Response> CreatePromotionWithWidget(PromotionModel param)
        {
            Response resp = new();
            MarsXApps.Service.Models.Promotion Inbound = new();
            PromotionModel Outbound = new();
            try
            {
                Inbound = _mapper.Map(param);
                Inbound.CmsWidgetNameNavigation = _mapper.Map(param.CmsWidgetModel);
                Inbound.CreateDate = ServerTime;
                Inbound.UpdateDate = ServerTime;

                bool duplicate = await _service.DuplicateKey(Inbound);

                if (duplicate == false)
                {
                    Outbound = await _service.CreatePromotionWithWidget(Inbound);

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

        public async Task<Response> UpdatePromotionWithWidget(PromotionModel param)
        {
            Response resp = new();
            MarsXApps.Service.Models.Promotion Inbound = new();
            PromotionModel Outbound = new();
            try
            {
                Inbound = _mapper.Map(param);
                Inbound.CmsWidgetNameNavigation = _mapper.Map(param.CmsWidgetModel);
                bool duplicate = await _service.DuplicateKey(Inbound);

                if (duplicate == false)
                {
                    Outbound = await _service.UpdatePromotionWithWidget(Inbound);

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
    }
}
