using AutoMapper;
using TryitterApi.Repository;
using Microsoft.EntityFrameworkCore;
using TryitterApi.Context;
using TryitterApi.DTOs.Mappings;
using TryitterApi.Controllers;
using TryitterApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using TryitterApi.Models;

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
        [Fact]
        public void GetCategorias_MatchResult()
        {
            //Arrange
            var controller = new StudentsController(repository, mapper);

            var data =  controller.Get();

            Assert.IsType<List<StudentDTO>>(data.Value);
            var std = data.Value.Should().BeAssignableTo<List<StudentDTO>>().Subject;
            Assert.Equal("Isabela Ponte", std[0].Name);
            Assert.Equal("isabelaponte@email.com", std[0].Email);
            Assert.Equal("Maria Clara", std[2].Name);
            Assert.Equal("clarinha@email.com", std[2].Email);
        }

        [Fact]
        public void GetStudentsById_Return_Ok()
        {
            // Given
            var controller = new StudentsController(repository, mapper);
            var stdudentId = 1;
        
            // When
        
        var data = controller.Get(stdudentId);
        //Assert

        Assert.IsType<StudentDTO>(data.Value);

            // Then
        }

        [Fact]
        public void GetStudentsById_Return_NotFound()
        {
            // Arrange
            var controller = new StudentsController(repository, mapper);
            var stdudentId = 010203;
            //Act
            var data = controller.Get(stdudentId);
            //Assert
            Assert.IsType<NotFoundObjectResult>(data.Result);
        }

        [Fact]
        public void Post_Studant_CreatedResult_ReturnOk()
        {
            //Arrage
            var controller = new StudentsController(repository, mapper);
            //Act
            var std = new Student()
            { Name = "Teste", Email = "teste@email.com"};
            var data = controller.Post(std);
            //Assert
            Assert.IsType<ObjectResult>(data); //CreatObjectResult
        }

        [Fact]
        public void Put_Student_ValidData_ReturnOk()
        {
            var controller = new StudentsController(repository, mapper);
            var stdId = 2;
            
            //Act
            var existingStudent = controller.Get(stdId);
            var result = existingStudent.Value.Should().BeAssignableTo<StudentDTO>().Subject;
            var stdDto = new Student
            {
                StudentId = stdId,
                Name = "Nome atulizado pelo Teste 1",
                Email = result.Email
            };

            var updateData = controller.Put(stdId, stdDto);

            //Assert
            Assert.IsType<OkResult>(updateData);
        }

    //     [Fact]
    //     public void Delete_Student_ResultOK()
    //     {
    //         //Arrange
    //         var controller = new StudentsController(repository, mapper);
    //         var sdtId = 3;

    //          //Act
    //          var data = controller.Delete(sdtId);
    //         //Assert

    //    Assert.IsType<Student>(data.Value);
    //     }
    }
}