using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    // A jointable to connect stock and user and achieve many-to-many relationship
    [Table("Portfolios")]
    public class Portfolio
    {
        public string AppUserId { get; set; }
        public int StockId { get; set; }

        // Navigation property (just for developer)
        public AppUser AppUser { get; set; }
        public Stock Stock { get; set; }
    }
}