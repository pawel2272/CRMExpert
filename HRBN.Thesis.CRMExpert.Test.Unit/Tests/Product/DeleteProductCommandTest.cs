using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Product;
using HRBN.Thesis.CRMExpert.Application.Mappers.Profiles;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Tests.Product
{
    public class DeleteProductCommandTest
    {
        private readonly IMapper _mapper;

        public DeleteProductCommandTest()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
        }

        [Fact]
        public async Task DeleteProduct_WhenItIsPossible_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                Guid guid = Guid.NewGuid();

                var command = new DeleteProductCommand(guid);

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.ProductsRepository.GetAsync(guid).Returns(new Domain.Core.Entities.Product());

                var handler = new DeleteProductCommandHandler(unitOfWorkSubstitute, _mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(true);
            }
        }

        [Fact]
        public async Task DeleteProduct_WhenItIsPossible_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new DeleteProductCommand(Guid.Empty);

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.ContactsRepository.GetAsync(Guid.Empty).ReturnsNull();

                var handler = new DeleteProductCommandHandler(unitOfWorkSubstitute, _mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(false);

                result.Errors.Count().Should().Be(0);

                result.Message.Should().Be("Product does not exist.");
            }
        }
    }
}