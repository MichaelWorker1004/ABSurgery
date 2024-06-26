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
            dto.UserId = 1234;
            dto.StartDate = DateTime.Now.AddDays(1);
            dto.EndDate = DateTime.Now.AddDays(5);
            dto.ClinicalLevelId = 1;
            dto.ClinicalLevel = Create<string>();
            dto.ClinicalActivityId = Create<int>();
            dto.ProgramName = Create<string>();
            dto.NonSurgicalActivity = Create<string>();
            dto.AlternateInstitutionName = Create<string>();
            dto.IsInternationalRotation = Create<bool>();
            dto.IsEssential = true;
            dto.IsCredit = true;
            dto.Other = Create<string>();
            dto.FourMonthRotationExplain = Create<string>();
            dto.NonPrimaryExplain = Create<string>();
            dto.NonClinicalExplain = Create<string>();
            dto.CreatedByUserId = 1234;
            dto.CreatedAtUtc = Create<System.DateTime>();
            dto.LastUpdatedAtUtc = Create<System.DateTime>();
            dto.LastUpdatedByUserId = 1234;
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
        
            var mocks = GetMockedCommand(false);

            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.TraineeClaim)
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(mocks.MockCommand)
                .WithRegisteredInstance(mocks.MockCommandFactory)
                .WithBusinessObject<IRotation, Rotation>()
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
            
            var mocks = GetMockedCommand(false);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.TraineeClaim)
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(mocks.MockCommand)
                .WithRegisteredInstance(mocks.MockCommandFactory)
                .WithBusinessObject<IRotation, Rotation>()
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
            
            var mocks = GetMockedCommand(false);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.TraineeClaim)
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(mocks.MockCommand)
                .WithRegisteredInstance(mocks.MockCommandFactory)
                .WithBusinessObject<IRotation, Rotation>()
                .Build();
        
            var factory = new RotationFactory();
            var sut = await factory.GetByIdAsync(expectedId);
        
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
        
            var mocks = GetMockedCommand(false);

            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.TraineeClaim)
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(mocks.MockCommand)
                .WithRegisteredInstance(mocks.MockCommandFactory)
                .WithBusinessObject<IRotation, Rotation>()
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
                .Excluding(m => m.Id)
                .Excluding(m => m.ClinicalLevel)
                .Excluding(m => m.CreatedAtUtc)
                .Excluding(m => m.LastUpdatedAtUtc)
                .Excluding(m => m.LastUpdatedByUserId)
                .Excluding(m => m.ClinicalActivity)
                .Excluding(m => m.CreatedByUserId)
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
        
            var mocks = GetMockedCommand(false);

            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.TraineeClaim)
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(mocks.MockCommand)
                .WithRegisteredInstance(mocks.MockCommandFactory)
                .WithBusinessObject<IRotation, Rotation>()
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
        
            var mocks = GetMockedCommand(false);

            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.TraineeClaim)
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(mocks.MockCommand)
                .WithRegisteredInstance(mocks.MockCommandFactory)
                .WithBusinessObject<IRotation, Rotation>()
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
                .Excluding(m => m.ClinicalLevel)
                .Excluding(m => m.CreatedByUserId)
                .Excluding(m => m.CreatedAtUtc)
                .Excluding(m => m.LastUpdatedAtUtc)
                .Excluding(m => m.ClinicalActivity)
                .Excluding(m => m.LastUpdatedByUserId)
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
                .ReturnsAsync(dto);
            
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<RotationDto>()))
                .ReturnsAsync(dto);
        
            var mocks = GetMockedCommand(false);

            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.TraineeClaim)
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(mocks.MockCommand)
                .WithRegisteredInstance(mocks.MockCommandFactory)
                .WithBusinessObject<IRotation, Rotation>()
                .Build();
        
            var factory = new RotationFactory();
            var sut = await factory.GetByIdAsync(expectedId);
            sut.StartDate = DateTime.Now.AddDays(2);
        
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

        (Mock<IOverlapConflictCommand> MockCommand, Mock<IOverlapConflictCommandFactory> MockCommandFactory) GetMockedCommand(bool hasConflict)
        {
            var mockCommandFactory = new Mock<IOverlapConflictCommandFactory>();
            var mockCommand = new Mock<IOverlapConflictCommand>();
            mockCommand.SetupGet(p => p.OverlapConflict).Returns(false);
            mockCommand.SetupGet(p => p.StartDate).Returns(new DateTime(2023, 10, 31));
            mockCommand.SetupGet(p => p.EndDate).Returns(new DateTime(2023, 11, 15));
            mockCommand.SetupGet(p => p.UserId).Returns(1234);
            mockCommand.SetupGet(p => p.RotationId).Returns(It.IsAny<int>());

            mockCommandFactory
                .Setup(f => f.CheckOverlapConflicts(It.IsAny<int>(),
                        It.IsAny<DateTime>(),
                        It.IsAny<DateTime>(),
                        It.IsAny<int?>()))
                .Returns(mockCommand.Object);

            return (mockCommand, mockCommandFactory);
        }
	}
}
