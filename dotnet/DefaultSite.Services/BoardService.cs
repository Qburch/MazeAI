using DefaultSite.Models.Domain;
using DefaultSite.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultSite.Services
{
    public class BoardService : IBoardService
    {
        private int[][] _board = new int[1][];

        private int _width = 0;
        private int _height = 0;

        private int _startingPosition = 0;
        private int _winningPosition = 0;
        private int _state = 0;

        public void SetBoard(int[][] grid)
        {
            _board = grid;
            _height = grid.Length;
            _width = grid[0].Length;
            _startingPosition = _width * 1 + 1; // top left without walls
            _state = _startingPosition;
            _winningPosition = _width * (_height) - 1; //bottom right with walls
        }

        public void ResetBoard()
        {
            _state = _startingPosition;
        }

        public int GetState()
        {
            return _state;
        }

        public List<int> GetPossibleMoves()
        {
            List<int> possibleMoves = new();

            int col = _state % _width;
            int row = _state / _width;

            if (row - 1 > -1 && _board[row - 1][col] == 0) possibleMoves.Add(0);
            if (col + 1 < _width && _board[row][col + 1] == 0) possibleMoves.Add(1);
            if (col - 1 > -1 && _board[row][col - 1] == 0) possibleMoves.Add(2);
            if (row + 1 < _height && _board[row + 1][col] == 0) possibleMoves.Add(3);

            return possibleMoves;
        }

        public MoveBoardResponse MakeMove(int action)
        {
            if (action == 0) _state -= _width;
            else if (action == 1) _state += 1;
            else if (action == 2) _state += _width;
            else if (action == 3) _state -= 1;

            return new MoveBoardResponse(){
                Reward = _state == _winningPosition ? 1 : 0,
                NextState = _state,
            };
        }

        public int GetRandomMove(Random rand)
        {
            // Get random choice from GetPossibleMoves()
            List<int> possibleMoves = GetPossibleMoves();
            int idx = rand.Next(0, possibleMoves.Count);
            return possibleMoves[idx];
        }

    }
}
