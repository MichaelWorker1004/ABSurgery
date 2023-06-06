using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.MedicalTraining;
using SurgeonPortal.Library.Contracts.MedicalTraining;
using SurgeonPortal.Library.MedicalTraining;
using System.Threading.Tasks;
using Ytg.UnitTest;
using MedicalTrainingNamespace = SurgeonPortal.Library.MedicalTraining;

namespace SurgeonPortal.Library.Tests.MedicalTraining
{
    [TestFixture] 
	public class MedicalTrainingTests : TestBase<string>
    {
        private MedicalTrainingDto CreateValidDto()
        {     
            var dto = Create<MedicalTrainingDto>();

            dto.Id = Create<int>();
            dto.UserId = Create<int>();
            dto.GraduateProfileId = Create<int>();
            dto.GraduateProfileDescription = Create<string>();
            dto.MedicalSchoolName = Create<string>();
            dto.MedicalSchoolCity = Create<string>();
            dto.MedicalSchoolStateId = Create<string>();
            dto.MedicalSchoolStateName = Create<string>();
            dto.MedicalSchoolCountryId = Create<string>();
            dto.MedicalSchoolCountryName = Create<string>();
            dto.DegreeId = Create<int>();
            dto.DegreeName = Create<string>();
            dto.MedicalSchoolCompletionYear = Create<string>();
            dto.ResidencyProgramName = Create<string>();
            dto.ResidencyCompletionYear = Create<string>();
            dto.ResidencyProgramOther = Create<string>();
            dto.CreatedAtUtc = Create<System.DateTime>();
            dto.CreatedByUserId = Create<int>();
            dto.LastUpdatedByUserId = Create<int>();
            dto.LastUpdatedAtUtc = Create<System.DateTime>();
    
            return dto;
        }

            #region MedicalTraining Business Rules
            #endregion

        #region GetByUserIdAsync MedicalTraining
        
        [Test]
        public async Task GetByUserIdAsync_CallsDalCorrectly()
        {
            
            var mockDal = new Mock<IMedicalTrainingDal>();
            mockDal.Setup(m => m.GetByUserIdAsync())
                .ReturnsAsync(Create<MedicalTrainingDto>());
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalTraining, MedicalTrainingNamespace.MedicalTraining>()
                .Build();
        
            var factory = new MedicalTrainingFactory();
            var sut = await factory.GetByUserIdAsync();
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByUserId_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IMedicalTrainingDal>();
            mockDal.Setup(m => m.GetByUserIdAsync())
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalTraining, MedicalTrainingNamespace.MedicalTraining>()
                .Build();
        
            var factory = new MedicalTrainingFactory();
            var sut = await factory.GetByUserIdAsync();
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region Create
        
        [Test]
        public async Task Create_CallsDalCorrectly()
        {
            var dto = CreateValidDto();
            MedicalTrainingDto passedDto = null;
        
            var mockDal = new Mock<IMedicalTrainingDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<MedicalTrainingDto>()))
                .Callback<MedicalTrainingDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalTraining, MedicalTrainingNamespace.MedicalTraining>()
                .Build();
        
            var factory = new MedicalTrainingFactory();
            var sut = factory.Create();
            
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.GraduateProfileId = dto.GraduateProfileId;
            sut.GraduateProfileDescription = dto.GraduateProfileDescription;
            sut.MedicalSchoolName = dto.MedicalSchoolName;
            sut.MedicalSchoolCity = dto.MedicalSchoolCity;
            sut.MedicalSchoolStateId = dto.MedicalSchoolStateId;
            sut.MedicalSchoolStateName = dto.MedicalSchoolStateName;
            sut.MedicalSchoolCountryId = dto.MedicalSchoolCountryId;
            sut.MedicalSchoolCountryName = dto.MedicalSchoolCountryName;
            sut.DegreeId = dto.DegreeId;
            sut.DegreeName = dto.DegreeName;
            sut.MedicalSchoolCompletionYear = dto.MedicalSchoolCompletionYear;
            sut.ResidencyProgramName = dto.ResidencyProgramName;
            sut.ResidencyCompletionYear = dto.ResidencyCompletionYear;
            sut.ResidencyProgramOther = dto.ResidencyProgramOther;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
        
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
                .Excluding(m => m.GraduateProfileDescription)
                .Excluding(m => m.MedicalSchoolStateName)
                .Excluding(m => m.MedicalSchoolCountryName)
                .Excluding(m => m.DegreeName)
                .Excluding(m => m.CreatedAtUtc)
                .Excluding(m => m.CreatedByUserId)
                .Excluding(m => m.LastUpdatedByUserId)
                .Excluding(m => m.LastUpdatedAtUtc)
                .ExcludingMissingMembers());
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task Create_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IMedicalTrainingDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<MedicalTrainingDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalTraining, MedicalTrainingNamespace.MedicalTraining>()
                .Build();
        
