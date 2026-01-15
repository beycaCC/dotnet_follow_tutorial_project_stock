using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class QueryObject
    {
        public String? Symbol { get; set; } = null;
        public String? CompanyName { get; set; } = null;
        public String? SortBy { get; set; } = null;
        public bool isDecending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}