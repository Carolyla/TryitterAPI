using AutoMapper;
using TryitterApi.Repository;
using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace TryitterApi.Test
{
    public class StudentsUnitTests
    {
        private IMapper mapper;
        private IUnitOfWork repository;
    }
}