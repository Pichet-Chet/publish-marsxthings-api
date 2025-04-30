//using System;
//using MarsXApps.Models;
//using MarsXApps.Models.Constants;
//using MarsXApps.Models.Filter.Authorized;
//using MarsXApps.Models.Filter.MasterData;
//using MarsXApps.Service.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;

//namespace MarsXApps.Module.Authorized.Repositories
//{
//    public interface ISysApplicationTokenRepositories : IDisposable
//    {
//        public Task<Response> GetAll(SysApplicationTokenFilter param);

//        public Task<Response> GetById(int id);

//        public Task<Response> Create(SysApplicationToken param);

//        public Task<Response> Update(SysApplicationToken param);

//    }

//    public class SysApplicationTokenRepositories : ISysApplicationTokenRepositories
//    {
//        DateTime ServerTime;

//        IConfigurationRoot configuration = new ConfigurationBuilder()
//                           .SetBasePath(Directory.GetCurrentDirectory())
//                           .AddJsonFile("appsettings.json")
//                           .Build();

//        private readonly MarscommuContext _context;


//        public SysApplicationTokenRepositories()
//        {
//            _context = new MarscommuContext();

//            ServerTime = Helper.GetDateTimeByGMT(Convert.ToInt32(configuration["APP:GMT"]));
//        }


//        public async Task<Response> GetAll(SysApplicationTokenFilter param)
//        {
//            Response resp = new Response();

//            try
//            {
//                var queryable = _context.SysApplicationTokens.AsQueryable();



//                #region Filter Data Zone

//                param.TrimAllProperties();

//                if (!string.IsNullOrEmpty(param.TextSearch))
//                {
//                    queryable = queryable
//                        .Where(x =>
//                        x.Name.Contains(param.TextSearch) ||
//                        x.CompanyName.Contains(param.TextSearch) ||
//                        x.ContactName.Contains(param.TextSearch) ||
//                        x.ContactTel.Contains(param.TextSearch))
//                        .AsQueryable();
//                }

//                if (param.Id != null)
//                {
//                    queryable = queryable.Where(x => x.Id != null).AsQueryable();

//                    queryable = queryable.Where(x => x.Id == param.Id).AsQueryable();
//                }

//                if (!string.IsNullOrEmpty(param.Name))
//                {
//                    queryable = queryable.Where(x => x.Name != null).AsQueryable();

//                    queryable = queryable.Where(x => x.Name.ToLower() == param.Name.ToLower()).AsQueryable();
//                }

//                if (!string.IsNullOrEmpty(param.CompanyName))
//                {
//                    queryable = queryable.Where(x => x.CompanyName != null).AsQueryable();

//                    queryable = queryable.Where(x => x.CompanyName.ToLower() == param.CompanyName.ToLower()).AsQueryable();
//                }

//                if (!string.IsNullOrEmpty(param.ContactName))
//                {
//                    queryable = queryable.Where(x => x.ContactName != null).AsQueryable();

//                    queryable = queryable.Where(x => x.ContactName.ToLower() == param.ContactName.ToLower()).AsQueryable();
//                }

//                if (!string.IsNullOrEmpty(param.ContactTel))
//                {
//                    queryable = queryable.Where(x => x.ContactTel != null).AsQueryable();

//                    queryable = queryable.Where(x => x.ContactTel.ToLower() == param.ContactTel.ToLower()).AsQueryable();
//                }

//                if (param.IsActive != null)
//                {
//                    queryable = queryable.Where(x => x.IsActive == param.IsActive).AsQueryable();

//                }

//                #endregion



//                #region Sorting

//                var myQueryable = SysApplicationTokenFilter.ApplySorting(queryable, param.SortName, param.SortType);

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

//            SysApplicationToken sysApplicationToken = new SysApplicationToken();

//            try
//            {
//                var queryable = _context.SysApplicationTokens.Where(x => x.Id == id).AsQueryable();

//                sysApplicationToken = await queryable.AsNoTracking().FirstOrDefaultAsync();

//                if (sysApplicationToken != null)
//                {
//                    resp.Status = Constants.StatusSuccess;
//                    resp.HttpCode = Constants.HttpCode200;
//                    resp.HttpMessage = Constants.HttpCode200Message;
//                    resp.Output = sysApplicationToken;
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

//        public async Task<Response> Create(SysApplicationToken param)
//        {
//            Response resp = new Response();

//            try
//            {
//                bool DuplicateKey = _context.SysApplicationTokens.Where(x =>
//                x.Name.ToLower() == param.Name.ToLower() &&
//                x.CompanyName.ToLower() == param.CompanyName.ToLower() &&
//                x.CompanyName.ToLower() == param.CompanyName.ToLower()).Any();

//                if (DuplicateKey == false)
//                {
//                    param.Key = Helper.GenerateSecretKey();
//                    param.CreatedDate = ServerTime;
//                    param.UpdatedDate = ServerTime;
//                    param.AccessToken = string.Empty;
//                    param.AccessTokenExpire = null;

//                    _context.SysApplicationTokens.Add(param);

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

//        public async Task<Response> Update(SysApplicationToken param)
//        {
//            Response resp = new Response();

//            try
//            {
//                var objUpdate = _context.SysApplicationTokens.Where(x => x.Id == param.Id).AsNoTracking().FirstOrDefault();

//                if (objUpdate != null)
//                {
//                    bool DuplicateKey = _context.SysApplicationTokens.Where(x =>
//                                        x.Name.ToLower() == param.Name.ToLower() &&
//                                        x.CompanyName.ToLower() == param.CompanyName.ToLower() &&
//                                        x.ContactName.ToLower() == param.ContactName.ToLower() &&
//                                        x.Id != param.Id).Any();

//                    if (DuplicateKey == false)
//                    {
//                        List<string> NotUpdate = new List<string>();

//                        NotUpdate.Add(nameof(objUpdate.Id));
//                        NotUpdate.Add(nameof(objUpdate.Uid));
//                        NotUpdate.Add(nameof(objUpdate.CreatedBy));
//                        NotUpdate.Add(nameof(objUpdate.CreatedDate));

//                        Helper.TransferData_ClassA_to_ClassB<SysApplicationToken, SysApplicationToken>(param, ref objUpdate, NotUpdate);

//                        objUpdate.UpdatedBy = param.UpdatedBy;
//                        objUpdate.UpdatedDate = ServerTime;

//                        _context.SysApplicationTokens.Update(objUpdate);

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

