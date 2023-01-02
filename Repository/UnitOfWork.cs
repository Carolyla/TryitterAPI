using TryitterApi.Context;

namespace TryitterApi.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private PostRepository _postRepo;
        private StudentRepository _studentRepo;
        public MyContext _context;

        public UnitOfWork(MyContext contexto)
        {
            _context = contexto;
        }

        public IPostRepository PostRepository
        {
            get
            {
                return _postRepo = _postRepo ?? new PostRepository(_context);
            }
        }

        public IStudentRepository StudentRepository
        {
            get
            {
                return _studentRepo = _studentRepo ?? new StudentRepository(_context);
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}