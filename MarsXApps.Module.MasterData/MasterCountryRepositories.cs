//using System;
//using System.Linq;
//using MarsXApps.Models;
//using MarsXApps.Models.Authorized;
//using MarsXApps.Models.Constants;
//using MarsXApps.Models.Filter.Authorized;
//using MarsXApps.Models.Filter.MasterData;
//using MarsXApps.Models.ThirdParty.SMSMKT.OtpValidate;
//using MarsXApps.Service.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;

//namespace MarsXApps.Module.MasterData
//{
//    public interface IMasterCountryRepositories : IDisposable
//    {
//        public Task<Response> GetAll(MasterCountryFilter param);

//        public Task<Response> GetById(int id);

//        public Task<Response> Create(MasterCountry param);

//        public Task<Response> Update(MasterCountry param);

//        public Task<Response> Dropdown(MasterCountryFilter param);

//    }

//    public class MasterCountryRepositories : IMasterCountryRepositories
//    {
//        DateTime ServerTime;

//        IConfigurationRoot configuration = new ConfigurationBuilder()
//                   .SetBasePath(Directory.GetCurrentDirectory())
//                   .AddJsonFile("appsettings.json")
//                   .Build();

//        private readonly MarscommuContext _context;


//        public MasterCountryRepositories()
//        {
//            _context = new MarscommuContext();

//            ServerTime = Helper.GetDateTimeByGMT(Convert.ToInt32(configuration["APP:GMT"]));

//        }


//        public async Task<Response> GetAll(MasterCountryFilter param)
//        {
//            Response resp = new Response();

//            try
//            {
//                var queryable = _context.MasterCountries.AsQueryable();


//                #region Filter Data Zone

//                param.TrimAllProperties();

//                if (!string.IsNullOrEmpty(param.TextSearch))
//                {
//                    queryable = queryable
//                        .Where(x => x.NameTh.Contains(param.TextSearch) ||
//                        x.NameEn.Contains(param.TextSearch) ||
//                        x.Code.Contains(param.TextSearch) ||
//                        x.CurrencyCode.Contains(param.TextSearch) ||
//                        x.Description.Contains(param.TextSearch)
//                    ).AsQueryable();
//                }

//                if (!string.IsNullOrEmpty(param.NameEn))
//                {
//                    queryable = queryable.Where(x => x.NameEn != null).AsQueryable();

//                    queryable = queryable.Where(x => x.NameEn.ToLower() == param.NameEn.ToLower()).AsQueryable();
//                }

//                if (!string.IsNullOrEmpty(param.NameTh))
//                {
//                    queryable = queryable.Where(x => x.NameTh != null).AsQueryable();

//                    queryable = queryable.Where(x => x.NameTh.ToLower() == param.NameTh.ToLower()).AsQueryable();
//                }

//                if (!string.IsNullOrEmpty(param.Code))
//                {
//                    queryable = queryable.Where(x => x.Code != null).AsQueryable();

//                    queryable = queryable.Where(x => x.Code.ToLower() == param.Code.ToLower()).AsQueryable();
//                }

//                if (!string.IsNullOrEmpty(param.CurrencyCode))
//                {
//                    queryable = queryable.Where(x => x.CurrencyCode != null).AsQueryable();

//                    queryable = queryable.Where(x => x.CurrencyCode.ToLower() == param.CurrencyCode.ToLower()).AsQueryable();
//                }

//                if (!string.IsNullOrEmpty(param.Description))
//                {
//                    queryable = queryable.Where(x => x.Description != null).AsQueryable();

//                    queryable = queryable.Where(x => x.Description.ToLower() == param.Description.ToLower()).AsQueryable();
//                }

//                if (param.IsActive != null)
//                {
//                    queryable = queryable.Where(x => x.IsActive == param.IsActive).AsQueryable();

//                }

//                #endregion


//                #region Sorting

//                var myQueryable = MasterCountryFilter.ApplySorting(queryable, param.SortName, param.SortType);

//                var output = await myQueryable.AsNoTracking().ToListAsync();

//                #endregion


//                #region Pagination

//                if (param.isAll != null)
//                {
//                    if (param.isAll == true)
//                    {
//                        output = output.ToList();
//                    }
//                    else
//                    {
//                        output = output
//                       .Skip((param.PageNumber - 1) * param.PageSize)
//                       .Take(param.PageSize)
//                       .ToList();
//                    }
//                }
//                else
//                {
//                    output = output
//                   .Skip((param.PageNumber - 1) * param.PageSize)
//                   .Take(param.PageSize)
//                   .ToList();
//                }


//                if (output != null && output.Count > 0)
//                {
//                    resp.Status = Constants.StatusSuccess;
//                    resp.HttpCode = Constants.HttpCode200;
//                    resp.HttpMessage = Constants.HttpCode200Message;
//                    resp.Output = output;
//                    resp.EffectRow = myQueryable.Count();

//                    resp.PageNumber = param.PageNumber;
//                    resp.PageSize = param.PageSize;
//                }

