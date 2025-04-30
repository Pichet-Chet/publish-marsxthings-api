//using System;
//using System.Collections.Generic;
//using MarsXApps.Models;
//using MarsXApps.Models.Constants;
//using MarsXApps.Models.Filter.MasterData;
//using MarsXApps.Service.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.VisualBasic.FileIO;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

//namespace MarsXApps.Module.MasterData
//{
//    public interface IMasterMachineBrandRepositories : IDisposable
//    {
//        public Task<Response> GetAll(MasterMachineBrandFilter param);

//        public Task<Response> GetById(int id);

//        public Task<Response> Create(MasterMachineBrand param);

//        public Task<Response> Update(MasterMachineBrand param);

//        public Task<Response> UpdateImage(int id, IFormFile? Image);

//        public Task<Response> Dropdown(MasterMachineBrandFilter param);



//    }


//    public class MasterMachineBrandRepositories : IMasterMachineBrandRepositories
//    {
//        DateTime ServerTime;

//        private readonly string[] ExtensionsImage = { ".jpg", ".jpeg", ".png" };

//        IConfigurationRoot configuration = new ConfigurationBuilder()
//                   .SetBasePath(Directory.GetCurrentDirectory())
//                   .AddJsonFile("appsettings.json")
//                   .Build();

//        private readonly MarscommuContext _context;


//        public MasterMachineBrandRepositories()
//        {
//            _context = new MarscommuContext();

//            ServerTime = Helper.GetDateTimeByGMT(Convert.ToInt32(configuration["APP:GMT"]));

//        }


//        public async Task<Response> GetAll(MasterMachineBrandFilter param)
//        {
//            Response resp = new Response();

//            try
//            {
//                var queryable = _context.MasterMachineBrands.AsQueryable();


//                #region Filter Data Zone

//                param.TrimAllProperties();

//                if (!string.IsNullOrEmpty(param.TextSearch))
//                {
//                    queryable = queryable
//                        .Where(x =>
//                        x.NameTh.Contains(param.TextSearch) ||
//                        x.NameEn.Contains(param.TextSearch) ||
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

//                var myQueryable = MasterMachineBrandFilter.ApplySorting(queryable, param.SortName, param.SortType);

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

//            MasterMachineBrand masterCountry = new MasterMachineBrand();

//            try
//            {
//                var queryable = _context.MasterMachineBrands.Where(x => x.Id == id).AsQueryable();

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

//        public async Task<Response> Create(MasterMachineBrand param)
//        {
//            Response resp = new Response();

//            try
//            {
//                bool DuplicateKey = _context.MasterMachineBrands
//                    .Where(x =>
//                x.NameTh.ToLower() == param.NameTh.ToLower() &&
//                x.NameEn.ToLower() == param.NameEn.ToLower()).Any();

//                if (DuplicateKey == false)
//                {
//                    param.CreatedDate = ServerTime;
//                    param.UpdatedDate = ServerTime;

//                    _context.MasterMachineBrands.Add(param);

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

//        public async Task<Response> Update(MasterMachineBrand param)
//        {
//            Response resp = new Response();

//            try
//            {
//                var objUpdate = _context.MasterMachineBrands.Where(x => x.Id == param.Id).AsNoTracking().FirstOrDefault();

//                if (objUpdate != null)
//                {
//                    bool DuplicateKey = _context.MasterMachineBrands.Where(x =>
//                                        x.NameTh.ToLower() == param.NameTh.ToLower() &&
//                                        x.NameEn.ToLower() == param.NameEn.ToLower() &&
//                                        x.Id != param.Id).Any();

//                    if (DuplicateKey == false)
//                    {
//                        List<string> NotUpdate = new List<string>();

//                        NotUpdate.Add(nameof(objUpdate.Id));
//                        NotUpdate.Add(nameof(objUpdate.CreatedBy));
//                        NotUpdate.Add(nameof(objUpdate.CreatedDate));

//                        Helper.TransferData_ClassA_to_ClassB<MasterMachineBrand, MasterMachineBrand>(param, ref objUpdate, NotUpdate);

