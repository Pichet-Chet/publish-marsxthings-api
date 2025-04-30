//using System;
//using MarsXApps.Models;
//using MarsXApps.Models.Constants;
//using MarsXApps.Models.Filter.Log;
//using MarsXApps.Service.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;

//namespace MarsXApps.Module.Logs
//{
//    public interface ILogAccessApplicationTokenRepositories : IDisposable
//    {
//        public Task<Response> GetAll(LogAccessApplicationTokenFilter param);

//        public Task<Response> GetById(int id);

//        public Task<Response> Create(LogAccessApplicationToken param);
//    }

//    public class LogAccessApplicationTokenRepositories : ILogAccessApplicationTokenRepositories
//    {
//        IConfigurationRoot configuration = new ConfigurationBuilder()
//                   .SetBasePath(Directory.GetCurrentDirectory())
//                   .AddJsonFile("appsettings.json")
//                   .Build();

//        private readonly MarscommuContext _context;


//        public LogAccessApplicationTokenRepositories()
//        {
//            _context = new MarscommuContext();
//        }


//        public async Task<Response> GetAll(LogAccessApplicationTokenFilter param)
//        {
//            Response resp = new Response();

//            try
//            {
//                var queryable = _context.LogAccessApplicationTokens.AsQueryable();


//                #region Filter Data Zone


//                if (param.SysApplicationTokenId != null)
//                {
//                    queryable = queryable.Where(x => x.SysApplicationTokenId == param.SysApplicationTokenId).AsQueryable();
//                }

//                #endregion


//                #region Sorting

//                var myQueryable = LogAccessApplicationTokenFilter.ApplySorting(queryable, param.SortName, param.SortType);

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

//            LogAccessApplicationToken logAccessApplicationToken = new LogAccessApplicationToken();

//            try
//            {
//                var queryable = _context.LogAccessApplicationTokens.Where(x => x.Id == id).AsQueryable();

//                logAccessApplicationToken = await queryable.AsNoTracking().FirstOrDefaultAsync();

//                if (logAccessApplicationToken != null)
//                {
//                    resp.Status = Constants.StatusSuccess;
//                    resp.HttpCode = Constants.HttpCode200;
//                    resp.HttpMessage = Constants.HttpCode200Message;
//                    resp.Output = logAccessApplicationToken;
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

//        public async Task<Response> Create(LogAccessApplicationToken param)
//        {
//            Response resp = new Response();

//            try
//            {
//                _context.LogAccessApplicationTokens.Add(param);

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

