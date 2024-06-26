using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.MedicalTraining;
using SurgeonPortal.Library.Contracts.Documents;
using SurgeonPortal.Library.Contracts.MedicalTraining;
using SurgeonPortal.Library.MedicalTraining;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.MedicalTraining
{
    [TestFixture] 
	public class UserCertificateTests : TestBase<int>
    {
        private UserCertificateDto CreateValidDto()
        {
            var dto = Create<UserCertificateDto>();
        
            dto.CertificateId = Create<int>();
            dto.UserId = 1234;
            dto.DocumentId = Create<int>();
            dto.CertificateTypeId = Create<int>();
            dto.CertificateType = Create<string>();
            dto.IssueDate = Create<System.DateTime>();
            dto.CertificateNumber = Create<string>();
            dto.CreatedByUserId = 1234;
            dto.CreatedAtUtc = Create<System.DateTime>();
            dto.LastUpdatedAtUtc = Create<System.DateTime>();
            dto.LastUpdatedByUserId = Create<int>();
        
            return dto;
        }
        
        

        #region DeleteAsync
        
        [Test]
        public async Task Delete_CallsDalCorrectly()
        {
            var expectedCertificateId = Create<int>();
            
            var dto = CreateValidDto();
            UserCertificateDto passedDto = null;
        
            var mockDal = new Mock<IUserCertificateDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedCertificateId))
                .ReturnsAsync(dto);
            
            mockDal.Setup(m => m.DeleteAsync(It.IsAny<UserCertificateDto>()))
                .Callback<UserCertificateDto>((p) => passedDto = p)
                .Returns(Task.CompletedTask);
        
            var mockDocumentFactory = new Mock<IDocumentFactory>();

            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(mockDocumentFactory)
                .WithBusinessObject<IUserCertificate, UserCertificate>()
                .Build();
        
            var factory = new UserCertificateFactory();
            var sut = await factory.GetByIdAsync(expectedCertificateId);
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

        #region GetByIdAsync UserCertificate
        
        [Test]
        public async Task GetByIdAsync_CallsDalCorrectly()
        {
            var expectedCertificateId = Create<int>();
            
            var mockDal = new Mock<IUserCertificateDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedCertificateId))
                .ReturnsAsync(Create<UserCertificateDto>());
            
            var mockDocumentFactory = new Mock<IDocumentFactory>();
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(mockDocumentFactory)
                .WithBusinessObject<IUserCertificate, UserCertificate>()
                .Build();
        
            var factory = new UserCertificateFactory();
            var sut = await factory.GetByIdAsync(expectedCertificateId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetById_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
            var expectedCertificateId = Create<int>();
            
            var mockDal = new Mock<IUserCertificateDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedCertificateId))
                .ReturnsAsync(dto);
            
            var mockDocumentFactory = new Mock<IDocumentFactory>();
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(mockDocumentFactory)
                .WithBusinessObject<IUserCertificate, UserCertificate>()
                .Build();
        
            var factory = new UserCertificateFactory();
            var sut = await factory.GetByIdAsync(expectedCertificateId);
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region Create
        
        [Test]
        public async Task Create_CallsDalCorrectly()
        {
            var dto = CreateValidDto();
            UserCertificateDto passedDto = null;
        
            var mockDal = new Mock<IUserCertificateDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<UserCertificateDto>()))
                .Callback<UserCertificateDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            var mockDocumentFactory = new Mock<IDocumentFactory>();

            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(mockDocumentFactory)
                .WithBusinessObject<IUserCertificate, UserCertificate>()
                .Build();
        
            var factory = new UserCertificateFactory();
            var sut = factory.Create();
            
            sut.CertificateId = dto.CertificateId;
            sut.UserId = dto.UserId;
            sut.DocumentId = dto.DocumentId;
            sut.CertificateTypeId = dto.CertificateTypeId;
            sut.CertificateType = dto.CertificateType;
            sut.IssueDate = dto.IssueDate;
            sut.CertificateNumber = dto.CertificateNumber;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null);
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                .Excluding(m => m.CertificateId)
                .Excluding(m => m.CertificateType)
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
        
            var mockDal = new Mock<IUserCertificateDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<UserCertificateDto>()))
                .ReturnsAsync(dto);
        
            var mockDocumentFactory = new Mock<IDocumentFactory>();

            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(mockDocumentFactory)
                .WithBusinessObject<IUserCertificate, UserCertificate>()
                .Build();
        
            var factory = new UserCertificateFactory();
            var sut = factory.Create();
            sut.CertificateId = dto.CertificateId;
            sut.UserId = dto.UserId;
            sut.DocumentId = dto.DocumentId;
            sut.CertificateTypeId = dto.CertificateTypeId;
            sut.CertificateType = dto.CertificateType;
            sut.IssueDate = dto.IssueDate;
            sut.CertificateNumber = dto.CertificateNumber;
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
	}
}
