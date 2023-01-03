// using AutoMapper;
// using TryitterApi.Repository;
// using Microsoft.EntityFrameworkCore;
// using TryitterApi.Context;

// namespace TryitterApixUnit
// {
//     public class StudentsUnitTests
//     {
//         private IMapper mapper;
//         private IUnitOfWork repository;

//         public static DbContextOptions<MyContext> dbContextOptions { get;}
//         public static string connectionString =
//         "Server=127.0.0.1,1433;Database=Tryitter;User ID = sa; Password=1q2w3e4r@#$;TrustServerCertificate=true";

//         static StudentsUnitTests()
//         {
//             dbContextOptions = new DbContextOptionsBuilder<MyContext>()
//             .UseSqlServer(connectionString)
//             .Options;
//         }
//     }
// }