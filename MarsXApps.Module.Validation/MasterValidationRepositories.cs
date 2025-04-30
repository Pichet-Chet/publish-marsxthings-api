//using System;
//using System.Reflection;
//using System.Runtime.CompilerServices;
//using MarsXApps.Models;
//using MarsXApps.Models.Constants;
//using MarsXApps.Service.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;

//namespace MarsXApps.Module.Validation
//{
//    public interface IMasterValidationRepositories : IDisposable
//    {

//        public Task<Response> SysCustomer(Guid Id);

//        public Task<Response> SasPeriodTime(int Id);
//        public Task<Response> SasAddress(int Id);
//        public Task<Response> SasStatus(int Id);


//        public Task<Response> MasterCountry(int Id);
//        public Task<Response> MasterGeography(int Id);
//        public Task<Response> MasterProvince(int Id);
//        public Task<Response> MasterDistrict(int Id);
//        public Task<Response> MasterSubdistrict(int Id);

//        public Task<Response> MasterCustomerGroup(int Id);
//        public Task<Response> MasterCustomerType(int Id);

//        public Task<Response> MasterMachineBrand(int Id);
//        public Task<Response> MasterMachineGroup(int Id);
//        public Task<Response> MasterMachineModel(int Id);
//        public Task<Response> MasterPaymentStatus(int Id);
//        public Task<Response> MasterPrefixName(int Id);

//    }

//    public class MasterValidationRepositories : IMasterValidationRepositories
//    {
//        DateTime ServerTime;

//        IConfigurationRoot configuration = new ConfigurationBuilder()
//                   .SetBasePath(Directory.GetCurrentDirectory())
//                   .AddJsonFile("appsettings.json")
//                   .Build();

//        private readonly MarscommuContext _context;


//        public MasterValidationRepositories()
//        {
//            _context = new MarscommuContext();

//            ServerTime = Helper.GetDateTimeByGMT(Convert.ToInt32(configuration["APP:GMT"]));

//        }

//        public static string GetCurrentMethodName([CallerMemberName] string methodName = "")
//        {
//            return methodName;
//        }

//        public async Task<Response> SysCustomer(Guid Id)
//        {
//            Response resp = new Response();

//            string MasterOf = GetCurrentMethodName().Remove(0, 3);

//            if (Id != null)
//            {
//                var findMasterValue = await _context.SysCustomers.Where(x => x.Id == Id).AnyAsync();

//                if (findMasterValue)
//                {
//                    resp.Status = Constants.StatusSuccess;
//                }
//                else
//                {
//                    resp.Status = Constants.StatusError;
//                    resp.HttpCode = Constants.HttpCode400;
//                    resp.HttpMessage = Constants.HttpCode400Message;
//                    resp.Message = Constants.ValidationNotFound + $" {MasterOf} " + Constants.ValidationRetryAgain;
//                }
//            }
//            else
//            {
//                resp.Status = Constants.StatusError;
//                resp.HttpCode = Constants.HttpCode400;
//                resp.HttpMessage = Constants.HttpCode400Message;
//                resp.Message = Constants.ValidationNotFound + $" {MasterOf} " + Constants.ValidationRetryAgain;
//            }

//            return resp;
//        }

//        public async Task<Response> SasPeriodTime(int Id)
//        {
//            Response resp = new Response();

//            string MasterOf = GetCurrentMethodName().Remove(0, 3);

//            if (Id != 0)
//            {
//                var findMasterValue = await _context.MasterCountries.Where(x => x.Id == Id).AnyAsync();

//                if (findMasterValue)
//                {
//                    resp.Status = Constants.StatusSuccess;
//                }
//                else
//                {
//                    resp.Status = Constants.StatusError;
//                    resp.HttpCode = Constants.HttpCode400;
//                    resp.HttpMessage = Constants.HttpCode400Message;
//                    resp.Message = Constants.ValidationNotFound + $" {MasterOf} " + Constants.ValidationRetryAgain;
//                }
//            }
//            else
//            {
//                resp.Status = Constants.StatusError;
//                resp.HttpCode = Constants.HttpCode400;
//                resp.HttpMessage = Constants.HttpCode400Message;
//                resp.Message = Constants.ValidationNotFound + $" {MasterOf} " + Constants.ValidationRetryAgain;
//            }

