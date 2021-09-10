using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBook_Library.Model.Dto
{
    public class CheckoutReturnDto
    {
        public string BookActivityId { get; set; }
        public string BookId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string NIN { get; set; }
        public decimal PenaltyFee { get; set; } 
        public int NoOfDaysLate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime? CheckInDate { get; set; } = null;
        public DateTime ExpectedDateOfReturn { get; set; }
    }
}
