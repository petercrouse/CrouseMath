using AutoMapper;
using CrouseMath.Application.Common.Mappings;
using System;
using CrouseMath.Application.Students.Queries.GetStudent;
using CrouseMath.Domain.Entities;
using Xunit;

namespace CrouseMath.Application.UnitTests.Common.Mappings
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }
    }
}
