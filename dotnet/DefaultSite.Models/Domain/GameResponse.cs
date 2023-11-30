using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultSite.Models.Domain
{
    public class GameResponse
    {
        public List<int> StateMoves { get; set; }
        public bool HasFoundExit { get; set; }
    }
}
