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
    public class CheckOutRepository :  ICheckOutRepository
    {
        private readonly EBookContext _ctx;

        public CheckOutRepository(EBookContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<bool> BookCheckOutAsync(BookActivity bookActivity)
        {
            await _ctx.BookActivities.AddAsync(bookActivity);

            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<BookActivity> GetBookActivityByIdAsync(string id)
        {
           return await _ctx.BookActivities.Include(x => x.Book).FirstOrDefaultAsync(x => x.BookActivityId == id);
        }

        public async Task<bool> UpdateAsync(BookActivity bookActivity)
        {
            _ctx.Update(bookActivity);

            return await _ctx.SaveChangesAsync() > 0;
        }
    }
}
