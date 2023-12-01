﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultSite.Models.Domain
{
    public class MazeResponse
    {
        public List<GameResponse> GamesData { get; set; }

        public bool FoundMostEfficientPath { get; set; }
    }
}
