using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Comments")] // define the table name manually, helps especially in many-to-many relationship
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? StockId { get; set; }
        // Navigation property
        public Stock? Stock { get; set; }
        
        // To make one-to-one connection and enable to show the user under each comment in api json output
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}