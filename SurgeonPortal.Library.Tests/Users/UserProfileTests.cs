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
	public class UserProfileTests : TestBase<string>
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
            dto.ABSId = Create<int?>();
            dto.CertificationStatusId = Create<int?>();
            dto.NPI = Create<string>();
            dto.Gender = Create<string>();
            dto.BirthDate = Create<System.DateTime?>();
            dto.Race = Create<string>();
            dto.Ethnicity = Create<string>();
            dto.FirstLanguage = Create<string>();
            dto.BestLanguage = Create<string>();
            dto.ReceiveComms = Create<bool?>();
            dto.UserConfirmed = Create<bool?>();
            dto.UserConfirmedDate = Create<System.DateTime?>();
            dto.CreatedByUserId = Create<int>();
            dto.CreatedAtUtc = Create<System.DateTime>();
            dto.LastUpdatedAtUtc = Create<System.DateTime>();
            dto.LastUpdatedByUserId = Create<int>();
    
            return dto;
        }

            

        #region GetByUserIdAsync UserProfile
        
        [Test]
        public async Task GetByUserIdAsync_CallsDalCorrectly()
        {
            var expectedUserId = Create<int>();
            
            var mockDal = new Mock<IUserProfileDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(Create<UserProfileDto>());
        
            UseMockServiceProvider()
                .WithMockedIdentity()
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
                .WithMockedIdentity()
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
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserProfile, UserProfile>()
                .Build();
        
            var factory = new UserProfileFactory();
            var sut = factory.Create();
            
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
            sut.ABSId = dto.ABSId;
            sut.CertificationStatusId = dto.CertificationStatusId;
            sut.NPI = dto.NPI;
            sut.Gender = dto.Gender;
            sut.BirthDate = dto.BirthDate;
            sut.Race = dto.Race;
            sut.Ethnicity = dto.Ethnicity;
            sut.FirstLanguage = dto.FirstLanguage;
            sut.BestLanguage = dto.BestLanguage;
            sut.ReceiveComms = dto.ReceiveComms;
            sut.UserConfirmed = dto.UserConfirmed;
            sut.UserConfirmedDate = dto.UserConfirmedDate;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null);
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                .Excluding(m => m.CreatedAtUtc)
                .Excluding(m => m.CreatedByUserId)
                .Excluding(m => m.LastUpdatedAtUtc)
                .Excluding(m => m.LastUpdatedByUserId)
                .Excluding(m => m.UserProfileId)
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
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserProfile, UserProfile>()
                .Build();
        
            var factory = new UserProfileFactory();
            var sut = factory.Create();
            sut.UserId = Create<int>();
        
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
            mockDal.Setup(m => m.GetByUserIdAsync(userId))
                        .ReturnsAsync(dto);
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<UserProfileDto>()))
                .Callback<UserProfileDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserProfile, UserProfile>()
                .Build();
        
            var factory = new UserProfileFactory();
            var sut = await factory.GetByUserIdAsync(userId);
            
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
            sut.ABSId = dto.ABSId;
            sut.CertificationStatusId = dto.CertificationStatusId;
            sut.NPI = dto.NPI;
            sut.Gender = dto.Gender;
            sut.BirthDate = dto.BirthDate;
            sut.Race = dto.Race;
            sut.Ethnicity = dto.Ethnicity;
            sut.FirstLanguage = dto.FirstLanguage;
            sut.BestLanguage = dto.BestLanguage;
            sut.ReceiveComms = dto.ReceiveComms;
            sut.UserConfirmed = dto.UserConfirmed;
            sut.UserConfirmedDate = dto.UserConfirmedDate;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
        
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
            sut.ABSId = dto.ABSId;
            sut.CertificationStatusId = dto.CertificationStatusId;
            sut.NPI = dto.NPI;
            sut.Gender = dto.Gender;
            sut.BirthDate = dto.BirthDate;
            sut.Race = dto.Race;
            sut.Ethnicity = dto.Ethnicity;
            sut.FirstLanguage = dto.FirstLanguage;
            sut.BestLanguage = dto.BestLanguage;
            sut.ReceiveComms = dto.ReceiveComms;
            sut.UserConfirmed = dto.UserConfirmed;
            sut.UserConfirmedDate = dto.UserConfirmedDate;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null, "The passedDto is null, which can mean that the DataPortal method was not called.");
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
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
            var expectedUserId = Create<int>();
            
            var dto = Create<UserProfileDto>();
        
            var mockDal = new Mock<IUserProfileDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(userId))
                        .ReturnsAsync(Create<UserProfileDto>());
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<UserProfileDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserProfile, UserProfile>()
                .Build();
        
            var factory = new UserProfileFactory();
            var sut = await factory.GetByUserIdAsync(userId);
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
