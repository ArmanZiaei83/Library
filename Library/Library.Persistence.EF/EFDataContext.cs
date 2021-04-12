using Library.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Library.Persistence.EF
{
    public class EFDataContext:DbContext
    {
        public EFDataContext(DbContextOptions<EFDataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Book>  Books { get; set; }
        public DbSet<BookCategory>  BookCategories { get; set; }
        public DbSet<Member>  Members { get; set; }
        public DbSet<Loan>  Loans { get; set; }
    }
}
