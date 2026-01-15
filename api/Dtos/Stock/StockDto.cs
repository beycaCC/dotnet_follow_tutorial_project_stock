using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;

namespace api.Dtos.Stock
{
    // In this example, StockDto helps cleaned up all comments from stocks
    // making sure that the APIs for stocks will only return data in stock table.
    public class StockDto 
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }  // could be as large as trillion, integer is not long enough
        public List<CommentDto> Comments { get; set; }  // add this line to making sure that calling stock API's will also include the related comments
    }
}