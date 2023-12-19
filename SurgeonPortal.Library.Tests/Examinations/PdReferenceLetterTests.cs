using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Examinations;
using SurgeonPortal.Library.Contracts.Email;
using SurgeonPortal.Library.Contracts.Examinations;
using SurgeonPortal.Library.Examinations;
using SurgeonPortal.Shared.ReferenceLetters;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Examinations
{
    [TestFixture] 
	public class PdReferenceLetterTests : TestBase<int>
    {
        private PdReferenceLetterDto CreateValidDto()
        {
            var dto = Create<PdReferenceLetterDto>();
        
            dto.Id = Create<decimal>();
            dto.UserId = 1234;
            dto.Hosp = Create<string>();
            dto.Official = Create<string>();
            dto.Title = Create<string>();
            dto.Email = Create<string>();
            dto.LetterSent = Create<System.DateTime?>();
            dto.Status = Create<int>();
        
            return dto;
        }
        
        

        #region GetByExamIdAsync PdReferenceLetter
        
        [Test]
        public async Task GetByExamIdAsync_CallsDalCorrectly()
        {
            var expectedExamId = Create<int>();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IPdReferenceLetterDal>();
            mockDal.Setup(m => m.GetByExamIdAsync(
                expectedUserId,
                expectedExamId))
                .ReturnsAsync(Create<PdReferenceLetterDto>());
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(GetEmailFactory())
                .WithRegisteredInstance(GetConfiguration())
                .WithBusinessObject<IPdReferenceLetter, PdReferenceLetter>()
                .Build();
        
            var factory = new PdReferenceLetterFactory();
            var sut = await factory.GetByExamIdAsync(expectedExamId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByExamId_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
            var expectedExamId = Create<int>();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IPdReferenceLetterDal>();
            mockDal.Setup(m => m.GetByExamIdAsync(
                expectedUserId,
                expectedExamId))
                .ReturnsAsync(dto);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(GetEmailFactory())
                .WithRegisteredInstance(GetConfiguration())
                .WithBusinessObject<IPdReferenceLetter, PdReferenceLetter>()
                .Build();
        
            var factory = new PdReferenceLetterFactory();
            var sut = await factory.GetByExamIdAsync(expectedExamId);
        
            dto.Should().BeEquivalentTo(sut, 
                options => options
                    .ExcludingMissingMembers()
                    .Excluding(m => m.ExamId)
                    .Excluding(m => m.IdCode));
        }
        
        #endregion

        #region Create
        
        [Test]
        public async Task Create_CallsDalCorrectly()
        {
            var dto = CreateValidDto();
            PdReferenceLetterDto passedDto = null;
        
            var mockDal = new Mock<IPdReferenceLetterDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<PdReferenceLetterDto>()))
                .Callback<PdReferenceLetterDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(GetEmailFactory())
                .WithRegisteredInstance(GetConfiguration())
                .WithBusinessObject<IPdReferenceLetter, PdReferenceLetter>()
                .Build();
        
            var factory = new PdReferenceLetterFactory();
            var sut = factory.Create();
            
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.Hosp = dto.Hosp;
            sut.Official = dto.Official;
            sut.Title = dto.Title;
            sut.Email = dto.Email;
            sut.LetterSent = dto.LetterSent;
            sut.Status = dto.Status;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null);
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                .Excluding(m => m.Id)
                .Excluding(m => m.LetterSent)
                .Excluding(m => m.Status)
                .Excluding(m => m.ExamId)
                .Excluding(m => m.IdCode)
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
        
            var mockDal = new Mock<IPdReferenceLetterDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<PdReferenceLetterDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(GetEmailFactory())
                .WithRegisteredInstance(GetConfiguration())
                .WithBusinessObject<IPdReferenceLetter, PdReferenceLetter>()
                .Build();
        
            var factory = new PdReferenceLetterFactory();
            var sut = factory.Create();
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.Hosp = dto.Hosp;
            sut.Official = dto.Official;
            sut.Title = dto.Title;
            sut.Email = dto.Email;
            sut.LetterSent = dto.LetterSent;
            sut.Status = dto.Status;
        
            await sut.SaveAsync();
            
            dto.Should().BeEquivalentTo(sut,
                options => options
                .Excluding(m => m.CreatedAtUtc)
                    .Excluding(m => m.CreatedByUserId)
                    .Excluding(m => m.LastUpdatedAtUtc)
                    .Excluding(m => m.LastUpdatedByUserId)
                    .Excluding(m => m.ExamId)
                    .Excluding(m => m.IdCode)
                    .ExcludingMissingMembers());
        }

        #endregion

        private static Mock<IEmailFactory> GetEmailFactory()
        {
            var mockEmailFactory = new Mock<IEmailFactory>();
            mockEmailFactory.Setup(e => e.Create()).Returns(new Mock<IEmail>().Object);
            return mockEmailFactory;
        }

        private static Mock<IOptions<ReferenceLettersConfiguration>> GetConfiguration()
        {
            var mockOptions = new Mock<IOptions<ReferenceLettersConfiguration>>();
            mockOptions.Setup(m => m.Value).Returns(new ReferenceLettersConfiguration());
            return mockOptions;
        }
    }
}
