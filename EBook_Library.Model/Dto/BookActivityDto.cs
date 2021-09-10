using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBook_Library.Model.Dto
{
    public class BookActivityDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime CheckInDate { get; set; } 
    }
}
