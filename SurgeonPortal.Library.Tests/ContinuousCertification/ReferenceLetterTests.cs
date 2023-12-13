using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.ContinuousCertification;
using SurgeonPortal.Library.ContinuousCertification;
using SurgeonPortal.Library.Contracts.ContinuousCertification;
using SurgeonPortal.Library.Contracts.Email;
using SurgeonPortal.Library.Contracts.EmailProvider;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.ContinuousCertification
{
    [TestFixture] 
	public class ReferenceLetterTests : TestBase<int>
    {
        private ReferenceLetterDto CreateValidDto()
        {
            var dto = Create<ReferenceLetterDto>();
        
            dto.UserId = 1234;
            dto.Official = Create<string>();
            dto.RoleName = Create<string>();
            dto.AltRoleName = Create<string>();
            dto.RoleId = Create<int>();
            dto.AltRoleId = Create<int?>();
            dto.Explain = Create<string>();
            dto.Title = Create<string>();
            dto.Email = Create<string>();
            dto.Phone = Create<string>();
            dto.Hosp = Create<string>();
            dto.City = Create<string>();
            dto.State = Create<string>();
            dto.FullName = Create<string>();
        
            return dto;
        }
        
        

        #region GetByIdAsync ReferenceLetter
        
        [Test]
        public async Task GetByIdAsync_CallsDalCorrectly()
        {
            var expectedId = Create<int>();
            
            var mockDal = new Mock<IReferenceLetterDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedId))
                .ReturnsAsync(Create<ReferenceLetterDto>());

            var mockEmailFactory = GetEmailFactory();

            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(mockEmailFactory)
                .WithBusinessObject<IReferenceLetter, ReferenceLetter>()
                .Build();
        
            var factory = new ReferenceLetterFactory();
            var sut = await factory.GetByIdAsync(expectedId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetById_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
            var expectedId = Create<int>();
            
            var mockDal = new Mock<IReferenceLetterDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedId))
                .ReturnsAsync(dto);

            var mockEmailFactory = GetEmailFactory();

            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(mockEmailFactory)
                .WithBusinessObject<IReferenceLetter, ReferenceLetter>()
                .Build();
        
            var factory = new ReferenceLetterFactory();
            var sut = await factory.GetByIdAsync(expectedId);
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region Create
        
        [Test]
        public async Task Create_CallsDalCorrectly()
        {
            var dto = CreateValidDto();
            ReferenceLetterDto passedDto = null;
        
            var mockDal = new Mock<IReferenceLetterDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<ReferenceLetterDto>()))
                .Callback<ReferenceLetterDto>((p) => passedDto = p)
                .ReturnsAsync(dto);

            var mockEmailFactory = GetEmailFactory();

            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(mockEmailFactory)
                .WithBusinessObject<IReferenceLetter, ReferenceLetter>()
                .Build();
        
            var factory = new ReferenceLetterFactory();
            var sut = factory.Create();
            
            sut.UserId = dto.UserId;
            sut.Official = dto.Official;
            sut.RoleName = dto.RoleName;
            sut.AltRoleName = dto.AltRoleName;
            sut.RoleId = dto.RoleId;
            sut.AltRoleId = dto.AltRoleId;
            sut.Explain = dto.Explain;
            sut.Title = dto.Title;
            sut.Email = dto.Email;
            sut.Phone = dto.Phone;
            sut.Hosp = dto.Hosp;
            sut.City = dto.City;
            sut.State = dto.State;
            sut.FullName = dto.FullName;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null);
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                .Excluding(m => m.RoleName)
                .Excluding(m => m.AltRoleName)
                .Excluding(m => m.FullName)
                .Excluding(m => m.CreatedAtUtc)
                .Excluding(m => m.CreatedByUserId)
                .Excluding(m => m.LastUpdatedAtUtc)
                .Excluding(m => m.LastUpdatedByUserId)
                .ExcludingMissingMembers());
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task Create_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IReferenceLetterDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<ReferenceLetterDto>()))
                .ReturnsAsync(dto);

            var mockEmailFactory = GetEmailFactory();

            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(mockEmailFactory)
                .WithBusinessObject<IReferenceLetter, ReferenceLetter>()
                .Build();
        
            var factory = new ReferenceLetterFactory();
            var sut = factory.Create();
            sut.UserId = dto.UserId;
            sut.Official = dto.Official;
            sut.RoleName = dto.RoleName;
            sut.AltRoleName = dto.AltRoleName;
            sut.RoleId = dto.RoleId;
            sut.AltRoleId = dto.AltRoleId;
            sut.Explain = dto.Explain;
            sut.Title = dto.Title;
            sut.Email = dto.Email;
            sut.Phone = dto.Phone;
            sut.Hosp = dto.Hosp;
            sut.City = dto.City;
            sut.State = dto.State;
            sut.FullName = dto.FullName;
        
            await sut.SaveAsync();
            
            dto.Should().BeEquivalentTo(sut,
                options => options
                .Excluding(m => m.CreatedAtUtc)
                    .Excluding(m => m.CreatedByUserId)
                    .Excluding(m => m.LastUpdatedAtUtc)
                    .Excluding(m => m.LastUpdatedByUserId)
                    .ExcludingMissingMembers());
        }

        #endregion

        private static Mock<IEmailFactory> GetEmailFactory()
        {
            var mockEmailFactory = new Mock<IEmailFactory>();
            mockEmailFactory.Setup(e => e.Create()).Returns(new Mock<IEmail>().Object);
            return mockEmailFactory;
        }
    }
}
