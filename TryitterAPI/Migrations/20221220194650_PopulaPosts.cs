using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TryitterAPI.Migrations
{
    /// <inheritdoc />
    public partial class PopulaPosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Posts(StudentId, Title, Content, ImageUrl, CreatedAt, UpdateAt) " +
            "Values(1,'Fim da Aceleração', 'Estamos caminhando para finalizar o último projeto, amém', 'image00.jpg', GETDATE(), GETDATE())");
            mb.Sql("Insert into Posts(StudentId, Title, Content, ImageUrl, CreatedAt, UpdateAt) " +
            "Values(2,'Novos Rumos', 'Grandes aprendizados na XP Inc', 'image.jpg', GETDATE(), GETDATE())");
            mb.Sql("Insert into Posts(StudentId, Title, Content, ImageUrl, CreatedAt, UpdateAt) " +
            "Values(3,'Macoratti', 'Mestre do C#', 'image2.jpg', GETDATE(), GETDATE())");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Posts");
        }
    }
}