//                else
//                {
//                    resp.Status = Constants.StatusError;
//                    resp.HttpCode = Constants.HttpCode400;
//                    resp.HttpMessage = Constants.HttpCode400Message;
//                    resp.Message = Constants.RecordDataNotFound;
//                }

//                #endregion

//            }
//            catch (Exception ex)
//            {
//                resp.Status = Constants.StatusError;
//                resp.HttpCode = Constants.HttpCode500;
//                resp.Message = Constants.HttpCode500Message;
//                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
//            }

//            return resp;
//        }

//        public async Task<Response> GetById(int id)
//        {
//            Response resp = new Response();

//            MasterCountry masterCountry = new MasterCountry();

//            try
//            {
//                var queryable = _context.MasterCountries.Where(x => x.Id == id).AsQueryable();

//                masterCountry = await queryable.AsNoTracking().FirstOrDefaultAsync();

//                if (masterCountry != null)
//                {
//                    resp.Status = Constants.StatusSuccess;
//                    resp.HttpCode = Constants.HttpCode200;
//                    resp.HttpMessage = Constants.HttpCode200Message;
//                    resp.Output = masterCountry;
//                }
//                else
//                {
//                    resp.Status = Constants.StatusError;
//                    resp.HttpCode = Constants.HttpCode400;
//                    resp.HttpMessage = Constants.HttpCode400Message;
//                    resp.Message = Constants.RecordDataNotFound;
//                }

//            }
//            catch (Exception ex)
//            {
//                resp.Status = Constants.StatusError;
//                resp.HttpCode = Constants.HttpCode500;
//                resp.Message = Constants.HttpCode500Message;
//                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
//            }

//            return resp;
//        }

//        public async Task<Response> Create(MasterCountry param)
//        {
//            Response resp = new Response();

//            try
//            {
//                bool DuplicateKey = _context.MasterCountries.Where(x =>
//                x.NameTh.ToLower() == param.NameTh.ToLower() &&
//                x.NameEn.ToLower() == param.NameEn.ToLower() &&
//                x.Code.ToLower() == param.Code.ToLower()).Any();

//                if (DuplicateKey == false)
//                {
//                    param.CreatedDate = ServerTime;

//                    param.UpdatedDate = ServerTime;

//                    _context.MasterCountries.Add(param);

//                    await _context.SaveChangesAsync();

//                    resp.Status = Constants.StatusSuccess;
//                    resp.HttpCode = Constants.HttpCode200;
//                    resp.HttpMessage = Constants.HttpCode200Message;
//                    resp.Output = param;

//                }
//                else
//                {
//                    resp.Status = Constants.StatusError;
//                    resp.HttpCode = Constants.HttpCode400;
//                    resp.HttpMessage = Constants.HttpCode400Message;
//                    resp.Message = Constants.InvalidDataDuplicate;
//                }
//            }
//            catch (Exception ex)
//            {
//                resp.Status = Constants.StatusError;
//                resp.HttpCode = Constants.HttpCode500;
//                resp.Message = Constants.HttpCode500Message;
//                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
//            }

//            return resp;
//        }

//        public async Task<Response> Update(MasterCountry param)
//        {
//            Response resp = new Response();

//            try
//            {
//                var objUpdate = _context.MasterCountries.Where(x => x.Id == param.Id).AsNoTracking().FirstOrDefault();

//                if (objUpdate != null)
//                {
//                    bool DuplicateKey = _context.MasterCountries.Where(x =>
//                                        x.NameTh.ToLower() == param.NameTh.ToLower() &&
//                                        x.NameEn.ToLower() == param.NameEn.ToLower() &&
//                                        x.Code.ToLower() == param.Code.ToLower() &&
//                                        x.Id != param.Id).Any();

//                    if (DuplicateKey == false)
//                    {
//                        List<string> NotUpdate = new List<string>();

//                        NotUpdate.Add(nameof(objUpdate.Id));
//                        NotUpdate.Add(nameof(objUpdate.CreatedBy));
//                        NotUpdate.Add(nameof(objUpdate.CreatedDate));
//                        NotUpdate.Add(nameof(objUpdate.Image));

//                        Helper.TransferData_ClassA_to_ClassB<MasterCountry, MasterCountry>(param, ref objUpdate, NotUpdate);

//                        objUpdate.UpdatedBy = param.UpdatedBy;
//                        objUpdate.UpdatedDate = ServerTime;

//                        _context.MasterCountries.Update(objUpdate);

//                        await _context.SaveChangesAsync();

//                        resp.Status = Constants.StatusSuccess;
//                        resp.HttpCode = Constants.HttpCode200;
//                        resp.HttpMessage = Constants.HttpCode200Message;
//                        resp.Output = objUpdate;

//                    }
//                    else
//                    {
//                        resp.Status = Constants.StatusError;
//                        resp.HttpCode = Constants.HttpCode400;
//                        resp.HttpMessage = Constants.HttpCode400Message;
//                        resp.Message = Constants.InvalidDataDuplicate;
//                    }
//                }
//                else
//                {
//                    resp.Status = Constants.StatusError;
//                    resp.HttpCode = Constants.HttpCode400;
//                    resp.HttpMessage = Constants.HttpCode400Message;
//                    resp.Message = Constants.UpdateDataNotFound;
//                }
//            }
//            catch (Exception ex)
//            {
//                resp.Status = Constants.StatusError;
//                resp.HttpCode = Constants.HttpCode500;
//                resp.Message = Constants.HttpCode500Message;
//                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
//            }

