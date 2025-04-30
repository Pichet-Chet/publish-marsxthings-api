using MarsXApps.Models;
using MarsXApps.Models.Constants;
using MarsXApps.Models.Customs;
using MarsXApps.Models.Customs.MethodType;
using MarsXApps.Models.Filter.MasterData;
using MarsXApps.Service;
using MarsXApps.Service.Models;

namespace MarsXApps.Module.Environment.MasterData
{
    public interface IMasterBranchRepositories
    {
        public Task<Response> GetAll(MasterBranchFilter param);

        public Task<Response> GetById(int id);

        public Task<Response> Create(MasterBranchModel param);

        public Task<Response> Update(MasterBranchModel param);
    }

    public class MasterBranchRepositories : IMasterBranchRepositories
    {
        DateTime ServerTime;

        private readonly Mapper _mapper;

        private readonly IMasterBranchService _service;

        public MasterBranchRepositories(IMasterBranchService service)
        {
            _mapper = new Mapper();

            _service = service;

            ServerTime = DateTime.Now.MarsX();
        }


        public async Task<Response> GetAll(MasterBranchFilter param)
        {
            Response resp = new Response();

            List<MasterBranchModel> Outbound = new List<MasterBranchModel>();

            try
            {
                Outbound = await _service.GetAll(param);

                if (Outbound.Count > 0)
                {
                    resp.Status = Constants.StatusSuccess;
                    resp.HttpCode = Constants.HttpCode200;
                    resp.HttpMessage = Constants.HttpCode200Message;

                    resp.PageNumber = param.PageNumber;
                    resp.PageSize = param.PageSize;
                    resp.EffectRow = Outbound.Count();

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

            MasterBranchModel Outbound = new MasterBranchModel();

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

        public async Task<Response> Create(MasterBranchModel param)
        {
            Response resp = new Response();

            MasterBranch Inbound = new MasterBranch();

            MasterBranchModel Outbound = new MasterBranchModel();

            try
            {
                Inbound = _mapper.Map(param);

                Inbound.CreatedDate = ServerTime;
                Inbound.UpdatedDate = ServerTime;

                bool duplicate = await _service.DuplicateKey(Inbound, MethodType.CREATE);

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

        public async Task<Response> Update(MasterBranchModel param)
        {
            Response resp = new Response();

            MasterBranch Inbound = new MasterBranch();

            MasterBranchModel Outbound = new MasterBranchModel();

            try
            {
                Inbound = _mapper.Map(param);

                var objUpdate = await _service.GetById(Inbound.Id);

                if (objUpdate != null)
                {
                    bool duplicate = await _service.DuplicateKey(Inbound, MethodType.UPDATE);

                    if (duplicate == false)
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
                        resp.Output = Constants.InvalidDataDuplicate;
                    }
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