//            return resp;
//        }

//        public async Task<Response> SasAddress(int Id)
//        {
//            Response resp = new Response();

//            string MasterOf = GetCurrentMethodName().Remove(0, 3);

//            if (Id != 0)
//            {
//                var findMasterValue = await _context.MasterCountries.Where(x => x.Id == Id).AnyAsync();

//                if (findMasterValue)
//                {
//                    resp.Status = Constants.StatusSuccess;
//                }
//                else
//                {
//                    resp.Status = Constants.StatusError;
//                    resp.HttpCode = Constants.HttpCode400;
//                    resp.HttpMessage = Constants.HttpCode400Message;
//                    resp.Message = Constants.ValidationNotFound + $" {MasterOf} " + Constants.ValidationRetryAgain;
//                }
//            }
//            else
//            {
//                resp.Status = Constants.StatusError;
//                resp.HttpCode = Constants.HttpCode400;
//                resp.HttpMessage = Constants.HttpCode400Message;
//                resp.Message = Constants.ValidationNotFound + $" {MasterOf} " + Constants.ValidationRetryAgain;
//            }

//            return resp;
//        }

//        public async Task<Response> SasStatus(int Id)
//        {
//            Response resp = new Response();

//            string MasterOf = GetCurrentMethodName().Remove(0, 3);

//            if (Id != 0)
//            {
//                var findMasterValue = await _context.SasStatuses.Where(x => x.Id == Id).AnyAsync();

//                if (findMasterValue)
//                {
//                    resp.Status = Constants.StatusSuccess;
//                }
//                else
//                {
//                    resp.Status = Constants.StatusError;
//                    resp.HttpCode = Constants.HttpCode400;
//                    resp.HttpMessage = Constants.HttpCode400Message;
//                    resp.Message = Constants.ValidationNotFound + $" {MasterOf} " + Constants.ValidationRetryAgain;
//                }
//            }
//            else
//            {
//                resp.Status = Constants.StatusError;
//                resp.HttpCode = Constants.HttpCode400;
//                resp.HttpMessage = Constants.HttpCode400Message;
//                resp.Message = Constants.ValidationNotFound + $" {MasterOf} " + Constants.ValidationRetryAgain;
//            }

//            return resp;
//        }

//        public async Task<Response> MasterCountry(int Id)
//        {
//            Response resp = new Response();

//            string MasterOf = GetCurrentMethodName().Remove(0, 6);

//            if (Id != 0)
//            {
//                var findMasterValue = await _context.MasterCountries.Where(x => x.Id == Id).AnyAsync();

//                if (findMasterValue)
//                {
//                    resp.Status = Constants.StatusSuccess;
//                }
//                else
//                {
//                    resp.Status = Constants.StatusError;
//                    resp.HttpCode = Constants.HttpCode400;
//                    resp.HttpMessage = Constants.HttpCode400Message;
//                    resp.Message = Constants.ValidationNotFound + $" {MasterOf} " + Constants.ValidationRetryAgain;
//                }
//            }
//            else
//            {
//                resp.Status = Constants.StatusError;
//                resp.HttpCode = Constants.HttpCode400;
//                resp.HttpMessage = Constants.HttpCode400Message;
//                resp.Message = Constants.ValidationNotFound + $" {MasterOf} " + Constants.ValidationRetryAgain;
//            }

//            return resp;
//        }

//        public async Task<Response> MasterGeography(int Id)
//        {
//            Response resp = new Response();

//            string MasterOf = GetCurrentMethodName().Remove(0, 6);

//            if (Id != 0)
//            {
//                var findMasterValue = await _context.MasterGeographies.Where(x => x.Id == Id).AnyAsync();

