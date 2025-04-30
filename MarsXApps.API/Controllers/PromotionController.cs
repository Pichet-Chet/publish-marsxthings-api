using MarsXApps.API.Helper;
using MarsXApps.Models;
using MarsXApps.Models.Constants;
using MarsXApps.Models.Customs;
using MarsXApps.Models.Filter.Promotion;
using MarsXApps.Module.Promotion.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MarsXApps.API.Controllers
{
    [Route("api/[controller]")]
    public class PromotionController : Controller
    {
        private readonly IPromotionRepository _promotionRepository;

        public PromotionController(IPromotionRepository promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> GetAll([FromQuery] PromotionFilter param)
        {
            Response resp = new Response();

            try
            {
                resp = await _promotionRepository.GetAll(param);
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
                resp = await _promotionRepository.GetById(Id);
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
        public async Task<IActionResult> Create([FromBody] PromotionModel param)
        {
            Response resp = new Response();

            try
            {
                resp = await _promotionRepository.Create(param);

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
        public async Task<IActionResult> Update([FromBody] PromotionModel param)
        {
            Response resp = new Response();

            try
            {

                resp = await _promotionRepository.Update(param);

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
        [Route("GetByCode/{Code}")]
        public async Task<IActionResult> GetByCode(string Code)
        {
            Response resp = new Response();

            try
            {
                resp = await _promotionRepository.GetByCode(Code);
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


        [HttpPost("CreatePromotionWithWidget")]
        public async Task<IActionResult> CreatePromotionWithWidget([FromBody] PromotionModel param)
        {
            Response resp = new Response();

            try
            {
                resp = await _promotionRepository.CreatePromotionWithWidget(param);

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

        [HttpPut("UpdatePromotionWithWidget")]
        public async Task<IActionResult> UpdatePromotionWithWidget([FromBody] PromotionModel param)
        {
            Response resp = new Response();
            try
            {
                resp = await _promotionRepository.UpdatePromotionWithWidget(param);
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

