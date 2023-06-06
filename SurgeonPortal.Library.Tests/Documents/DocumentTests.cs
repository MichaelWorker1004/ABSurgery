using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Documents;
using SurgeonPortal.Library.Contracts.Documents;
using SurgeonPortal.Library.Documents;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Documents
{
    [TestFixture] 
	public class DocumentTests : TestBase<string>
    {
        private DocumentDto CreateValidDto()
        {     
            var dto = Create<DocumentDto>();

            dto.Id = Create<int>();
            dto.UserId = Create<int>();
            dto.StreamId = Create<System.Guid>();
            dto.DocumentTypeId = Create<int>();
            dto.DocumentName = Create<string>();
            dto.DocumentType = Create<string>();
            dto.InternalViewOnly = Create<bool>();
            dto.CreatedByUserId = Create<int>();
            dto.UploadedBy = Create<string>();
            dto.UploadedDateUtc = Create<System.DateTime>();
            dto.CreatedAtUtc = Create<System.DateTime>();
            dto.LastUpdatedAtUtc = Create<System.DateTime>();
            dto.LastUpdatedByUserId = Create<int>();
    
            return dto;
        }

            #region Document Business Rules
            [Test]
            public async Task IsRequired_GetById_Id_Fails()
            {
                var dto = CreateValidDto();
            
                var expectedId = Create<int>();
            
                var mockDal = new Mock<IDocumentDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByIdAsync(expectedId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity()
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IDocument, Document>()
                    .Build();
            
                var factory = new DocumentFactory();
                var sut = await factory.GetByIdAsync(expectedId);
                
                sut.Id = default;
            
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            
                //Ensure that the save fails...
                var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(() => sut.SaveAsync());
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
                Assert.That(sut.GetBrokenRules()[0].Description == "Id is required", $"Expected the rule description to be 'Id is required', have {sut.GetBrokenRules()[0].Description}");
                Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
                Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
            }
            
            [Test]
            public async Task IsRequired_GetById_Id_Passes()
            {
                var dto = CreateValidDto();
            
                var expectedId = Create<int>();
            
                var mockDal = new Mock<IDocumentDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByIdAsync(expectedId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity()
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IDocument, Document>()
                    .Build();
            
                var factory = new DocumentFactory();
                var sut = await factory.GetByIdAsync(expectedId);
                
                sut.Id = Create<int>();
            
                Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
            
            }
            [Test]
            public async Task IsRequired_GetById_UserId_Fails()
            {
                var dto = CreateValidDto();
            
                var expectedId = Create<int>();
            
                var mockDal = new Mock<IDocumentDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByIdAsync(expectedId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity()
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IDocument, Document>()
                    .Build();
            
                var factory = new DocumentFactory();
                var sut = await factory.GetByIdAsync(expectedId);
                
                sut.UserId = default;
            
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            
                //Ensure that the save fails...
                var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(() => sut.SaveAsync());
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
                Assert.That(sut.GetBrokenRules()[0].Description == "UserId is required", $"Expected the rule description to be 'UserId is required', have {sut.GetBrokenRules()[0].Description}");
                Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
                Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
            }
            
            [Test]
            public async Task IsRequired_GetById_UserId_Passes()
            {
                var dto = CreateValidDto();
            
                var expectedId = Create<int>();
            
                var mockDal = new Mock<IDocumentDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByIdAsync(expectedId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity()
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IDocument, Document>()
                    .Build();
            
                var factory = new DocumentFactory();
                var sut = await factory.GetByIdAsync(expectedId);
                
                sut.UserId = Create<int>();
            
                Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
            
            }
            [Test]
            public async Task IsRequired_GetById_DocumentTypeId_Fails()
            {
                var dto = CreateValidDto();
            
                var expectedId = Create<int>();
            
                var mockDal = new Mock<IDocumentDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByIdAsync(expectedId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity()
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IDocument, Document>()
                    .Build();
            
                var factory = new DocumentFactory();
                var sut = await factory.GetByIdAsync(expectedId);
                
                sut.DocumentTypeId = default;
            
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
            
                //Ensure that the save fails...
                var ex = Assert.ThrowsAsync<Csla.Rules.ValidationException>(() => sut.SaveAsync());
                Assert.That(sut.GetBrokenRules().Count == 1, $"Expected 1 broken rule, have {sut.GetBrokenRules().Count} ");
                Assert.That(sut.GetBrokenRules()[0].Description == "DocumentTypeId is required", $"Expected the rule description to be 'DocumentTypeId is required', have {sut.GetBrokenRules()[0].Description}");
                Assert.That(sut.GetBrokenRules()[0].Severity == Csla.Rules.RuleSeverity.Error, $"Expected the rule severity to be Error, have {sut.GetBrokenRules()[0].Severity}");
                Assert.That(ex.Message, Is.EqualTo("Object is not valid and can not be saved"));
            }
            
            [Test]
            public async Task IsRequired_GetById_DocumentTypeId_Passes()
            {
                var dto = CreateValidDto();
            
                var expectedId = Create<int>();
            
                var mockDal = new Mock<IDocumentDal>(MockBehavior.Strict);
                mockDal.Setup(m => m.GetByIdAsync(expectedId))
                    .ReturnsAsync(dto);
            
                UseMockServiceProvider()
                    .WithMockedIdentity()
                    .WithRegisteredInstance(mockDal)
                    .WithBusinessObject<IDocument, Document>()
                    .Build();
            
                var factory = new DocumentFactory();
                var sut = await factory.GetByIdAsync(expectedId);
                
                sut.DocumentTypeId = Create<int>();
            
                Assert.That(sut.GetBrokenRules().Count == 0, $"Expected 0 broken rule, have {sut.GetBrokenRules().Count} ");
            
            }
            #endregion

        #region DeleteAsync
        
        [Test]
        public async Task Delete_CallsDalCorrectly()
        {
            var expectedId = Create<int>();
            
            var dto = CreateValidDto();
            DocumentDto passedDto = null;
        
            var mockDal = new Mock<IDocumentDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedId))
                .ReturnsAsync(dto);
            mockDal.Setup(m => m.DeleteAsync(It.IsAny<DocumentDto>()))
                .Callback<DocumentDto>((p) => passedDto = p)
                .Returns(Task.CompletedTask);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
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
            
            var mockDal = new Mock<IDocumentDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedId))
                .ReturnsAsync(Create<DocumentDto>());
        
            UseMockServiceProvider()
                .WithMockedIdentity()
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
        
            var mockDal = new Mock<IDocumentDal>();
            mockDal.Setup(m => m.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IDocument, Document>()
                .Build();
        
            var factory = new DocumentFactory();
            var sut = await factory.GetByIdAsync(Create<int>());
        
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
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IDocument, Document>()
                .Build();
        
            var factory = new DocumentFactory();
            var sut = factory.Create();
            
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.StreamId = dto.StreamId;
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
                .Excluding(m => m.CreatedAtUtc)
                .Excluding(m => m.CreatedByUserId)
                .Excluding(m => m.LastUpdatedAtUtc)
                .Excluding(m => m.LastUpdatedByUserId)
                .Excluding(m => m.Id)
                .Excluding(m => m.DocumentType)
                .Excluding(m => m.UploadedBy)
                .Excluding(m => m.UploadedDateUtc)
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
        
            var mockDal = new Mock<IDocumentDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<DocumentDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IDocument, Document>()
                .Build();
        
            var factory = new DocumentFactory();
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
            var expectedId = Create<int>();
            
            var dto = Create<DocumentDto>();
            DocumentDto passedDto = null;
        
            var mockDal = new Mock<IDocumentDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedId))
                        .ReturnsAsync(dto);
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<DocumentDto>()))
                .Callback<DocumentDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IDocument, Document>()
                .Build();
        
            var factory = new DocumentFactory();
            var sut = await factory.GetByIdAsync(expectedId);
            
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.StreamId = dto.StreamId;
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
        
            // We now change all properties on the SUT to make it Dirty
            // or the SaveAsync() will not be called. :)
            dto = CreateValidDto();
        
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.StreamId = dto.StreamId;
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
        
            Assert.That(passedDto, Is.Not.Null, "The passedDto is null, which can mean that the DataPortal method was not called.");
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                    .Excluding(m => m.CreatedAtUtc)
                    .Excluding(m => m.CreatedByUserId)
                    .Excluding(m => m.LastUpdatedAtUtc)
                    .Excluding(m => m.LastUpdatedByUserId)
                    .Excluding(m => m.DocumentType)
                    .Excluding(m => m.CreatedByUserId)
                    .Excluding(m => m.UploadedBy)
                    .Excluding(m => m.UploadedDateUtc)
                    .Excluding(m => m.CreatedAtUtc)
                    .Excluding(m => m.LastUpdatedAtUtc)
                .ExcludingMissingMembers());
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task Update_YieldsCorrectResult()
        {
            var expectedId = Create<int>();
            
            var dto = Create<DocumentDto>();
        
            var mockDal = new Mock<IDocumentDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedId))
                        .ReturnsAsync(Create<DocumentDto>());
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<DocumentDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IDocument, Document>()
                .Build();
        
            var factory = new DocumentFactory();
            var sut = await factory.GetByIdAsync(expectedId);
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