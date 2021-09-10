using EBook_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBook_Library.Core.Interface
{
    public interface ICheckOutRepository
    {
        Task<bool> BookCheckOutAsync(BookActivity bookActivity);
        Task<BookActivity> GetBookActivityByIdAsync(string id);
        Task<bool> UpdateAsync(BookActivity bookActivity);
    }
}
