using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Examinations.GQ;
using SurgeonPortal.Library.Contracts.Examinations.GQ;
using SurgeonPortal.Library.Examinations.GQ;
using System;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Examinations.GQ
{
    [TestFixture] 
	public class AdditionalTrainingTests : TestBase<int>
    {
        private AdditionalTrainingDto CreateValidDto()
        {     
            var dto = Create<AdditionalTrainingDto>();

            dto.TrainingId = Create<int>();
            dto.DateEnded = Create<System.DateTime>();
            dto.DateStarted = Create<System.DateTime>();
            dto.Other = Create<string>();
            dto.InstitutionId = Create<int>();
            dto.InstitutionName = Create<string>();
            dto.City = Create<string>();
            dto.StateId = Create<string>();
            dto.State = Create<string>();
            dto.TypeOfTraining = Create<string>();
    
            return dto;
        }

            

        #region GetByTrainingIdAsync AdditionalTraining
        
        [Test]
        public async Task GetByTrainingIdAsync_CallsDalCorrectly()
        {
            var expectedTrainingId = Create<int>();
            
            var mockDal = new Mock<IAdditionalTrainingDal>();
            mockDal.Setup(m => m.GetByTrainingIdAsync(expectedTrainingId))
                .ReturnsAsync(Create<AdditionalTrainingDto>());
        
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAdditionalTraining, AdditionalTraining>()
                .Build();
        
            var factory = new AdditionalTrainingFactory();
            var sut = await factory.GetByTrainingIdAsync(expectedTrainingId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByTrainingId_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
            var expectedTrainingId = Create<int>();
        
            var mockDal = new Mock<IAdditionalTrainingDal>();
            mockDal.Setup(m => m.GetByTrainingIdAsync(expectedTrainingId))
                .ReturnsAsync(dto);
        
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAdditionalTraining, AdditionalTraining>()
                .Build();
        
            var factory = new AdditionalTrainingFactory();
            var sut = await factory.GetByTrainingIdAsync(expectedTrainingId);
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region Create
        
        [Test]
        public async Task Create_CallsDalCorrectly()
        {
            var dto = CreateValidDto();
            AdditionalTrainingDto passedDto = null;
        
            var mockDal = new Mock<IAdditionalTrainingDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<AdditionalTrainingDto>()))
                .Callback<AdditionalTrainingDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAdditionalTraining, AdditionalTraining>()
                .Build();
        
            var factory = new AdditionalTrainingFactory();
            var sut = factory.Create();
            
            sut.TrainingId = dto.TrainingId;
            sut.DateEnded = dto.DateEnded;
            sut.DateStarted = dto.DateStarted;
            sut.Other = dto.Other;
            sut.InstitutionId = dto.InstitutionId;
            sut.InstitutionName = dto.InstitutionName;
            sut.City = dto.City;
            sut.StateId = dto.StateId;
            sut.State = dto.State;
            sut.TypeOfTraining = dto.TypeOfTraining;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null);
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                .Excluding(m => m.TrainingId)
                .Excluding(m => m.InstitutionName)
                .Excluding(m => m.State)
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
        
            var mockDal = new Mock<IAdditionalTrainingDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<AdditionalTrainingDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAdditionalTraining, AdditionalTraining>()
                .Build();
        
            var factory = new AdditionalTrainingFactory();
            var sut = factory.Create();
            sut.DateEnded = Create<DateTime>();
        
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
            var expectedTrainingId = Create<int>();
            
            var dto = CreateValidDto();
            AdditionalTrainingDto passedDto = null;
        
            var mockDal = new Mock<IAdditionalTrainingDal>();
            mockDal.Setup(m => m.GetByTrainingIdAsync(expectedTrainingId))
                        .ReturnsAsync(dto);
            
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<AdditionalTrainingDto>()))
                .Callback<AdditionalTrainingDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAdditionalTraining, AdditionalTraining>()
                .Build();
        
            var factory = new AdditionalTrainingFactory();
            var sut = await factory.GetByTrainingIdAsync(expectedTrainingId);
            
            sut.TrainingId = dto.TrainingId;
            sut.DateEnded = dto.DateEnded;
            sut.DateStarted = dto.DateStarted;
            sut.Other = dto.Other;
            sut.InstitutionId = dto.InstitutionId;
            sut.InstitutionName = dto.InstitutionName;
            sut.City = dto.City;
            sut.StateId = dto.StateId;
            sut.State = dto.State;
            sut.TypeOfTraining = dto.TypeOfTraining;
        
            // We now change all properties on the SUT to make it Dirty
            // or the SaveAsync() will not be called. :)
            dto = CreateValidDto();
        
            sut.TrainingId = dto.TrainingId;
            sut.DateEnded = dto.DateEnded;
            sut.DateStarted = dto.DateStarted;
            sut.Other = dto.Other;
            sut.InstitutionId = dto.InstitutionId;
            sut.InstitutionName = dto.InstitutionName;
            sut.City = dto.City;
            sut.StateId = dto.StateId;
            sut.State = dto.State;
            sut.TypeOfTraining = dto.TypeOfTraining;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null, "The passedDto is null, which can mean that the DataPortal method was not called.");
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                .Excluding(m => m.InstitutionName)
                .Excluding(m => m.State)
                    .Excluding(m => m.CreatedAtUtc)
                    .Excluding(m => m.CreatedByUserId)
                    .Excluding(m => m.LastUpdatedAtUtc)
                    .Excluding(m => m.LastUpdatedByUserId)
                .ExcludingMissingMembers());
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task Update_YieldsCorrectResult()
        {
            var expectedTrainingId = Create<int>();
            
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IAdditionalTrainingDal>();
            mockDal.Setup(m => m.GetByTrainingIdAsync(expectedTrainingId))
                        .ReturnsAsync(Create<AdditionalTrainingDto>());
            
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<AdditionalTrainingDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAdditionalTraining, AdditionalTraining>()
                .Build();
        
            var factory = new AdditionalTrainingFactory();
            var sut = await factory.GetByTrainingIdAsync(expectedTrainingId);
            sut.TrainingId = Create<int>();
        
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
