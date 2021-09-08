using EBook_Library.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBook_Library.Data
{
    public class EBookContext : IdentityDbContext<AppUser>
    {
        public EBookContext(DbContextOptions<EBookContext> options) : base(options)
        {
        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookActivity> BookActivities { get; set; }
    }
}
