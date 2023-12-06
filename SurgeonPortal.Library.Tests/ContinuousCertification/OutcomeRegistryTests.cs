using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.ContinuousCertification;
using SurgeonPortal.Library.ContinuousCertification;
using SurgeonPortal.Library.Contracts.ContinuousCertification;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.ContinuousCertification
{
    [TestFixture] 
	public class OutcomeRegistryTests : TestBase<int>
    {
        private OutcomeRegistryDto CreateValidDto()
        {
            var dto = Create<OutcomeRegistryDto>();
        
            dto.Id = Create<int>();
            dto.UserId = 1234;
            dto.SurgeonSpecificRegistry = Create<string>();
            dto.RegistryComments = Create<string>();
            dto.RegisteredWithACHQC = Create<bool>();
            dto.RegisteredWithCESQIP = Create<bool>();
            dto.RegisteredWithMBSAQIP = Create<bool>();
            dto.RegisteredWithABA = Create<bool>();
            dto.RegisteredWithASBS = Create<bool>();
            dto.RegisteredWithMSQC = Create<bool>();
            dto.RegisteredWithABMS = Create<bool>();
            dto.RegisteredWithNCDB = Create<bool>();
            dto.RegisteredWithRQRS = Create<bool>();
            dto.RegisteredWithNSQIP = Create<bool>();
            dto.RegisteredWithNTDB = Create<bool>();
            dto.RegisteredWithSTS = Create<bool>();
            dto.RegisteredWithTQIP = Create<bool>();
            dto.RegisteredWithUNOS = Create<bool>();
            dto.RegisteredWithNCDR = Create<bool>();
            dto.RegisteredWithSVS = Create<bool>();
            dto.RegisteredWithELSO = Create<bool>();
            dto.RegisteredWithSSR = Create<bool>();
            dto.UserConfirmed = Create<bool?>();
            dto.UserConfirmedDateUtc = Create<System.DateTime?>();
            dto.CreatedByUserId = 1234;
            dto.CreatedAtUtc = Create<System.DateTime>();
            dto.LastUpdatedAtUtc = Create<System.DateTime>();
            dto.LastUpdatedByUserId = 1234;
        
            return dto;
        }
        
        

        #region GetByUserIdAsync OutcomeRegistry
        
        [Test]
        public async Task GetByUserIdAsync_CallsDalCorrectly()
        {
            var expectedUserID = 1234;
            
            var mockDal = new Mock<IOutcomeRegistryDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserID))
                .ReturnsAsync(Create<OutcomeRegistryDto>());
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IOutcomeRegistry, OutcomeRegistry>()
                .Build();
        
            var factory = new OutcomeRegistryFactory();
            var sut = await factory.GetByUserIdAsync();
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByUserId_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
            var expectedUserID = 1234;
            
            var mockDal = new Mock<IOutcomeRegistryDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserID))
                .ReturnsAsync(dto);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IOutcomeRegistry, OutcomeRegistry>()
                .Build();
        
            var factory = new OutcomeRegistryFactory();
            var sut = await factory.GetByUserIdAsync();
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region Create
        
        [Test]
        public async Task Create_CallsDalCorrectly()
        {
            var dto = CreateValidDto();
            OutcomeRegistryDto passedDto = null;
        
            var mockDal = new Mock<IOutcomeRegistryDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<OutcomeRegistryDto>()))
                .Callback<OutcomeRegistryDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IOutcomeRegistry, OutcomeRegistry>()
                .Build();
        
            var factory = new OutcomeRegistryFactory();
            var sut = factory.Create();
            
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.SurgeonSpecificRegistry = dto.SurgeonSpecificRegistry;
            sut.RegistryComments = dto.RegistryComments;
            sut.RegisteredWithACHQC = dto.RegisteredWithACHQC;
            sut.RegisteredWithCESQIP = dto.RegisteredWithCESQIP;
            sut.RegisteredWithMBSAQIP = dto.RegisteredWithMBSAQIP;
            sut.RegisteredWithABA = dto.RegisteredWithABA;
            sut.RegisteredWithASBS = dto.RegisteredWithASBS;
            sut.RegisteredWithMSQC = dto.RegisteredWithMSQC;
            sut.RegisteredWithABMS = dto.RegisteredWithABMS;
            sut.RegisteredWithNCDB = dto.RegisteredWithNCDB;
            sut.RegisteredWithRQRS = dto.RegisteredWithRQRS;
            sut.RegisteredWithNSQIP = dto.RegisteredWithNSQIP;
            sut.RegisteredWithNTDB = dto.RegisteredWithNTDB;
            sut.RegisteredWithSTS = dto.RegisteredWithSTS;
            sut.RegisteredWithTQIP = dto.RegisteredWithTQIP;
            sut.RegisteredWithUNOS = dto.RegisteredWithUNOS;
            sut.RegisteredWithNCDR = dto.RegisteredWithNCDR;
            sut.RegisteredWithSVS = dto.RegisteredWithSVS;
            sut.RegisteredWithELSO = dto.RegisteredWithELSO;
            sut.RegisteredWithSSR = dto.RegisteredWithSSR;
            sut.UserConfirmed = dto.UserConfirmed;
            sut.UserConfirmedDateUtc = dto.UserConfirmedDateUtc;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null);
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                .Excluding(m => m.Id)
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
        
            var mockDal = new Mock<IOutcomeRegistryDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<OutcomeRegistryDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IOutcomeRegistry, OutcomeRegistry>()
                .Build();
        
            var factory = new OutcomeRegistryFactory();
            var sut = factory.Create();
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.SurgeonSpecificRegistry = dto.SurgeonSpecificRegistry;
            sut.RegistryComments = dto.RegistryComments;
            sut.RegisteredWithACHQC = dto.RegisteredWithACHQC;
            sut.RegisteredWithCESQIP = dto.RegisteredWithCESQIP;
            sut.RegisteredWithMBSAQIP = dto.RegisteredWithMBSAQIP;
            sut.RegisteredWithABA = dto.RegisteredWithABA;
            sut.RegisteredWithASBS = dto.RegisteredWithASBS;
            sut.RegisteredWithMSQC = dto.RegisteredWithMSQC;
            sut.RegisteredWithABMS = dto.RegisteredWithABMS;
            sut.RegisteredWithNCDB = dto.RegisteredWithNCDB;
            sut.RegisteredWithRQRS = dto.RegisteredWithRQRS;
            sut.RegisteredWithNSQIP = dto.RegisteredWithNSQIP;
            sut.RegisteredWithNTDB = dto.RegisteredWithNTDB;
            sut.RegisteredWithSTS = dto.RegisteredWithSTS;
            sut.RegisteredWithTQIP = dto.RegisteredWithTQIP;
            sut.RegisteredWithUNOS = dto.RegisteredWithUNOS;
            sut.RegisteredWithNCDR = dto.RegisteredWithNCDR;
            sut.RegisteredWithSVS = dto.RegisteredWithSVS;
            sut.RegisteredWithELSO = dto.RegisteredWithELSO;
            sut.RegisteredWithSSR = dto.RegisteredWithSSR;
            sut.UserConfirmed = dto.UserConfirmed;
            sut.UserConfirmedDateUtc = dto.UserConfirmedDateUtc;
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
            var expectedUserID = 1234;
            
            var dto = CreateValidDto();
            OutcomeRegistryDto passedDto = null;
        
            var mockDal = new Mock<IOutcomeRegistryDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserID))
                .ReturnsAsync(dto);
            
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<OutcomeRegistryDto>()))
                .Callback<OutcomeRegistryDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IOutcomeRegistry, OutcomeRegistry>()
                .Build();
        
            var factory = new OutcomeRegistryFactory();
            var sut = await factory.GetByUserIdAsync();
            
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.SurgeonSpecificRegistry = dto.SurgeonSpecificRegistry;
            sut.RegistryComments = dto.RegistryComments;
            sut.RegisteredWithACHQC = dto.RegisteredWithACHQC;
            sut.RegisteredWithCESQIP = dto.RegisteredWithCESQIP;
            sut.RegisteredWithMBSAQIP = dto.RegisteredWithMBSAQIP;
            sut.RegisteredWithABA = dto.RegisteredWithABA;
            sut.RegisteredWithASBS = dto.RegisteredWithASBS;
            sut.RegisteredWithMSQC = dto.RegisteredWithMSQC;
            sut.RegisteredWithABMS = dto.RegisteredWithABMS;
            sut.RegisteredWithNCDB = dto.RegisteredWithNCDB;
            sut.RegisteredWithRQRS = dto.RegisteredWithRQRS;
            sut.RegisteredWithNSQIP = dto.RegisteredWithNSQIP;
            sut.RegisteredWithNTDB = dto.RegisteredWithNTDB;
            sut.RegisteredWithSTS = dto.RegisteredWithSTS;
            sut.RegisteredWithTQIP = dto.RegisteredWithTQIP;
            sut.RegisteredWithUNOS = dto.RegisteredWithUNOS;
            sut.RegisteredWithNCDR = dto.RegisteredWithNCDR;
            sut.RegisteredWithSVS = dto.RegisteredWithSVS;
            sut.RegisteredWithELSO = dto.RegisteredWithELSO;
            sut.RegisteredWithSSR = dto.RegisteredWithSSR;
            sut.UserConfirmed = dto.UserConfirmed;
            sut.UserConfirmedDateUtc = dto.UserConfirmedDateUtc;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
        
            // We now change all properties on the SUT to make it Dirty
            // or the SaveAsync() will not be called. :)
            dto = CreateValidDto();
        
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.SurgeonSpecificRegistry = dto.SurgeonSpecificRegistry;
            sut.RegistryComments = dto.RegistryComments;
            sut.RegisteredWithACHQC = dto.RegisteredWithACHQC;
            sut.RegisteredWithCESQIP = dto.RegisteredWithCESQIP;
            sut.RegisteredWithMBSAQIP = dto.RegisteredWithMBSAQIP;
            sut.RegisteredWithABA = dto.RegisteredWithABA;
            sut.RegisteredWithASBS = dto.RegisteredWithASBS;
            sut.RegisteredWithMSQC = dto.RegisteredWithMSQC;
            sut.RegisteredWithABMS = dto.RegisteredWithABMS;
            sut.RegisteredWithNCDB = dto.RegisteredWithNCDB;
            sut.RegisteredWithRQRS = dto.RegisteredWithRQRS;
            sut.RegisteredWithNSQIP = dto.RegisteredWithNSQIP;
            sut.RegisteredWithNTDB = dto.RegisteredWithNTDB;
            sut.RegisteredWithSTS = dto.RegisteredWithSTS;
            sut.RegisteredWithTQIP = dto.RegisteredWithTQIP;
            sut.RegisteredWithUNOS = dto.RegisteredWithUNOS;
            sut.RegisteredWithNCDR = dto.RegisteredWithNCDR;
            sut.RegisteredWithSVS = dto.RegisteredWithSVS;
            sut.RegisteredWithELSO = dto.RegisteredWithELSO;
            sut.RegisteredWithSSR = dto.RegisteredWithSSR;
            sut.UserConfirmed = dto.UserConfirmed;
            sut.UserConfirmedDateUtc = dto.UserConfirmedDateUtc;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null, "The passedDto is null, which can mean that the DataPortal method was not called.");
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                .Excluding(m => m.Id)
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
            var expectedUserID = 1234;
            
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IOutcomeRegistryDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserID))
                .ReturnsAsync(dto);
            
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<OutcomeRegistryDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IOutcomeRegistry, OutcomeRegistry>()
                .Build();
        
            var factory = new OutcomeRegistryFactory();
            var sut = await factory.GetByUserIdAsync();
            sut.SurgeonSpecificRegistry = Create<string>();
        
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
