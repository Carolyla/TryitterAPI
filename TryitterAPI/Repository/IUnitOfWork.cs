namespace TryitterApi.Repository
{
    public interface IUnitOfWork
    {
        IPostRepository PostRepository { get; }

        IStudentRepository StudentRepository { get;}
        void Commit();
    }
}
