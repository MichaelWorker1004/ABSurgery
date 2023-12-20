using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Examinations;
using SurgeonPortal.Library.Contracts.Documents;
using SurgeonPortal.Library.Contracts.Examinations;
using SurgeonPortal.Library.Examinations;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Examinations
{
    [TestFixture] 
	public class AccommodationTests : TestBase<int>
    {
        private AccommodationDto CreateValidDto()
        {
            var dto = Create<AccommodationDto>();
        
            dto.Id = Create<int>();
            dto.UserId = 1234;
            dto.AccommodationID = Create<int>();
            dto.AccommodationName = Create<string>();
            dto.DocumentId = Create<int?>();
            dto.DocumentName = Create<string>();
            dto.ExamID = Create<int?>();
            dto.CreatedByUserId = 1234;
            dto.CreatedAtUtc = Create<System.DateTime>();
            dto.LastUpdatedAtUtc = Create<System.DateTime>();
            dto.LastUpdatedByUserId = 1234;
        
            return dto;
        }
        
        

        #region DeleteAsync
        
        [Test]
        public async Task Delete_CallsDalCorrectly()
        {
            var expectedId = Create<int>();
            var expectedExamId = Create<int>();
            
            var dto = CreateValidDto();
            AccommodationDto passedDto = null;
        
            var mockDal = new Mock<IAccommodationDal>();
            mockDal.Setup(m => m.GetByExamIdAsync(
                1234,
                expectedExamId))
                .ReturnsAsync(dto);
            
            mockDal.Setup(m => m.DeleteAsync(It.IsAny<AccommodationDto>()))
                .Callback<AccommodationDto>((p) => passedDto = p)
                .Returns(Task.CompletedTask);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(new Mock<IDocumentFactory>())
                .WithBusinessObject<IAccommodation, Accommodation>()
                .Build();
        
            var factory = new AccommodationFactory();
            var sut = await factory.GetByExamIdAsync(expectedExamId);
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

        #region GetByExamIdAsync Accommodation
        
        [Test]
        public async Task GetByExamIdAsync_CallsDalCorrectly()
        {
            var expectedExamId = Create<int>();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IAccommodationDal>();
            mockDal.Setup(m => m.GetByExamIdAsync(
                expectedUserId,
                expectedExamId))
                .ReturnsAsync(Create<AccommodationDto>());
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
				.WithRegisteredInstance(new Mock<IDocumentFactory>())
				.WithBusinessObject<IAccommodation, Accommodation>()
                .Build();
        
            var factory = new AccommodationFactory();
            var sut = await factory.GetByExamIdAsync(expectedExamId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByExamId_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
            var expectedExamId = Create<int>();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IAccommodationDal>();
            mockDal.Setup(m => m.GetByExamIdAsync(
                expectedUserId,
                expectedExamId))
                .ReturnsAsync(dto);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
				.WithRegisteredInstance(new Mock<IDocumentFactory>())
				.WithBusinessObject<IAccommodation, Accommodation>()
                .Build();
        
            var factory = new AccommodationFactory();
            var sut = await factory.GetByExamIdAsync(expectedExamId);
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region Create
        
        [Test]
        public async Task Create_CallsDalCorrectly()
        {
            var dto = CreateValidDto();
            AccommodationDto passedDto = null;
        
            var mockDal = new Mock<IAccommodationDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<AccommodationDto>()))
                .Callback<AccommodationDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
				.WithRegisteredInstance(new Mock<IDocumentFactory>())
				.WithBusinessObject<IAccommodation, Accommodation>()
                .Build();
        
            var factory = new AccommodationFactory();
            var sut = factory.Create();
            
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.AccommodationID = dto.AccommodationID;
            sut.AccommodationName = dto.AccommodationName;
            sut.DocumentId = dto.DocumentId;
            sut.DocumentName = dto.DocumentName;
            sut.ExamID = dto.ExamID;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null);
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                .Excluding(m => m.Id)
                .Excluding(m => m.AccommodationName)
                .Excluding(m => m.DocumentName)
                .Excluding(m => m.CreatedByUserId)
                .Excluding(m => m.CreatedAtUtc)
                .Excluding(m => m.LastUpdatedAtUtc)
                .Excluding(m => m.LastUpdatedByUserId)
                .ExcludingMissingMembers());
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task Create_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IAccommodationDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<AccommodationDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
				.WithRegisteredInstance(new Mock<IDocumentFactory>())
				.WithBusinessObject<IAccommodation, Accommodation>()
                .Build();
        
            var factory = new AccommodationFactory();
            var sut = factory.Create();
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.AccommodationID = dto.AccommodationID;
            sut.AccommodationName = dto.AccommodationName;
            sut.DocumentId = dto.DocumentId;
            sut.DocumentName = dto.DocumentName;
            sut.ExamID = dto.ExamID;
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
            var expectedExamId = Create<int>();
            var expectedUserId = 1234;
            
            var dto = CreateValidDto();
            AccommodationDto passedDto = null;
        
            var mockDal = new Mock<IAccommodationDal>();
            mockDal.Setup(m => m.GetByExamIdAsync(
                expectedUserId,
                expectedExamId))
                .ReturnsAsync(dto);
            
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<AccommodationDto>()))
                .Callback<AccommodationDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
				.WithRegisteredInstance(new Mock<IDocumentFactory>())
				.WithBusinessObject<IAccommodation, Accommodation>()
                .Build();
        
            var factory = new AccommodationFactory();
            var sut = await factory.GetByExamIdAsync(expectedExamId);
            
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.AccommodationID = dto.AccommodationID;
            sut.AccommodationName = dto.AccommodationName;
            sut.DocumentId = dto.DocumentId;
            sut.DocumentName = dto.DocumentName;
            sut.ExamID = dto.ExamID;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
        
            // We now change all properties on the SUT to make it Dirty
            // or the SaveAsync() will not be called. :)
            dto = CreateValidDto();
        
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.AccommodationID = dto.AccommodationID;
            sut.AccommodationName = dto.AccommodationName;
            sut.DocumentId = dto.DocumentId;
            sut.DocumentName = dto.DocumentName;
            sut.ExamID = dto.ExamID;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null, "The passedDto is null, which can mean that the DataPortal method was not called.");
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                .Excluding(m => m.AccommodationName)
                .Excluding(m => m.DocumentName)
                .Excluding(m => m.CreatedByUserId)
                .Excluding(m => m.CreatedAtUtc)
                .Excluding(m => m.LastUpdatedAtUtc)
                .Excluding(m => m.LastUpdatedByUserId)
                .ExcludingMissingMembers());
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task Update_YieldsCorrectResult()
        {
            var expectedExamId = Create<int>();
            var expectedUserId = 1234;
            
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IAccommodationDal>();
            mockDal.Setup(m => m.GetByExamIdAsync(
                expectedUserId,
                expectedExamId))
                .ReturnsAsync(dto);
            
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<AccommodationDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
				.WithRegisteredInstance(new Mock<IDocumentFactory>())
				.WithBusinessObject<IAccommodation, Accommodation>()
                .Build();
        
            var factory = new AccommodationFactory();
            var sut = await factory.GetByExamIdAsync(expectedExamId);
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
