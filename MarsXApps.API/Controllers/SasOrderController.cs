using MarsXApps.API.Helper;
using MarsXApps.Models;
using MarsXApps.Models.Constants;
using MarsXApps.Models.Customs;
using MarsXApps.Models.Customs.Request.SasOrder;
using MarsXApps.Models.Filter.ServiceAfterSale;
using MarsXApps.Module.ServiceAfterSale.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarsXApps.API.Controllers
{
    [Route("api/[controller]")]
    public class SasOrderController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly ISasOrderRepositories _repositories;

        public SasOrderController(ISasOrderRepositories repositories, IConfiguration configuration)
        {
            _repositories = repositories;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] SasOrderFilter param)
        {
            Response resp = new Response();

            try
            {
               resp = await _repositories.GetAll(param);
            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.HttpMessage = Constants.HttpCode500Message;
                resp.Message = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return StatusCode(resp.HttpCode, AppHelper.GetResponseController(resp));
        }

        [HttpGet]
        [Route("header")]
        public async Task<IActionResult> GetAllHeader([FromQuery] SasOrderFilter param)
        {
            Response resp = new Response();

            try
            {
                resp = await _repositories.GetHeader(param);
            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.HttpMessage = Constants.HttpCode500Message;
                resp.Message = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return StatusCode(resp.HttpCode, AppHelper.GetResponseController(resp));
        }

        [HttpGet]
        [Route("detail/{headerId}")]
        public async Task<IActionResult> GetDetailOrder(int headerId)
        {
            Response resp = new Response();

            try
            {
                resp = await _repositories.GetDetail(headerId);
            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.HttpMessage = Constants.HttpCode500Message;
                resp.Message = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return StatusCode(resp.HttpCode, AppHelper.GetResponseController(resp));
        }

        [HttpGet]
        [Route("detail/{headerId}/payment")]
        public async Task<IActionResult> GetDetailOrderPayment(int headerId)
        {
            Response resp = new Response();

            try
            {
                resp = await _repositories.GetDetailPayment(headerId);
            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.HttpMessage = Constants.HttpCode500Message;
                resp.Message = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return StatusCode(resp.HttpCode, AppHelper.GetResponseController(resp));
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            Response resp = new Response();

            try
            {
                resp = await _repositories.GetById(Id);
            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.HttpMessage = Constants.HttpCode500Message;
                resp.Message = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return StatusCode(resp.HttpCode, AppHelper.GetResponseController(resp));
        }

        [HttpGet]
        [Route("byJobRef/{jobRef}")]
        public async Task<IActionResult> GetByJobRef(string jobRef)
        {
            Response resp = new Response();

            try
            {
                resp = await _repositories.GetByJobRef(jobRef);
            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.HttpMessage = Constants.HttpCode500Message;
                resp.Message = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return StatusCode(resp.HttpCode, AppHelper.GetResponseController(resp));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SasOrderModel param)
        {
            Response resp = new Response();

            try
            {
                resp = await _repositories.Create(param);
            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.HttpMessage = Constants.HttpCode500Message;
                resp.Message = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return StatusCode(resp.HttpCode, AppHelper.GetResponseController(resp));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SasOrderModel param)
        {
            Response resp = new Response();

            try
            {
                resp = await _repositories.Update(param);
            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.HttpMessage = Constants.HttpCode500Message;
                resp.Message = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return StatusCode(resp.HttpCode, AppHelper.GetResponseController(resp));
        }

        [HttpGet]
        [Route("status")]
        public async Task<IActionResult> GetByStatus(List<int> statusID, List<string> statusCode, Pagination param)
        {
            Response resp = new Response();

            try
            {
                resp = await _repositories.GetStatus(statusID, statusCode, param);
            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.HttpMessage = Constants.HttpCode500Message;
                resp.Message = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return StatusCode(resp.HttpCode, AppHelper.GetResponseController(resp));
        }

        [HttpPut]
        [Route("cancel")]
        public async Task<IActionResult> Cancel([FromBody] SasOrderTransactionModel param)
        {
            Response resp = new Response();

            try
            {
                resp = await _repositories.Cancel(param);
            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.HttpMessage = Constants.HttpCode500Message;
                resp.Message = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return StatusCode(resp.HttpCode, AppHelper.GetResponseController(resp));
        }

        [HttpPut]
        [Route("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus([FromBody] SasOrderUpdateStatusModel param)
        {
            Response resp = new Response();

            try
            {
                resp = await _repositories.UpdateStatus(param);
            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.HttpMessage = Constants.HttpCode500Message;
                resp.Message = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return StatusCode(resp.HttpCode, AppHelper.GetResponseController(resp));
        }

        [HttpPut]
        [Route("UpdateStatusWithBranchCode")]
        public async Task<IActionResult> UpdateStatus([FromBody] SasOrderUpdateStatusWithBranchCodeModel param)
        {
            Response resp = new Response();

            try
            {
                resp = await _repositories.UpdateStatusWithBranchCode(param);
            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.HttpMessage = Constants.HttpCode500Message;
                resp.Message = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return StatusCode(resp.HttpCode, AppHelper.GetResponseController(resp));
        }

        [HttpPut]
        [Route("UpdateStatusWithCaecJobNo")]
        public async Task<IActionResult> UpdateJobNo([FromBody] SasOrderUpdateStatusWithCaecJobNoModel param)
        {
            Response resp = new Response();

            try
            {
                resp = await _repositories.UpdateStatusWithCaecJobNo(param);
            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.HttpMessage = Constants.HttpCode500Message;
                resp.Message = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return StatusCode(resp.HttpCode, AppHelper.GetResponseController(resp));
        }

        [HttpPut]
        [Route("Image")]
        public async Task<IActionResult> UploadSlip(int orderId, IFormFile? FileUpload)
        {
            Response resp = new Response();

            try
            {
                resp = await _repositories.AddPaymentSlipImage(orderId, FileUpload);
            }
            catch (Exception ex)
            {
                resp.Status = Constants.StatusError;
                resp.HttpCode = Constants.HttpCode500;
                resp.HttpMessage = Constants.HttpCode500Message;
                resp.Message = ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
            }

            return StatusCode(resp.HttpCode, AppHelper.GetResponseController(resp));
        }
    }
}

