using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Book_Library.Models
{
    public class Book
    {
        public string BookId { get; set; } = new Guid().ToString();
        public string Title { get; set; }
        public string  ISBN { get; set; }
        public string  PublishYear { get; set; }
        public decimal CoverPrice { get; set; }
        public int AvailabilityStatus { get; set; }
    }
}
