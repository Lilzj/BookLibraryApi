using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EBook_Library.Models
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string  ISBN { get; set; }
        public string  PublishYear { get; set; }
        public decimal CoverPrice { get; set; }
        public int AvailabilityStatus { get; set; }
    }
}
