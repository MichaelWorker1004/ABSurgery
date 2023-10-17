using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.GraduateMedicalEducation;
using SurgeonPortal.Library.Contracts.GraduateMedicalEducation;
using SurgeonPortal.Library.GraduateMedicalEducation;
using System;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.GraduateMedicalEducation
{
    [TestFixture] 
	public class RotationTests : TestBase<int>
    {
        private RotationDto CreateValidDto()
        {
            var dto = Create<RotationDto>();
        
            dto.Id = Create<int>();
            dto.UserId = Create<int>();
            dto.StartDate = Create<System.DateTime>();
            dto.EndDate = Create<System.DateTime>();
            dto.ClinicalLevelId = Create<int>();
            dto.ClinicalLevel = Create<string>();
            dto.ClinicalActivityId = Create<int>();
            dto.ProgramName = Create<string>();
            dto.NonSurgicalActivity = Create<string>();
            dto.AlternateInstitutionName = Create<string>();
            dto.IsInternationalRotation = Create<bool>();
            dto.IsEssential = Create<bool>();
            dto.IsCredit = Create<bool>();
            dto.Other = Create<string>();
            dto.FourMonthRotationExplain = Create<string>();
            dto.NonPrimaryExplain = Create<string>();
            dto.NonClinicalExplain = Create<string>();
            dto.CreatedByUserId = Create<int>();
            dto.CreatedAtUtc = Create<System.DateTime>();
            dto.LastUpdatedAtUtc = Create<System.DateTime>();
            dto.LastUpdatedByUserId = Create<int>();
            dto.ClinicalActivity = Create<string>();
        
            return dto;
        }
        
        
        #region DeleteAsync
        
        [Test]
        public async Task Delete_CallsDalCorrectly()
        {
            var expectedId = Create<int>();
            
            var dto = CreateValidDto();
            RotationDto passedDto = null;
        
            var mockDal = new Mock<IRotationDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedId))
                .ReturnsAsync(dto);
            
            mockDal.Setup(m => m.DeleteAsync(It.IsAny<RotationDto>()))
                .Callback<RotationDto>((p) => passedDto = p)
                .Returns(Task.CompletedTask);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.TraineeClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IRotation, Rotation>()
				.WithBusinessObject<IOverlapConflictCommandFactory, OverlapConflictCommandFactory>()
                .Build();
        
            var factory = new RotationFactory();
            var sut = await factory.GetByIdAsync(expectedId);
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

        #region GetByIdAsync Rotation
        
        [Test]
        public async Task GetByIdAsync_CallsDalCorrectly()
        {
            var expectedId = Create<int>();
            
            var mockDal = new Mock<IRotationDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedId))
                .ReturnsAsync(Create<RotationDto>());
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.TraineeClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IRotation, Rotation>()
				.WithBusinessObject<IOverlapConflictCommandFactory, OverlapConflictCommandFactory>()
                .Build();
        
            var factory = new RotationFactory();
            var sut = await factory.GetByIdAsync(expectedId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetById_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
            var expectedId = Create<int>();
            
            var mockDal = new Mock<IRotationDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedId))
                .ReturnsAsync(dto);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.TraineeClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IRotation, Rotation>()
				.WithBusinessObject<IOverlapConflictCommandFactory, OverlapConflictCommandFactory>()
                .Build();
        
            var factory = new RotationFactory();
            var sut = await factory.GetByIdAsync(Create<int>());
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region Create
        
        [Test]
        public async Task Create_CallsDalCorrectly()
        {
            var dto = CreateValidDto();
            RotationDto passedDto = null;
        
            var mockDal = new Mock<IRotationDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<RotationDto>()))
                .Callback<RotationDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.TraineeClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IRotation, Rotation>()
                .WithBusinessObject<IOverlapConflictCommandFactory, OverlapConflictCommandFactory>()
                .Build();
        
            var factory = new RotationFactory();
            var sut = factory.Create();
            
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.StartDate = dto.StartDate;
            sut.EndDate = dto.EndDate;
            sut.ClinicalLevelId = dto.ClinicalLevelId;
            sut.ClinicalLevel = dto.ClinicalLevel;
            sut.ClinicalActivityId = dto.ClinicalActivityId;
            sut.ProgramName = dto.ProgramName;
            sut.NonSurgicalActivity = dto.NonSurgicalActivity;
            sut.AlternateInstitutionName = dto.AlternateInstitutionName;
            sut.IsInternationalRotation = dto.IsInternationalRotation;
            sut.IsEssential = dto.IsEssential;
            sut.IsCredit = dto.IsCredit;
            sut.Other = dto.Other;
            sut.FourMonthRotationExplain = dto.FourMonthRotationExplain;
            sut.NonPrimaryExplain = dto.NonPrimaryExplain;
            sut.NonClinicalExplain = dto.NonClinicalExplain;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
            sut.ClinicalActivity = dto.ClinicalActivity;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null);
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                .Excluding(m => m.CreatedAtUtc)
                .Excluding(m => m.CreatedByUserId)
                .Excluding(m => m.LastUpdatedAtUtc)
                .Excluding(m => m.LastUpdatedByUserId)
                .Excluding(m => m.Id)
                .Excluding(m => m.ClinicalLevel)
                .Excluding(m => m.CreatedAtUtc)
                .Excluding(m => m.LastUpdatedAtUtc)
                .Excluding(m => m.LastUpdatedByUserId)
                .Excluding(m => m.ClinicalActivity)
                .ExcludingMissingMembers());
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task Create_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IRotationDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<RotationDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.TraineeClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IRotation, Rotation>()
				.WithBusinessObject<IOverlapConflictCommandFactory, OverlapConflictCommandFactory>()
                .Build();
        
            var factory = new RotationFactory();
            var sut = factory.Create();
            sut.StartDate = Create<DateTime>();
        
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
            var expectedId = Create<int>();
            
            var dto = CreateValidDto();
            RotationDto passedDto = null;
        
            var mockDal = new Mock<IRotationDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedId))
                .ReturnsAsync(dto);
            
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<RotationDto>()))
                .Callback<RotationDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.TraineeClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IRotation, Rotation>()
				.WithBusinessObject<IOverlapConflictCommandFactory, OverlapConflictCommandFactory>()
                .Build();
        
            var factory = new RotationFactory();
            var sut = await factory.GetByIdAsync(expectedId);
            
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.StartDate = dto.StartDate;
            sut.EndDate = dto.EndDate;
            sut.ClinicalLevelId = dto.ClinicalLevelId;
            sut.ClinicalLevel = dto.ClinicalLevel;
            sut.ClinicalActivityId = dto.ClinicalActivityId;
            sut.ProgramName = dto.ProgramName;
            sut.NonSurgicalActivity = dto.NonSurgicalActivity;
            sut.AlternateInstitutionName = dto.AlternateInstitutionName;
            sut.IsInternationalRotation = dto.IsInternationalRotation;
            sut.IsEssential = dto.IsEssential;
            sut.IsCredit = dto.IsCredit;
            sut.Other = dto.Other;
            sut.FourMonthRotationExplain = dto.FourMonthRotationExplain;
            sut.NonPrimaryExplain = dto.NonPrimaryExplain;
            sut.NonClinicalExplain = dto.NonClinicalExplain;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
            sut.ClinicalActivity = dto.ClinicalActivity;
        
            // We now change all properties on the SUT to make it Dirty
            // or the SaveAsync() will not be called. :)
            dto = CreateValidDto();
        
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.StartDate = dto.StartDate;
            sut.EndDate = dto.EndDate;
            sut.ClinicalLevelId = dto.ClinicalLevelId;
            sut.ClinicalLevel = dto.ClinicalLevel;
            sut.ClinicalActivityId = dto.ClinicalActivityId;
            sut.ProgramName = dto.ProgramName;
            sut.NonSurgicalActivity = dto.NonSurgicalActivity;
            sut.AlternateInstitutionName = dto.AlternateInstitutionName;
            sut.IsInternationalRotation = dto.IsInternationalRotation;
            sut.IsEssential = dto.IsEssential;
            sut.IsCredit = dto.IsCredit;
            sut.Other = dto.Other;
            sut.FourMonthRotationExplain = dto.FourMonthRotationExplain;
            sut.NonPrimaryExplain = dto.NonPrimaryExplain;
            sut.NonClinicalExplain = dto.NonClinicalExplain;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
            sut.ClinicalActivity = dto.ClinicalActivity;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null, "The passedDto is null, which can mean that the DataPortal method was not called.");
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                    .Excluding(m => m.CreatedAtUtc)
                    .Excluding(m => m.CreatedByUserId)
                    .Excluding(m => m.LastUpdatedAtUtc)
                    .Excluding(m => m.LastUpdatedByUserId)
                    .Excluding(m => m.ClinicalLevel)
                    .Excluding(m => m.CreatedByUserId)
                    .Excluding(m => m.CreatedAtUtc)
                    .Excluding(m => m.LastUpdatedAtUtc)
                    .Excluding(m => m.ClinicalActivity)
                .ExcludingMissingMembers());
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task Update_YieldsCorrectResult()
        {
            var expectedId = Create<int>();
            
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IRotationDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedId))
                .ReturnsAsync(Create<RotationDto>());
            
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<RotationDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.TraineeClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IRotation, Rotation>()
				.WithBusinessObject<IOverlapConflictCommandFactory, OverlapConflictCommandFactory>()
                .Build();
        
            var factory = new RotationFactory();
            var sut = await factory.GetByIdAsync(expectedId);
            sut.Id = Create<int>();
        
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
