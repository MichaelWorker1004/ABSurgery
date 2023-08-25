using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.ProfessionalStanding;
using SurgeonPortal.Library.Contracts.ProfessionalStanding;
using SurgeonPortal.Library.ProfessionalStanding;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.ProfessionalStanding
{
    [TestFixture] 
	public class UserProfessionalStandingTests : TestBase<string>
    {
        private UserProfessionalStandingDto CreateValidDto()
        {     
            var dto = Create<UserProfessionalStandingDto>();

            dto.Id = Create<int>();
            dto.UserId = Create<int>();
            dto.PrimaryPracticeId = Create<int?>();
            dto.PrimaryPractice = Create<string>();
            dto.OrganizationTypeId = Create<int?>();
            dto.OrganizationType = Create<string>();
            dto.ExplanationOfNonPrivileges = Create<string>();
            dto.ExplanationOfNonClinicalActivities = Create<string>();
            dto.CreatedByUserId = Create<int>();
            dto.CreatedAtUtc = Create<System.DateTime>();
            dto.LastUpdatedAtUtc = Create<System.DateTime>();
            dto.LastUpdatedByUserId = Create<int>();
    
            return dto;
        }

            

        #region GetByUserIdAsync UserProfessionalStanding
        
        [Test]
        public async Task GetByUserIdAsync_CallsDalCorrectly()
        {
            
            var mockDal = new Mock<IUserProfessionalStandingDal>();
            mockDal.Setup(m => m.GetByUserIdAsync())
                .ReturnsAsync(Create<UserProfessionalStandingDto>());
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserProfessionalStanding, UserProfessionalStanding>()
                .Build();
        
            var factory = new UserProfessionalStandingFactory();
            var sut = await factory.GetByUserIdAsync();
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByUserId_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IUserProfessionalStandingDal>();
            mockDal.Setup(m => m.GetByUserIdAsync())
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserProfessionalStanding, UserProfessionalStanding>()
                .Build();
        
            var factory = new UserProfessionalStandingFactory();
            var sut = await factory.GetByUserIdAsync();
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region Create
        
        [Test]
        public async Task Create_CallsDalCorrectly()
        {
            var dto = CreateValidDto();
            UserProfessionalStandingDto passedDto = null;
        
            var mockDal = new Mock<IUserProfessionalStandingDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<UserProfessionalStandingDto>()))
                .Callback<UserProfessionalStandingDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserProfessionalStanding, UserProfessionalStanding>()
                .Build();
        
            var factory = new UserProfessionalStandingFactory();
            var sut = factory.Create();
            
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.PrimaryPracticeId = dto.PrimaryPracticeId;
            sut.PrimaryPractice = dto.PrimaryPractice;
            sut.OrganizationTypeId = dto.OrganizationTypeId;
            sut.OrganizationType = dto.OrganizationType;
            sut.ExplanationOfNonPrivileges = dto.ExplanationOfNonPrivileges;
            sut.ExplanationOfNonClinicalActivities = dto.ExplanationOfNonClinicalActivities;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null);
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                .Excluding(m => m.CreatedAtUtc)
                .Excluding(m => m.CreatedByUserId)
                .Excluding(m => m.LastUpdatedAtUtc)
                .Excluding(m => m.LastUpdatedByUserId)
                .Excluding(m => m.Id)
                .Excluding(m => m.UserId)
                .Excluding(m => m.PrimaryPractice)
                .Excluding(m => m.OrganizationType)
                .Excluding(m => m.CreatedByUserId)
                .Excluding(m => m.CreatedAtUtc)
                .Excluding(m => m.LastUpdatedAtUtc)
                .Excluding(m => m.LastUpdatedByUserId)
                .ExcludingMissingMembers());
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task Create_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IUserProfessionalStandingDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<UserProfessionalStandingDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserProfessionalStanding, UserProfessionalStanding>()
                .Build();
        
            var factory = new UserProfessionalStandingFactory();
            var sut = factory.Create();
            sut.PrimaryPracticeId = Create<int>();
        
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

        #region Update
        
        [Test]
        public async Task Update_CallsDalCorrectly()
        {
            
            var dto = Create<UserProfessionalStandingDto>();
            UserProfessionalStandingDto passedDto = null;
        
            var mockDal = new Mock<IUserProfessionalStandingDal>();
            mockDal.Setup(m => m.GetByUserIdAsync())
                        .ReturnsAsync(dto);
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<UserProfessionalStandingDto>()))
                .Callback<UserProfessionalStandingDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserProfessionalStanding, UserProfessionalStanding>()
                .Build();
        
            var factory = new UserProfessionalStandingFactory();
            var sut = await factory.GetByUserIdAsync();
            
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.PrimaryPracticeId = dto.PrimaryPracticeId;
            sut.PrimaryPractice = dto.PrimaryPractice;
            sut.OrganizationTypeId = dto.OrganizationTypeId;
            sut.OrganizationType = dto.OrganizationType;
            sut.ExplanationOfNonPrivileges = dto.ExplanationOfNonPrivileges;
            sut.ExplanationOfNonClinicalActivities = dto.ExplanationOfNonClinicalActivities;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
        
            // We now change all properties on the SUT to make it Dirty
            // or the SaveAsync() will not be called. :)
            dto = CreateValidDto();
        
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.PrimaryPracticeId = dto.PrimaryPracticeId;
            sut.PrimaryPractice = dto.PrimaryPractice;
            sut.OrganizationTypeId = dto.OrganizationTypeId;
            sut.OrganizationType = dto.OrganizationType;
            sut.ExplanationOfNonPrivileges = dto.ExplanationOfNonPrivileges;
            sut.ExplanationOfNonClinicalActivities = dto.ExplanationOfNonClinicalActivities;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null, "The passedDto is null, which can mean that the DataPortal method was not called.");
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                    .Excluding(m => m.CreatedAtUtc)
                    .Excluding(m => m.CreatedByUserId)
                    .Excluding(m => m.LastUpdatedAtUtc)
                    .Excluding(m => m.LastUpdatedByUserId)
                    .Excluding(m => m.Id)
                    .Excluding(m => m.UserId)
                    .Excluding(m => m.PrimaryPractice)
                    .Excluding(m => m.OrganizationType)
                    .Excluding(m => m.CreatedByUserId)
                    .Excluding(m => m.CreatedAtUtc)
                    .Excluding(m => m.LastUpdatedAtUtc)
                    .Excluding(m => m.LastUpdatedByUserId)
                .ExcludingMissingMembers());
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task Update_YieldsCorrectResult()
        {
            
            var dto = Create<UserProfessionalStandingDto>();
        
            var mockDal = new Mock<IUserProfessionalStandingDal>();
            mockDal.Setup(m => m.GetByUserIdAsync())
                        .ReturnsAsync(Create<UserProfessionalStandingDto>());
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<UserProfessionalStandingDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserProfessionalStanding, UserProfessionalStanding>()
                .Build();
        
            var factory = new UserProfessionalStandingFactory();
            var sut = await factory.GetByUserIdAsync();
            sut.PrimaryPracticeId = Create<int>();
        
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
	}
}
