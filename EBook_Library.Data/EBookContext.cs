using EBook_Library.Models;
using Microsoft.EntityFrameworkCore;

namespace EBook_Library.Data
{
    public class EBookContext : DbContext
    {
        public EBookContext(DbContextOptions<EBookContext> options) : base(options)
        {
        }
      
        public DbSet<Book> Books { get; set; }
        public DbSet<BookActivity> BookActivities { get; set; }
    }
}
