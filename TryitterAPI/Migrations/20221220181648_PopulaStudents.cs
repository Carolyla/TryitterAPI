using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TryitterAPI.Migrations
{
    /// <inheritdoc />
    public partial class PopulaStudents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb) 
        {
            mb.Sql("Insert into Students(Name, Email, Password) Values('Isabela Ponte', 'isabelaponte@email.com', 'senha123')");
            mb.Sql("Insert into Students(Name, Email, Password) Values('Thiago Silva', 'tiagosilva@email.com', '12345678')");
            mb.Sql("Insert into Students(Name, Email, Password) Values('Maria Clara', 'clarinha@email.com', 'abcdefg')");
        }
     

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Students");
        }
    }
}
