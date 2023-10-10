using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.Library.Contracts.Users;
using SurgeonPortal.Library.Users;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Users
{
    [TestFixture] 
	public class UserProfileTests : TestBase<int>
    {
        private UserProfileDto CreateValidDto()
        {     
            var dto = Create<UserProfileDto>();

            dto.UserProfileId = Create<int>();
            dto.UserId = Create<int>();
            dto.FirstName = Create<string>();
            dto.MiddleName = Create<string>();
            dto.LastName = Create<string>();
            dto.Suffix = Create<string>();
            dto.DisplayName = Create<string>();
            dto.OfficePhoneNumber = Create<string>();
            dto.MobilePhoneNumber = Create<string>();
            dto.BirthCity = Create<string>();
            dto.BirthState = Create<string>();
            dto.BirthCountry = Create<string>();
            dto.CountryCitizenship = Create<string>();
            dto.AbsId = Create<string>();
            dto.CertificationStatus = Create<string>();
            dto.NPI = Create<string>();
            dto.GenderId = Create<int?>();
            dto.BirthDate = Create<System.DateTime?>();
            dto.Race = Create<string>();
            dto.Ethnicity = Create<string>();
            dto.FirstLanguageId = Create<int?>();
            dto.BestLanguageId = Create<int?>();
            dto.ReceiveComms = Create<bool?>();
            dto.UserConfirmed = Create<bool?>();
            dto.UserConfirmedDate = Create<System.DateTime?>();
            dto.CreatedByUserId = Create<int>();
            dto.CreatedAtUtc = Create<System.DateTime>();
            dto.LastUpdatedAtUtc = Create<System.DateTime>();
            dto.LastUpdatedByUserId = Create<int>();
            dto.Street1 = Create<string>();
            dto.Street2 = Create<string>();
            dto.City = Create<string>();
            dto.State = Create<string>();
            dto.ZipCode = Create<string>();
            dto.Country = Create<string>();
    
            return dto;
        }

            #region UserProfile Business Rules
            [Test]
            public async Task IsRequired_GetByUserId_FirstName_Fails()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.FirstName = default;
            
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            
                //Ensure that the save fails...
                var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(() => sut.SaveAsync());
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
                Assert.That(sut.GetBrokenRules()[0].Description == "FirstName is required", $"Expected the rule description to be 'FirstName is required', have {sut.GetBrokenRules()[0].Description}");
                Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
                Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
            }
            
            [Test]
            public async Task IsRequired_GetByUserId_FirstName_Passes()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.FirstName = Create<string>();
            
                Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
            
            }
            [Test]
            public async Task IsRequired_GetByUserId_LastName_Fails()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.LastName = default;
            
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            
                //Ensure that the save fails...
                var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(() => sut.SaveAsync());
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
                Assert.That(sut.GetBrokenRules()[0].Description == "LastName is required", $"Expected the rule description to be 'LastName is required', have {sut.GetBrokenRules()[0].Description}");
                Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
                Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
            }
            
            [Test]
            public async Task IsRequired_GetByUserId_LastName_Passes()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.LastName = Create<string>();
            
                Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
            
            }
            [Test]
            public async Task IsRequired_GetByUserId_DisplayName_Fails()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.DisplayName = default;
            
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            
                //Ensure that the save fails...
                var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(() => sut.SaveAsync());
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
                Assert.That(sut.GetBrokenRules()[0].Description == "DisplayName is required", $"Expected the rule description to be 'DisplayName is required', have {sut.GetBrokenRules()[0].Description}");
                Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
                Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
            }
            
            [Test]
            public async Task IsRequired_GetByUserId_DisplayName_Passes()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.DisplayName = Create<string>();
            
                Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
            
            }
            [Test]
            public async Task IsRequired_GetByUserId_OfficePhoneNumber_Fails()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.OfficePhoneNumber = default;
            
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            
                //Ensure that the save fails...
                var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(() => sut.SaveAsync());
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
                Assert.That(sut.GetBrokenRules()[0].Description == "OfficePhoneNumber is required", $"Expected the rule description to be 'OfficePhoneNumber is required', have {sut.GetBrokenRules()[0].Description}");
                Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
                Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
            }
            
            [Test]
            public async Task IsRequired_GetByUserId_OfficePhoneNumber_Passes()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.OfficePhoneNumber = Create<string>();
            
                Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
            
            }
            [Test]
            public async Task IsRequired_GetByUserId_BirthCity_Fails()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.BirthCity = default;
            
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            
                //Ensure that the save fails...
                var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(() => sut.SaveAsync());
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
                Assert.That(sut.GetBrokenRules()[0].Description == "BirthCity is required", $"Expected the rule description to be 'BirthCity is required', have {sut.GetBrokenRules()[0].Description}");
                Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
                Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
            }
            
            [Test]
            public async Task IsRequired_GetByUserId_BirthCity_Passes()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.BirthCity = Create<string>();
            
                Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
            
            }
            [Test]
            public async Task IsRequired_GetByUserId_BirthCountry_Fails()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.BirthCountry = default;
            
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            
                //Ensure that the save fails...
                var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(() => sut.SaveAsync());
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
                Assert.That(sut.GetBrokenRules()[0].Description == "BirthCountry is required", $"Expected the rule description to be 'BirthCountry is required', have {sut.GetBrokenRules()[0].Description}");
                Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
                Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
            }
            
            [Test]
            public async Task IsRequired_GetByUserId_BirthCountry_Passes()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.BirthCountry = Create<string>();
            
                Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
            
            }
            [Test]
            public async Task IsRequired_GetByUserId_CountryCitizenship_Fails()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.CountryCitizenship = default;
            
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            
                //Ensure that the save fails...
                var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(() => sut.SaveAsync());
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
                Assert.That(sut.GetBrokenRules()[0].Description == "CountryCitizenship is required", $"Expected the rule description to be 'CountryCitizenship is required', have {sut.GetBrokenRules()[0].Description}");
                Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
                Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
            }
            
            [Test]
            public async Task IsRequired_GetByUserId_CountryCitizenship_Passes()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.CountryCitizenship = Create<string>();
            
                Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
            
            }
            [Test]
            public async Task IsRequired_GetByUserId_GenderId_Fails()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.GenderId = default;
            
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            
                //Ensure that the save fails...
                var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(() => sut.SaveAsync());
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
                Assert.That(sut.GetBrokenRules()[0].Description == "GenderId is required", $"Expected the rule description to be 'GenderId is required', have {sut.GetBrokenRules()[0].Description}");
                Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
                Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
            }
            
            [Test]
            public async Task IsRequired_GetByUserId_GenderId_Passes()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.GenderId = Create<int?>();
            
                Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
            
            }
            [Test]
            public async Task IsRequired_GetByUserId_BirthDate_Fails()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.BirthDate = default;
            
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            
                //Ensure that the save fails...
                var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(() => sut.SaveAsync());
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
                Assert.That(sut.GetBrokenRules()[0].Description == "BirthDate is required", $"Expected the rule description to be 'BirthDate is required', have {sut.GetBrokenRules()[0].Description}");
                Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
                Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
            }
            
            [Test]
            public async Task IsRequired_GetByUserId_BirthDate_Passes()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.BirthDate = Create<System.DateTime?>();
            
                Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
            
            }
            [Test]
            public async Task IsRequired_GetByUserId_Race_Fails()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.Race = default;
            
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            
                //Ensure that the save fails...
                var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(() => sut.SaveAsync());
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
                Assert.That(sut.GetBrokenRules()[0].Description == "Race is required", $"Expected the rule description to be 'Race is required', have {sut.GetBrokenRules()[0].Description}");
                Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
                Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
            }
            
            [Test]
            public async Task IsRequired_GetByUserId_Race_Passes()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.Race = Create<string>();
            
                Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
            
            }
            [Test]
            public async Task IsRequired_GetByUserId_Ethnicity_Fails()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.Ethnicity = default;
            
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            
                //Ensure that the save fails...
                var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(() => sut.SaveAsync());
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
                Assert.That(sut.GetBrokenRules()[0].Description == "Ethnicity is required", $"Expected the rule description to be 'Ethnicity is required', have {sut.GetBrokenRules()[0].Description}");
                Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
                Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
            }
            
            [Test]
            public async Task IsRequired_GetByUserId_Ethnicity_Passes()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.Ethnicity = Create<string>();
            
                Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
            
            }
            [Test]
            public async Task IsRequired_GetByUserId_FirstLanguageId_Fails()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.FirstLanguageId = default;
            
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            
                //Ensure that the save fails...
                var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(() => sut.SaveAsync());
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
                Assert.That(sut.GetBrokenRules()[0].Description == "FirstLanguageId is required", $"Expected the rule description to be 'FirstLanguageId is required', have {sut.GetBrokenRules()[0].Description}");
                Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
                Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
            }
            
            [Test]
            public async Task IsRequired_GetByUserId_FirstLanguageId_Passes()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.FirstLanguageId = Create<int?>();
            
                Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
            
            }
            [Test]
            public async Task IsRequired_GetByUserId_BestLanguageId_Fails()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.BestLanguageId = default;
            
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            
                //Ensure that the save fails...
                var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(() => sut.SaveAsync());
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
                Assert.That(sut.GetBrokenRules()[0].Description == "BestLanguageId is required", $"Expected the rule description to be 'BestLanguageId is required', have {sut.GetBrokenRules()[0].Description}");
                Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
                Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
            }
            
            [Test]
            public async Task IsRequired_GetByUserId_BestLanguageId_Passes()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.BestLanguageId = Create<int?>();
            
                Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
            
            }
            [Test]
            public async Task IsRequired_GetByUserId_ReceiveComms_Fails()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.ReceiveComms = default;
            
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            
                //Ensure that the save fails...
                var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(() => sut.SaveAsync());
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
                Assert.That(sut.GetBrokenRules()[0].Description == "ReceiveComms is required", $"Expected the rule description to be 'ReceiveComms is required', have {sut.GetBrokenRules()[0].Description}");
                Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
                Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
            }
            
            [Test]
            public async Task IsRequired_GetByUserId_ReceiveComms_Passes()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.ReceiveComms = Create<bool?>();
            
                Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
            
            }
            [Test]
            public async Task IsRequired_GetByUserId_UserConfirmed_Fails()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.UserConfirmed = default;
            
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            
                //Ensure that the save fails...
                var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(() => sut.SaveAsync());
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
                Assert.That(sut.GetBrokenRules()[0].Description == "UserConfirmed is required", $"Expected the rule description to be 'UserConfirmed is required', have {sut.GetBrokenRules()[0].Description}");
                Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
                Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
            }
            
            [Test]
            public async Task IsRequired_GetByUserId_UserConfirmed_Passes()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.UserConfirmed = Create<bool?>();
            
                Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
            
            }
            [Test]
            public async Task IsRequired_GetByUserId_Street1_Fails()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.Street1 = default;
            
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            
                //Ensure that the save fails...
                var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(() => sut.SaveAsync());
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
                Assert.That(sut.GetBrokenRules()[0].Description == "Street1 is required", $"Expected the rule description to be 'Street1 is required', have {sut.GetBrokenRules()[0].Description}");
                Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
                Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
            }
            
            [Test]
            public async Task IsRequired_GetByUserId_Street1_Passes()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.Street1 = Create<string>();
            
                Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
            
            }
            [Test]
            public async Task IsRequired_GetByUserId_City_Fails()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.City = default;
            
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            
                //Ensure that the save fails...
                var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(() => sut.SaveAsync());
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
                Assert.That(sut.GetBrokenRules()[0].Description == "City is required", $"Expected the rule description to be 'City is required', have {sut.GetBrokenRules()[0].Description}");
                Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
                Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
            }
            
            [Test]
            public async Task IsRequired_GetByUserId_City_Passes()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.City = Create<string>();
            
                Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
            
            }
            [Test]
            public async Task IsRequired_GetByUserId_ZipCode_Fails()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.ZipCode = default;
            
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            
                //Ensure that the save fails...
                var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(() => sut.SaveAsync());
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
                Assert.That(sut.GetBrokenRules()[0].Description == "ZipCode is required", $"Expected the rule description to be 'ZipCode is required', have {sut.GetBrokenRules()[0].Description}");
                Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
                Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
            }
            
            [Test]
            public async Task IsRequired_GetByUserId_ZipCode_Passes()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.ZipCode = Create<string>();
            
                Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
            
            }
            [Test]
            public async Task IsRequired_GetByUserId_Country_Fails()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.Country = default;
            
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            
                //Ensure that the save fails...
                var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(() => sut.SaveAsync());
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
                Assert.That(sut.GetBrokenRules()[0].Description == "Country is required", $"Expected the rule description to be 'Country is required', have {sut.GetBrokenRules()[0].Description}");
                Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
                Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
            }
            
            [Test]
            public async Task IsRequired_GetByUserId_Country_Passes()
            {
                var dto = CreateValidDto();
            
                var expectedUserId = Create<int>();
            
                var mockDal = new Mock<IUserProfileDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity(1234, "SomeUser")
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IUserProfile, UserProfile>()
                    .Build();
            
                var factory = new UserProfileFactory();
                var sut = await factory.GetByUserIdAsync(expectedUserId);
                
                sut.Country = Create<string>();
            
                Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
            
            }
            #endregion

        #region GetByUserIdAsync UserProfile
        
        [Test]
        public async Task GetByUserIdAsync_CallsDalCorrectly()
        {
            var expectedUserId = Create<int>();
            
            var mockDal = new Mock<IUserProfileDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(Create<UserProfileDto>());
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserProfile, UserProfile>()
                .Build();
        
            var factory = new UserProfileFactory();
            var sut = await factory.GetByUserIdAsync(expectedUserId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByUserId_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IUserProfileDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(It.IsAny<int>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserProfile, UserProfile>()
                .Build();
        
            var factory = new UserProfileFactory();
            var sut = await factory.GetByUserIdAsync(Create<int>());
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region Create
        
        [Test]
        public async Task Create_CallsDalCorrectly()
        {
            var dto = CreateValidDto();
            UserProfileDto passedDto = null;
        
            var mockDal = new Mock<IUserProfileDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<UserProfileDto>()))
                .Callback<UserProfileDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserProfile, UserProfile>()
                .Build();
        
            var factory = new UserProfileFactory();
            var sut = factory.Create();
            
            sut.UserProfileId = dto.UserProfileId;
            sut.UserId = dto.UserId;
            sut.FirstName = dto.FirstName;
            sut.MiddleName = dto.MiddleName;
            sut.LastName = dto.LastName;
            sut.Suffix = dto.Suffix;
            sut.DisplayName = dto.DisplayName;
            sut.OfficePhoneNumber = dto.OfficePhoneNumber;
            sut.MobilePhoneNumber = dto.MobilePhoneNumber;
            sut.BirthCity = dto.BirthCity;
            sut.BirthState = dto.BirthState;
            sut.BirthCountry = dto.BirthCountry;
            sut.CountryCitizenship = dto.CountryCitizenship;
            sut.GenderId = dto.GenderId;
            sut.BirthDate = dto.BirthDate;
            sut.Race = dto.Race;
            sut.Ethnicity = dto.Ethnicity;
            sut.FirstLanguageId = dto.FirstLanguageId;
            sut.BestLanguageId = dto.BestLanguageId;
            sut.ReceiveComms = dto.ReceiveComms;
            sut.UserConfirmed = dto.UserConfirmed;
            sut.UserConfirmedDate = dto.UserConfirmedDate;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
            sut.Street1 = dto.Street1;
            sut.Street2 = dto.Street2;
            sut.City = dto.City;
            sut.State = dto.State;
            sut.ZipCode = dto.ZipCode;
            sut.Country = dto.Country;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null);
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                .Excluding(m => m.CreatedAtUtc)
                .Excluding(m => m.CreatedByUserId)
                .Excluding(m => m.LastUpdatedAtUtc)
                .Excluding(m => m.LastUpdatedByUserId)
                .Excluding(m => m.UserProfileId)
                .Excluding(m => m.UserId)
                .Excluding(m => m.CertificationStatus)
                .Excluding(m => m.CreatedByUserId)
                .Excluding(m => m.LastUpdatedByUserId)
                .ExcludingMissingMembers());
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task Create_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IUserProfileDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<UserProfileDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserProfile, UserProfile>()
                .Build();
        
            var factory = new UserProfileFactory();
            var sut = factory.Create();
            sut.FirstName = Create<string>();
        
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
            var expectedUserId = Create<int>();
            
            var dto = Create<UserProfileDto>();
            UserProfileDto passedDto = null;
        
            var mockDal = new Mock<IUserProfileDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                        .ReturnsAsync(dto);
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<UserProfileDto>()))
                .Callback<UserProfileDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserProfile, UserProfile>()
                .Build();
        
            var factory = new UserProfileFactory();
            var sut = await factory.GetByUserIdAsync(expectedUserId);
            
            sut.UserProfileId = dto.UserProfileId;
            sut.UserId = dto.UserId;
            sut.FirstName = dto.FirstName;
            sut.MiddleName = dto.MiddleName;
            sut.LastName = dto.LastName;
            sut.Suffix = dto.Suffix;
            sut.DisplayName = dto.DisplayName;
            sut.OfficePhoneNumber = dto.OfficePhoneNumber;
            sut.MobilePhoneNumber = dto.MobilePhoneNumber;
            sut.BirthCity = dto.BirthCity;
            sut.BirthState = dto.BirthState;
            sut.BirthCountry = dto.BirthCountry;
            sut.CountryCitizenship = dto.CountryCitizenship;
            sut.GenderId = dto.GenderId;
            sut.BirthDate = dto.BirthDate;
            sut.Race = dto.Race;
            sut.Ethnicity = dto.Ethnicity;
            sut.FirstLanguageId = dto.FirstLanguageId;
            sut.BestLanguageId = dto.BestLanguageId;
            sut.ReceiveComms = dto.ReceiveComms;
            sut.UserConfirmed = dto.UserConfirmed;
            sut.UserConfirmedDate = dto.UserConfirmedDate;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
            sut.Street1 = dto.Street1;
            sut.Street2 = dto.Street2;
            sut.City = dto.City;
            sut.State = dto.State;
            sut.ZipCode = dto.ZipCode;
            sut.Country = dto.Country;
        
            // We now change all properties on the SUT to make it Dirty
            // or the SaveAsync() will not be called. :)
            dto = CreateValidDto();
        
            sut.UserProfileId = dto.UserProfileId;
            sut.UserId = dto.UserId;
            sut.FirstName = dto.FirstName;
            sut.MiddleName = dto.MiddleName;
            sut.LastName = dto.LastName;
            sut.Suffix = dto.Suffix;
            sut.DisplayName = dto.DisplayName;
            sut.OfficePhoneNumber = dto.OfficePhoneNumber;
            sut.MobilePhoneNumber = dto.MobilePhoneNumber;
            sut.BirthCity = dto.BirthCity;
            sut.BirthState = dto.BirthState;
            sut.BirthCountry = dto.BirthCountry;
            sut.CountryCitizenship = dto.CountryCitizenship;
            sut.GenderId = dto.GenderId;
            sut.BirthDate = dto.BirthDate;
            sut.Race = dto.Race;
            sut.Ethnicity = dto.Ethnicity;
            sut.FirstLanguageId = dto.FirstLanguageId;
            sut.BestLanguageId = dto.BestLanguageId;
            sut.ReceiveComms = dto.ReceiveComms;
            sut.UserConfirmed = dto.UserConfirmed;
            sut.UserConfirmedDate = dto.UserConfirmedDate;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
            sut.Street1 = dto.Street1;
            sut.Street2 = dto.Street2;
            sut.City = dto.City;
            sut.State = dto.State;
            sut.ZipCode = dto.ZipCode;
            sut.Country = dto.Country;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null, "The passedDto is null, which can mean that the DataPortal method was not called.");
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                    .Excluding(m => m.CreatedAtUtc)
                    .Excluding(m => m.CreatedByUserId)
                    .Excluding(m => m.LastUpdatedAtUtc)
                    .Excluding(m => m.LastUpdatedByUserId)
                    .Excluding(m => m.UserId)
                    .Excluding(m => m.CertificationStatus)
                    .Excluding(m => m.CreatedByUserId)
                    .Excluding(m => m.LastUpdatedByUserId)
                .ExcludingMissingMembers());
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task Update_YieldsCorrectResult()
        {
            var expectedUserId = Create<int>();
            
            var dto = Create<UserProfileDto>();
        
            var mockDal = new Mock<IUserProfileDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                        .ReturnsAsync(Create<UserProfileDto>());
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<UserProfileDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserProfile, UserProfile>()
                .Build();
        
            var factory = new UserProfileFactory();
            var sut = await factory.GetByUserIdAsync(expectedUserId);
            sut.UserProfileId = Create<int>();
        
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
