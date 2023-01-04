using AutoMapper;
using TryitterApi.Repository;
using Microsoft.EntityFrameworkCore;
using TryitterApi.Context;
using TryitterApi.DTOs.Mappings;
using TryitterApi.Controllers;
using TryitterApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace TryitterApixUnit
{
    public class StudentsUnitTests
    {
        private IMapper mapper;
        private IUnitOfWork repository;

        public static DbContextOptions<MyContext> dbContextOptions { get;}
        public static string connectionString =
        "Server=127.0.0.1,1433;Database=Tryitter;User ID = sa; Password=1q2w3e4r@#$;TrustServerCertificate=true";

        static StudentsUnitTests()
        {
            dbContextOptions = new DbContextOptionsBuilder<MyContext>()
            .UseSqlServer(connectionString)
            .Options;
        }

        public StudentsUnitTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            mapper = config.CreateMapper();

            var context = new MyContext(dbContextOptions);

            repository = new UnitOfWork(context);
        }

        // tests unitário ==========================================================

        [Fact]
        public void GetCategorias_Return_OkResult()
        {
            //Arrange
            var controller =  new StudentsController(repository, mapper);
            //Act
            var data = controller.Get();
            //Assert
            Assert.IsType<List<StudentDTO>>(data.Value);
        
           
        }

        // [Fact]
        // public void GetCategorias_Return_BadRequestResult()
        // {
        //     //Arrange
        //     var controller = new StudentsController(repository, mapper);
        //     //Act
        //     var data = controller.Get();
        //     //Assert
        //     Assert.IsType<BadRequestResult>(data.Result);


        // }
    }
}