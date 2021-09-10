using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBook_Library.Common
{
    public class UserAuth
    {
        public bool IsAuthenticated { get; set; } = false;
        public bool IsAdmin { get; set; } = false;
    }
}
