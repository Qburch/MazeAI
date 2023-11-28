using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultSite.Models.Domain
{
    public class MazeResponse
    {
        public List<int>? FinalEpisodeMoves { get; set; }
        public bool HasFoundExit { get; set; }
    }
}
