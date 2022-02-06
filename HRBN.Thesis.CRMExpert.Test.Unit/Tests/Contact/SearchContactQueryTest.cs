using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Contact;
using HRBN.Thesis.CRMExpert.Application.Mappers.Profiles;
using HRBN.Thesis.CRMExpert.Domain.Core.Enums;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using HRBN.Thesis.CRMExpert.Infrastructure.Dto;
using NSubstitute;
using Xunit;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Tests.Contact
{
    public class SearchContactQueryTest
    {
        [Fact]
        public async Task SearchContact_WhenItExists_ReturnCorrectData()
        {
            using (var sut = new SystemUnderTest())
            {
                var contact = (Domain.Core.Entities.Contact) sut.CreateContact();
                var contacts = new List<Domain.Core.Entities.Contact>() {contact};
                var pageResult =
                    new PageResult<Domain.Core.Entities.Contact>(contacts, 1, 10, 1, null, SortDirection.ASC,
                        "FirstName");
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                var query = new SearchContactsQuery()
                {
                    UserId = Guid.Empty,
                    SearchPhrase = contact.FirstName,
                    PageNumber = 1,
                    PageSize = 10,
                    OrderBy = "FirstName",
                    SortDirection = SortDirection.DESC
                };

                unitOfWorkSubstitute
                    .ContactsRepository
                    .SearchAsync(query.UserId, query.SearchPhrase, query.PageNumber, query.PageSize, query.OrderBy,
                        query.SortDirection)
                    .Returns(pageResult);

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new SearchContactsQueryHandler(unitOfWorkSubstitute, mapper);
                var contactQuery = await handler.HandleAsync(query);

                foreach (var cntct in contactQuery.Items)
                {
                    cntct.Id.Should().Be(contact.Id);
                }
            }
        }
    }
}