using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.MedicalTraining;
using SurgeonPortal.Library.Contracts.MedicalTraining;
using SurgeonPortal.Library.MedicalTraining;
using System;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.MedicalTraining
{
    [TestFixture] 
	public class AdvancedTrainingTests : TestBase<int>
    {
        private AdvancedTrainingDto CreateValidDto()
        {
            var dto = Create<AdvancedTrainingDto>();
        
            dto.Id = Create<int>();
            dto.UserId = 1234;
            dto.TrainingTypeId = Create<int?>();
            dto.TrainingType = Create<string>();
            dto.ProgramId = Create<int?>();
            dto.InstitutionName = Create<string>();
            dto.City = Create<string>();
            dto.State = Create<string>();
            dto.Other = Create<string>();
            dto.StartDate = DateTime.Now.AddDays(1);
            dto.EndDate = DateTime.Now.AddDays(5);
            dto.CreatedByUserId = 1234;
            dto.CreatedAtUtc = Create<System.DateTime>();
            dto.LastUpdatedAtUtc = Create<System.DateTime>();
            dto.LastUpdatedByUserId = 1234;
        
            return dto;
        }
        
        #region AdvancedTraining Business Rules
        [Test]
        public async Task IsRequired_GetByTrainingId_TrainingTypeId_Fails()
        {
            var dto = CreateValidDto();
            var expectedId = Create<int>();
            
            var mockDal = new Mock<IAdvancedTrainingDal>(MockBehavior.Strict);
            mockDal.Setup(m => m.GetByTrainingIdAsync(expectedId))
                .ReturnsAsync(dto);
            
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAdvancedTraining, AdvancedTraining>()
                .Build();
        
            var factory = new AdvancedTrainingFactory();
            var sut = await factory.GetByTrainingIdAsync(expectedId);
            
            sut.TrainingTypeId = default;
        
            Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
        
            //Ensure that the save fails...
            var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(async () => await sut.SaveAsync());
            Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            Assert.That(sut.GetBrokenRules()[0].Description == "TrainingTypeId is required", $"Expected the rule description to be 'TrainingTypeId is required', have {sut.GetBrokenRules()[0].Description}");
            Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
            Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
        }
        
        [Test]
        public async Task IsRequired_GetByTrainingId_TrainingTypeId_Passes()
        {
            var dto = CreateValidDto();
            var expectedId = Create<int>();
            
            var mockDal = new Mock<IAdvancedTrainingDal>(MockBehavior.Strict);
            mockDal.Setup(m => m.GetByTrainingIdAsync(expectedId))
                .ReturnsAsync(dto);
            
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAdvancedTraining, AdvancedTraining>()
                .Build();
        
            var factory = new AdvancedTrainingFactory();
            var sut = await factory.GetByTrainingIdAsync(expectedId);
            
            sut.TrainingTypeId = dto.TrainingTypeId;
        
            Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
        
        }
        [Test]
        public async Task IsRequired_GetByTrainingId_StartDate_Fails()
        {
            var dto = CreateValidDto();
            var expectedId = Create<int>();
            
            var mockDal = new Mock<IAdvancedTrainingDal>(MockBehavior.Strict);
            mockDal.Setup(m => m.GetByTrainingIdAsync(expectedId))
                .ReturnsAsync(dto);
            
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAdvancedTraining, AdvancedTraining>()
                .Build();
        
            var factory = new AdvancedTrainingFactory();
            var sut = await factory.GetByTrainingIdAsync(expectedId);
            
            sut.StartDate = default;
        
            Assert.That(sut.GetBrokenRules().Count == 2, $"Expected 2 broken rule, have {sut.GetBrokenRules().Count} ");
        
            //Ensure that the save fails...
            var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(async () => await sut.SaveAsync());
            Assert.That(sut.GetBrokenRules().Count == 2, $"Expected 2 broken rule, have {sut.GetBrokenRules().Count} ");
            Assert.That(sut.GetBrokenRules()[0].Description == "StartDate is required", $"Expected the rule description to be 'StartDate is required', have {sut.GetBrokenRules()[0].Description}");
            Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
            Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
        }
        
        [Test]
        public async Task IsRequired_GetByTrainingId_StartDate_Passes()
        {
            var dto = CreateValidDto();
            var expectedId = Create<int>();
            
            var mockDal = new Mock<IAdvancedTrainingDal>(MockBehavior.Strict);
            mockDal.Setup(m => m.GetByTrainingIdAsync(expectedId))
                .ReturnsAsync(dto);
            
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAdvancedTraining, AdvancedTraining>()
                .Build();
        
            var factory = new AdvancedTrainingFactory();
            var sut = await factory.GetByTrainingIdAsync(expectedId);
            
            sut.StartDate = dto.StartDate;
            sut.EndDate = dto.EndDate;
        
            Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
        
        }
        [Test]
        public async Task IsRequired_GetByTrainingId_EndDate_Fails()
        {
            var dto = CreateValidDto();
            var expectedId = Create<int>();
            
            var mockDal = new Mock<IAdvancedTrainingDal>(MockBehavior.Strict);
            mockDal.Setup(m => m.GetByTrainingIdAsync(expectedId))
                .ReturnsAsync(dto);
            
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAdvancedTraining, AdvancedTraining>()
                .Build();
        
            var factory = new AdvancedTrainingFactory();
            var sut = await factory.GetByTrainingIdAsync(expectedId);
            
            sut.EndDate = default;
        
            Assert.That(sut.GetBrokenRules().Count == 2, $"Expected 2 broken rule, have {sut.GetBrokenRules().Count} ");
        
            //Ensure that the save fails...
            var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(async () => await sut.SaveAsync());
            Assert.That(sut.GetBrokenRules().Count == 2, $"Expected 2 broken rules, have {sut.GetBrokenRules().Count} ");
            Assert.That(sut.GetBrokenRules()[0].Description == "EndDate is required", $"Expected the rule description to be 'EndDate is required', have {sut.GetBrokenRules()[0].Description}");
            Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
            Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
        }
        
        [Test]
        public async Task IsRequired_GetByTrainingId_EndDate_Passes()
        {
            var dto = CreateValidDto();
            var expectedId = Create<int>();
            
            var mockDal = new Mock<IAdvancedTrainingDal>(MockBehavior.Strict);
            mockDal.Setup(m => m.GetByTrainingIdAsync(expectedId))
                .ReturnsAsync(dto);
            
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAdvancedTraining, AdvancedTraining>()
                .Build();
        
            var factory = new AdvancedTrainingFactory();
            var sut = await factory.GetByTrainingIdAsync(expectedId);
            
            sut.EndDate = dto.EndDate;
            sut.StartDate = dto.StartDate;
        
            Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
        
        }
        #endregion

        #region GetByTrainingIdAsync AdvancedTraining
        
        [Test]
        public async Task GetByTrainingIdAsync_CallsDalCorrectly()
        {
            var expectedId = Create<int>();
            
            var mockDal = new Mock<IAdvancedTrainingDal>();
            mockDal.Setup(m => m.GetByTrainingIdAsync(expectedId))
                .ReturnsAsync(Create<AdvancedTrainingDto>());
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAdvancedTraining, AdvancedTraining>()
                .Build();
        
            var factory = new AdvancedTrainingFactory();
            var sut = await factory.GetByTrainingIdAsync(expectedId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByTrainingId_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
            var expectedId = Create<int>();
            
            var mockDal = new Mock<IAdvancedTrainingDal>();
            mockDal.Setup(m => m.GetByTrainingIdAsync(expectedId))
                .ReturnsAsync(dto);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAdvancedTraining, AdvancedTraining>()
                .Build();
        
            var factory = new AdvancedTrainingFactory();
            var sut = await factory.GetByTrainingIdAsync(expectedId);
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region Create
        
        [Test]
        public async Task Create_CallsDalCorrectly()
        {
            var dto = CreateValidDto();
            AdvancedTrainingDto passedDto = null;
        
            var mockDal = new Mock<IAdvancedTrainingDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<AdvancedTrainingDto>()))
                .Callback<AdvancedTrainingDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAdvancedTraining, AdvancedTraining>()
                .Build();
        
            var factory = new AdvancedTrainingFactory();
            var sut = factory.Create();
            
            sut.TrainingTypeId = dto.TrainingTypeId;
            sut.ProgramId = dto.ProgramId;
            sut.Other = dto.Other;
            sut.StartDate = dto.StartDate;
            sut.EndDate = dto.EndDate;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null);
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                .Excluding(m => m.Id)
                .Excluding(m => m.TrainingType)
                .Excluding(m => m.InstitutionName)
                .Excluding(m => m.City)
                .Excluding(m => m.State)
                .Excluding(m => m.CreatedAtUtc)
                .Excluding(m => m.LastUpdatedAtUtc)
                .Excluding(m => m.LastUpdatedByUserId)
                .Excluding(m => m.CreatedByUserId)
                .ExcludingMissingMembers());
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task Create_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IAdvancedTrainingDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<AdvancedTrainingDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAdvancedTraining, AdvancedTraining>()
                .Build();
        
            var factory = new AdvancedTrainingFactory();
            var sut = factory.Create();
            sut.TrainingTypeId = dto.TrainingTypeId;
            sut.ProgramId = dto.ProgramId;
            sut.Other = dto.Other;
            sut.StartDate = dto.StartDate;
            sut.EndDate = dto.EndDate;
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
            var expectedId = Create<int>();
            
            var dto = CreateValidDto();
            AdvancedTrainingDto passedDto = null;
        
            var mockDal = new Mock<IAdvancedTrainingDal>();
            mockDal.Setup(m => m.GetByTrainingIdAsync(expectedId))
                .ReturnsAsync(dto);
            
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<AdvancedTrainingDto>()))
                .Callback<AdvancedTrainingDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAdvancedTraining, AdvancedTraining>()
                .Build();
        
            var factory = new AdvancedTrainingFactory();
            var sut = await factory.GetByTrainingIdAsync(expectedId);
            
            sut.TrainingTypeId = dto.TrainingTypeId;
            sut.ProgramId = dto.ProgramId;
            sut.Other = dto.Other;
            sut.StartDate = dto.StartDate;
            sut.EndDate = dto.EndDate;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
        
            // We now change all properties on the SUT to make it Dirty
            // or the SaveAsync() will not be called. :)
            dto = CreateValidDto();
        
            sut.TrainingTypeId = dto.TrainingTypeId;
            sut.ProgramId = dto.ProgramId;
            sut.Other = dto.Other;
            sut.StartDate = dto.StartDate;
            sut.EndDate = dto.EndDate;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null, "The passedDto is null, which can mean that the DataPortal method was not called.");
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                .Excluding(m => m.TrainingType)
                .Excluding(m => m.InstitutionName)
                .Excluding(m => m.City)
                .Excluding(m => m.State)
                .Excluding(m => m.CreatedByUserId)
                .Excluding(m => m.CreatedAtUtc)
                .Excluding(m => m.LastUpdatedAtUtc)
                .Excluding(m => m.LastUpdatedByUserId)
                .Excluding(m => m.Id)
                .ExcludingMissingMembers());
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task Update_YieldsCorrectResult()
        {
            var expectedId = Create<int>();
            
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IAdvancedTrainingDal>();
            mockDal.Setup(m => m.GetByTrainingIdAsync(expectedId))
                .ReturnsAsync(Create<AdvancedTrainingDto>());
            
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<AdvancedTrainingDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAdvancedTraining, AdvancedTraining>()
                .Build();
        
            var factory = new AdvancedTrainingFactory();
            var sut = await factory.GetByTrainingIdAsync(expectedId);
            sut.Other = Create<string>();
        
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
