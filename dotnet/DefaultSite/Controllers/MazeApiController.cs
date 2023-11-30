using DefaultSite.Models.Domain;
using DefaultSite.Services;
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
        public MazeApiController(ILogger<MazeApiController> logger) : base(logger) {
        }

        [HttpPost]
        public ActionResult<ItemResponse<MazeResponse>> GetMostEfficientPath(int[][] grid)
        {
            int iCode = 200;
            BaseResponse response;
            try
            {
                MazeResponse data = RunMaze(grid);
                response = new ItemResponse<MazeResponse> { Item = data };
            }
            catch (Exception ex)
            {
                iCode = 500;
                response = new ErrorResponse("Error Intializing Board");
                base.Logger.LogError(ex.Message);
            }
            return StatusCode(iCode, response);
        }

        private MazeResponse RunMaze(int[][] grid)
        {
            IMazeAIService mazeService = new MazeAIService(grid);

            MazeResponse response = new MazeResponse();
            response.GamesData = new List<GameResponse>();

            int minSteps = 200;
            int consecutiveMin = 0;

            List<int> moves = new List<int>();
            for (int i = 0; i < 200; i++)
            {
                GameResponse gameResponse = new GameResponse();
                gameResponse.StateMoves = new List<int>();

                if (i > 0) mazeService.ResetBoard();

                bool done = false;
                int steps = 0;
                while (!done)
                {
                    MoveAIResponse moveResponse = mazeService.MakeMove();
                    done = moveResponse.IsDone;
                    gameResponse.StateMoves.Add(moveResponse.NextState);
                    steps++;
                }
                gameResponse.HasFoundExit = done;

                response.GamesData.Add(gameResponse);

                if (steps == minSteps) consecutiveMin++;
                else consecutiveMin = 0;

                if (steps < minSteps) minSteps = steps;
                
                if(consecutiveMin == 5)
                {
                    response.FoundMostEfficientPath = true;
                    break;
                }
            }
            return response;
        }
    }
}
