using MarsXApps.Models;
using MarsXApps.Models.Constants;
using MarsXApps.Models.Customs;
using MarsXApps.Models.Customs.MethodType;
using MarsXApps.Models.Filter.Posting;
using MarsXApps.Models.Filter.ServiceAfterSale;
using MarsXApps.Service;
using MarsXApps.Service.Models;
using MarsXApps.Service.Validate;
using Microsoft.Extensions.Configuration;

namespace MarsXApps.Module.ServiceAfterSale.Repositories
{
    public interface IPostingTransactionRepositories
    {
        public Task<Response> GetAll(PostingTransactionFilter param);

        public Task<Response> GetById(int id);

        public Task<Response> GetByCustomerId(Guid customerId);

        public Task<Response> GetDetailByCustomerId(int id, Guid customerId);

        public Task<Response> Create(PostingTransactionModel param);

        public Task<Response> Update(PostingTransactionModel param);

        public Task<Response> UpdateIsActive(int id, bool isActive);

    }

    public class PostingTransactionRepositories : IPostingTransactionRepositories
    {
        DateTime ServerTime;

        private readonly Mapper _mapper;

        private readonly IPostingTransactionService _service;

        private readonly IPostingLimitService _serviceLimit;

        private readonly IMasterConfigurationService _servicemasterConfiguration;



        public PostingTransactionRepositories(IPostingTransactionService service, IPostingLimitService serviceLimit, IMasterConfigurationService masterConfiguration)
        {
            _mapper = new Mapper();

            _service = service;

            _serviceLimit = serviceLimit;

            _servicemasterConfiguration = masterConfiguration;

            ServerTime = DateTime.Now.MarsX();
        }


        public async Task<Response> GetAll(PostingTransactionFilter param)
        {
            Response resp = new Response();

            List<PostingTransactionModel> Outbound = new List<PostingTransactionModel>();

            try
            {
                if (param.SysCustomersId != null)
                {
                    var GetCurrentLimit = await _serviceLimit.GetByCustomerId((Guid)param.SysCustomersId);

                    if (GetCurrentLimit == null)
                    {
                        PostingLimit postingLimit = new PostingLimit();

                        postingLimit.SysCustomersId = (Guid)param.SysCustomersId;

                        var GetSetting = await _servicemasterConfiguration.GetByKey("POSTING", "LIMIT");

                        postingLimit.Limit = Convert.ToInt32(GetSetting);

                        _serviceLimit.Create(postingLimit);
                    }
                }


                Outbound = await _service.GetAll(param);

                if (Outbound.Count > 0)
                {
                    resp.Status = Constants.StatusSuccess;
                    resp.HttpCode = Constants.HttpCode200;
                    resp.HttpMessage = Constants.HttpCode200Message;
                    resp.Output = Outbound;
                    resp.PageNumber = param.PageNumber;
                    resp.PageSize = param.PageSize;
                    resp.EffectRow = Outbound.Count();
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

            PostingTransactionModel Outbound = new PostingTransactionModel();

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

        public async Task<Response> GetByCustomerId(Guid customerId)
        {
            Response resp = new Response();

            List<PostingTransactionModel> Outbound = new List<PostingTransactionModel>();

            try
            {
                var GetCurrentLimit = await _serviceLimit.GetByCustomerId(customerId);

                if (GetCurrentLimit == null)
                {
                    PostingLimit postingLimit = new PostingLimit();

                    var GetSetting = await _servicemasterConfiguration.GetByKey("POSTING", "LIMIT");

                    postingLimit.Limit = Convert.ToInt32(GetSetting);

                    _serviceLimit.Create(postingLimit);
                }

                Outbound = await _service.GetByCustomerId(customerId);

                if (Outbound != null)
                {
                    resp.Status = Constants.StatusSuccess;
                    resp.HttpCode = Constants.HttpCode200;
                    resp.HttpMessage = Constants.HttpCode200Message;
                    resp.Output = Outbound;
                    resp.EffectRow = Outbound.Count();
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

        public async Task<Response> GetDetailByCustomerId(int id, Guid customerId)
        {
            Response resp = new Response();

            PostingTransactionModel Outbound = new PostingTransactionModel();

            try
            {
                var GetCurrentLimit = await _serviceLimit.GetByCustomerId(customerId);

                if (GetCurrentLimit == null)
                {
                    PostingLimit postingLimit = new PostingLimit();

                    var GetSetting = await _servicemasterConfiguration.GetByKey("POSTING", "LIMIT");

                    postingLimit.Limit = Convert.ToInt32(GetSetting);

                    _serviceLimit.Create(postingLimit);
                }

                Outbound = await _service.GetDetailByCustomerId(id, customerId);

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

        public async Task<Response> Create(PostingTransactionModel param)
        {
            Response resp = new Response();

            PostingTransaction Inbound = new PostingTransaction();

            PostingTransactionModel Outbound = new PostingTransactionModel();

            try
            {
                Inbound = _mapper.Map(param);

                Inbound.CreatedDate = ServerTime;

                Inbound.UpdatedDate = ServerTime;

                var GetLimitPosting = await _serviceLimit.GetByCustomerId(param.SysCustomersId);

                var GetCurrentPostingCount = await _service.GetCountByCustomerId(param.SysCustomersId);

                if (GetCurrentPostingCount <= GetLimitPosting.Limit)
                {
                    Outbound = await _service.Create(Inbound);

                    Outbound = await _service.UpdateImage((int)Outbound.Id, param.FeatureImageUpload);

                    resp.Status = Constants.StatusSuccess;
                    resp.HttpCode = Constants.HttpCode200;
                    resp.HttpMessage = Constants.HttpCode200Message;
                    resp.Output = Outbound;
                }
                else
                {
                    resp.Status = Constants.StatusError;
                    resp.HttpCode = Constants.HttpCode400;
                    resp.HttpMessage = Constants.HttpCode200Message;
                    resp.Message = Constants.PostingLimitTooMany;
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

        public async Task<Response> Update(PostingTransactionModel param)
        {
            Response resp = new Response();

            PostingTransaction Inbound = new PostingTransaction();

            PostingTransactionModel Outbound = new PostingTransactionModel();

            try
            {
                Inbound = _mapper.Map(param);

                var objUpdate = await _service.GetById(Inbound.Id);

                if (objUpdate != null)
                {
                    Inbound.UpdatedDate = ServerTime;

                    if (param.FeatureImageUpload == null)
                    {
                        var oldImage = await _service.GetByIdWithOutImage(Inbound.Id);

                        Inbound.FeatureImage = oldImage.FeatureImage;
                    }

                    Outbound = await _service.Update(Inbound);

                    if (param.FeatureImageUpload != null)
                    {
                        Outbound = await _service.UpdateImage((int)Outbound.Id, param.FeatureImageUpload);
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

        public async Task<Response> UpdateIsActive(int id, bool isActive)
        {
            Response resp = new Response();

            PostingTransaction Inbound = new PostingTransaction();

            PostingTransactionModel Outbound = new PostingTransactionModel();

            try
            {
                var objUpdate = await _service.GetByIdWithOutImage(id);

                Inbound = _mapper.Map(objUpdate);

                if (objUpdate != null)
                {
                    Inbound.IsActive = isActive;
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

