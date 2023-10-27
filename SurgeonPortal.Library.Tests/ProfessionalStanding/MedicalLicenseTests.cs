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
	public class MedicalLicenseTests : TestBase<int>
    {
        private MedicalLicenseDto CreateValidDto()
        {     
            var dto = Create<MedicalLicenseDto>();

            dto.LicenseId = Create<decimal>();
            dto.UserId = 1234;
            dto.IssuingStateId = Create<string>();
            dto.IssuingState = Create<string>();
            dto.LicenseNumber = Create<string>();
            dto.LicenseTypeId = Create<int?>();
            dto.LicenseType = Create<string>();
            dto.IssueDate = Create<System.DateTime?>();
            dto.ExpireDate = Create<System.DateTime?>();
            dto.ReportingOrganization = Create<string>();
    
            return dto;
        }

            

        #region DeleteAsync
        
        [Test]
        public async Task Delete_CallsDalCorrectly()
        {
            var expectedLicenseId = Create<int>();
            var expectedUserId = 1234;
            
            var dto = CreateValidDto();
            MedicalLicenseDto passedDto = null;
        
            var mockDal = new Mock<IMedicalLicenseDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedLicenseId))
                .ReturnsAsync(dto);
            
            mockDal.Setup(m => m.DeleteAsync(It.IsAny<MedicalLicenseDto>()))
                .Callback<MedicalLicenseDto>((p) => passedDto = p)
                .Returns(Task.CompletedTask);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalLicense, MedicalLicense>()
                .Build();
        
            var factory = new MedicalLicenseFactory();
            var sut = await factory.GetByIdAsync(expectedLicenseId);
            sut.Delete();
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null);
        
            dto.Should().BeEquivalentTo(sut,
                options => options
                    .Excluding(m => m.CreatedAtUtc)
                    .Excluding(m => m.CreatedByUserId)
                    .Excluding(m => m.LastUpdatedAtUtc)
                    .Excluding(m => m.LastUpdatedByUserId)
                    .ExcludingMissingMembers());
        
            mockDal.VerifyAll();
        }
        
        #endregion

        #region GetByIdAsync MedicalLicense
        
        [Test]
        public async Task GetByIdAsync_CallsDalCorrectly()
        {
            var expectedLicenseId = Create<int>();
            
            var mockDal = new Mock<IMedicalLicenseDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedLicenseId))
                .ReturnsAsync(Create<MedicalLicenseDto>());
        
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalLicense, MedicalLicense>()
                .Build();
        
            var factory = new MedicalLicenseFactory();
            var sut = await factory.GetByIdAsync(expectedLicenseId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetById_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
            var expectedLicenseId = Create<int>();
        
            var mockDal = new Mock<IMedicalLicenseDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedLicenseId))
                .ReturnsAsync(dto);
        
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalLicense, MedicalLicense>()
                .Build();
        
            var factory = new MedicalLicenseFactory();
            var sut = await factory.GetByIdAsync(Create<int>());
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region Create
        
        [Test]
        public async Task Create_CallsDalCorrectly()
        {
            var dto = CreateValidDto();
            MedicalLicenseDto passedDto = null;
        
            var mockDal = new Mock<IMedicalLicenseDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<MedicalLicenseDto>()))
                .Callback<MedicalLicenseDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalLicense, MedicalLicense>()
                .Build();
        
            var factory = new MedicalLicenseFactory();
            var sut = factory.Create();
            
            sut.LicenseId = dto.LicenseId;
            sut.UserId = dto.UserId;
            sut.IssuingStateId = dto.IssuingStateId;
            sut.IssuingState = dto.IssuingState;
            sut.LicenseNumber = dto.LicenseNumber;
            sut.LicenseTypeId = dto.LicenseTypeId;
            sut.LicenseType = dto.LicenseType;
            sut.IssueDate = dto.IssueDate;
            sut.ExpireDate = dto.ExpireDate;
            sut.ReportingOrganization = dto.ReportingOrganization;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null);
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                .Excluding(m => m.CreatedAtUtc)
                .Excluding(m => m.CreatedByUserId)
                .Excluding(m => m.LastUpdatedAtUtc)
                .Excluding(m => m.LastUpdatedByUserId)
                .Excluding(m => m.LicenseId)
                .Excluding(m => m.IssuingState)
                .Excluding(m => m.LicenseType)
                .ExcludingMissingMembers());
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task Create_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IMedicalLicenseDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<MedicalLicenseDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalLicense, MedicalLicense>()
                .Build();
        
            var factory = new MedicalLicenseFactory();
            var sut = factory.Create();
            sut.IssuingStateId = Create<string>();
        
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
            var expectedLicenseId = Create<int>();
            
            var dto = CreateValidDto();
            MedicalLicenseDto passedDto = null;
        
            var mockDal = new Mock<IMedicalLicenseDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedLicenseId))
                        .ReturnsAsync(dto);
            
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<MedicalLicenseDto>()))
                .Callback<MedicalLicenseDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalLicense, MedicalLicense>()
                .Build();
        
            var factory = new MedicalLicenseFactory();
            var sut = await factory.GetByIdAsync(expectedLicenseId);
            
            sut.LicenseId = dto.LicenseId;
            sut.UserId = dto.UserId;
            sut.IssuingStateId = dto.IssuingStateId;
            sut.IssuingState = dto.IssuingState;
            sut.LicenseNumber = dto.LicenseNumber;
            sut.LicenseTypeId = dto.LicenseTypeId;
            sut.LicenseType = dto.LicenseType;
            sut.IssueDate = dto.IssueDate;
            sut.ExpireDate = dto.ExpireDate;
            sut.ReportingOrganization = dto.ReportingOrganization;
        
            // We now change all properties on the SUT to make it Dirty
            // or the SaveAsync() will not be called. :)
            dto = CreateValidDto();
        
            sut.LicenseId = dto.LicenseId;
            sut.UserId = dto.UserId;
            sut.IssuingStateId = dto.IssuingStateId;
            sut.IssuingState = dto.IssuingState;
            sut.LicenseNumber = dto.LicenseNumber;
            sut.LicenseTypeId = dto.LicenseTypeId;
            sut.LicenseType = dto.LicenseType;
            sut.IssueDate = dto.IssueDate;
            sut.ExpireDate = dto.ExpireDate;
            sut.ReportingOrganization = dto.ReportingOrganization;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null, "The passedDto is null, which can mean that the DataPortal method was not called.");
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                    .Excluding(m => m.CreatedAtUtc)
                    .Excluding(m => m.CreatedByUserId)
                    .Excluding(m => m.LastUpdatedAtUtc)
                    .Excluding(m => m.LastUpdatedByUserId)
                    .Excluding(m => m.IssuingState)
                    .Excluding(m => m.LicenseType)
                .ExcludingMissingMembers());
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task Update_YieldsCorrectResult()
        {
            var expectedLicenseId = Create<int>();
            
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IMedicalLicenseDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedLicenseId))
                        .ReturnsAsync(Create<MedicalLicenseDto>());
            
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<MedicalLicenseDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalLicense, MedicalLicense>()
                .Build();
        
            var factory = new MedicalLicenseFactory();
            var sut = await factory.GetByIdAsync(expectedLicenseId);
            sut.LicenseId = Create<int>();
        
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
