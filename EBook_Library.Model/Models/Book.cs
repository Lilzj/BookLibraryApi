using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EBook_Library.Models
{
    public class Book
    {
        public string BookId { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string  ISBN { get; set; }
        public string  PublishYear { get; set; }
        public decimal CoverPrice { get; set; }
        public bool AvailabilityStatus { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateModeified { get; set; } = DateTime.Now;
        public IEnumerable<BookActivity> bookActivities { get; set; }
    }
}
