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
        private int _startingPosition;
        private int _winningPosition;

        private int _cols;
        private int _rows;

        private int[][] _board;
        private int _state;

        public BoardService(int[][] grid)
        {
            _board = grid;
            _rows = grid.Length;
            _cols = grid[0].Length;
            _startingPosition = _cols * 1 + 1; // top left with walls
            _state = _startingPosition;
            _winningPosition = _cols * (_rows) - 2; // one from bottom right in wall
        }

        public void ResetBoard()
        {
            _state = _startingPosition;
        }

        public int GetState()
        {
            return _state;
        }

        public List<int> GetPossibleMoves(int aState)
        {
            List<int> possibleMoves = new();

            int col = aState % _cols;
            int row = aState / _cols;

            if (row - 1 > -1 && _board[row - 1][col] == 0) possibleMoves.Add(0);
            if (col + 1 < _cols && _board[row][col + 1] == 0) possibleMoves.Add(1);
            if (col - 1 > -1 && _board[row][col - 1] == 0) possibleMoves.Add(2);
            if (row + 1 < _rows && _board[row + 1][col] == 0) possibleMoves.Add(3);

            return possibleMoves;
        }

        public MoveBoardResponse MakeMove(int action)
        {
            if (action == 0) _state -= _cols;
            else if (action == 1) _state += 1;
            else if (action == 2) _state -= 1;
            else if (action == 3) _state += _cols;

            return new MoveBoardResponse(){
                Reward = _state == _winningPosition ? 1 : 0,
                NextState = _state,
            };
        }

        public int GetRandomMove(Random rand)
        {
            List<int> possibleMoves = GetPossibleMoves(_state);
            int idx = rand.Next(0, possibleMoves.Count);
            return possibleMoves[idx];
        }

    }
}