//                if (findMasterValue)
//                {
//                    resp.Status = Constants.StatusSuccess;
//                }
//                else
//                {
//                    resp.Status = Constants.StatusError;
//                    resp.HttpCode = Constants.HttpCode400;
//                    resp.HttpMessage = Constants.HttpCode400Message;
//                    resp.Message = Constants.ValidationNotFound + $" {MasterOf} " + Constants.ValidationRetryAgain;
//                }
//            }
//            else
//            {
//                resp.Status = Constants.StatusError;
//                resp.HttpCode = Constants.HttpCode400;
//                resp.HttpMessage = Constants.HttpCode400Message;
//                resp.Message = Constants.ValidationNotFound + $" {MasterOf} " + Constants.ValidationRetryAgain;
//            }

//            return resp;
//        }

//        public async Task<Response> MasterProvince(int Id)
//        {
//            Response resp = new Response();

//            string MasterOf = GetCurrentMethodName().Remove(0, 6);

//            if (Id != 0)
//            {
//                var findMasterValue = await _context.MasterProvinces.Where(x => x.Id == Id).AnyAsync();

//                if (findMasterValue)
//                {
//                    resp.Status = Constants.StatusSuccess;
//                }
//                else
//                {
//                    resp.Status = Constants.StatusError;
//                    resp.HttpCode = Constants.HttpCode400;
//                    resp.HttpMessage = Constants.HttpCode400Message;
//                    resp.Message = Constants.ValidationNotFound + $" {MasterOf} " + Constants.ValidationRetryAgain;
//                }
//            }
//            else
//            {
//                resp.Status = Constants.StatusError;
//                resp.HttpCode = Constants.HttpCode400;
//                resp.HttpMessage = Constants.HttpCode400Message;
//                resp.Message = Constants.ValidationNotFound + $" {MasterOf} " + Constants.ValidationRetryAgain;
//            }

//            return resp;
//        }

//        public async Task<Response> MasterDistrict(int Id)
//        {
//            Response resp = new Response();

//            string MasterOf = GetCurrentMethodName().Remove(0, 6);

//            if (Id != 0)
//            {
//                var findMasterValue = await _context.MasterDistricts.Where(x => x.Id == Id).AnyAsync();

//                if (findMasterValue)
//                {
//                    resp.Status = Constants.StatusSuccess;
//                }
//                else
//                {
//                    resp.Status = Constants.StatusError;
//                    resp.HttpCode = Constants.HttpCode400;
//                    resp.HttpMessage = Constants.HttpCode400Message;
//                    resp.Message = Constants.ValidationNotFound + $" {MasterOf} " + Constants.ValidationRetryAgain;
//                }
//            }
//            else
//            {
//                resp.Status = Constants.StatusError;
//                resp.HttpCode = Constants.HttpCode400;
//                resp.HttpMessage = Constants.HttpCode400Message;
//                resp.Message = Constants.ValidationNotFound + $" {MasterOf} " + Constants.ValidationRetryAgain;
//            }

//            return resp;
//        }

//        public async Task<Response> MasterSubdistrict(int Id)
//        {
//            Response resp = new Response();

//            string MasterOf = GetCurrentMethodName().Remove(0, 6);

//            if (Id != 0)
//            {
//                var findMasterValue = await _context.MasterSubdistricts.Where(x => x.Id == Id).AnyAsync();

//                if (findMasterValue)
//                {
//                    resp.Status = Constants.StatusSuccess;
//                }
//                else
//                {
//                    resp.Status = Constants.StatusError;
//                    resp.HttpCode = Constants.HttpCode400;
//                    resp.HttpMessage = Constants.HttpCode400Message;
//                    resp.Message = Constants.ValidationNotFound + $" {MasterOf} " + Constants.ValidationRetryAgain;
//                }
//            }
//            else
//            {
//                resp.Status = Constants.StatusError;
//                resp.HttpCode = Constants.HttpCode400;
//                resp.HttpMessage = Constants.HttpCode400Message;
//                resp.Message = Constants.ValidationNotFound + $" {MasterOf} " + Constants.ValidationRetryAgain;
//            }

