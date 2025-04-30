using MarsXApps.Models;
using MarsXApps.Models.Constants;
using MarsXApps.Models.Customs;
using MarsXApps.Models.Customs.Request.SasOrder;
using MarsXApps.Models.Customs.Return.SasOrder;
using MarsXApps.Models.Filter.MasterData;
using MarsXApps.Models.Filter.ServiceAfterSale;
using MarsXApps.Module.Environment.MasterData;
using MarsXApps.Service;
using MarsXApps.Service.Models;
using MarsXApps.Service.Validate;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using static System.Net.Mime.MediaTypeNames;

namespace MarsXApps.Module.ServiceAfterSale.Repositories
{
    public interface ISasOrderRepositories
    {
        public Task<Response> GetAll(SasOrderFilter param);

        public Task<Response> GetHeader(SasOrderFilter param);

        public Task<Response> GetDetail(int headerId);

        public Task<Response> GetDetailPayment(int headerId);

        public Task<Response> GetById(int id);

        public Task<Response> GetByJobRef(string jobRef);

        public Task<Response> Create(SasOrderModel param);

        public Task<Response> Update(SasOrderModel param);

        public Task<Response> GetStatus(List<int> statusID, List<string> statusCode, Pagination param);

        public Task<Response> Cancel(SasOrderTransactionModel param);

        public Task<Response> UpdateStatus(SasOrderUpdateStatusModel param);

        public Task<Response> UpdateStatusWithBranchCode(SasOrderUpdateStatusWithBranchCodeModel param);

        public Task<Response> UpdateStatusWithCaecJobNo(SasOrderUpdateStatusWithCaecJobNoModel param);

        public Task<Response> AddPaymentSlipImage(int orderId, IFormFile? FileUpload);

    }

    public class SasOrderRepositories : ISasOrderRepositories
    {
        DateTime ServerTime;

        private readonly Mapper _mapper;

        private readonly IMasterDataValidationService _validate;

        private readonly ISasOrderService _service;

        private readonly ISasOrderTransactionService _serviceTransaction;

        private readonly ISasOrderPaymentService _serviceOrderPayment;

        private readonly IMasterPaymentChannelService _servicePaymentChannel;

        private readonly ISysDocumentControlService _sysDocumentControlService;

        private readonly ISasStatusService _sasStatusService;

        private readonly IServicePriceService _servicePriceService;

        private readonly IMasterBranchService _masterBranchService;

        public SasOrderRepositories(
            ISasOrderService service,
            ISasOrderTransactionService serviceTransaction,
            ISasOrderPaymentService serviceOrderPayment,
            IMasterPaymentChannelService masterPaymentChannel,
            ISasStatusService sasStatusService,
            ISysDocumentControlService sysDocumentControlService,
            IServicePriceService servicePriceService,
            IMasterDataValidationService validate,
            IMasterBranchService masterBranchService)
        {
            _mapper = new Mapper();

            _service = service;

            _serviceTransaction = serviceTransaction;

            _serviceOrderPayment = serviceOrderPayment;

            _sasStatusService = sasStatusService;

            _sysDocumentControlService = sysDocumentControlService;

            _servicePriceService = servicePriceService;

            _validate = validate;

            _servicePaymentChannel = masterPaymentChannel;

            _masterBranchService = masterBranchService;

            ServerTime = DateTime.Now.MarsX();
        }

