using DefaultSite.Models.Domain;
using DefaultSite.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultSite.Services
{
    public class MazeAIService : IMazeAIService
    {
        private Random _rand = new Random();

        private double _epsilon = 0.175;
        private const double _gamma = 0.8;
        private const double _decay = 0.05;


        private double[][] _qTable;
        private IBoardService _board;

        public MazeAIService(int[][] grid)
        {
            _board = new BoardService(grid);

            int totalStates = grid.Length * grid[0].Length;
            int actionsPerState = 4;

            _qTable = new double[totalStates][];
            for (int i = 0; i < totalStates; i++)
            {
                _qTable[i] = Enumerable.Repeat(0, actionsPerState).Select(x => _rand.NextDouble()).ToArray();
            }

        }

        public void ResetBoard()
        {
            _board.ResetBoard();
            _epsilon -= _epsilon * _decay;
        }

        public MoveAIResponse MakeMove()
        {
            int state = _board.GetState();
            int action;
            if (_rand.NextDouble() < _epsilon) action = _board.GetRandomMove(_rand);
            else action = Array.IndexOf(_qTable[state], GetMaxQ(state));

            MoveBoardResponse moveBoardResponse = _board.MakeMove(action);
            _qTable[state][action] = moveBoardResponse.Reward + _gamma * GetMaxQ(moveBoardResponse.NextState);
            
            return new MoveAIResponse()
            {
                IsDone = moveBoardResponse.Reward == 1,
                NextState = moveBoardResponse.NextState,
            };
        }

        private double GetMaxQ(int aState)
        {
            double maxQ = double.MinValue;
            foreach(int action in _board.GetPossibleMoves(aState))
            {
                maxQ = Math.Max(maxQ, _qTable[aState][action]);
            }
            return maxQ;
        }
    }
}
