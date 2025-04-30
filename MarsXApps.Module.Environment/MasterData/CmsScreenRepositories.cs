using MarsXApps.Models;
using MarsXApps.Models.Constants;
using MarsXApps.Models.Customs;
using MarsXApps.Models.Customs.MethodType;
using MarsXApps.Models.Filter.MasterData;
using MarsXApps.Service;
using MarsXApps.Service.Models;
using MarsXApps.Service.Validate;
using Microsoft.Extensions.Configuration;


namespace MarsXApps.Module.Environment.MasterData
{
    public interface ICmsScreenRepositories
    {
        public Task<Response> GetAll(CmsScreenFilter param);

        public Task<Response> GetById(int id);

        public Task<Response> GetByName(string name);


        public Task<Response> Create(CmsScreenModel param);

        public Task<Response> Update(CmsScreenModel param);
    }

    public class CmsScreenRepositories : ICmsScreenRepositories
    {
        DateTime ServerTime;

        IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();

        private readonly Mapper _mapper;

        private readonly ICmsScreenService _cmsScreenService;

        private readonly ICmsContentService _cmsContentService;

        private readonly ICmsWidgetService _cmsWidgetService;


        public CmsScreenRepositories(
            ICmsScreenService cmsScreenService,
            ICmsContentService cmsContentService,
            ICmsWidgetService cmsWidgetService
            )
        {
            _mapper = new Mapper();

            _cmsScreenService = cmsScreenService;

            _cmsContentService = cmsContentService;

            _cmsWidgetService = cmsWidgetService;

            ServerTime = DateTime.Now.MarsX();

        }

        public async Task<Response> GetAll(CmsScreenFilter param)
        {
            Response resp = new();
            try
            {
                CmsContentFilter cmsContentFilter = new();
                CmsWidgetFilter cmsWidgetFilter = new();
                List<CmsScreenModel> Outbound = await _cmsScreenService.GetAll(param);
                List<CmsContentModel> ContentService = await _cmsContentService.GetAll(cmsContentFilter);
                List<CmsWidgetModel> WidgetService = await _cmsWidgetService.GetAll(cmsWidgetFilter);

                foreach (var screen in Outbound)
                {
                    screen.cmsContentModels = ContentService.Where(c => c.CmsScreenId == screen.Id).ToList();
                    foreach (var content in screen.cmsContentModels)
                    {
                        content.cmsWidgets = WidgetService.Where(w => w.CmsContentId == content.Id).ToList();
                    }
                }

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
            Response resp = new();
            try
            {
                CmsScreenModel outbound = await _cmsScreenService.GetById(id);
                CmsContentFilter cmsContentFilter = new();
                CmsWidgetFilter cmsWidgetFilter = new();
                if (outbound != null)
                {
                    var contents = await _cmsContentService.GetAll(cmsContentFilter);
                    var widgets = await _cmsWidgetService.GetAll(cmsWidgetFilter);
                    outbound.cmsContentModels = contents.Where(c => c.CmsScreenId == outbound.Id).ToList();
                    foreach (var content in outbound.cmsContentModels)
                    {
                        content.cmsWidgets = widgets.Where(w => w.CmsContentId == content.Id).ToList();
                    }

                    resp.Status = Constants.StatusSuccess;
                    resp.HttpCode = Constants.HttpCode200;
                    resp.HttpMessage = Constants.HttpCode200Message;
                    resp.Output = outbound;
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
                resp.InnerException = ex.InnerException?.Message ?? ex.Message;
            }

            return resp;
        }

        public async Task<Response> GetByName(string name)
        {
            Response resp = new();
            try
            {
                CmsScreenModel outbound = await _cmsScreenService.GetByName(name);
                CmsContentFilter cmsContentFilter = new();
                CmsWidgetFilter cmsWidgetFilter = new();
                if (outbound != null)
                {
                    var contents = await _cmsContentService.GetAll(cmsContentFilter);
                    var widgets = await _cmsWidgetService.GetAll(cmsWidgetFilter);
                    outbound.cmsContentModels = contents.Where(c => c.CmsScreenId == outbound.Id).ToList();
                    foreach (var content in outbound.cmsContentModels)
                    {
                        content.cmsWidgets = widgets.Where(w => w.CmsContentId == content.Id).ToList();
                    }

                    resp.Status = Constants.StatusSuccess;
                    resp.HttpCode = Constants.HttpCode200;
                    resp.HttpMessage = Constants.HttpCode200Message;
                    resp.Output = outbound;
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
                resp.InnerException = ex.InnerException?.Message ?? ex.Message;
            }

            return resp;
        }

        public async Task<Response> Create(CmsScreenModel param)
        {
            Response resp = new Response();

            CmsScreen Inbound = new CmsScreen();

            CmsScreenModel Outbound = new CmsScreenModel();

            try
            {
                Inbound = _mapper.Map(param);

                Inbound.CreatedDate = ServerTime;
                Inbound.UpdatedDate = ServerTime;

                bool duplicate = await _cmsScreenService.DuplicateKey(Inbound, MethodType.CREATE);

                if (duplicate == false)
                {
                    Outbound = await _cmsScreenService.Create(Inbound);

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

        public async Task<Response> Update(CmsScreenModel param)
        {
            Response resp = new Response();

            CmsScreen Inbound = new CmsScreen();

            CmsScreenModel Outbound = new CmsScreenModel();

            try
            {
                Inbound = _mapper.Map(param);

                var objUpdate = await _cmsScreenService.GetById(Inbound.Id);

                if (objUpdate != null)
                {
                    bool duplicate = await _cmsScreenService.DuplicateKey(Inbound, MethodType.UPDATE);

                    if (duplicate == false)
                    {
                        Inbound.UpdatedDate = ServerTime;

                        Outbound = await _cmsScreenService.Update(Inbound);

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

