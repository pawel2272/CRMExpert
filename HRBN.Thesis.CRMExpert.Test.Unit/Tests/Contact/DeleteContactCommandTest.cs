using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Contact;
using HRBN.Thesis.CRMExpert.Application.Mappers.Profiles;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Tests.Contact
{
    public class DeleteContactCommandTest
    {
        private readonly IMapper _mapper;

        public DeleteContactCommandTest()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));;
        }
        
        [Fact]
        public async Task DeleteContact_WhenItIsPossible_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                Guid guid = Guid.NewGuid();

                var command = new DeleteContactCommand(guid);

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.ContactsRepository.GetAsync(guid).Returns(new Domain.Core.Entities.Contact());

                var handler = new DeleteContactCommandHandler(unitOfWorkSubstitute, _mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(true);
            }
        }

        [Fact]
        public async Task DeleteContact_WhenItIsPossible_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new DeleteContactCommand(Guid.Empty);

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.ContactsRepository.GetAsync(Guid.Empty).ReturnsNull();

                var handler = new DeleteContactCommandHandler(unitOfWorkSubstitute, _mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(false);

                result.Errors.Count().Should().Be(0);

                result.Message.Should().Be("Contact does not exist.");
            }
        }
    }
}
