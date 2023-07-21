using LibraryApp.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DAL.Data
{
    public class LibraryContext:IdentityDbContext<AppUser>
    {
        public LibraryContext(DbContextOptions options):base(options) 
        {
            
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<BorrowingOP> BorrowingOPs { get;set; }

    }
}
