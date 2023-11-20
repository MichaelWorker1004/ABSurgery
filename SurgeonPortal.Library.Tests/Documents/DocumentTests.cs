using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts;
using SurgeonPortal.DataAccess.Contracts.Documents;
using SurgeonPortal.DataAccess.Contracts.Storage;
using SurgeonPortal.Library;
using SurgeonPortal.Library.Contracts;
using SurgeonPortal.Library.Contracts.Documents;
using SurgeonPortal.Library.Documents;
using System;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Documents
{
    [TestFixture] 
	public class DocumentTests : TestBase<int>
    {
        private DocumentDto CreateValidDto()
        {
            var dto = Create<DocumentDto>();
        
            dto.Id = Create<int>();
            dto.UserId = 1234;
            dto.StreamId = Create<System.Guid>();
            dto.DocumentTypeId = Create<int>();
            dto.DocumentName = Create<string>();
            dto.DocumentType = Create<string>();
            dto.InternalViewOnly = Create<bool>();
            dto.CreatedByUserId = 1234;
            dto.UploadedBy = Create<string>();
            dto.UploadedDateUtc = Create<System.DateTime>();
            dto.CreatedAtUtc = Create<System.DateTime>();
            dto.LastUpdatedAtUtc = Create<System.DateTime>();
            dto.LastUpdatedByUserId = Create<int>();
        
            return dto;
        }
        
        

        #region DeleteAsync
        
        [Test]
        public async Task Delete_CallsDalCorrectly()
        {
            var expectedId = Create<int>();
            
            var dto = CreateValidDto();
            DocumentDto passedDto = null;
        
            var mockDal = new Mock<IDocumentDal>();
            mockDal.Setup(m => m.GetByIdAsync(
                expectedId,
                1234))
                .ReturnsAsync(dto);
            
            mockDal.Setup(m => m.DeleteAsync(It.IsAny<DocumentDto>()))
                .Callback<DocumentDto>((p) => passedDto = p)
                .Returns(Task.CompletedTask);
        
            var mockStorageDal = new Mock<IStorageDal>();

            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockStorageDal)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IDocument, Document>()
                .Build();
        
            var factory = new DocumentFactory();
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

        #region GetByIdAsync Document
        
        [Test]
        public async Task GetByIdAsync_CallsDalCorrectly()
        {
            var expectedId = Create<int>();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IDocumentDal>();
            mockDal.Setup(m => m.GetByIdAsync(
                expectedId,
                expectedUserId))
                .ReturnsAsync(Create<DocumentDto>());
            
        
            var mockStorageDal = new Mock<IStorageDal>();

            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockStorageDal)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IDocument, Document>()
                .Build();
        
            var factory = new DocumentFactory();
            var sut = await factory.GetByIdAsync(expectedId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetById_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
            var expectedId = Create<int>();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IDocumentDal>();
            mockDal.Setup(m => m.GetByIdAsync(
                expectedId,
                expectedUserId))
                .ReturnsAsync(dto);
            
        
            var mockStorageDal = new Mock<IStorageDal>();

            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockStorageDal)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IDocument, Document>()
                .Build();
        
            var factory = new DocumentFactory();
            var sut = await factory.GetByIdAsync(expectedId);
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region Create
        
        [Test]
        public async Task Create_CallsDalCorrectly()
        {
            var dto = CreateValidDto();
            DocumentDto passedDto = null;
        
            var mockDal = new Mock<IDocumentDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<DocumentDto>()))
                .Callback<DocumentDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            var mockStorageDal = new Mock<IStorageDal>();

            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockStorageDal)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IDocument, Document>()
                .Build();
        
            var factory = new DocumentFactory();
            var sut = factory.Create();
            
            sut.DocumentTypeId = dto.DocumentTypeId;
            sut.DocumentName = dto.DocumentName;
            sut.DocumentType = dto.DocumentType;
            sut.InternalViewOnly = dto.InternalViewOnly;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.UploadedBy = dto.UploadedBy;
            sut.UploadedDateUtc = dto.UploadedDateUtc;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null);
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                .Excluding(m => m.Id)
                .Excluding(m => m.DocumentType)
                .Excluding(m => m.UploadedBy)
                .Excluding(m => m.UploadedDateUtc)
                .Excluding(m => m.CreatedAtUtc)
                .Excluding(m => m.LastUpdatedAtUtc)
                .Excluding(m => m.LastUpdatedByUserId)
                .Excluding(m => m.CreatedByUserId)
                .Excluding(m => m.StreamId)
                .ExcludingMissingMembers());
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task Create_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IDocumentDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<DocumentDto>()))
                .ReturnsAsync(dto);
        
            var mockStorageDal = new Mock<IStorageDal>();

            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockStorageDal)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IDocument, Document>()
                .Build();
        
            var factory = new DocumentFactory();
            var sut = factory.Create();
            sut.DocumentTypeId = dto.DocumentTypeId;
            sut.DocumentName = dto.DocumentName;
            sut.DocumentType = dto.DocumentType;
            sut.InternalViewOnly = dto.InternalViewOnly;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.UploadedBy = dto.UploadedBy;
            sut.UploadedDateUtc = dto.UploadedDateUtc;
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
