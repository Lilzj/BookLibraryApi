using EBook_Library.Model.Dto;
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
        Task<bool> AddBookAsync(Book book);
        Task<Book> GetBookByIdAsync(string bookId);
        Task<IEnumerable<Book>> SearchBook(SearchDto query);
        Task<IEnumerable<Book>> GetAllBooksAsync();

    }
}
