﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Business.Entities.Search
{
    public class SearchRequest
    {
        public string SearchField { get; set; }

        public bool MatchAllKeywords { get; set; }

        public bool IsWildCardSearch { get; set; }

        public string Query { get; set; }

        public int Count { get; set; }
    }
}
