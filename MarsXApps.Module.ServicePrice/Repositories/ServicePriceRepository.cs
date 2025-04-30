using MarsXApps.Models;
using MarsXApps.Models.Constants;
using MarsXApps.Models.Customs;
using MarsXApps.Models.Customs.CAEC;
using MarsXApps.Models.Filter.ServiceAfterSale;
using MarsXApps.Service;
using MarsXApps.Service.Models;
using MarsXApps.Service.Validate;
using Microsoft.Extensions.Configuration;

namespace MarsXApps.Module.ServicePrice.Repositories
{
    public interface IServicePriceRepository
    {
        public Task<Response> GetPMPrice(string mcModel);
        public Task<Response> GetPMPriceModels();
        public Task<Response> GetPMPriceHour(string mcModel, int pmHour);
    }

    public class ServicePriceRepository : IServicePriceRepository
    {
        DateTime ServerTime;

        IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();

        private readonly Mapper _mapper;


        private readonly IServicePriceService _service;

        public ServicePriceRepository(IServicePriceService service)
        {
            _service = service;

            _mapper = new Mapper();


            ServerTime = DateTime.Now.MarsX();

        }

        public async Task<Response> GetPMPriceModels()
        {
            Response resp = new Response();

            List<SysCustomerMachineModelAll> Outbound = new List<SysCustomerMachineModelAll>();

            try
            {
                var respModels = await _service.GetPMPriceModels();

                if (respModels.Count > 0)
                {
                    foreach (var item in respModels)
                    {
                        SysCustomerMachineModelAll obj = new SysCustomerMachineModelAll();

                        obj.Key = item;
                        obj.Name = item;

                        Outbound.Add(obj);
                    }

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
        public async Task<Response> GetPMPrice(string mcModel)
        {
            Response resp = new Response();

            List<MachinePMModel> Outbound = new List<MachinePMModel>();

            try
            {
                Outbound = await _service.GetPMPrice(mcModel);

                if (Outbound.Count > 0)
                {
                    Outbound = (from t in Outbound
                                group t by new { t.mcModelGobal, t.pmHour }
                                into grp
                                select new MachinePMModel
                                {
                                    mcModelGobal = grp.Key.mcModelGobal,
                                    pmHour = grp.Key.pmHour,
                                    noOfItems = grp.Count(),
                                    uiKeyValue = $"{grp.Key.mcModelGobal}|{grp.Key.pmHour}",
                                    sumPrice = grp.Sum(s => Convert.ToDecimal(s.unitPrice)).ToString()

                                }).OrderBy(o => o.pmHour)
                     .ToList();

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
        public async Task<Response> GetPMPriceHour(string mcModel, int pmHour)
        {
            Response resp = new Response();

            List<MachinePMModel> Outbound = new List<MachinePMModel>();

            try
            {
                Outbound = await _service.GetPMPrice(mcModel);

                if (Outbound.Count > 0)
                {
                    Outbound = Outbound.Where(w => w.pmHour == pmHour).OrderBy(o=>o.seqNo).ToList();
                    var sumPrice = Outbound.Sum(s => Convert.ToDecimal(s.unitPrice)).ToString();
                    Outbound.ForEach(f => f.sumPrice = sumPrice);
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




