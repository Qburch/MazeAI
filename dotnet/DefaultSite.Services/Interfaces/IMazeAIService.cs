using DefaultSite.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultSite.Services.Interfaces
{
    public interface IMazeAIService
    {
        void InitiateBoard(int[][] grid);
        MoveAIResponse MakeMove();
        void ResetBoard();
    }
}