//            return resp;
//        }

//        public async Task<Response> Dropdown(MasterCountryFilter param)
//        {
//            Response resp = new Response();

//            List<MasterCountry> masterCountries = new List<MasterCountry>();

//            List<DropdownModel> list = new List<DropdownModel>();

//            try
//            {
//                var queryable = _context.MasterCountries.AsQueryable();

//                #region Filter Data Zone

//                param.TrimAllProperties();

//                if (!string.IsNullOrEmpty(param.TextSearch))
//                {
//                    queryable = queryable
//                        .Where(x => x.NameTh.Contains(param.TextSearch) ||
//                        x.NameEn.Contains(param.TextSearch) ||
//                        x.Code.Contains(param.TextSearch) ||
//                        x.CurrencyCode.Contains(param.TextSearch) ||
//                        x.Description.Contains(param.TextSearch)
//                    ).AsQueryable();
//                }

//                if (!string.IsNullOrEmpty(param.NameEn))
//                {
//                    queryable = queryable.Where(x => x.NameEn != null).AsQueryable();

//                    queryable = queryable.Where(x => x.NameEn.ToLower() == param.NameEn.ToLower()).AsQueryable();
//                }

//                if (!string.IsNullOrEmpty(param.NameTh))
//                {
//                    queryable = queryable.Where(x => x.NameTh != null).AsQueryable();

//                    queryable = queryable.Where(x => x.NameTh.ToLower() == param.NameTh.ToLower()).AsQueryable();
//                }

//                if (!string.IsNullOrEmpty(param.Code))
//                {
//                    queryable = queryable.Where(x => x.Code != null).AsQueryable();

//                    queryable = queryable.Where(x => x.Code.ToLower() == param.Code.ToLower()).AsQueryable();
//                }

//                if (!string.IsNullOrEmpty(param.CurrencyCode))
//                {
//                    queryable = queryable.Where(x => x.CurrencyCode != null).AsQueryable();

//                    queryable = queryable.Where(x => x.CurrencyCode.ToLower() == param.CurrencyCode.ToLower()).AsQueryable();
//                }

//                if (!string.IsNullOrEmpty(param.Description))
//                {
//                    queryable = queryable.Where(x => x.Description != null).AsQueryable();

//                    queryable = queryable.Where(x => x.Description.ToLower() == param.Description.ToLower()).AsQueryable();
//                }

//                if (param.IsActive != null)
//                {
//                    queryable = queryable.Where(x => x.IsActive == param.IsActive).AsQueryable();

//                }

//                #endregion

//                #region Sorting

//                var myQueryable = MasterCountryFilter.ApplySorting(queryable, param.SortName, param.SortType);

//                masterCountries = await myQueryable.AsNoTracking().ToListAsync();

//                #endregion

//                if (masterCountries != null && masterCountries.Count > 0)
//                {
//                    foreach (var item in masterCountries)
//                    {
//                        DropdownModel dropdownModel = new DropdownModel();

//                        dropdownModel.Value = item.Id;

//                        dropdownModel.Option1 = item.NameTh == null ? Constants.Identify : item.NameTh;
//                        dropdownModel.Option2 = item.NameEn == null ? Constants.Identify : item.NameEn;
//                        dropdownModel.Option3 = item.Code == null ? Constants.Identify : item.Code;
//                        dropdownModel.Option4 = item.CurrencyCode == null ? Constants.Identify : item.CurrencyCode;
//                        dropdownModel.IsActive = item.IsActive;

//                        list.Add(dropdownModel);
//                    }
                   
//                    resp.Status = Constants.StatusSuccess;
//                    resp.HttpCode = Constants.HttpCode200;
//                    resp.HttpMessage = Constants.HttpCode200Message;
//                    resp.Output = list;
//                }
//                else
//                {
//                    resp.Status = Constants.StatusError;
//                    resp.HttpCode = Constants.HttpCode400;
//                    resp.HttpMessage = Constants.HttpCode400Message;
//                    resp.Message = Constants.RecordDataNotFound;
//                }
//            }
//            catch (Exception ex)
//            {
//                resp.Status = Constants.StatusError;
//                resp.HttpCode = Constants.HttpCode500;
//                resp.Message = Constants.HttpCode500Message;
//                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
//            }

//            return resp;
//        }


//        #region IDispose Zone

//        private bool DisposedValue;

//        protected virtual void Dispose(bool disposing)
//        {
//            if (!DisposedValue)
//            {
//                if (disposing)
//                {
//                    _context.Dispose();
//                }

//                DisposedValue = true;
//            }
//        }

//        public void Dispose()
//        {
//            Dispose(disposing: true);
//            GC.SuppressFinalize(this);
//        }

//        #endregion

//    }
//}