//            return resp;
//        }

//        public async Task<Response> MasterCustomerGroup(int Id)
//        {
//            Response resp = new Response();

//            string MasterOf = GetCurrentMethodName().Remove(0, 6);

//            if (Id != 0)
//            {
//                var findMasterValue = await _context.MasterCustomerGroups.Where(x => x.Id == Id).AnyAsync();

//                if (findMasterValue)
//                {
//                    resp.Status = Constants.StatusSuccess;
//                }
//                else
//                {
//                    resp.Status = Constants.StatusError;
//                    resp.HttpCode = Constants.HttpCode400;
//                    resp.HttpMessage = Constants.HttpCode400Message;
//                    resp.Message = Constants.ValidationNotFound + $" {MasterOf} " + Constants.ValidationRetryAgain;
//                }
//            }
//            else
//            {
//                resp.Status = Constants.StatusError;
//                resp.HttpCode = Constants.HttpCode400;
//                resp.HttpMessage = Constants.HttpCode400Message;
//                resp.Message = Constants.ValidationNotFound + $" {MasterOf} " + Constants.ValidationRetryAgain;
//            }

//            return resp;
//        }

//        public async Task<Response> MasterCustomerType(int Id)
//        {
//            Response resp = new Response();

//            string MasterOf = GetCurrentMethodName().Remove(0, 6);

//            if (Id != 0)
//            {
//                var findMasterValue = await _context.MasterCustomerTypes.Where(x => x.Id == Id).AnyAsync();

//                if (findMasterValue)
//                {
//                    resp.Status = Constants.StatusSuccess;
//                }
//                else
//                {
//                    resp.Status = Constants.StatusError;
//                    resp.HttpCode = Constants.HttpCode400;
//                    resp.HttpMessage = Constants.HttpCode400Message;
//                    resp.Message = Constants.ValidationNotFound + $" {MasterOf} " + Constants.ValidationRetryAgain;
//                }
//            }
//            else
//            {
//                resp.Status = Constants.StatusError;
//                resp.HttpCode = Constants.HttpCode400;
//                resp.HttpMessage = Constants.HttpCode400Message;
//                resp.Message = Constants.ValidationNotFound + $" {MasterOf} " + Constants.ValidationRetryAgain;
//            }

//            return resp;
//        }

//        public async Task<Response> MasterMachineBrand(int Id)
//        {
//            Response resp = new Response();

//            string MasterOf = GetCurrentMethodName().Remove(0, 6);

//            if (Id != 0)
//            {
//                var findMasterValue = await _context.MasterMachineBrands.Where(x => x.Id == Id).AnyAsync();

//                if (findMasterValue)
//                {
//                    resp.Status = Constants.StatusSuccess;
//                }
//                else
//                {
//                    resp.Status = Constants.StatusError;
//                    resp.HttpCode = Constants.HttpCode400;
//                    resp.HttpMessage = Constants.HttpCode400Message;
//                    resp.Message = Constants.ValidationNotFound + $" {MasterOf} " + Constants.ValidationRetryAgain;
//                }
//            }
//            else
//            {
//                resp.Status = Constants.StatusError;
//                resp.HttpCode = Constants.HttpCode400;
//                resp.HttpMessage = Constants.HttpCode400Message;
//                resp.Message = Constants.ValidationNotFound + $" {MasterOf} " + Constants.ValidationRetryAgain;
//            }

//            return resp;
//        }

//        public async Task<Response> MasterMachineGroup(int Id)
//        {
//            Response resp = new Response();

//            string MasterOf = GetCurrentMethodName().Remove(0, 6);

//            if (Id != 0)
//            {
//                var findMasterValue = await _context.MasterMachineGroups.Where(x => x.Id == Id).AnyAsync();

