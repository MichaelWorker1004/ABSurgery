using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.MedicalTraining;
using SurgeonPortal.Library.Contracts.MedicalTraining;
using SurgeonPortal.Library.MedicalTraining;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.MedicalTraining
{
    [TestFixture] 
	public class UserCertificateReadOnlyListTests : TestBase<int>
    {

        [Test]
        public async Task GetByUserIdAsync_CallsDalCorrectly()
        {
            
            var mockDal = new Mock<IUserCertificateReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync())
                .ReturnsAsync(CreateMany<UserCertificateReadOnlyDto>());
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserCertificateReadOnlyList, UserCertificateReadOnlyList>()
                .WithBusinessObject<IUserCertificateReadOnly, UserCertificateReadOnly>()
                .Build();
        
            var factory = new UserCertificateReadOnlyListFactory();
            var sut = await factory.GetByUserIdAsync();
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByUserIdAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<UserCertificateReadOnlyDto>();
        
            var mockDal = new Mock<IUserCertificateReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync())
                .ReturnsAsync(expectedDtos);
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserCertificateReadOnlyList, UserCertificateReadOnlyList>()
                .WithBusinessObject<IUserCertificateReadOnly, UserCertificateReadOnly>()
                .Build();
        
            var factory = new UserCertificateReadOnlyListFactory();
            var sut = await factory.GetByUserIdAsync();
        
            Assert.That(sut, Has.Count.EqualTo(3));
            expectedDtos.Should().BeEquivalentTo(sut, options => 
            {
                options.ExcludingMissingMembers();
                return options;
            });
        }
	}
}
