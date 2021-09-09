using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBook_Library.Models
{
    public class BookActivity
    {
        public string BookActivityId { get; set; }
        public Book Book { get; set; }
        public AppUser AppUser { get; set; }
        public decimal PenaltyFee { get; set; }
        public int NoOfDaysLate { get; set; }
        public DateTime CheckOutDate { get; set; } = DateTime.Now;
        public DateTime CheckInDate { get; set; }
        public DateTime ExpectedDateOfReturn { get; set; }

        public BookActivity()
        {
            ExpectedDateOfReturn = AddBusinessDays(CheckOutDate, 10);
        }


        private DateTime AddBusinessDays(DateTime dt, int nDays)
        {
            int weeks = nDays / 5;
            nDays %= 5;
            while (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
                dt = dt.AddDays(1);

            while (nDays-- > 0)
            {
                dt = dt.AddDays(1);
                if (dt.DayOfWeek == DayOfWeek.Saturday)
                    dt = dt.AddDays(2);
            }
            return dt.AddDays(weeks * 7);
        }
    }
}
