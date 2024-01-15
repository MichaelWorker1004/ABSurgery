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
	public class UserAppointmentTests : TestBase<int>
    {
        private UserAppointmentDto CreateValidDto()
        {
            var dto = Create<UserAppointmentDto>();
        
            dto.ApptId = Create<decimal>();
            dto.UserId = 1234;
            dto.PracticeTypeId = Create<int?>();
            dto.PracticeType = Create<string>();
            dto.AppointmentTypeId = Create<int?>();
            dto.PrimaryAppointment = Create<bool?>();
            dto.AppointmentType = Create<string>();
            dto.OrganizationTypeId = Create<int?>();
            dto.AuthorizingOfficial = Create<string>();
            dto.OrganizationType = Create<string>();
            dto.OrganizationId = Create<int?>();
            dto.StateCode = Create<string>();
            dto.Other = Create<string>();
            dto.OrganizationName = Create<string>();
        
            return dto;
        }
        
        

        #region DeleteAsync
        
        [Test]
        public async Task Delete_CallsDalCorrectly()
        {
            var expectedApptId = Create<int>();
            
            var dto = CreateValidDto();
            UserAppointmentDto passedDto = null;
        
            var mockDal = new Mock<IUserAppointmentDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedApptId))
                .ReturnsAsync(dto);
            
            mockDal.Setup(m => m.DeleteAsync(It.IsAny<UserAppointmentDto>()))
                .Callback<UserAppointmentDto>((p) => passedDto = p)
                .Returns(Task.CompletedTask);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserAppointment, UserAppointment>()
                .Build();
        
            var factory = new UserAppointmentFactory();
            var sut = await factory.GetByIdAsync(expectedApptId);
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

        #region GetByIdAsync UserAppointment
        
        [Test]
        public async Task GetByIdAsync_CallsDalCorrectly()
        {
            var expectedApptId = Create<int>();
            
            var mockDal = new Mock<IUserAppointmentDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedApptId))
                .ReturnsAsync(Create<UserAppointmentDto>());
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserAppointment, UserAppointment>()
                .Build();
        
            var factory = new UserAppointmentFactory();
            var sut = await factory.GetByIdAsync(expectedApptId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetById_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
            var expectedApptId = Create<int>();
            
            var mockDal = new Mock<IUserAppointmentDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedApptId))
                .ReturnsAsync(dto);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserAppointment, UserAppointment>()
                .Build();
        
            var factory = new UserAppointmentFactory();
            var sut = await factory.GetByIdAsync(expectedApptId);
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region Create
        
        [Test]
        public async Task Create_CallsDalCorrectly()
        {
            var dto = CreateValidDto();
            UserAppointmentDto passedDto = null;
        
            var mockDal = new Mock<IUserAppointmentDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<UserAppointmentDto>()))
                .Callback<UserAppointmentDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserAppointment, UserAppointment>()
                .Build();
        
            var factory = new UserAppointmentFactory();
            var sut = factory.Create();
            
            sut.ApptId = dto.ApptId;
            sut.UserId = dto.UserId;
            sut.PracticeTypeId = dto.PracticeTypeId;
            sut.PracticeType = dto.PracticeType;
            sut.AppointmentTypeId = dto.AppointmentTypeId;
            sut.PrimaryAppointment = dto.PrimaryAppointment;
            sut.AppointmentType = dto.AppointmentType;
            sut.OrganizationTypeId = dto.OrganizationTypeId;
            sut.AuthorizingOfficial = dto.AuthorizingOfficial;
            sut.OrganizationType = dto.OrganizationType;
            sut.OrganizationId = dto.OrganizationId;
            sut.StateCode = dto.StateCode;
            sut.Other = dto.Other;
            sut.OrganizationName = dto.OrganizationName;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null);
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                .Excluding(m => m.ApptId)
                .Excluding(m => m.PracticeType)
                .Excluding(m => m.AppointmentType)
                .Excluding(m => m.OrganizationType)
                .Excluding(m => m.OrganizationName)
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
        
            var mockDal = new Mock<IUserAppointmentDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<UserAppointmentDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserAppointment, UserAppointment>()
                .Build();
        
            var factory = new UserAppointmentFactory();
            var sut = factory.Create();
            sut.ApptId = dto.ApptId;
            sut.UserId = dto.UserId;
            sut.PracticeTypeId = dto.PracticeTypeId;
            sut.PracticeType = dto.PracticeType;
            sut.AppointmentTypeId = dto.AppointmentTypeId;
            sut.PrimaryAppointment = dto.PrimaryAppointment;
            sut.AppointmentType = dto.AppointmentType;
            sut.OrganizationTypeId = dto.OrganizationTypeId;
            sut.AuthorizingOfficial = dto.AuthorizingOfficial;
            sut.OrganizationType = dto.OrganizationType;
            sut.OrganizationId = dto.OrganizationId;
            sut.StateCode = dto.StateCode;
            sut.Other = dto.Other;
            sut.OrganizationName = dto.OrganizationName;
        
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
            var expectedApptId = Create<int>();
            
            var dto = CreateValidDto();
            UserAppointmentDto passedDto = null;
        
            var mockDal = new Mock<IUserAppointmentDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedApptId))
                .ReturnsAsync(dto);
            
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<UserAppointmentDto>()))
                .Callback<UserAppointmentDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserAppointment, UserAppointment>()
                .Build();
        
            var factory = new UserAppointmentFactory();
            var sut = await factory.GetByIdAsync(expectedApptId);
            
            sut.ApptId = dto.ApptId;
            sut.UserId = dto.UserId;
            sut.PracticeTypeId = dto.PracticeTypeId;
            sut.PracticeType = dto.PracticeType;
            sut.AppointmentTypeId = dto.AppointmentTypeId;
            sut.PrimaryAppointment = dto.PrimaryAppointment;
            sut.AppointmentType = dto.AppointmentType;
            sut.OrganizationTypeId = dto.OrganizationTypeId;
            sut.AuthorizingOfficial = dto.AuthorizingOfficial;
            sut.OrganizationType = dto.OrganizationType;
            sut.OrganizationId = dto.OrganizationId;
            sut.StateCode = dto.StateCode;
            sut.Other = dto.Other;
            sut.OrganizationName = dto.OrganizationName;
        
            // We now change all properties on the SUT to make it Dirty
            // or the SaveAsync() will not be called. :)
            dto = CreateValidDto();
        
            sut.ApptId = dto.ApptId;
            sut.UserId = dto.UserId;
            sut.PracticeTypeId = dto.PracticeTypeId;
            sut.PracticeType = dto.PracticeType;
            sut.AppointmentTypeId = dto.AppointmentTypeId;
            sut.PrimaryAppointment = dto.PrimaryAppointment;
            sut.AppointmentType = dto.AppointmentType;
            sut.OrganizationTypeId = dto.OrganizationTypeId;
            sut.AuthorizingOfficial = dto.AuthorizingOfficial;
            sut.OrganizationType = dto.OrganizationType;
            sut.OrganizationId = dto.OrganizationId;
            sut.StateCode = dto.StateCode;
            sut.Other = dto.Other;
            sut.OrganizationName = dto.OrganizationName;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null, "The passedDto is null, which can mean that the DataPortal method was not called.");
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                .Excluding(m => m.PracticeType)
                .Excluding(m => m.AppointmentType)
                .Excluding(m => m.OrganizationType)
                .Excluding(m => m.OrganizationName)
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
            var expectedApptId = Create<int>();
            
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IUserAppointmentDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedApptId))
                .ReturnsAsync(dto);
            
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<UserAppointmentDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserAppointment, UserAppointment>()
                .Build();
        
            var factory = new UserAppointmentFactory();
            var sut = await factory.GetByIdAsync(expectedApptId);
            sut.ApptId = Create<int>();
        
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