        public async Task<Response> GetAll(SasOrderFilter param)
        {
            Response resp = new Response();

            List<SasOrderModel> Outbound = new List<SasOrderModel>();

            try
            {
                Outbound = await _service.GetAll(param);

                if (Outbound.Count > 0)
                {
                    resp.Status = Constants.StatusSuccess;
                    resp.HttpCode = Constants.HttpCode200;
                    resp.HttpMessage = Constants.HttpCode200Message;
                    resp.Output = Outbound;

                    resp.PageNumber = param.PageNumber;
                    resp.PageSize = param.PageSize;
                    resp.EffectRow = await _service.CountAsync();
                }
                else
                {
                    resp.Status = Constants.StatusError;
                    resp.HttpCode = Constants.HttpCode400;
                    resp.HttpMessage = Constants.HttpCode204Message;
                    resp.Message = Constants.RecordDataNotFound;
                }

            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.Message = Constants.HttpCode500Message;
                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return resp;
        }

        public async Task<Response> GetHeader(SasOrderFilter param)
        {
            Response resp = new Response();

            List<SasOrderModel> Outbound = new List<SasOrderModel>();

            List<SasOrderHeaderReturnModel> HeaderList = new List<SasOrderHeaderReturnModel>();

            try
            {
                Outbound = await _service.GetAll(param);

                if (Outbound.Count > 0)
                {
                    foreach (var item in Outbound)
                    {
                        SasOrderHeaderReturnModel obj = new SasOrderHeaderReturnModel();

                        obj.Id = item.Id;
                        obj.OrderNo = string.IsNullOrEmpty(item.OrderNo) ? string.Empty : item.OrderNo;
                        obj.Type = item.ServiceType;
                        obj.SysCustomerId = item.SysCustomerId.ToString();
                        obj.ContactName = item.Firstname;
                        obj.StatusValue = item.SasStatusModel == null ? null : item.SasStatusModel.Value;
                        obj.StatusNameTh = item.SasStatusModel == null ? null : item.SasStatusModel.NameTh;
                        obj.StatusNameEn = item.SasStatusModel == null ? null : item.SasStatusModel.NameEn;
                        obj.Description = item.Description;
                        obj.CreateBy = item.CreateBy;
                        obj.CreateDate = item.CreateDate;
                        obj.UpdatedDate = item.UpdateDate;
                        obj.MachineModel = item.SasStatusModel == null ? null : item.MachineModel;
                        obj.ServiceDate = item.ServiceDate.ToString("dd/MM/yyyy");
                        obj.ServicePeriodTime = item.ServicePeriodTime;
                        obj.AddressTh = item.Address + " " + item.MasterSubdistrictModel?.NameTh + " " + item.MasterDistrictModel?.NameTh + " " + item.MasterProvinceModel?.NameTh + " " + item.MasterSubdistrictModel?.ZipCode;
                        obj.AddressEn = item.Address + " " + item.MasterSubdistrictModel?.NameEn + " " + item.MasterDistrictModel?.NameEn + " " + item.MasterProvinceModel?.NameEn + " " + item.MasterSubdistrictModel?.ZipCode;
                        obj.CaecJobNo = item.CaecJobNo;
                        obj.MachineModel = string.IsNullOrEmpty(item.MachineSerialNo) ? string.Empty : item.MachineSerialNo;
                        obj.MachineChassic = item.MachineChassic;


                        if (!string.IsNullOrEmpty(obj.CaecJobNo))
                        {
                            obj.SumPrice = await _servicePriceService.SummaryPriceJobNo(obj.CaecJobNo);

                            if (decimal.TryParse(obj.SumPrice, out decimal sumPriceValue))
                            {
                                obj.SumPrice = sumPriceValue.ToString("N2");
                            }
                            else
                            {
                                obj.SumPrice = "N/A";
                            }
                        }
                        else
                        {
                            obj.SumPrice = "N/A";
                        }


                        if (!string.IsNullOrEmpty(item.CaecBranchCode))
                        {
                            MasterBranchFilter filter = new MasterBranchFilter();
                            filter.Code = item.CaecBranchCode;

                            var branches = await _masterBranchService.GetAll(filter);

                            obj.BranchName = branches?.FirstOrDefault()?.NameTh ?? "";
                        }
                        else
                        {
                            obj.BranchName = "";
                        }


                        HeaderList.Add(obj);
                    }

                    resp.Status = Constants.StatusSuccess;
                    resp.HttpCode = Constants.HttpCode200;
                    resp.HttpMessage = Constants.HttpCode200Message;

                    resp.PageNumber = param.PageNumber;
                    resp.PageSize = param.PageSize;
                    resp.EffectRow = await _service.CountAsync();

                    resp.Output = HeaderList;
                }
                else
                {
                    resp.Status = Constants.StatusError;
                    resp.HttpCode = Constants.HttpCode400;
                    resp.HttpMessage = Constants.HttpCode204Message;
                    resp.Message = Constants.RecordDataNotFound;
                }

            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.Message = Constants.HttpCode500Message;
                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return resp;
        }

        public async Task<Response> GetDetail(int headerId)
        {
            Response resp = new Response();

            SasOrderModel Outbound = new SasOrderModel();

            List<SasOrderTranReturnModel> sasOrderTrans = new List<SasOrderTranReturnModel>();

            try
            {
                Outbound = await _service.GetById(headerId);

                if (Outbound != null)
                {
                    SasOrderDetialReturnModel obj = new SasOrderDetialReturnModel();

                    obj.Id = Outbound.Id;
                    obj.OrderNo = Outbound.OrderNo;
                    obj.Type = Outbound.ServiceType;
                    obj.StatusValue = Outbound.SasStatusModel.Value;
                    obj.StatusNameTh = Outbound.SasStatusModel.NameTh;
                    obj.StatusNameEn = Outbound.SasStatusModel.NameEn;
                    obj.OrderNo = Outbound.OrderNo;
                    obj.ContactName = Outbound.Firstname;
                    obj.ContactTel = Outbound.Telephone;
                    obj.AddressTh = Outbound.Address + " " + Outbound.MasterSubdistrictModel?.NameTh + " " + Outbound.MasterDistrictModel?.NameTh + " " + Outbound.MasterProvinceModel?.NameTh + " " + Outbound.MasterSubdistrictModel?.ZipCode;
                    obj.AddressEn = Outbound.Address + " " + Outbound.MasterSubdistrictModel?.NameEn + " " + Outbound.MasterDistrictModel?.NameEn + " " + Outbound.MasterProvinceModel?.NameEn + " " + Outbound.MasterSubdistrictModel?.ZipCode;
                    obj.MachineModel = Outbound.MachineModel;
                    obj.MachineSerial = Outbound.MachineSerialNo;
                    obj.MachineChassic = Outbound.MachineChassic;


                    if (Outbound.SasOrderTransactionModel != null && Outbound.SasOrderTransactionModel.Count > 0)
                    {
                        foreach (var item in Outbound.SasOrderTransactionModel.OrderByDescending(x => x.CreatedDate))
                        {
                            SasOrderTranReturnModel tran = new SasOrderTranReturnModel();

                            var statusModel = await _sasStatusService.GetByValue(item.SasStatusValue);

                            tran.tranDate = item.CreatedDate;
                            tran.StatusValue = statusModel.Value;
                            tran.NameTh = statusModel.NameTh;
                            tran.NameEn = statusModel.NameEn;
                            tran.SubTitleNameTh = statusModel.SubTitleTh;
                            tran.SubTitleNameEn = statusModel.SubTitleEn;
                            tran.Remark = item.Remark;
                            if (item.SasStatusValue.ToUpper() == "PAYMENT")
                            {
                                var paymentModel = await _serviceOrderPayment.GetByOrderId(headerId);
                                if (paymentModel != null)
                                {
                                    tran.ImageUrl = paymentModel.ImageUrl;
                                }
                            }

                            sasOrderTrans.Add(tran);
                        }
                    }

                    obj.sasOrderTrans = sasOrderTrans;


                    resp.Status = Constants.StatusSuccess;
                    resp.HttpCode = Constants.HttpCode200;
                    resp.HttpMessage = Constants.HttpCode200Message;
                    resp.Output = obj;
                }
                else
                {
                    resp.Status = Constants.StatusError;
                    resp.HttpCode = Constants.HttpCode400;
                    resp.HttpMessage = Constants.HttpCode204Message;
                    resp.Message = Constants.RecordDataNotFound;
                }

            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.Message = Constants.HttpCode500Message;
                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return resp;
        }

        public async Task<Response> GetDetailPayment(int headerId)
        {
            Response resp = new Response();

            SasOrderModel Outbound = new SasOrderModel();

            List<SasOrderDetialPaymentReturnModel> sasOrderDetialPayments = new List<SasOrderDetialPaymentReturnModel>();

            MasterPaymentChannelFilter paymentChannelFilter = new MasterPaymentChannelFilter();

            List<PaymentChannel> paymentChannels = new List<PaymentChannel>();

            List<CaecItemMaster> caecItemMasters = new List<CaecItemMaster>();

            try
            {
                Outbound = await _service.GetById(headerId);

                if (Outbound != null)
                {
                    SasOrderDetialPaymentReturnModel obj = new SasOrderDetialPaymentReturnModel();

                    obj.Id = Outbound.Id;
                    obj.Type = Outbound.ServiceType;
                    obj.StatusValue = Outbound.SasStatusModel.Value;
                    obj.StatusNameTh = Outbound.SasStatusModel.NameTh;
                    obj.StatusNameEn = Outbound.SasStatusModel.NameEn;

                    obj.CreateBy = Outbound.CreateBy;
                    obj.CreateDate = Outbound.CreateDate;
                    obj.UpdatedDate = Outbound.UpdateDate;

                    obj.OrderNo = Outbound.OrderNo;
                    obj.ContactName = Outbound.Firstname;
                    obj.ContactTel = Outbound.Telephone;

                    obj.ServiceDate = Outbound.ServiceDate;
                    obj.ServicePeriodTime = Outbound.ServicePeriodTime;
                    obj.MachineModel = Outbound.MachineModel;
                    obj.MachineSerial = Outbound.MachineSerialNo;
                    obj.MachineChassic = Outbound.MachineChassic;
                    obj.InsuranceType = "ในประกัน";

                    obj.CaecJobNo = Outbound.CaecJobNo;

                    obj.AddressTh = Outbound.Address + " " + Outbound.MasterSubdistrictModel?.NameTh + " " + Outbound.MasterDistrictModel?.NameTh + " " + Outbound.MasterProvinceModel?.NameTh + " " + Outbound.MasterSubdistrictModel?.ZipCode;
                    obj.AddressEn = Outbound.Address + " " + Outbound.MasterSubdistrictModel?.NameEn + " " + Outbound.MasterDistrictModel?.NameEn + " " + Outbound.MasterProvinceModel?.NameEn + " " + Outbound.MasterSubdistrictModel?.ZipCode;

                    var getMasterPayment = await _servicePaymentChannel.GetAll(paymentChannelFilter);

                    if (getMasterPayment != null && getMasterPayment.Count > 0)
                    {
                        foreach (var item in getMasterPayment)
                        {
                            PaymentChannel paymentChannel = new PaymentChannel();

                            paymentChannel.Code = item.Code;

                            paymentChannel.BankNameTh = item.BankNameTh;
                            paymentChannel.BankNameEn = item.BankNameEn;
                            paymentChannel.BankIcon = item.BankIcon;
                            paymentChannel.BankNo = item.BankNo;
                            paymentChannel.NameTh = item.NameTh;
                            paymentChannel.NameEn = item.NameEn;
                            paymentChannel.Image = item.Image;

                            paymentChannels.Add(paymentChannel);
                        }
                    }

                    obj.paymentChannels = paymentChannels;

                    var getWorkSheetsDetail = !string.IsNullOrEmpty(obj.CaecJobNo) ? await _servicePriceService.WorkSheetsDetailByJobNo(obj.CaecJobNo) : null;

                    if (getWorkSheetsDetail != null && getWorkSheetsDetail.Count > 0)
                    {
                        foreach (var item in getWorkSheetsDetail)
                        {
                            CaecItemMaster caecItemMaster = new CaecItemMaster();

                            caecItemMaster.ItemName = item.itemsDetail;
                            caecItemMaster.Quantity = item.noOfItems.ToString();
                            caecItemMaster.Price = item.unitPrice.ToString("N2");

                            caecItemMasters.Add(caecItemMaster);
                        }

                        obj.SumPrice = await _servicePriceService.SummaryPriceJobNo(obj.CaecJobNo);

                        if (decimal.TryParse(obj.SumPrice, out decimal sumPriceValue))
                        {
                            obj.SumPrice = sumPriceValue.ToString("N2");
                        }
                        else
                        {
                            obj.SumPrice = "N/A";
                        }

                    }
                    else
                    {
                        CaecItemMaster caecItemMaster = new CaecItemMaster();

                        caecItemMaster.ItemName = "N/A";
                        caecItemMaster.Quantity = "N/A";
                        caecItemMaster.Price = "N/A";

                        caecItemMasters.Add(caecItemMaster);

                        obj.SumPrice = "N/A";
                    }


                    obj.caecItemMasters = caecItemMasters;

                    resp.Status = Constants.StatusSuccess;
                    resp.HttpCode = Constants.HttpCode200;
                    resp.HttpMessage = Constants.HttpCode200Message;
                    resp.Output = obj;
                }
                else
                {
                    resp.Status = Constants.StatusError;
                    resp.HttpCode = Constants.HttpCode400;
                    resp.HttpMessage = Constants.HttpCode204Message;
                    resp.Message = Constants.RecordDataNotFound;
                }

            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.Message = Constants.HttpCode500Message;
                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return resp;
        }

        public async Task<Response> GetById(int id)
        {
            Response resp = new Response();

            SasOrderModel Outbound = new SasOrderModel();

            try
            {
                Outbound = await _service.GetById(id);

                if (Outbound != null)
                {
                    var tranPaid = Outbound.SasOrderTransactionModel?.FirstOrDefault(f => f.SasStatusValue == "PAYMENT");
                    if (tranPaid != null)
                    {
                        var paymentModel = await _serviceOrderPayment.GetByOrderId(Outbound.Id);
                        if (paymentModel != null)
                        {
                            tranPaid.ImageUrl = paymentModel.ImageUrl;
                        }
                    }
                    resp.Status = Constants.StatusSuccess;
                    resp.HttpCode = Constants.HttpCode200;
                    resp.HttpMessage = Constants.HttpCode200Message;
                    resp.Output = Outbound;
                }
                else
                {
                    resp.Status = Constants.StatusError;
                    resp.HttpCode = Constants.HttpCode400;
                    resp.HttpMessage = Constants.HttpCode204Message;
                    resp.Message = Constants.RecordDataNotFound;
                }

            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.Message = Constants.HttpCode500Message;
                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return resp;
        }


        public async Task<Response> Create(SasOrderModel param)
        {


            Response resp = new Response();

            SasOrder Inbound = new SasOrder();

            SasOrderTransaction InboundTransaction = new SasOrderTransaction();

            SasOrderModel Outbound = new SasOrderModel();

            SysDocumentControlModel Document = new SysDocumentControlModel();

            try
            {
                //param.OrderNo = Helper.GenerateShort12Guid().ToUpper();

                Document.Code = "OR";
                Document.Type = param.ServiceType.ToUpper();

                param.OrderNo = await _sysDocumentControlService.GenerteDocument(Document);

                param.CreateBy = param.CreateBy;
                param.UpdateBy = param.CreateBy;

                param.CreateDate = ServerTime;
                param.UpdateDate = ServerTime;

                Inbound = _mapper.Map(param);

                #region Valiedation Zone

                Func<Task<Response>>[] validationFunctions = new Func<Task<Response>>[]
                {
                    async () => await _validate.SysCustomer(Inbound.SysCustomerId),
                    //async () => await _validate.MasterProvince(Inbound.MasterProvincesId),
                    //async () => await _validate.MasterDistrict(Inbound.MasterDistrictsId),
                    //async () => await _validate.MasterSubdistrict(Inbound.MasterSubdistrictsId),
                    async () => await _validate.SasStatus(Inbound.SasStatusId),
                    async () => await _validate.SasStatusValue(Inbound.SasStatusValue),


                };

                foreach (var func in validationFunctions)
                {
                    var validate = await func();

                    if (validate != null)
                    {
                        if (validate.Status == false)
                        {
                            resp.Status = Constants.StatusError;
                            resp.HttpCode = Constants.HttpCode400;
                            resp.HttpMessage = Constants.HttpCode204Message;
                            resp.Message = validate.Message;

                            return resp;
                        }
                    }
                }

                #endregion

                Inbound.CreatedDate = ServerTime;
                Inbound.UpdatedDate = ServerTime;

                Outbound = await _service.Create(Inbound);

                if (Outbound != null)
                {
                    InboundTransaction.CreatedDate = ServerTime;
                    InboundTransaction.UpdatedDate = ServerTime;
                    InboundTransaction.CreatedBy = param.CreateBy;
                    InboundTransaction.UpdatedBy = param.CreateBy;
                    InboundTransaction.SasOrderId = Inbound.Id;
                    InboundTransaction.SasStatusId = Inbound.SasStatusId;
                    InboundTransaction.SasStatusValue = Inbound.SasStatusValue;
                    InboundTransaction.IsActive = true;

                    await _serviceTransaction.Create(InboundTransaction);
                }

                resp.Status = Constants.StatusSuccess;
                resp.HttpCode = Constants.HttpCode200;
                resp.HttpMessage = Constants.HttpCode200Message;
                resp.Output = Outbound;
            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.Message = Constants.HttpCode500Message;
                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return resp;
        }

        public async Task<Response> Update(SasOrderModel param)
        {
            Response resp = new Response();

            SasOrder Inbound = new SasOrder();

            SasOrderModel Outbound = new SasOrderModel();

            try
            {
                Inbound = _mapper.Map(param);

                #region Valiedation Zone

                Func<Task<Response>>[] validationFunctions = new Func<Task<Response>>[]
                {
                    async () => await _validate.SysCustomer(Inbound.SysCustomerId),
                };

                foreach (var func in validationFunctions)
                {
                    var validate = await func();

                    if (validate != null)
                    {
                        if (validate.Status == false)
                        {
                            resp.Status = Constants.StatusError;
                            resp.HttpCode = Constants.HttpCode400;
                            resp.HttpMessage = Constants.HttpCode204Message;
                            resp.Message = validate.Message;

                            return resp;
                        }
                    }
                }

                #endregion

                var objUpdate = await _service.GetById(Inbound.Id);

                if (objUpdate != null)
                {
                    var getStatus = await _sasStatusService.GetByValue(param.SasStatusValue);

                    if (getStatus == null)
                    {
                        resp.Status = Constants.StatusError;
                        resp.HttpCode = Constants.HttpCode400;
                        resp.HttpMessage = Constants.HttpCode204Message;
                        resp.Message = Constants.UpdateDataNotFound;

                        return resp;
                    }

                    Inbound.SasStatusId = getStatus.Id;
                    Inbound.CreatedBy = objUpdate.CreateBy;
                    Inbound.CreatedDate = objUpdate.CreateDate == null ? DateTime.Now.MarsX() : objUpdate.CreateDate.Value.MarsX();
                    Inbound.ServiceDate = objUpdate.ServiceDate;
                    Inbound.UpdatedDate = ServerTime;

                    Outbound = await _service.Update(Inbound);

                    if (Inbound.SasStatusValue != objUpdate.SasStatusValue)
                    {
                        var masterStatus = await _sasStatusService.GetByValue(param.SasStatusValue);

                        SasOrderTransaction transactionModel = new SasOrderTransaction();

                        transactionModel.SasOrderId = Inbound.Id;
                        transactionModel.SasStatusId = masterStatus.Id;
                        transactionModel.SasStatusValue = masterStatus.Value;
                        transactionModel.Remark = param.TransRemark;
                        transactionModel.CaecUserName = param.Username == null ? "caec" : param.Username;
                        transactionModel.CreatedBy = param.Username == null ? "caec" : param.Username;
                        transactionModel.UpdatedBy = param.Username == null ? "caec" : param.Username;
                        transactionModel.IsActive = true;
                        transactionModel.CreatedDate = ServerTime;
                        transactionModel.UpdatedDate = ServerTime;

                        var aaa = await _serviceTransaction.Create(transactionModel);
                    }

                    resp.Status = Constants.StatusSuccess;
                    resp.HttpCode = Constants.HttpCode200;
                    resp.HttpMessage = Constants.HttpCode200Message;
                    resp.Output = Outbound;
                }
                else
                {
                    resp.Status = Constants.StatusError;
                    resp.HttpCode = Constants.HttpCode400;
                    resp.HttpMessage = Constants.HttpCode204Message;
                    resp.Message = Constants.UpdateDataNotFound;
                }
            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.Message = Constants.HttpCode500Message;
                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return resp;
        }

        public async Task<Response> GetStatus(List<int> statusID, List<string> statusCode, Pagination param)
        {
            Response resp = new Response();

            List<SasOrderModel> Outbound = new List<SasOrderModel>();

            try
            {
                Outbound = await _service.GetStatus(statusID, statusCode, param);

                if (Outbound.Count > 0)
                {
                    resp.Status = Constants.StatusSuccess;
                    resp.HttpCode = Constants.HttpCode200;
                    resp.HttpMessage = Constants.HttpCode200Message;
                    resp.Output = Outbound;
                }
                else
                {
                    resp.Status = Constants.StatusError;
                    resp.HttpCode = Constants.HttpCode400;
                    resp.HttpMessage = Constants.HttpCode204Message;
                    resp.Message = Constants.RecordDataNotFound;
                }
            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.Message = Constants.HttpCode500Message;
                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return resp;
        }

        public async Task<Response> Cancel(SasOrderTransactionModel param)
        {
            Response resp = new Response();

            SasOrder Inbound = new SasOrder();

            SasOrderModel Outbound = new SasOrderModel();

            SasOrderTransaction InboundTransaction = new SasOrderTransaction();

            try
            {

                var objUpdate = await _service.GetById(param.SasOrderId);

                if (Inbound != null)
                {
                    Helper.TransferData_ClassA_to_ClassB<SasOrderModel, SasOrder>(objUpdate, ref Inbound, null);

                    Inbound.CreatedBy = objUpdate.CreateBy;
                    Inbound.CreatedDate = objUpdate.CreateDate.Value;

                    Inbound.SasStatusId = param.SasStatusId;
                    Inbound.SasStatusValue = param.SasStatusValue;
                    Inbound.UpdatedBy = param.UpdatedBy;
                    Inbound.UpdatedDate = ServerTime;

                    Outbound = await _service.Cancel(Inbound);

                    if (Outbound != null)
                    {
                        InboundTransaction.CreatedDate = ServerTime;
                        InboundTransaction.UpdatedDate = ServerTime;

                        InboundTransaction.CreatedBy = param.UpdatedBy;
                        InboundTransaction.UpdatedBy = param.UpdatedBy;
                        InboundTransaction.SasOrderId = param.SasOrderId;
                        InboundTransaction.SasStatusId = param.SasStatusId;
                        InboundTransaction.SasStatusValue = param.SasStatusValue;

                        InboundTransaction.Remark = param.Remark;
                        InboundTransaction.IsActive = true;

                        await _serviceTransaction.Create(InboundTransaction);
                    }

                    resp.Status = Constants.StatusSuccess;
                    resp.HttpCode = Constants.HttpCode200;
                    resp.HttpMessage = Constants.HttpCode200Message;
                    resp.Output = Outbound;
                }
                else
                {
                    resp.Status = Constants.StatusError;
                    resp.HttpCode = Constants.HttpCode400;
                    resp.HttpMessage = Constants.HttpCode204Message;
                    resp.Message = Constants.UpdateDataNotFound;
                }

            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.Message = Constants.HttpCode500Message;
                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return resp;
        }

        public async Task<Response> UpdateStatus(SasOrderUpdateStatusModel param)
        {
            Response resp = new Response();

            SasOrder Inbound = new SasOrder();

            SasOrderModel Outbound = new SasOrderModel();

            try
            {

                var objUpdate = await _service.GetById(param.Id);

                if (Inbound != null)
                {
                    var masterStatus = await _sasStatusService.GetByValue(param.SasStatusValue);

                    Helper.TransferData_ClassA_to_ClassB<SasOrderModel, SasOrder>(objUpdate, ref Inbound, null);

                    Inbound.CreatedBy = objUpdate.CreateBy;
                    Inbound.CreatedDate = objUpdate.CreateDate.Value;

                    Inbound.SasStatusId = masterStatus.Id;
                    Inbound.SasStatusValue = param.SasStatusValue;

                    Inbound.UpdatedBy = param.UpdateBy;
                    Inbound.UpdatedDate = ServerTime;

                    Outbound = await _service.Update(Inbound);

                    SasOrderTransaction transactionModel = new SasOrderTransaction();

                    transactionModel.SasOrderId = Inbound.Id;
                    transactionModel.SasStatusId = masterStatus.Id;
                    transactionModel.SasStatusValue = masterStatus.Value;
                    transactionModel.CaecUserName = param.UpdateBy == null ? "caec" : param.UpdateBy;
                    transactionModel.CreatedBy = param.UpdateBy == null ? "caec" : param.UpdateBy.ToString();
                    transactionModel.UpdatedBy = param.UpdateBy == null ? "caec" : param.UpdateBy.ToString();
                    transactionModel.CreatedDate = ServerTime;
                    transactionModel.UpdatedDate = ServerTime;
                    transactionModel.IsActive = true;
                    transactionModel.Remark = param.Remark;

                    await _serviceTransaction.Create(transactionModel);

                    resp.Status = Constants.StatusSuccess;
                    resp.HttpCode = Constants.HttpCode200;
                    resp.HttpMessage = Constants.HttpCode200Message;
                    resp.Output = Outbound;
                }
                else
                {
                    resp.Status = Constants.StatusError;
                    resp.HttpCode = Constants.HttpCode400;
                    resp.HttpMessage = Constants.HttpCode204Message;
                    resp.Message = Constants.UpdateDataNotFound;
                }

            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.Message = Constants.HttpCode500Message;
                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return resp;
        }

        public async Task<Response> UpdateStatusWithBranchCode(SasOrderUpdateStatusWithBranchCodeModel param)
        {
            Response resp = new Response();

            SasOrder Inbound = new SasOrder();

            SasOrderModel Outbound = new SasOrderModel();

            try
            {
                var objUpdate = await _service.GetById(param.Id);

                if (objUpdate != null)
                {
                    var masterStatus = await _sasStatusService.GetByValue(param.SasStatusValue);

                    if (masterStatus == null)
                    {
                        resp.Status = Constants.StatusError;
                        resp.HttpCode = Constants.HttpCode400;
                        resp.HttpMessage = Constants.HttpCode204Message;
                        resp.Message = Constants.UpdateDataNotFound;

                        return resp;
                    }

                    Helper.TransferData_ClassA_to_ClassB<SasOrderModel, SasOrder>(objUpdate, ref Inbound, null);

                    Inbound.CreatedBy = objUpdate.CreateBy;
                    Inbound.CreatedDate = objUpdate.CreateDate.Value;

                    Inbound.SasStatusId = masterStatus.Id;
                    Inbound.SasStatusValue = param.SasStatusValue;
                    Inbound.CaecBranchCode = param.BranchCode;
                    Inbound.UpdatedBy = objUpdate.UpdateBy;
                    Inbound.UpdatedDate = ServerTime;

                    Outbound = await _service.Update(Inbound);

                    SasOrderTransaction transactionModel = new SasOrderTransaction();

                    transactionModel.SasOrderId = Inbound.Id;
                    transactionModel.SasStatusId = masterStatus.Id;
                    transactionModel.SasStatusValue = masterStatus.Value;
                    transactionModel.CaecUserName = param.CaecUserName == null ? "caec" : param.CaecUserName;
                    transactionModel.CreatedBy = param.CaecUserName == null ? "caec" : param.CaecUserName.ToString();
                    transactionModel.UpdatedBy = param.CaecUserName == null ? "caec" : param.CaecUserName.ToString();
                    transactionModel.IsActive = true;
                    transactionModel.Remark = param.Remark;

                    var aaa = await _serviceTransaction.Create(transactionModel);

                    resp.Status = Constants.StatusSuccess;
                    resp.HttpCode = Constants.HttpCode200;
                    resp.HttpMessage = Constants.HttpCode200Message;
                    resp.Output = Outbound;
                }
                else
                {
                    resp.Status = Constants.StatusError;
                    resp.HttpCode = Constants.HttpCode400;
                    resp.HttpMessage = Constants.HttpCode204Message;
                    resp.Message = Constants.UpdateDataNotFound;
                }

            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.Message = Constants.HttpCode500Message;
                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return resp;
        }

        public async Task<Response> UpdateStatusWithCaecJobNo(SasOrderUpdateStatusWithCaecJobNoModel param)
        {
            Response resp = new Response();

            SasOrder Inbound = new SasOrder();

            SasOrderModel Outbound = new SasOrderModel();

            try
            {
                var objUpdate = await _service.GetById(param.Id);

                if (objUpdate != null)
                {
                    var masterStatus = await _sasStatusService.GetByValue(param.SasStatusValue);

                    if (masterStatus == null)
                    {
                        resp.Status = Constants.StatusError;
                        resp.HttpCode = Constants.HttpCode400;
                        resp.HttpMessage = Constants.HttpCode204Message;
                        resp.Message = Constants.UpdateDataNotFound;

                        return resp;
                    }

                    Helper.TransferData_ClassA_to_ClassB<SasOrderModel, SasOrder>(objUpdate, ref Inbound, null);

                    Inbound.CreatedBy = objUpdate.CreateBy;
                    Inbound.CreatedDate = objUpdate.CreateDate.Value;

                    Inbound.SasStatusId = masterStatus.Id;
                    Inbound.SasStatusValue = param.SasStatusValue;
                    Inbound.CaecJobNo = param.CaecJobNo;
                    Inbound.UpdatedBy = objUpdate.UpdateBy;
                    Inbound.UpdatedDate = ServerTime;

                    Outbound = await _service.Update(Inbound);

                    SasOrderTransaction transactionModel = new SasOrderTransaction();

                    transactionModel.SasOrderId = Inbound.Id;
                    transactionModel.SasStatusId = masterStatus.Id;
                    transactionModel.SasStatusValue = masterStatus.Value;
                    transactionModel.CaecUserName = param.CaecUserName == null ? "caec" : param.CaecUserName;
                    transactionModel.CreatedBy = param.CaecUserName == null ? "caec" : param.CaecUserName.ToString();
                    transactionModel.UpdatedBy = param.CaecUserName == null ? "caec" : param.CaecUserName.ToString();
                    transactionModel.CreatedDate = ServerTime;
                    transactionModel.UpdatedDate = ServerTime;
                    transactionModel.IsActive = true;
                    transactionModel.Remark = param.Remark;

                    var aaa = await _serviceTransaction.Create(transactionModel);

                    resp.Status = Constants.StatusSuccess;
                    resp.HttpCode = Constants.HttpCode200;
                    resp.HttpMessage = Constants.HttpCode200Message;
                    resp.Output = Outbound;
                }
                else
                {
                    resp.Status = Constants.StatusError;
                    resp.HttpCode = Constants.HttpCode400;
                    resp.HttpMessage = Constants.HttpCode204Message;
                    resp.Message = Constants.UpdateDataNotFound;
                }

            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.Message = Constants.HttpCode500Message;
                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return resp;
        }

        public async Task<Response> AddPaymentSlipImage(int orderId, IFormFile? Image)
        {
            Response resp = new Response();

            SasOrder Inbound = new SasOrder();

            SasOrderModel Outbound = new SasOrderModel();

            SasOrderPayment inboundPayment = new SasOrderPayment();

            SasOrderPaymentModel OutboundPayment = new SasOrderPaymentModel();

            SasOrderTransaction InboundTransaction = new SasOrderTransaction();


            try
            {
                var objUpdate = await _service.GetById(orderId);

                inboundPayment.SasOrderId = orderId;
                inboundPayment.CreatedBy = objUpdate.CreateBy;
                inboundPayment.UpdatedBy = objUpdate.UpdateBy;
                inboundPayment.CreatedDate = objUpdate.CreateDate.Value;
                inboundPayment.UpdatedDate = objUpdate.UpdateDate.Value;


                OutboundPayment = await _serviceOrderPayment.AddPaymentSlipImage(inboundPayment, Image);

                if (OutboundPayment != null)
                {
                    Helper.TransferData_ClassA_to_ClassB<SasOrderModel, SasOrder>(objUpdate, ref Inbound, null);

                    Inbound.CreatedBy = objUpdate.CreateBy;
                    Inbound.CreatedDate = objUpdate.CreateDate.Value;

                    Inbound.SasStatusId = 4;
                    Inbound.SasStatusValue = "PAYMENT";
                    Inbound.UpdatedBy = objUpdate.UpdateBy;
                    Inbound.UpdatedDate = ServerTime;

                    Outbound = await _serviceOrderPayment.UpdatePayment(Inbound);

                    if (Outbound != null)
                    {
                        InboundTransaction.CreatedDate = ServerTime;
                        InboundTransaction.UpdatedDate = ServerTime;

                        InboundTransaction.CreatedBy = objUpdate.CreateBy;
                        InboundTransaction.UpdatedBy = objUpdate.CreateBy;
                        InboundTransaction.SasOrderId = Outbound.Id;
                        InboundTransaction.SasStatusId = Outbound.SasStatusId;
                        InboundTransaction.SasStatusValue = Outbound.SasStatusValue;

                        //InboundTransaction.Remark = Outbound.Remark;
                        InboundTransaction.IsActive = true;

                        await _serviceTransaction.Create(InboundTransaction);
                    }

                    resp.Status = Constants.StatusSuccess;
                    resp.HttpCode = Constants.HttpCode200;
                    resp.HttpMessage = Constants.HttpCode200Message;
                    resp.Output = Outbound;
                }
                else
                {
                    resp.Status = Constants.StatusError;
                    resp.HttpCode = Constants.HttpCode400;
                    resp.HttpMessage = Constants.HttpCode204Message;
                    resp.Message = Constants.RecordDataNotFound;
                }

            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.Message = Constants.HttpCode500Message;
                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return resp; throw new NotImplementedException();
        }


        public async Task<Response> GetByJobRef(string jobRef)
        {
            Response resp = new Response();

            SasOrderModel Outbound = new SasOrderModel();

            try
            {
                Outbound = await _service.GetByJobRef(jobRef);

                if (Outbound != null)
                {
                    //var tranPaid = Outbound.SasOrderTransactionModel?.FirstOrDefault(f => f.SasStatusValue == "PAYMENT");
                    //if (tranPaid != null)
                    //{
                    //    var paymentModel = await _serviceOrderPayment.GetByOrderId(Outbound.Id);
                    //    if (paymentModel != null)
                    //    {
                    //        tranPaid.ImageUrl = paymentModel.ImageUrl;
                    //    }
                    //}
                    resp.Status = Constants.StatusSuccess;
                    resp.HttpCode = Constants.HttpCode200;
                    resp.HttpMessage = Constants.HttpCode200Message;
                    resp.Output = Outbound;
                }
                else
                {
                    resp.Status = Constants.StatusError;
                    resp.HttpCode = Constants.HttpCode400;
                    resp.HttpMessage = Constants.HttpCode204Message;
                    resp.Message = Constants.RecordDataNotFound;
                }

            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.Message = Constants.HttpCode500Message;
                resp.InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return resp;
        }
    }
}

