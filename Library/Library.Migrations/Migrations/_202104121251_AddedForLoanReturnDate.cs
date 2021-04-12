using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Migrations.Migrations
{
    [Migration(202104121251)]
    public class _202104121251_AddedForLoanReturnDate : Migration
    {

        public override void Up()
        {
            Alter
                .Table("Loans")
                .AddColumn("ReturnDate").AsDateTime().NotNullable();
        }
        public override void Down()
        {
            Delete.Column("ReturnDate").FromTable("Loans");
        }
    }
}