//                        objUpdate.UpdatedBy = param.UpdatedBy;
//                        objUpdate.UpdatedDate = ServerTime;

//                        _context.MasterMachineBrands.Update(objUpdate);

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

//        public async Task<Response> Dropdown(MasterMachineBrandFilter param)
//        {
//            Response resp = new Response();

//            List<MasterMachineBrand> masterMachineBrands = new List<MasterMachineBrand>();

//            List<DropdownModel> list = new List<DropdownModel>();

//            try
//            {
//                var queryable = _context.MasterMachineBrands.AsQueryable();

//                #region Filter Data Zone

//                param.TrimAllProperties();

//                if (!string.IsNullOrEmpty(param.TextSearch))
//                {
//                    queryable = queryable
//                        .Where(x => x.NameTh.Contains(param.TextSearch) ||
//                        x.NameEn.Contains(param.TextSearch) ||
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

//                var myQueryable = MasterMachineBrandFilter.ApplySorting(queryable, param.SortName, param.SortType);

//                masterMachineBrands = await myQueryable.AsNoTracking().ToListAsync();

//                #endregion

//                if (masterMachineBrands != null && masterMachineBrands.Count > 0)
//                {
//                    foreach (var item in masterMachineBrands)
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

//        public async Task<Response> UpdateImage(int id, IFormFile? Image)
//        {
//            Response resp = new Response();

//            try
//            {
//                var objUpdate = await _context.MasterMachineBrands.Where(x => x.Id == id).FirstOrDefaultAsync();

//                if (objUpdate != null)
//                {
//                    var ext = Path.GetExtension(Image.FileName).ToLowerInvariant();

//                    if (!string.IsNullOrEmpty(ext) || ExtensionsImage.Contains(ext))
//                    {
//                        string fileType = string.Empty;

//                        Guid newGuid = Guid.NewGuid();

//                        string CurrentDirectory = Directory.GetCurrentDirectory();

//                        string Read = configuration["FILEUPLOAD:READ_FILE"];

//                        string Write = configuration["FILEUPLOAD:MACHINE_BRAND"];

//                        string FileName = newGuid.ToString();

//                        string LocationPath = System.IO.Path.Combine($"{CurrentDirectory}/{Write}/{objUpdate.Id}");


//                        if (Image.ContentType == "image/jpeg")
//                        {
//                            fileType = ".jpg";
//                        }
//                        else if (Image.ContentType == "image/png")
//                        {
//                            fileType = ".png";
//                        }
//                        else
//                        {
//                            resp.Status = Constants.StatusError;
//                            resp.HttpCode = Constants.HttpCode400;
//                            resp.HttpMessage = Constants.HttpCode400Message;
//                            resp.Message = Constants.InvalidFileImage;

//                            return resp;
//                        }

//                        if (!Directory.Exists(LocationPath))
//                        {
//                            Directory.CreateDirectory(LocationPath);
//                        }

//                        string fileNameAttach = $"{FileName}{fileType}";

//                        string fullPathUrl = System.IO.Path.Combine(LocationPath, fileNameAttach);

//                        using (var stream = new FileStream(LocationPath + fileNameAttach, FileMode.Create))
//                        {
//                            Image.CopyTo(stream);

//                            objUpdate.Image = $"{Write}{fileNameAttach}";

//                            _context.MasterMachineBrands.Update(objUpdate);

//                            await _context.SaveChangesAsync();

//                            objUpdate.Image = Read + objUpdate.Image;

//                            resp.Status = Constants.StatusSuccess;
//                            resp.HttpCode = Constants.HttpCode200;
//                            resp.HttpMessage = Constants.HttpCode200Message;
//                            resp.Output = objUpdate;
//                        }

//                    }
//                    else
//                    {
//                        resp.Status = Constants.StatusError;
//                        resp.HttpCode = Constants.HttpCode400;
//                        resp.HttpMessage = Constants.HttpCode400Message;
//                        resp.Message = Constants.InvalidFileImage;
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

