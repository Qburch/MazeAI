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
        MoveAIResponse MakeMove();
        void ResetBoard();
    }
}
