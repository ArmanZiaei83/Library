using Library.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Persistence.EF.Books
{
    class BookEntityMap : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> _)
        {
            _.ToTable("Books");

            _.HasKey(_ => _.Id);

            _.Property(_ => _.Id).IsRequired().ValueGeneratedOnAdd();

            _.Property(_ => _.Name).IsRequired();

            _.Property(_ => _.Author).IsRequired();

            _.Property(_ => _.AgeGroup).IsRequired();

            _.Property(_ => _.BookCategoryId).IsRequired();

            _.HasOne(_ => _.BookCategory)
                .WithMany(_ => _.Books)
                .HasForeignKey(_ => _.BookCategoryId);

        }
    }
}
