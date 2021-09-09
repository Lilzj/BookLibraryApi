using EBook_Library.Core.Interface;
using EBook_Library.Data;
using EBook_Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBook_Library.Core.Implementation
{
    public class BookRepository : IBookRepository
    {
        private readonly EBookContext _ctx;

        public BookRepository(EBookContext eBookContext)
        {
            _ctx = eBookContext;
        }

        private async Task<bool> SavedAsync()
        {
            bool ValueToReturned;
            if (await _ctx.SaveChangesAsync() > 0)
                ValueToReturned = true;
            else
                ValueToReturned = false;

            return ValueToReturned;
        }

        public async Task<bool> AddBookAsync(Book book)
        {
            await _ctx.Books.AddAsync(book);

            return await SavedAsync();
        }

        public async Task<Book> GetBookByIdAsync(string bookId)
        {
            var book = await _ctx.Books.FirstOrDefaultAsync(x => x.BookId == bookId);

            return book;
        }
    }
}
