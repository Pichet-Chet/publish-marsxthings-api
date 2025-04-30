//using System;
//using MarsXApps.Models;
//using MarsXApps.Models.Constants;
//using MarsXApps.Models.Filter.MasterData;
//using MarsXApps.Service.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;

//namespace MarsXApps.Module.MasterData
//{
//    public interface IMasterPaymentStatusRepositories : IDisposable
//    {
//        public Task<Response> GetAll(MasterPaymentStatusFilter param);

//        public Task<Response> GetById(int id);

//        public Task<Response> Create(MasterPaymentStatus param);

//        public Task<Response> Update(MasterPaymentStatus param);

//        public Task<Response> Dropdown(MasterPaymentStatusFilter param);

//    }

//    public class MasterPaymentStatusRepositories : IMasterPaymentStatusRepositories
//    {
//        DateTime ServerTime;

//        IConfigurationRoot configuration = new ConfigurationBuilder()
//                   .SetBasePath(Directory.GetCurrentDirectory())
//                   .AddJsonFile("appsettings.json")
//                   .Build();

//        private readonly MarscommuContext _context;


//        public MasterPaymentStatusRepositories()
//        {
//            _context = new MarscommuContext();

//            ServerTime = Helper.GetDateTimeByGMT(Convert.ToInt32(configuration["APP:GMT"]));

//        }


//        public async Task<Response> GetAll(MasterPaymentStatusFilter param)
//        {
//            Response resp = new Response();

//            try
//            {
//                var queryable = _context.MasterPaymentStatuses.AsQueryable();


//                #region Filter Data Zone

//                param.TrimAllProperties();

//                if (!string.IsNullOrEmpty(param.TextSearch))
//                {
//                    queryable = queryable
//                        .Where(x =>
//                        x.Value.Contains(param.TextSearch) ||
//                        x.NameTh.Contains(param.TextSearch) ||
//                        x.NameEn.Contains(param.TextSearch) ||
//                        x.Description.Contains(param.TextSearch)
//                    ).AsQueryable();
//                }

//                if (!string.IsNullOrEmpty(param.Value))
//                {
//                    queryable = queryable.Where(x => x.Value != null).AsQueryable();

//                    queryable = queryable.Where(x => x.Value.ToLower() == param.Value.ToLower()).AsQueryable();
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

//                var myQueryable = MasterPaymentStatusFilter.ApplySorting(queryable, param.SortName, param.SortType);

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

//            MasterPaymentStatus result = new MasterPaymentStatus();

//            try
//            {
//                var queryable = _context.MasterPaymentStatuses.Where(x => x.Id == id).AsQueryable();

//                result = await queryable.AsNoTracking().FirstOrDefaultAsync();

//                if (result != null)
//                {
//                    resp.Status = Constants.StatusSuccess;
//                    resp.HttpCode = Constants.HttpCode200;
//                    resp.HttpMessage = Constants.HttpCode200Message;
//                    resp.Output = result;
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

//        public async Task<Response> Create(MasterPaymentStatus param)
//        {
//            Response resp = new Response();

//            try
//            {
//                bool DuplicateKey = _context.MasterPaymentStatuses.Where(x =>
//                x.Value.ToLower() == param.Value.ToLower() &&
//                x.NameTh.ToLower() == param.NameTh.ToLower() &&
//                x.NameEn.ToLower() == param.NameEn.ToLower()).Any();

//                if (DuplicateKey == false)
//                {
//                    param.CreatedDate = ServerTime;
//                    param.UpdatedDate = ServerTime;

//                    _context.MasterPaymentStatuses.Add(param);

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

//        public async Task<Response> Update(MasterPaymentStatus param)
//        {
//            Response resp = new Response();

//            try
//            {
//                var objUpdate = _context.MasterPaymentStatuses.Where(x => x.Id == param.Id).AsNoTracking().FirstOrDefault();

//                if (objUpdate != null)
//                {
//                    bool DuplicateKey = _context.MasterPaymentStatuses.Where(x =>
//                                        x.Value.ToLower() == param.Value.ToLower() &&
//                                        x.NameTh.ToLower() == param.NameTh.ToLower() &&
//                                        x.NameEn.ToLower() == param.NameEn.ToLower() &&
//                                        x.Id != param.Id).Any();

//                    if (DuplicateKey == false)
//                    {
//                        List<string> NotUpdate = new List<string>();

//                        NotUpdate.Add(nameof(objUpdate.Id));
//                        NotUpdate.Add(nameof(objUpdate.Value));
//                        NotUpdate.Add(nameof(objUpdate.CreatedBy));
//                        NotUpdate.Add(nameof(objUpdate.CreatedDate));

//                        Helper.TransferData_ClassA_to_ClassB<MasterPaymentStatus, MasterPaymentStatus>(param, ref objUpdate, NotUpdate);

//                        objUpdate.UpdatedBy = param.UpdatedBy;
//                        objUpdate.UpdatedDate = ServerTime;

//                        _context.MasterPaymentStatuses.Update(objUpdate);

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

//        public async Task<Response> Dropdown(MasterPaymentStatusFilter param)
//        {
//            Response resp = new Response();

//            List<MasterPaymentStatus> masterPaymentStatuses = new List<MasterPaymentStatus>();

//            List<DropdownModel> list = new List<DropdownModel>();

//            try
//            {
//                var queryable = _context.MasterPaymentStatuses.AsQueryable();

//                #region Filter Data Zone

//                param.TrimAllProperties();

//                if (!string.IsNullOrEmpty(param.TextSearch))
//                {
//                    queryable = queryable
//                        .Where(x =>
//                        x.Value.Contains(param.TextSearch) ||
//                        x.NameTh.Contains(param.TextSearch) ||
//                        x.NameEn.Contains(param.TextSearch) ||
//                        x.Description.Contains(param.TextSearch)
//                    ).AsQueryable();
//                }

//                if (!string.IsNullOrEmpty(param.Value))
//                {
//                    queryable = queryable.Where(x => x.Value != null).AsQueryable();

//                    queryable = queryable.Where(x => x.Value.ToLower() == param.Value.ToLower()).AsQueryable();
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

//                var myQueryable = MasterPaymentStatusFilter.ApplySorting(queryable, param.SortName, param.SortType);

//                masterPaymentStatuses = await myQueryable.AsNoTracking().ToListAsync();

//                #endregion

//                if (masterPaymentStatuses != null && masterPaymentStatuses.Count > 0)
//                {
//                    foreach (var item in masterPaymentStatuses)
//                    {
//                        DropdownModel dropdownModel = new DropdownModel();

//                        dropdownModel.Value = item.Id;
//                        dropdownModel.Option1 = item.Value == null ? Constants.Identify : item.Value;
//                        dropdownModel.Option2 = item.NameTh == null ? Constants.Identify : item.NameTh;
//                        dropdownModel.Option3 = item.NameEn == null ? Constants.Identify : item.NameEn;
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

