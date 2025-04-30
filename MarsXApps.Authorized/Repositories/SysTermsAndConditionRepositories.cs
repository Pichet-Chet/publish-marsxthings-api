//using System;
//using MarsXApps.Models;
//using MarsXApps.Models.Constants;
//using MarsXApps.Models.Filter.Authorized;
//using MarsXApps.Service.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;

//namespace MarsXApps.Module.Authorized.Repositories
//{
//    public interface ISysTermsAndConditionRepositories : IDisposable
//    {
//        public Task<Response> GetAll(SysTermsAndConditionFilter param);

//        public Task<Response> GetById(int id);

//        public Task<Response> GetLastVersionActive();

//        public Task<Response> Create(SysTermsAndCondition param);

//        public Task<Response> Update(SysTermsAndCondition param);
//    }


//    public class SysTermsAndConditionRepositories : ISysTermsAndConditionRepositories
//    {
//        DateTime ServerTime;

//        IConfigurationRoot configuration = new ConfigurationBuilder()
//                   .SetBasePath(Directory.GetCurrentDirectory())
//                   .AddJsonFile("appsettings.json")
//                   .Build();

//        private readonly MarscommuContext _context;

//        public SysTermsAndConditionRepositories()
//        {
//            _context = new MarscommuContext();

//            ServerTime = Helper.GetDateTimeByGMT(Convert.ToInt32(configuration["APP:GMT"]));
//        }



//        public async Task<Response> GetAll(SysTermsAndConditionFilter param)
//        {
//            Response resp = new Response();

//            try
//            {
//                var queryable = _context.SysTermsAndConditions.AsQueryable();

//                #region Filter Data Zone

//                param.TrimAllProperties();

//                if (!string.IsNullOrEmpty(param.TextSearch))
//                {
//                    queryable = queryable
//                        .Where(x =>
//                        x.Version.Contains(param.TextSearch)).AsQueryable();
//                }



//                if (!string.IsNullOrEmpty(param.Version))
//                {
//                    queryable = queryable.Where(x => x.Version != null).AsQueryable();

//                    queryable = queryable.Where(x => x.Version.ToLower() == param.Version.ToLower()).AsQueryable();
//                }

//                if (param.IsActive != null)
//                {
//                    queryable = queryable.Where(x => x.IsActive == param.IsActive).AsQueryable();

//                }

//                #endregion


//                #region Sorting

//                var myQueryable = SysTermsAndConditionFilter.ApplySorting(queryable, param.SortName, param.SortType);

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

//            SysTermsAndCondition result = new SysTermsAndCondition();

//            try
//            {
//                var queryable = _context.SysTermsAndConditions.Where(x => x.Id == id).AsQueryable();

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

//        public async Task<Response> GetLastVersionActive()
//        {
//            Response resp = new Response();

//            SysTermsAndCondition result = new SysTermsAndCondition();

//            try
//            {
//                var queryable = _context.SysTermsAndConditions.Where(x => x.IsActive == true).AsQueryable();

//                result = await queryable.AsNoTracking().OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync();

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

//        public async Task<Response> Create(SysTermsAndCondition param)
//        {
//            Response resp = new Response();

//            try
//            {
//                bool DuplicateKey = _context.SysTermsAndConditions.Where(x =>
//                x.Version.ToLower() == param.Version.ToLower()).Any();

//                if (DuplicateKey == false)
//                {
//                    param.CreatedDate = ServerTime;
//                    param.UpdatedDate = ServerTime;

//                    _context.SysTermsAndConditions.Add(param);

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

//        public async Task<Response> Update(SysTermsAndCondition param)
//        {
//            Response resp = new Response();

//            try
//            {
//                bool DuplicateKey = _context.SysTermsAndConditions.Where(x =>
//                x.Version.ToLower() == param.Version.ToLower()).Any();

//                if (DuplicateKey == false)
//                {
//                    var objUpdate = await _context.SysTermsAndConditions.Where(x => x.Id == param.Id).FirstOrDefaultAsync();

//                    if (objUpdate != null)
//                    {
//                        objUpdate.IsActive = false;

//                        SysTermsAndCondition sysTermsAndCondition = new SysTermsAndCondition();
//                        sysTermsAndCondition.Content = param.Content;
//                        sysTermsAndCondition.Version = param.Version;
//                        sysTermsAndCondition.EffectiveDate = param.EffectiveDate;
//                        sysTermsAndCondition.ExpireDate = param.ExpireDate;
//                        sysTermsAndCondition.IsActive = true;
//                        sysTermsAndCondition.CreatedBy = param.CreatedBy;
//                        sysTermsAndCondition.CreatedDate = ServerTime;
//                        sysTermsAndCondition.UpdatedBy = param.CreatedBy;
//                        sysTermsAndCondition.UpdatedDate = ServerTime;
                       
//                        _context.Update(objUpdate);
//                        _context.Add(sysTermsAndCondition);

//                        await _context.SaveChangesAsync();

//                    }
//                    else
//                    {
//                        resp.Status = Constants.StatusError;
//                        resp.HttpCode = Constants.HttpCode400;
//                        resp.HttpMessage = Constants.HttpCode400Message;
//                        resp.Message = Constants.UpdateDataNotFound;
//                    }
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

