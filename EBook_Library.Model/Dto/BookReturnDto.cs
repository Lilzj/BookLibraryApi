using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBook_Library.Model.Dto
{
    public class BookReturnDto
    {
        public string BookId { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string PublishYear { get; set; }
        public decimal CoverPrice { get; set; }
        public bool AvailabilityStatus { get; set; }
        public IEnumerable<BookActivityDto> bookActivities { get; set; }
    }
}
