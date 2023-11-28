using DefaultSite.Models.Domain;
using DefaultSite.Services.Interfaces;
using DefaultSite.Web.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DefaultSite.Web.Api.Controllers
{
    [Route("api/maze")]
    [ApiController]
    public class MazeApiController : BaseApiController
    {
        IMazeAIService _service;

        public MazeApiController(IMazeAIService service, ILogger<MazeApiController> logger) : base(logger) {
            _service = service;
        }

        [HttpPost]
        public ActionResult<SuccessResponse> CreateBoard(int[][] grid)
        {
            int iCode = 201;
            BaseResponse response;
            try
            {
                _service.InitiateBoard(grid);
                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                iCode = 500;
                response = new ErrorResponse("Error Intializing Board");
                base.Logger.LogError(ex.Message);
            }
            return StatusCode(iCode, response);
        }

        [HttpPut]
        public ActionResult<SuccessResponse> ResetBoard()
        {
            int iCode = 200;
            BaseResponse response;
            try
            {
                _service.ResetBoard();
                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                iCode = 500;
                response = new ErrorResponse("Error Intializing Board");
                base.Logger.LogError(ex.Message);
            }
            return StatusCode(iCode, response);
        }

        [HttpGet]
        public ActionResult<ItemResponse<MoveAIResponse>> MakeMove()
        {
            int iCode = 200;
            BaseResponse response;
            try
            {
                response = new ItemResponse<MoveAIResponse>
                {
                    Item = _service.MakeMove()
                };
            }
            catch (Exception ex)
            {
                iCode = 500;
                response = new ErrorResponse("Error Intializing Board");
                base.Logger.LogError(ex.Message);
            }
            return StatusCode(iCode, response);
        }
    }
}
