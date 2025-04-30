//using System;
//using MarsXApps.Models;
//using MarsXApps.Models.Constants;
//using MarsXApps.Models.Filter.Authorized;
//using MarsXApps.Service.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;

//namespace MarsXApps.Module.Authorized.Repositories
//{
//    public interface ISysTermsAndConditionsTransactionRepository : IDisposable
//    {
//        public Task<Response> GetAll(SysTermsAndConditionsTransactionFilter param);

//        public Task<Response> GetById(int id);

//        public Task<Response> GetByCustomerLastEvent(Guid id);

//    }

//    public class SysTermsAndConditionsTransactionRepository : ISysTermsAndConditionsTransactionRepository
//    {

//        IConfigurationRoot configuration = new ConfigurationBuilder()
//                   .SetBasePath(Directory.GetCurrentDirectory())
//                   .AddJsonFile("appsettings.json")
//                   .Build();


//        private readonly MarscommuContext _context;

//        public SysTermsAndConditionsTransactionRepository()
//        {
//            _context = new MarscommuContext();

//            DateTime ServerTime = Helper.GetDateTimeByGMT(Convert.ToInt32(configuration["APP:END_POINT"]));
//        }



//        public async Task<Response> GetAll(SysTermsAndConditionsTransactionFilter param)
//        {
//            Response resp = new Response();

//            try
//            {
//                var queryable = _context.SysTermsAndConditionsTransactions.AsQueryable();

//                #region Filter Data Zone

//                param.TrimAllProperties();


//                if (param.SysCustomersId != null)
//                {
//                    queryable = queryable.Where(x => x.SysCustomersId == param.SysCustomersId).AsQueryable();

//                }

//                if (param.SysTermsAndConditionsId != null)
//                {
//                    queryable = queryable.Where(x => x.SysTermsAndConditionsId == param.SysTermsAndConditionsId).AsQueryable();

//                }

//                #endregion


//                #region Sorting

//                var myQueryable = SysTermsAndConditionsTransactionFilter.ApplySorting(queryable, param.SortName, param.SortType);

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

//            SysTermsAndConditionsTransaction result = new SysTermsAndConditionsTransaction();

//            try
//            {
//                var queryable = _context.SysTermsAndConditionsTransactions.Where(x => x.Id == id).AsQueryable();

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

//        public async Task<Response> GetByCustomerLastEvent(Guid id)
//        {
//            Response resp = new Response();

//            SysTermsAndConditionsTransaction result = new SysTermsAndConditionsTransaction();

//            try
//            {
//                var getLastTerms = await _context.SysTermsAndConditions.Where(x => x.IsActive == true).FirstOrDefaultAsync();

//                var queryable = _context.SysTermsAndConditionsTransactions.Where(x => x.SysCustomersId == id && x.SysTermsAndConditionsId == getLastTerms.Id).AsQueryable();

//                result = await queryable.AsNoTracking().FirstOrDefaultAsync();

//                if (result != null)
//                {
//                    if (result.Consideration != true)
//                    {
//                        resp.Status = Constants.StatusSuccess;
//                        resp.HttpCode = Constants.HttpCode200;
//                        resp.HttpMessage = Constants.HttpCode200Message;
//                        resp.Output = result;
//                    }
//                    else
//                    {
//                        resp.Status = Constants.StatusError;
//                        resp.HttpCode = Constants.HttpCode400;
//                        resp.HttpMessage = Constants.HttpCode400Message;
//                        resp.Message = Constants.LastConsiderationIsReject;
//                    }
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

