using DefaultSite.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultSite.Services.Interfaces
{
    public interface IBoardService
    {
        void SetBoard(int[][] grid);
        void ResetBoard();
        int GetState();
        List<int> GetPossibleMoves();
        public int GetRandomMove(Random rand);
        MoveBoardResponse MakeMove(int action);
    }
}
