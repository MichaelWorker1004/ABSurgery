using FluentAssertions;
using MedicalTrainingNamespace = SurgeonPortal.Library.MedicalTraining;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.MedicalTraining;
using SurgeonPortal.Library.Contracts.MedicalTraining;
using SurgeonPortal.Library.MedicalTraining;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.MedicalTraining
{
    [TestFixture] 
	public class MedicalTrainingTests : TestBase<int>
    {
        private MedicalTrainingDto CreateValidDto()
        {
            var dto = Create<MedicalTrainingDto>();
        
            dto.Id = Create<int>();
            dto.UserId = 1234;
            dto.GraduateProfileId = Create<int?>();
            dto.GraduateProfileDescription = Create<string>();
            dto.MedicalSchoolName = Create<string>().Substring(0, 29);
            dto.MedicalSchoolCity = Create<string>().Substring(0, 29);
            dto.MedicalSchoolStateId = Create<string>();
            dto.MedicalSchoolStateName = Create<string>();
            dto.MedicalSchoolCountryId = Create<string>();
            dto.MedicalSchoolCountryName = Create<string>();
            dto.DegreeId = Create<int?>();
            dto.DegreeName = Create<string>();
            dto.MedicalSchoolCompletionYear = "2005";
            dto.ResidencyProgramName = Create<string>();
            dto.ResidencyCompletionYear = "2010";
            dto.ResidencyProgramOther = Create<string>();
            dto.CreatedAtUtc = Create<System.DateTime>();
            dto.CreatedByUserId = 1234;
            dto.LastUpdatedByUserId = 1234;
            dto.LastUpdatedAtUtc = Create<System.DateTime>();
        
            return dto;
        }
        
        #region MedicalTraining Business Rules
        [Test]
        public async Task IsRequired_GetByUserId_GraduateProfileId_Fails()
        {
            var dto = CreateValidDto();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IMedicalTrainingDal>(MockBehavior.Strict);
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(dto);
            
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalTraining, MedicalTrainingNamespace.MedicalTraining>()
                .Build();
        
            var factory = new MedicalTrainingFactory();
            var sut = await factory.GetByUserIdAsync();
            
            sut.GraduateProfileId = default;
        
            Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
        
            //Ensure that the save fails...
            var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(async () => await sut.SaveAsync());
            Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            Assert.That(sut.GetBrokenRules()[0].Description == "GraduateProfileId is required", $"Expected the rule description to be 'GraduateProfileId is required', have {sut.GetBrokenRules()[0].Description}");
            Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
            Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
        }
        
        [Test]
        public async Task IsRequired_GetByUserId_GraduateProfileId_Passes()
        {
            var dto = CreateValidDto();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IMedicalTrainingDal>(MockBehavior.Strict);
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(dto);
            
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalTraining, MedicalTrainingNamespace.MedicalTraining>()
                .Build();
        
            var factory = new MedicalTrainingFactory();
            var sut = await factory.GetByUserIdAsync();
            
            sut.GraduateProfileId = Create<int?>();
        
            Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
        
        }
        [Test]
        public async Task IsRequired_GetByUserId_MedicalSchoolName_Fails()
        {
            var dto = CreateValidDto();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IMedicalTrainingDal>(MockBehavior.Strict);
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(dto);
            
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalTraining, MedicalTrainingNamespace.MedicalTraining>()
                .Build();
        
            var factory = new MedicalTrainingFactory();
            var sut = await factory.GetByUserIdAsync();
            
            sut.MedicalSchoolName = default;
        
            Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
        
            //Ensure that the save fails...
            var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(async () => await sut.SaveAsync());
            Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            Assert.That(sut.GetBrokenRules()[0].Description == "MedicalSchoolName is required", $"Expected the rule description to be 'MedicalSchoolName is required', have {sut.GetBrokenRules()[0].Description}");
            Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
            Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
        }
        
        [Test]
        public async Task IsRequired_GetByUserId_MedicalSchoolName_Passes()
        {
            var dto = CreateValidDto();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IMedicalTrainingDal>(MockBehavior.Strict);
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(dto);
            
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalTraining, MedicalTrainingNamespace.MedicalTraining>()
                .Build();
        
            var factory = new MedicalTrainingFactory();
            var sut = await factory.GetByUserIdAsync();
            
            sut.MedicalSchoolName = Create<string>();
        
            Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
        
        }
        [Test]
        public async Task IsRequired_GetByUserId_MedicalSchoolCity_Fails()
        {
            var dto = CreateValidDto();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IMedicalTrainingDal>(MockBehavior.Strict);
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(dto);
            
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalTraining, MedicalTrainingNamespace.MedicalTraining>()
                .Build();
        
            var factory = new MedicalTrainingFactory();
            var sut = await factory.GetByUserIdAsync();
            
            sut.MedicalSchoolCity = default;
        
            Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
        
            //Ensure that the save fails...
            var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(async () => await sut.SaveAsync());
            Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            Assert.That(sut.GetBrokenRules()[0].Description == "MedicalSchoolCity is required", $"Expected the rule description to be 'MedicalSchoolCity is required', have {sut.GetBrokenRules()[0].Description}");
            Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
            Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
        }
        
        [Test]
        public async Task IsRequired_GetByUserId_MedicalSchoolCity_Passes()
        {
            var dto = CreateValidDto();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IMedicalTrainingDal>(MockBehavior.Strict);
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(dto);
            
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalTraining, MedicalTrainingNamespace.MedicalTraining>()
                .Build();
        
            var factory = new MedicalTrainingFactory();
            var sut = await factory.GetByUserIdAsync();
            
            sut.MedicalSchoolCity = Create<string>();
        
            Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
        
        }
        [Test]
        public async Task IsRequired_GetByUserId_MedicalSchoolCountryId_Fails()
        {
            var dto = CreateValidDto();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IMedicalTrainingDal>(MockBehavior.Strict);
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(dto);
            
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalTraining, MedicalTrainingNamespace.MedicalTraining>()
                .Build();
        
            var factory = new MedicalTrainingFactory();
            var sut = await factory.GetByUserIdAsync();
            
            sut.MedicalSchoolCountryId = default;
        
            Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
        
            //Ensure that the save fails...
            var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(async () => await sut.SaveAsync());
            Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            Assert.That(sut.GetBrokenRules()[0].Description == "MedicalSchoolCountryId is required", $"Expected the rule description to be 'MedicalSchoolCountryId is required', have {sut.GetBrokenRules()[0].Description}");
            Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
            Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
        }
        
        [Test]
        public async Task IsRequired_GetByUserId_MedicalSchoolCountryId_Passes()
        {
            var dto = CreateValidDto();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IMedicalTrainingDal>(MockBehavior.Strict);
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(dto);
            
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalTraining, MedicalTrainingNamespace.MedicalTraining>()
                .Build();
        
            var factory = new MedicalTrainingFactory();
            var sut = await factory.GetByUserIdAsync();
            
            sut.MedicalSchoolCountryId = Create<string>();
        
            Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
        
        }
        [Test]
        public async Task IsRequired_GetByUserId_DegreeId_Fails()
        {
            var dto = CreateValidDto();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IMedicalTrainingDal>(MockBehavior.Strict);
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(dto);
            
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalTraining, MedicalTrainingNamespace.MedicalTraining>()
                .Build();
        
            var factory = new MedicalTrainingFactory();
            var sut = await factory.GetByUserIdAsync();
            
            sut.DegreeId = default;
        
            Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
        
            //Ensure that the save fails...
            var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(async () => await sut.SaveAsync());
            Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            Assert.That(sut.GetBrokenRules()[0].Description == "DegreeId is required", $"Expected the rule description to be 'DegreeId is required', have {sut.GetBrokenRules()[0].Description}");
            Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
            Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
        }
        
        [Test]
        public async Task IsRequired_GetByUserId_DegreeId_Passes()
        {
            var dto = CreateValidDto();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IMedicalTrainingDal>(MockBehavior.Strict);
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(dto);
            
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalTraining, MedicalTrainingNamespace.MedicalTraining>()
                .Build();
        
            var factory = new MedicalTrainingFactory();
            var sut = await factory.GetByUserIdAsync();
            
            sut.DegreeId = Create<int?>();
        
            Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
        
        }
        [Test]
        public async Task IsRequired_GetByUserId_MedicalSchoolCompletionYear_Fails()
        {
            var dto = CreateValidDto();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IMedicalTrainingDal>(MockBehavior.Strict);
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(dto);
            
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalTraining, MedicalTrainingNamespace.MedicalTraining>()
                .Build();
        
            var factory = new MedicalTrainingFactory();
            var sut = await factory.GetByUserIdAsync();
            
            sut.MedicalSchoolCompletionYear = default;
        
            Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
        
            //Ensure that the save fails...
            var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(async () => await sut.SaveAsync());
            Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            Assert.That(sut.GetBrokenRules()[0].Description == "MedicalSchoolCompletionYear is required", $"Expected the rule description to be 'MedicalSchoolCompletionYear is required', have {sut.GetBrokenRules()[0].Description}");
            Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
            Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
        }
        
        [Test]
        public async Task IsRequired_GetByUserId_MedicalSchoolCompletionYear_Passes()
        {
            var dto = CreateValidDto();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IMedicalTrainingDal>(MockBehavior.Strict);
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(dto);
            
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalTraining, MedicalTrainingNamespace.MedicalTraining>()
                .Build();
        
            var factory = new MedicalTrainingFactory();
            var sut = await factory.GetByUserIdAsync();
            
            sut.MedicalSchoolCompletionYear = Create<string>();
        
            Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
        
        }
        [Test]
        public async Task IsRequired_GetByUserId_ResidencyProgramName_Fails()
        {
            var dto = CreateValidDto();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IMedicalTrainingDal>(MockBehavior.Strict);
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(dto);
            
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalTraining, MedicalTrainingNamespace.MedicalTraining>()
                .Build();
        
            var factory = new MedicalTrainingFactory();
            var sut = await factory.GetByUserIdAsync();
            
            sut.ResidencyProgramName = default;
        
            Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
        
            //Ensure that the save fails...
            var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(async () => await sut.SaveAsync());
            Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            Assert.That(sut.GetBrokenRules()[0].Description == "ResidencyProgramName is required", $"Expected the rule description to be 'ResidencyProgramName is required', have {sut.GetBrokenRules()[0].Description}");
            Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
            Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
        }
        
        [Test]
        public async Task IsRequired_GetByUserId_ResidencyProgramName_Passes()
        {
            var dto = CreateValidDto();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IMedicalTrainingDal>(MockBehavior.Strict);
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(dto);
            
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalTraining, MedicalTrainingNamespace.MedicalTraining>()
                .Build();
        
            var factory = new MedicalTrainingFactory();
            var sut = await factory.GetByUserIdAsync();
            
            sut.ResidencyProgramName = Create<string>();
        
            Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
        
        }
        [Test]
        public async Task IsRequired_GetByUserId_ResidencyCompletionYear_Fails()
        {
            var dto = CreateValidDto();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IMedicalTrainingDal>(MockBehavior.Strict);
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(dto);
            
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalTraining, MedicalTrainingNamespace.MedicalTraining>()
                .Build();
        
            var factory = new MedicalTrainingFactory();
            var sut = await factory.GetByUserIdAsync();
            
            sut.ResidencyCompletionYear = default;
        
            Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
        
            //Ensure that the save fails...
            var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(async () => await sut.SaveAsync());
            Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            Assert.That(sut.GetBrokenRules()[0].Description == "ResidencyCompletionYear is required", $"Expected the rule description to be 'ResidencyCompletionYear is required', have {sut.GetBrokenRules()[0].Description}");
            Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
            Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
        }
        
        [Test]
        public async Task IsRequired_GetByUserId_ResidencyCompletionYear_Passes()
        {
            var dto = CreateValidDto();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IMedicalTrainingDal>(MockBehavior.Strict);
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(dto);
            
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalTraining, MedicalTrainingNamespace.MedicalTraining>()
                .Build();
        
            var factory = new MedicalTrainingFactory();
            var sut = await factory.GetByUserIdAsync();
            
            sut.ResidencyCompletionYear = Create<string>();
        
            Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
        
        }
        [Test]
        public async Task IsRequired_GetByUserId_ResidencyProgramOther_Fails()
        {
            var dto = CreateValidDto();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IMedicalTrainingDal>(MockBehavior.Strict);
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(dto);
            
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalTraining, MedicalTrainingNamespace.MedicalTraining>()
                .Build();
        
            var factory = new MedicalTrainingFactory();
            var sut = await factory.GetByUserIdAsync();
            
            sut.ResidencyProgramOther = default;
        
            Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
        
            //Ensure that the save fails...
            var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(async () => await sut.SaveAsync());
            Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            Assert.That(sut.GetBrokenRules()[0].Description == "ResidencyProgramOther is required", $"Expected the rule description to be 'ResidencyProgramOther is required', have {sut.GetBrokenRules()[0].Description}");
            Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
            Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
        }
        
        [Test]
        public async Task IsRequired_GetByUserId_ResidencyProgramOther_Passes()
        {
            var dto = CreateValidDto();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IMedicalTrainingDal>(MockBehavior.Strict);
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(dto);
            
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalTraining, MedicalTrainingNamespace.MedicalTraining>()
                .Build();
        
            var factory = new MedicalTrainingFactory();
            var sut = await factory.GetByUserIdAsync();
            
            sut.ResidencyProgramOther = Create<string>();
        
            Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
        
        }
        #endregion

        #region GetByUserIdAsync MedicalTraining
        
        [Test]
        public async Task GetByUserIdAsync_CallsDalCorrectly()
        {
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IMedicalTrainingDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(Create<MedicalTrainingDto>());
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
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
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IMedicalTrainingDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(dto);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
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
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalTraining, MedicalTrainingNamespace.MedicalTraining>()
                .Build();
        
            var factory = new MedicalTrainingFactory();
            var sut = factory.Create();
            
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
                .Excluding(m => m.Id)
                .Excluding(m => m.GraduateProfileDescription)
                .Excluding(m => m.MedicalSchoolStateName)
                .Excluding(m => m.MedicalSchoolCountryName)
                .Excluding(m => m.DegreeName)
                .Excluding(m => m.CreatedAtUtc)
                .Excluding(m => m.LastUpdatedByUserId)
                .Excluding(m => m.LastUpdatedAtUtc)
                .Excluding(m => m.CreatedByUserId)
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
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalTraining, MedicalTrainingNamespace.MedicalTraining>()
                .Build();
        
            var factory = new MedicalTrainingFactory();
            var sut = factory.Create();
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
            MedicalTrainingDto passedDto = null;
        
            var mockDal = new Mock<IMedicalTrainingDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(dto);
            
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<MedicalTrainingDto>()))
                .Callback<MedicalTrainingDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalTraining, MedicalTrainingNamespace.MedicalTraining>()
                .Build();
        
            var factory = new MedicalTrainingFactory();
            var sut = await factory.GetByUserIdAsync();
            
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
                .Excluding(m => m.GraduateProfileDescription)
                .Excluding(m => m.MedicalSchoolStateName)
                .Excluding(m => m.MedicalSchoolCountryName)
                .Excluding(m => m.DegreeName)
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
            var expectedUserId = 1234;
            
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IMedicalTrainingDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(Create<MedicalTrainingDto>());
            
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<MedicalTrainingDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalTraining, MedicalTrainingNamespace.MedicalTraining>()
                .Build();
        
            var factory = new MedicalTrainingFactory();
            var sut = await factory.GetByUserIdAsync();
            sut.DegreeName = Create<string>();
        
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
