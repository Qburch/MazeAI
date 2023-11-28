using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultSite.Models.Domain
{
    public class MoveAIResponse
    {
        public int NextState { get; set; }
        public bool IsDone { get; set; }
    }
}
