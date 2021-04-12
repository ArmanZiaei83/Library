using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Migrations.Migrations
{
    [Migration(202104101842)]
    public class _202104101842_CreatedTables : Migration
    {
        public override void Up()
        {
            Create.Table("BookCategories")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Title").AsString().NotNullable();

            Create.Table("Books")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Author").AsString().NotNullable()
                .WithColumn("AgeGroup").AsInt32().NotNullable()
                .WithColumn("BookCategoryId").AsInt32().NotNullable()
                    .ForeignKey("FK_Books_BookCategories", "BookCategories", "Id");

            Create.Table("Members")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("FullName").AsString().NotNullable()
                .WithColumn("Address").AsString().NotNullable()
                .WithColumn("Age").AsInt32().NotNullable();

            Create.Table("Loans")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("BookId").AsInt32().NotNullable()
                    .ForeignKey("FK_Loans_Books", "Books", "Id")
                .WithColumn("MemeberId").AsInt32().NotNullable()
                    .ForeignKey("FK_Loans_Members", "Members", "Id");

        }

        public override void Down()
        {
            Delete.Table("Loans");
            Delete.Table("Members");
            Delete.Table("Books");
            Delete.Table("BookCategories");
        }

    }
}