//                if (findMasterValue)
//                {
//                    resp.Status = Constants.StatusSuccess;
//                }
//                else
//                {
//                    resp.Status = Constants.StatusError;
//                    resp.HttpCode = Constants.HttpCode400;
//                    resp.HttpMessage = Constants.HttpCode400Message;
//                    resp.Message = Constants.ValidationNotFound + $" {MasterOf} " + Constants.ValidationRetryAgain;
//                }
//            }
//            else
//            {
//                resp.Status = Constants.StatusError;
//                resp.HttpCode = Constants.HttpCode400;
//                resp.HttpMessage = Constants.HttpCode400Message;
//                resp.Message = Constants.ValidationNotFound + $" {MasterOf} " + Constants.ValidationRetryAgain;
//            }

//            return resp;
//        }

//        public async Task<Response> MasterMachineModel(int Id)
//        {
//            Response resp = new Response();

//            string MasterOf = GetCurrentMethodName().Remove(0, 6);

//            if (Id != 0)
//            {
//                var findMasterValue = await _context.MasterMachineModels.Where(x => x.Id == Id).AnyAsync();

//                if (findMasterValue)
//                {
//                    resp.Status = Constants.StatusSuccess;
//                }
//                else
//                {
//                    resp.Status = Constants.StatusError;
//                    resp.HttpCode = Constants.HttpCode400;
//                    resp.HttpMessage = Constants.HttpCode400Message;
//                    resp.Message = Constants.ValidationNotFound + $" {MasterOf} " + Constants.ValidationRetryAgain;
//                }
//            }
//            else
//            {
//                resp.Status = Constants.StatusError;
//                resp.HttpCode = Constants.HttpCode400;
//                resp.HttpMessage = Constants.HttpCode400Message;
//                resp.Message = Constants.ValidationNotFound + $" {MasterOf} " + Constants.ValidationRetryAgain;
//            }

//            return resp;
//        }

//        public async Task<Response> MasterPaymentStatus(int Id)
//        {
//            Response resp = new Response();

//            string MasterOf = GetCurrentMethodName().Remove(0, 6);

//            if (Id != 0)
//            {
//                var findMasterValue = await _context.MasterPaymentStatuses.Where(x => x.Id == Id).AnyAsync();

//                if (findMasterValue)
//                {
//                    resp.Status = Constants.StatusSuccess;
//                }
//                else
//                {
//                    resp.Status = Constants.StatusError;
//                    resp.HttpCode = Constants.HttpCode400;
//                    resp.HttpMessage = Constants.HttpCode400Message;
//                    resp.Message = Constants.ValidationNotFound + $" {MasterOf} " + Constants.ValidationRetryAgain;
//                }
//            }
//            else
//            {
//                resp.Status = Constants.StatusError;
//                resp.HttpCode = Constants.HttpCode400;
//                resp.HttpMessage = Constants.HttpCode400Message;
//                resp.Message = Constants.ValidationNotFound + $" {MasterOf} " + Constants.ValidationRetryAgain;
//            }

//            return resp;
//        }

//        public async Task<Response> MasterPrefixName(int Id)
//        {
//            Response resp = new Response();

//            string MasterOf = GetCurrentMethodName().Remove(0, 6);

//            if (Id != 0)
//            {
//                var findMasterValue = await _context.MasterPrefixNames.Where(x => x.Id == Id).AnyAsync();

//                if (findMasterValue)
//                {
//                    resp.Status = Constants.StatusSuccess;
//                }
//                else
//                {
//                    resp.Status = Constants.StatusError;
//                    resp.HttpCode = Constants.HttpCode400;
//                    resp.HttpMessage = Constants.HttpCode400Message;
//                    resp.Message = Constants.ValidationNotFound + $" {MasterOf} " + Constants.ValidationRetryAgain;
//                }
//            }
//            else
//            {
//                resp.Status = Constants.StatusError;
//                resp.HttpCode = Constants.HttpCode400;
//                resp.HttpMessage = Constants.HttpCode400Message;
//                resp.Message = Constants.ValidationNotFound + $" {MasterOf} " + Constants.ValidationRetryAgain;
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

