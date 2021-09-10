using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBook_Library.Model.Dto
{
    public class CheckoutDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string NIN { get; set; }
        public DateTime CheckOutDate { get; set; } = DateTime.Now;
        public DateTime ExpectedDateOfReturn { get; set; }
    }
}
