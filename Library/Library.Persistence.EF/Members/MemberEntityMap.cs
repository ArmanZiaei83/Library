using Library.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Persistence.EF.Members
{
    class MemberEntityMap : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> _)
        {
            _.ToTable("Members");

            _.HasKey(_ => _.Id);

            _.Property(_ => _.Id).IsRequired().ValueGeneratedOnAdd();

            _.Property(_ => _.FullName).IsRequired();

            _.Property(_ => _.Age).IsRequired();

            _.Property(_ => _.Address).IsRequired();


        }
    }
}
