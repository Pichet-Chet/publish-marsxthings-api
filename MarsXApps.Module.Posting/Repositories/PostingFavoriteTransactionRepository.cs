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
    public interface IPostingFavoriteTransactionRepositories
    {
        public Task<Response> GetAll(PostingFavoriteTransactionFilter param);

        public Task<Response> GetById(int id);

        public Task<Response> Create(PostingFavoriteTransactionModel param);

        public Task<Response> Update(PostingFavoriteTransactionModel param);

        public Task<Response> Delete(int delete);

        public Task<Response> DeleteBySysCustomer(int PostingTransactionId, Guid SysCustomerId);


    }

    public class PostingFavoriteTransactionRepositories : IPostingFavoriteTransactionRepositories
    {
        DateTime ServerTime;

        private readonly Mapper _mapper;

        private readonly IPostingFavoriteTransactionService _service;

        private readonly IPostingTransactionService _servicePostingTransaction;


        public PostingFavoriteTransactionRepositories(IPostingFavoriteTransactionService service, IPostingTransactionService servicePostingTransaction)
        {
            _mapper = new Mapper();

            _service = service;

            _servicePostingTransaction = servicePostingTransaction;

            ServerTime = DateTime.Now.MarsX();
        }


        public async Task<Response> GetAll(PostingFavoriteTransactionFilter param)
        {
            Response resp = new Response();

            List<PostingFavoriteTransactionModel> Outbound = new List<PostingFavoriteTransactionModel>();

            try
            {
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

            PostingFavoriteTransactionModel Outbound = new PostingFavoriteTransactionModel();

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

        public async Task<Response> Create(PostingFavoriteTransactionModel param)
        {
            Response resp = new Response();

            PostingFavoriteTransaction Inbound = new PostingFavoriteTransaction();

            PostingFavoriteTransactionModel Outbound = new PostingFavoriteTransactionModel();

            try
            {
                var GetPostingType = await _servicePostingTransaction.GetPostingType(param.PostingTransactionId);


                Inbound = _mapper.Map(param);

                Inbound.CreatedDate = ServerTime;
                Inbound.PostingTypeId = GetPostingType;

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

        public async Task<Response> Update(PostingFavoriteTransactionModel param)
        {
            Response resp = new Response();

            PostingFavoriteTransaction Inbound = new PostingFavoriteTransaction();

            PostingFavoriteTransactionModel Outbound = new PostingFavoriteTransactionModel();

            try
            {
                Inbound = _mapper.Map(param);

                var objUpdate = await _service.GetById((int)Inbound.Id);

                if (objUpdate != null)
                {
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

        public async Task<Response> Delete(int id)
        {
            Response resp = new Response();

            PostingFavoriteTransactionModel Outbound = new PostingFavoriteTransactionModel();

            try
            {
                Outbound = await _service.Delete(id);

                if (Outbound != null)
                {
                    resp.Status = Constants.StatusSuccess;
                    resp.HttpCode = Constants.HttpCode200;
                    resp.HttpMessage = Constants.HttpCode200Message;
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

        public async Task<Response> DeleteBySysCustomer(int PostingTransactionId, Guid SysCustomerId)
        {
            Response resp = new Response();

            PostingFavoriteTransactionModel Outbound = new PostingFavoriteTransactionModel();

            try
            {
                Outbound = await _service.DeleteBySysCustomer(PostingTransactionId, SysCustomerId);

                if (Outbound != null)
                {
                    resp.Status = Constants.StatusSuccess;
                    resp.HttpCode = Constants.HttpCode200;
                    resp.HttpMessage = Constants.HttpCode200Message;
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

