using DefaultSite.Models.Domain;
using DefaultSite.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultSite.Services
{
    public class MazeService
    {
        private IMazeAIService _service;
        public MazeService(IMazeAIService service)
        {
            _service = service;
        }

        public MazeResponse RunMaze(int[][] grid)
        {
            MazeResponse response = new MazeResponse()
            {
                HasFoundExit = true,
            };
            _service.InitiateBoard(grid);
            int prevSteps = int.MinValue;
            int consecutiveEqualSteps = 0;

            int startingPosition = grid[0].Length * 1 + 1;
            List<int> moves = new List<int>();
            for (int i = 0; i < 100; i++)
            {
                if (i > 0)
                {
                    _service.ResetBoard();
                    moves.Clear();
                }
                moves.Add(startingPosition);
                bool done = false;
                int steps = 0;
                while (!done && steps < 200)
                {
                    MoveAIResponse moveResponse = _service.MakeMove();
                    done = moveResponse.IsDone;
                    moves.Add(moveResponse.NextState);
                    steps++;
                }
                if (steps == 200)
                {
                    response.HasFoundExit = false;
                    break;
                }
                if (prevSteps == steps) consecutiveEqualSteps++;
                else consecutiveEqualSteps = 0;

                if(consecutiveEqualSteps == 4)
                {
                    response.FinalEpisodeMoves = move;
                    break;
                }
                prevSteps = steps;
            }
            return response;
        }
    }
}
