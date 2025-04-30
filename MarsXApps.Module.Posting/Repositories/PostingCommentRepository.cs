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
    public interface IPostingCommentRepositories
    {
        public Task<Response> GetAll(PostingCommentFilter param);

        public Task<Response> GetById(int id);

        public Task<Response> GetPostingTransaction(int id);

        public Task<Response> Create(PostingCommentModel param);

        public Task<Response> Update(PostingCommentModel param);
    }

    public class PostingCommentRepositories : IPostingCommentRepositories
    {
        DateTime ServerTime;

        private readonly Mapper _mapper;

        private readonly Helper _helper;

        private readonly IPostingCommentService _service;

        private readonly ISysCustomerService _sysCustomerService;

        public PostingCommentRepositories(IPostingCommentService service, ISysCustomerService sysCustomerService)
        {
            _mapper = new Mapper();

            _service = service;

            _sysCustomerService = sysCustomerService;

            ServerTime = DateTime.Now.MarsX();
        }


        public async Task<Response> GetAll(PostingCommentFilter param)
        {
            Response resp = new Response();

            List<PostingCommentModel> Outbound = new List<PostingCommentModel>();

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

            PostingCommentModel Outbound = new PostingCommentModel();

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


        public async Task<Response> GetPostingTransaction(int id)
        {
            Response resp = new Response();

            List<PostingCommentModel> Outbound = new List<PostingCommentModel>();

            try
            {
                Outbound = await _service.GetPostingTransaction(id);

                if (Outbound != null && Outbound.Count > 0)
                {
                    foreach (var item in Outbound)
                    {
                        string sysCustomer = Convert.ToString(item.SysCustomersId);

                        var GetCustomerInfo = await _sysCustomerService.GetById(sysCustomer);

                        if (item.CreatedDate.HasValue)
                        {
                            TimeSpan timeSpan = DateTime.Now - item.CreatedDate.Value;

                            if (timeSpan.TotalHours < 24)
                            {
                                item.CommentAgoTh = await Helper.GetTimeAgoInThai(item.CreatedDate.Value);
                                item.CommentAgoEn = await Helper.GetTimeAgoInEnglish(item.CreatedDate.Value);
                            }
                            else
                            {
                                item.CommentAgoTh = await Helper.ConvertToDateTh(item.CreatedDate.Value);
                                item.CommentAgoEn = await Helper.ConvertToDateEn(item.CreatedDate.Value);
                            }
                        }
                        else
                        {
                            item.CommentAgoTh = "ไม่พบวันที่";
                            item.CommentAgoEn = "Date not available";
                        }


                        item.FirstName = GetCustomerInfo.FirstName;
                        item.ProfileImage = GetCustomerInfo.Image;
                    }

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

        public async Task<Response> Create(PostingCommentModel param)
        {
            Response resp = new Response();

            PostingComment Inbound = new PostingComment();

            PostingCommentModel Outbound = new PostingCommentModel();

            try
            {
                Inbound = _mapper.Map(param);

                Inbound.CreatedDate = ServerTime;

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

        public async Task<Response> Update(PostingCommentModel param)
        {
            Response resp = new Response();

            PostingComment Inbound = new PostingComment();

            PostingCommentModel Outbound = new PostingCommentModel();

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


    }
}

