using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Picklists;
using SurgeonPortal.Library.Contracts.Picklists;
using SurgeonPortal.Library.Picklists;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Picklists
{
    [TestFixture] 
	public class OrganizationTypeReadOnlyListTests : TestBase<int>
    {

        [Test]
        public async Task GetAllAsync_CallsDalCorrectly()
        {
            
            var mockDal = new Mock<IOrganizationTypeReadOnlyDal>();
            mockDal.Setup(m => m.GetAllAsync())
                .ReturnsAsync(CreateMany<OrganizationTypeReadOnlyDto>());
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IOrganizationTypeReadOnlyList, OrganizationTypeReadOnlyList>()
                .WithBusinessObject<IOrganizationTypeReadOnly, OrganizationTypeReadOnly>()
                .Build();
        
            var factory = new OrganizationTypeReadOnlyListFactory();
            var sut = await factory.GetAllAsync();
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetAllAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<OrganizationTypeReadOnlyDto>();
        
            var mockDal = new Mock<IOrganizationTypeReadOnlyDal>();
            mockDal.Setup(m => m.GetAllAsync())
                .ReturnsAsync(expectedDtos);
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IOrganizationTypeReadOnlyList, OrganizationTypeReadOnlyList>()
                .WithBusinessObject<IOrganizationTypeReadOnly, OrganizationTypeReadOnly>()
                .Build();
        
            var factory = new OrganizationTypeReadOnlyListFactory();
            var sut = await factory.GetAllAsync();
        
            Assert.That(sut, Has.Count.EqualTo(3));
            expectedDtos.Should().BeEquivalentTo(sut, options => 
            {
                options.ExcludingMissingMembers();
                return options;
            });
        }
	}
}
