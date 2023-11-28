using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultSite.Models.Domain
{
    public class MoveBoardResponse
    {
        public int NextState { get; set; }
        public int Reward { get; set; }
    }
}
