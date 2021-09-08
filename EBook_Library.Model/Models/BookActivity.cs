using EBook_Library.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBook_Library.Models
{
    public class BookActivity
    {
        public string BookActivityId { get; set; } = new Guid().ToString();
        public Book Books { get; set; }
        public AppUser AppUsers { get; set; }
        public decimal PenaltyFee { get; set; }
        public int NoOfDaysLate { get; set; }
        public DateTime CheckOutDate { get; set; } = DateTime.Now;
        public DateTime CheckInDate { get; set; }
        public DateTime ExpectedDateOfReturn { get; set; }

        public BookActivity()
        {
            ExpectedDateOfReturn = Utilities.AddBusinessDays(CheckOutDate, 10);
        }
    }
}
