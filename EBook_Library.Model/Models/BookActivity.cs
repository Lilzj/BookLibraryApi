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
        public Guid BookActivityId { get; set; }
        public Book Book { get; set; }
        public AppUser AppUser { get; set; }
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
