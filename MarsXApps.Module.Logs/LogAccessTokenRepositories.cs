//using System;
//using MarsXApps.Models;
//using MarsXApps.Models.Constants;
//using MarsXApps.Models.Filter.Log;
//using MarsXApps.Models.Filter.MasterData;
//using MarsXApps.Service.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;

//namespace MarsXApps.Module.Logs
//{
//    public interface ILogAccessTokenRepositories : IDisposable
//    {
//        public Task<Response> GetAll(LogAccessTokenFilter param);

//        public Task<Response> GetById(int id);

//        public Task<Response> Create(LogAccessToken param);
//    }

//    public class LogAccessTokenRepositories : ILogAccessTokenRepositories
//    {
//        IConfigurationRoot configuration = new ConfigurationBuilder()
//                   .SetBasePath(Directory.GetCurrentDirectory())
//                   .AddJsonFile("appsettings.json")
//                   .Build();

//        private readonly MarscommuContext _context;


//        public LogAccessTokenRepositories()
//        {
//            _context = new MarscommuContext();
//        }


//        public async Task<Response> GetAll(LogAccessTokenFilter param)
//        {
//            Response resp = new Response();

//            try
//            {
//                var queryable = _context.LogAccessTokens.AsQueryable();


//                #region Filter Data Zone


//                if (param.SysCustomerId != null)
//                {
//                    queryable = queryable.Where(x => x.SysCustomersId == param.SysCustomerId).AsQueryable();
//                }

//                #endregion


//                #region Sorting

//                var myQueryable = LogAccessTokenFilter.ApplySorting(queryable, param.SortName, param.SortType);

//                var output = myQueryable.AsNoTracking().ToList();

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

//            LogAccessToken logAccessToken = new LogAccessToken();

//            try
//            {
//                var queryable = _context.LogAccessTokens.Where(x => x.Id == id).AsQueryable();

//                logAccessToken = await queryable.AsNoTracking().FirstOrDefaultAsync();

//                if (logAccessToken != null)
//                {
//                    resp.Status = Constants.StatusSuccess;
//                    resp.HttpCode = Constants.HttpCode200;
//                    resp.HttpMessage = Constants.HttpCode200Message;
//                    resp.Output = logAccessToken;
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

//        public async Task<Response> Create(LogAccessToken param)
//        {
//            Response resp = new Response();

//            try
//            {
//                _context.LogAccessTokens.Add(param);

//                await _context.SaveChangesAsync();

//                resp.Status = Constants.StatusSuccess;
//                resp.HttpCode = Constants.HttpCode200;
//                resp.HttpMessage = Constants.HttpCode200Message;
//                resp.Output = param;
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

