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
	public class SanctionsTests : TestBase<int>
    {
        private SanctionsDto CreateValidDto()
        {
            var dto = Create<SanctionsDto>();
        
            dto.Id = Create<int>();
            dto.UserId = 1234;
            dto.HadDrugAlchoholTreatment = Create<bool>();
            dto.HadHospitalPrivilegesDenied = Create<bool>();
            dto.HadLicenseRestricted = Create<bool>();
            dto.HadHospitalPrivilegesRestricted = Create<bool>();
            dto.HadFelonyConviction = Create<bool>();
            dto.HadCensure = Create<bool>();
            dto.Explanation = Create<string>();
            dto.CreatedByUserId = 1234;
            dto.CreatedAtUtc = Create<System.DateTime>();
            dto.LastUpdatedAtUtc = Create<System.DateTime>();
            dto.LastUpdatedByUserId = 1234;
        
            return dto;
        }
        
        

        #region GetByUserIdAsync Sanctions
        
        [Test]
        public async Task GetByUserIdAsync_CallsDalCorrectly()
        {
            var expectedUserId = 1234;
            
            var mockDal = new Mock<ISanctionsDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(Create<SanctionsDto>());
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ISanctions, Sanctions>()
                .Build();
        
            var factory = new SanctionsFactory();
            var sut = await factory.GetByUserIdAsync();
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByUserId_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<ISanctionsDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(dto);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ISanctions, Sanctions>()
                .Build();
        
            var factory = new SanctionsFactory();
            var sut = await factory.GetByUserIdAsync();
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region Create
        
        [Test]
        public async Task Create_CallsDalCorrectly()
        {
            var dto = CreateValidDto();
            SanctionsDto passedDto = null;
        
            var mockDal = new Mock<ISanctionsDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<SanctionsDto>()))
                .Callback<SanctionsDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ISanctions, Sanctions>()
                .Build();
        
            var factory = new SanctionsFactory();
            var sut = factory.Create();
            
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.HadDrugAlchoholTreatment = dto.HadDrugAlchoholTreatment;
            sut.HadHospitalPrivilegesDenied = dto.HadHospitalPrivilegesDenied;
            sut.HadLicenseRestricted = dto.HadLicenseRestricted;
            sut.HadHospitalPrivilegesRestricted = dto.HadHospitalPrivilegesRestricted;
            sut.HadFelonyConviction = dto.HadFelonyConviction;
            sut.HadCensure = dto.HadCensure;
            sut.Explanation = dto.Explanation;
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
        
            var mockDal = new Mock<ISanctionsDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<SanctionsDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ISanctions, Sanctions>()
                .Build();
        
            var factory = new SanctionsFactory();
            var sut = factory.Create();
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.HadDrugAlchoholTreatment = dto.HadDrugAlchoholTreatment;
            sut.HadHospitalPrivilegesDenied = dto.HadHospitalPrivilegesDenied;
            sut.HadLicenseRestricted = dto.HadLicenseRestricted;
            sut.HadHospitalPrivilegesRestricted = dto.HadHospitalPrivilegesRestricted;
            sut.HadFelonyConviction = dto.HadFelonyConviction;
            sut.HadCensure = dto.HadCensure;
            sut.Explanation = dto.Explanation;
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
            SanctionsDto passedDto = null;
        
            var mockDal = new Mock<ISanctionsDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(dto);
            
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<SanctionsDto>()))
                .Callback<SanctionsDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ISanctions, Sanctions>()
                .Build();
        
            var factory = new SanctionsFactory();
            var sut = await factory.GetByUserIdAsync();
            
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.HadDrugAlchoholTreatment = dto.HadDrugAlchoholTreatment;
            sut.HadHospitalPrivilegesDenied = dto.HadHospitalPrivilegesDenied;
            sut.HadLicenseRestricted = dto.HadLicenseRestricted;
            sut.HadHospitalPrivilegesRestricted = dto.HadHospitalPrivilegesRestricted;
            sut.HadFelonyConviction = dto.HadFelonyConviction;
            sut.HadCensure = dto.HadCensure;
            sut.Explanation = dto.Explanation;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
        
            // We now change all properties on the SUT to make it Dirty
            // or the SaveAsync() will not be called. :)
            dto = CreateValidDto();
        
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.HadDrugAlchoholTreatment = dto.HadDrugAlchoholTreatment;
            sut.HadHospitalPrivilegesDenied = dto.HadHospitalPrivilegesDenied;
            sut.HadLicenseRestricted = dto.HadLicenseRestricted;
            sut.HadHospitalPrivilegesRestricted = dto.HadHospitalPrivilegesRestricted;
            sut.HadFelonyConviction = dto.HadFelonyConviction;
            sut.HadCensure = dto.HadCensure;
            sut.Explanation = dto.Explanation;
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
            var expectedUserId = 1234;
            
            var dto = CreateValidDto();
        
            var mockDal = new Mock<ISanctionsDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(Create<SanctionsDto>());
            
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<SanctionsDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ISanctions, Sanctions>()
                .Build();
        
            var factory = new SanctionsFactory();
            var sut = await factory.GetByUserIdAsync();
            sut.HadDrugAlchoholTreatment = Create<bool>();
        
            await sut.SaveAsync();
            
            dto.Should().BeEquivalentTo(sut,
                options => options
                    .Excluding(m => m.CreatedAtUtc)
                    .Excluding(m => m.CreatedByUserId)
                    .Excluding(m => m.LastUpdatedAtUtc)
                    .Excluding(m => m.LastUpdatedByUserId)
                    .Excluding(m => m.UserId)
                    .Excluding(m => m.Id)
                    .Excluding(m => m.Explanation)
                .ExcludingMissingMembers());
        }
        
        #endregion
	}
}
