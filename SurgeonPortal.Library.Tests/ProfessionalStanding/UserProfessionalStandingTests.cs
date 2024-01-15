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
	public class UserProfessionalStandingTests : TestBase<int>
    {
        private UserProfessionalStandingDto CreateValidDto()
        {
            var dto = Create<UserProfessionalStandingDto>();
        
            dto.Id = Create<int>();
            dto.UserId = 1234;
            dto.ExplanationOfNonPrivileges = Create<string>();
            dto.ExplanationOfNonClinicalActivities = Create<string>();
            dto.ClinicallyActive = Create<int>();
            dto.CreatedByUserId = 1234;
            dto.CreatedAtUtc = Create<System.DateTime>();
            dto.LastUpdatedAtUtc = Create<System.DateTime>();
            dto.LastUpdatedByUserId = 1234;
        
            return dto;
        }
        
        #region UserProfessionalStanding Business Rules
        [Test]
        public async Task IsRequired_GetByUserId_ExplanationOfNonPrivileges_Fails()
        {
            var dto = CreateValidDto();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IUserProfessionalStandingDal>(MockBehavior.Strict);
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(dto);
            
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserProfessionalStanding, UserProfessionalStanding>()
                .Build();
        
            var factory = new UserProfessionalStandingFactory();
            var sut = await factory.GetByUserIdAsync();
            
            sut.ExplanationOfNonPrivileges = default;
        
            Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
        
            //Ensure that the save fails...
            var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(async () => await sut.SaveAsync());
            Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            Assert.That(sut.GetBrokenRules()[0].Description == "ExplanationOfNonPrivileges is required", $"Expected the rule description to be 'ExplanationOfNonPrivileges is required', have {sut.GetBrokenRules()[0].Description}");
            Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
            Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
        }
        
        [Test]
        public async Task IsRequired_GetByUserId_ExplanationOfNonPrivileges_Passes()
        {
            var dto = CreateValidDto();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IUserProfessionalStandingDal>(MockBehavior.Strict);
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(dto);
            
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserProfessionalStanding, UserProfessionalStanding>()
                .Build();
        
            var factory = new UserProfessionalStandingFactory();
            var sut = await factory.GetByUserIdAsync();
            
            sut.ExplanationOfNonPrivileges = dto.ExplanationOfNonPrivileges;
        
            Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
        
        }
        #endregion

        #region GetByUserIdAsync UserProfessionalStanding
        
        [Test]
        public async Task GetByUserIdAsync_CallsDalCorrectly()
        {
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IUserProfessionalStandingDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(Create<UserProfessionalStandingDto>());

			var (mockCommand, mockCommandFactory) = GetMockedCommandFactory(true);

			UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
				.WithRegisteredInstance(mockCommand)
				.WithRegisteredInstance(mockCommandFactory)
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
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IUserProfessionalStandingDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(dto);

			var (mockCommand, mockCommandFactory) = GetMockedCommandFactory(true);

			UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
				.WithRegisteredInstance(mockCommand)
				.WithRegisteredInstance(mockCommandFactory)
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

            var (mockCommand, mockCommandFactory) = GetMockedCommandFactory(true);

            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(mockCommand)
                .WithRegisteredInstance(mockCommandFactory)
                .WithBusinessObject<IUserProfessionalStanding, UserProfessionalStanding>()
                .Build();
        
            var factory = new UserProfessionalStandingFactory();
            var sut = factory.Create();
            
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.ExplanationOfNonPrivileges = dto.ExplanationOfNonPrivileges;
            sut.ExplanationOfNonClinicalActivities = dto.ExplanationOfNonClinicalActivities;
            sut.ClinicallyActive = dto.ClinicallyActive;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null);
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                .Excluding(m => m.Id)
                .Excluding(m => m.ClinicallyActive)
                .Excluding(m => m.CreatedAtUtc)
                .Excluding(m => m.LastUpdatedAtUtc)
                .Excluding(m => m.CreatedByUserId)
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

			var (mockCommand, mockCommandFactory) = GetMockedCommandFactory(true);

			UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
				.WithRegisteredInstance(mockCommand)
				.WithRegisteredInstance(mockCommandFactory)
				.WithBusinessObject<IUserProfessionalStanding, UserProfessionalStanding>()
                .Build();
        
            var factory = new UserProfessionalStandingFactory();
            var sut = factory.Create();
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.ExplanationOfNonPrivileges = dto.ExplanationOfNonPrivileges;
            sut.ExplanationOfNonClinicalActivities = dto.ExplanationOfNonClinicalActivities;
            sut.ClinicallyActive = dto.ClinicallyActive;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
        
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
            var expectedUserId = 1234;
            
            var dto = CreateValidDto();
            UserProfessionalStandingDto passedDto = null;
        
            var mockDal = new Mock<IUserProfessionalStandingDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(dto);
            
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<UserProfessionalStandingDto>()))
                .Callback<UserProfessionalStandingDto>((p) => passedDto = p)
                .ReturnsAsync(dto);

			var (mockCommand, mockCommandFactory) = GetMockedCommandFactory(true);

			UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
				.WithRegisteredInstance(mockCommand)
				.WithRegisteredInstance(mockCommandFactory)
				.WithBusinessObject<IUserProfessionalStanding, UserProfessionalStanding>()
                .Build();
        
            var factory = new UserProfessionalStandingFactory();
            var sut = await factory.GetByUserIdAsync();
            
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.ExplanationOfNonPrivileges = dto.ExplanationOfNonPrivileges;
            sut.ExplanationOfNonClinicalActivities = dto.ExplanationOfNonClinicalActivities;
            sut.ClinicallyActive = dto.ClinicallyActive;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
        
            // We now change all properties on the SUT to make it Dirty
            // or the SaveAsync() will not be called. :)
            dto = CreateValidDto();
        
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.ExplanationOfNonPrivileges = dto.ExplanationOfNonPrivileges;
            sut.ExplanationOfNonClinicalActivities = dto.ExplanationOfNonClinicalActivities;
            sut.ClinicallyActive = dto.ClinicallyActive;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null, "The passedDto is null, which can mean that the DataPortal method was not called.");
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                .Excluding(m => m.Id)
                .Excluding(m => m.ClinicallyActive)
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
            var expectedUserId = 1234;
            
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IUserProfessionalStandingDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(dto);
            
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<UserProfessionalStandingDto>()))
                .ReturnsAsync(dto);

			var (mockCommand, mockCommandFactory) = GetMockedCommandFactory(true);

			UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
				.WithRegisteredInstance(mockCommand)
				.WithRegisteredInstance(mockCommandFactory)
				.WithBusinessObject<IUserProfessionalStanding, UserProfessionalStanding>()
                .Build();
        
            var factory = new UserProfessionalStandingFactory();
            var sut = await factory.GetByUserIdAsync();
        
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

		static (Mock<IGetClinicallyActiveCommand> mockCommand, Mock<IGetClinicallyActiveCommandFactory> mockCommandFactory) GetMockedCommandFactory(bool clinicallyActive)
		{
			var mockCommand = new Mock<IGetClinicallyActiveCommand>();
			mockCommand.SetupGet(x => x.ClinicallyActive).Returns(clinicallyActive);

			var mockCommandFactory = new Mock<IGetClinicallyActiveCommandFactory>();
			mockCommandFactory.Setup(x => x.GetClinicallyActiveByUserId()).Returns(mockCommand.Object);

			return (mockCommand, mockCommandFactory);
		}
	}
}
