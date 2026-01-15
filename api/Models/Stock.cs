using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Stock
    {
        // ("prop" + tab)
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Purchase { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }  // could be as large as trillion, integer is not long enough

        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}