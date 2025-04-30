using MarsXApps.Models;
using MarsXApps.Models.Constants;
using MarsXApps.Models.Customs;
using MarsXApps.Models.Customs.Request.SysCustomer;
using MarsXApps.Models.Filter.Authorized;
using MarsXApps.Service;
using MarsXApps.Service.Validate;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace MarsXApps.Module.ServiceAfterSale.Repositories
{
    public interface ISysCustomerRepositories
    {
        public Task<Response> UpdateProfile(SysCustomerModel param);

        public Task<Response> GetAll(CustomerFilter param);

        public Task<Response> GetById(string id);

        public Task<Response> GetByUserName(string username);

        public Task<Response> GetByPhone(string phone);


        public Task<Response> DeleteById(string id);

        public Task<Response> UpdateImage(Guid id, IFormFile? Image);
    }

    public class SysCustomerRepositories : ISysCustomerRepositories
    {
        DateTime ServerTime;

        private readonly string[] ExtensionsImage = { ".jpg", ".jpeg", ".png" };

        private readonly Mapper _mapper;

        private readonly ISysCustomerService _service;

        public SysCustomerRepositories(ISysCustomerService service)
        {
            _mapper = new Mapper();

            _service = service;

            ServerTime = DateTime.Now.MarsX();
        }

        public async Task<Response> UpdateProfile(SysCustomerModel param)
        {
            Response resp = new Response();

            SysCustomerModel Outbound = new SysCustomerModel();

            try
            {
                if (!string.IsNullOrEmpty(param.Email))
                {
                    bool duplicate = await _service.verifyEmailDuplicate(param.Email, param.Id);

                    if (duplicate == true)
                    {
                        resp.Status = Constants.StatusError;
                        resp.HttpCode = Constants.HttpCode400;
                        resp.HttpMessage = Constants.HttpCode204Message;
                        resp.Message = Constants.InvalidEmailDuplicate;

                        return resp;
                    }
                }

                if (!string.IsNullOrEmpty(param.Username))
                {
                    bool duplicate = await _service.verifyUsernameDuplicate(param.Username, param.Id);

                    if (duplicate == true)
                    {
                        resp.Status = Constants.StatusError;
                        resp.HttpCode = Constants.HttpCode400;
                        resp.HttpMessage = Constants.HttpCode204Message;
                        resp.Message = Constants.InvalidUsernameDuplicate;

                        return resp;
                    }
                }

                Outbound = await _service.UpdateProfile(param);

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

        public async Task<Response> GetAll(CustomerFilter param)
        {
            Response resp = new Response();

            List<SysCustomerModel> Outbound = new List<SysCustomerModel>();

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

        public async Task<Response> GetById(string id)
        {
            Response resp = new Response();

            SysCustomerModel Outbound = new SysCustomerModel();

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

        public async Task<Response> GetByUserName(string username)
        {
            Response resp = new Response();

            SysCustomerModel Outbound = new SysCustomerModel();

            try
            {
                Outbound = await _service.GetByUserName(username);

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

        public async Task<Response> GetByPhone(string phone)
        {
            Response resp = new Response();

            SysCustomerModel Outbound = new SysCustomerModel();

            try
            {
                Outbound = await _service.GetByPhone(phone);

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

        public async Task<Response> DeleteById(string id)
        {
            Response resp = new Response();

            SysCustomerModel Outbound = new SysCustomerModel();

            try
            {
                Outbound = await _service.DeleteById(id);

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

        public async Task<Response> UpdateImage(Guid id, IFormFile? Image)
        {
            Response resp = new Response();
            SysCustomerModel Outbound = new SysCustomerModel();

            try
            {
                Outbound = await _service.UpdateImage(id, Image);

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


    }
}