            var factory = new MedicalTrainingFactory();
            var sut = factory.Create();
            sut.GraduateProfileId = Create<int>();
        
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
            
            var dto = Create<MedicalTrainingDto>();
            MedicalTrainingDto passedDto = null;
        
            var mockDal = new Mock<IMedicalTrainingDal>();
            mockDal.Setup(m => m.GetByUserIdAsync())
                        .ReturnsAsync(dto);
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<MedicalTrainingDto>()))
                .Callback<MedicalTrainingDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalTraining, MedicalTrainingNamespace.MedicalTraining>()
                .Build();
        
            var factory = new MedicalTrainingFactory();
            var sut = await factory.GetByUserIdAsync();
            
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.GraduateProfileId = dto.GraduateProfileId;
            sut.GraduateProfileDescription = dto.GraduateProfileDescription;
            sut.MedicalSchoolName = dto.MedicalSchoolName;
            sut.MedicalSchoolCity = dto.MedicalSchoolCity;
            sut.MedicalSchoolStateId = dto.MedicalSchoolStateId;
            sut.MedicalSchoolStateName = dto.MedicalSchoolStateName;
            sut.MedicalSchoolCountryId = dto.MedicalSchoolCountryId;
            sut.MedicalSchoolCountryName = dto.MedicalSchoolCountryName;
            sut.DegreeId = dto.DegreeId;
            sut.DegreeName = dto.DegreeName;
            sut.MedicalSchoolCompletionYear = dto.MedicalSchoolCompletionYear;
            sut.ResidencyProgramName = dto.ResidencyProgramName;
            sut.ResidencyCompletionYear = dto.ResidencyCompletionYear;
            sut.ResidencyProgramOther = dto.ResidencyProgramOther;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
        
            // We now change all properties on the SUT to make it Dirty
            // or the SaveAsync() will not be called. :)
            dto = CreateValidDto();
        
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.GraduateProfileId = dto.GraduateProfileId;
            sut.GraduateProfileDescription = dto.GraduateProfileDescription;
            sut.MedicalSchoolName = dto.MedicalSchoolName;
            sut.MedicalSchoolCity = dto.MedicalSchoolCity;
            sut.MedicalSchoolStateId = dto.MedicalSchoolStateId;
            sut.MedicalSchoolStateName = dto.MedicalSchoolStateName;
            sut.MedicalSchoolCountryId = dto.MedicalSchoolCountryId;
            sut.MedicalSchoolCountryName = dto.MedicalSchoolCountryName;
            sut.DegreeId = dto.DegreeId;
            sut.DegreeName = dto.DegreeName;
            sut.MedicalSchoolCompletionYear = dto.MedicalSchoolCompletionYear;
            sut.ResidencyProgramName = dto.ResidencyProgramName;
            sut.ResidencyCompletionYear = dto.ResidencyCompletionYear;
            sut.ResidencyProgramOther = dto.ResidencyProgramOther;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null, "The passedDto is null, which can mean that the DataPortal method was not called.");
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                    .Excluding(m => m.CreatedAtUtc)
                    .Excluding(m => m.CreatedByUserId)
                    .Excluding(m => m.LastUpdatedAtUtc)
                    .Excluding(m => m.LastUpdatedByUserId)
                    .Excluding(m => m.GraduateProfileDescription)
                    .Excluding(m => m.MedicalSchoolStateName)
                    .Excluding(m => m.MedicalSchoolCountryName)
                    .Excluding(m => m.DegreeName)
                    .Excluding(m => m.CreatedAtUtc)
                    .Excluding(m => m.CreatedByUserId)
                    .Excluding(m => m.LastUpdatedByUserId)
                    .Excluding(m => m.LastUpdatedAtUtc)
                .ExcludingMissingMembers());
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task Update_YieldsCorrectResult()
        {
            
            var dto = Create<MedicalTrainingDto>();
        
            var mockDal = new Mock<IMedicalTrainingDal>();
            mockDal.Setup(m => m.GetByUserIdAsync())
                        .ReturnsAsync(Create<MedicalTrainingDto>());
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<MedicalTrainingDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalTraining, MedicalTrainingNamespace.MedicalTraining>()
                .Build();
        
            var factory = new MedicalTrainingFactory();
            var sut = await factory.GetByUserIdAsync();
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
