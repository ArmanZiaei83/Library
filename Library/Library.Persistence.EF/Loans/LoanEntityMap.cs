using Library.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Persistence.EF.Loans
{
    public class LoanEntityMap : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> _)
        {
            _.ToTable("Loans");

            _.HasKey(_ => _.Id);

            _.Property(_ => _.Id).IsRequired().ValueGeneratedOnAdd();

            _.Property(_ => _.BookId).IsRequired();

            _.Property(_ => _.MemeberId).IsRequired();

            _.HasOne(_ => _.Book)
                .WithMany(_ => _.Loans)
                .HasForeignKey(_ => _.BookId);

            _.HasOne(_ => _.Member)
                .WithMany(_ => _.Loans)
                .HasForeignKey(_ => _.MemeberId);
        }
    }
}
