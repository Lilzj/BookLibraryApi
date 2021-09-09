using EBook_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBook_Library.Core.Interface
{
    public interface IBookRepository
    {
        public Task<bool> AddBookAsync(Book book);
        public Task<Book> GetBookByIdAsync(string bookId);
    }
}
