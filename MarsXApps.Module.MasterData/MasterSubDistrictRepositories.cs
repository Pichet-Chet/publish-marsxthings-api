//using System;
//using MarsXApps.Models;
//using MarsXApps.Models.Constants;
//using MarsXApps.Models.Filter.MasterData;
//using MarsXApps.Service.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;

//namespace MarsXApps.Module.MasterData
//{
//    public interface IMasterSubDistrictRepositories : IDisposable
//    {
//        public Task<Response> GetAll(MasterSubDistrictFilter param);

//        public Task<Response> GetById(int id);

//        public Task<Response> Create(MasterSubdistrict param);

//        public Task<Response> Update(MasterSubdistrict param);

//        public Task<Response> Dropdown(MasterSubDistrictFilter param);
//    }

//    public class MasterSubDistrictRepositories : IMasterSubDistrictRepositories
//    {
//        DateTime ServerTime;

//        IConfigurationRoot configuration = new ConfigurationBuilder()
//                           .SetBasePath(Directory.GetCurrentDirectory())
//                           .AddJsonFile("appsettings.json")
//                           .Build();

//        private readonly MarscommuContext _context;


//        public MasterSubDistrictRepositories()
//        {
//            _context = new MarscommuContext();

//            ServerTime = Helper.GetDateTimeByGMT(Convert.ToInt32(configuration["APP:GMT"]));

//        }


//        public async Task<Response> GetAll(MasterSubDistrictFilter param)
//        {
//            Response resp = new Response();

//            try
//            {
//                var queryable = _context.MasterSubdistricts.AsQueryable();
//                var tempMasterDistricts = _context.MasterDistricts.AsQueryable();

//                #region Filter Data Zone

//                param.TrimAllProperties();

//                if (param.MasterDistrictsId != null)
//                {
//                    queryable = queryable.Where(x => x.MasterDistrictsId != null).AsQueryable();

//                    queryable = queryable.Where(x => x.MasterDistrictsId == param.MasterDistrictsId).AsQueryable();
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

//                var myQueryable = MasterSubDistrictFilter.ApplySorting(queryable, param.SortName, param.SortType);

//                var output = await myQueryable.AsNoTracking().ToListAsync();

//                #endregion


//                #region Tranform Data

//                //if (output != null && output.Count > 0)
//                //{
//                //    foreach (var item in output)
//                //    {
//                //        item.MasterDistricts = await tempMasterDistricts.Where(x => x.Id == item.MasterDistrictsId).FirstOrDefaultAsync();
//                //    }
//                //}

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

//            MasterSubdistrict masterSubdistrict = new MasterSubdistrict();

//            try
//            {
//                var queryable = _context.MasterSubdistricts.Where(x => x.Id == id).AsQueryable();

//                masterSubdistrict = await queryable.AsNoTracking().FirstOrDefaultAsync();

//                #region Tranform Data

//                //if (masterSubdistrict != null)
//                //{
//                //    masterSubdistrict.MasterDistricts = await _context.MasterDistricts.Where(x => x.Id == masterSubdistrict.MasterDistrictsId).FirstOrDefaultAsync();
//                //}

//                #endregion

//                if (masterSubdistrict != null)
//                {
//                    resp.Status = Constants.StatusSuccess;
//                    resp.HttpCode = Constants.HttpCode200;
//                    resp.HttpMessage = Constants.HttpCode200Message;
//                    resp.Output = masterSubdistrict;
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

//        public async Task<Response> Create(MasterSubdistrict param)
//        {
//            Response resp = new Response();

//            try
//            {
//                bool DuplicateKey = _context.MasterSubdistricts.Where(x =>
//                x.NameTh.ToLower() == param.NameTh.ToLower() &&
//                x.NameEn.ToLower() == param.NameEn.ToLower()).Any();

//                if (DuplicateKey == false)
//                {
//                    param.CreatedDate = ServerTime;

//                    param.UpdatedDate = ServerTime;

//                    _context.MasterSubdistricts.Add(param);

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

//        public async Task<Response> Update(MasterSubdistrict param)
//        {
//            Response resp = new Response();

//            try
//            {
//                var objUpdate = _context.MasterSubdistricts.Where(x => x.Id == param.Id).AsNoTracking().FirstOrDefault();

//                if (objUpdate != null)
//                {
//                    bool DuplicateKey = _context.MasterSubdistricts.Where(x =>
//                                        x.NameTh.ToLower() == param.NameTh.ToLower() &&
//                                        x.NameEn.ToLower() == param.NameEn.ToLower() &&
//                                        x.Id != param.Id).Any();

//                    if (DuplicateKey == false)
//                    {
//                        List<string> NotUpdate = new List<string>();

//                        NotUpdate.Add(nameof(objUpdate.Id));
//                        NotUpdate.Add(nameof(objUpdate.MasterDistrictsId));
//                        NotUpdate.Add(nameof(objUpdate.CreatedBy));
//                        NotUpdate.Add(nameof(objUpdate.CreatedDate));

//                        Helper.TransferData_ClassA_to_ClassB<MasterSubdistrict, MasterSubdistrict>(param, ref objUpdate, NotUpdate);

//                        objUpdate.UpdatedBy = param.UpdatedBy;
//                        objUpdate.UpdatedDate = ServerTime;

//                        _context.MasterSubdistricts.Update(objUpdate);

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

//        public async Task<Response> Dropdown(MasterSubDistrictFilter param)
//        {
//            Response resp = new Response();

//            List<MasterSubdistrict> masterSubdistricts = new List<MasterSubdistrict>();

//            List<DropdownModel> list = new List<DropdownModel>();

//            try
//            {
//                var queryable = _context.MasterSubdistricts.AsQueryable();

//                var tempMasterDistricts = _context.MasterDistricts.AsQueryable();

//                #region Filter Data Zone

//                if (param.MasterDistrictsId != null)
//                {
//                    queryable = queryable.Where(x => x.MasterDistrictsId != null).AsQueryable();

//                    queryable = queryable.Where(x => x.MasterDistrictsId == param.MasterDistrictsId).AsQueryable();
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

//                var myQueryable = MasterSubDistrictFilter.ApplySorting(queryable, param.SortName, param.SortType);

//                masterSubdistricts = await myQueryable.AsNoTracking().ToListAsync();

//                #endregion

//                if (masterSubdistricts != null && masterSubdistricts.Count > 0)
//                {
//                    foreach (var item in masterSubdistricts)
//                    {
//                        DropdownModel dropdownModel = new DropdownModel();

//                        dropdownModel.Value = item.Id;

//                        dropdownModel.Option1 = item.NameTh == null ? Constants.Identify : item.NameTh;
//                        dropdownModel.Option2 = item.NameEn == null ? Constants.Identify : item.NameEn;
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

