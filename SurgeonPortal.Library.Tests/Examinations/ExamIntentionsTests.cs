using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Examinations;
using SurgeonPortal.Library.Contracts.Examinations;
using SurgeonPortal.Library.Examinations;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Examinations
{
    [TestFixture] 
	public class ExamIntentionsTests : TestBase<int>
    {
        private ExamIntentionsDto CreateValidDto()
        {
            var dto = Create<ExamIntentionsDto>();
        
            dto.ExamName = Create<string>();
            dto.ExamDate = Create<System.DateTime?>();
            dto.DateReceived = Create<System.DateTime?>();
            dto.Intention = Create<bool>();
        
            return dto;
        }
        
        

        #region GetByExamIdAsync ExamIntentions
        
        [Test]
        public async Task GetByExamIdAsync_CallsDalCorrectly()
        {
            var expectedExamId = Create<int>();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IExamIntentionsDal>();
            mockDal.Setup(m => m.GetByExamIdAsync(
                expectedUserId,
                expectedExamId))
                .ReturnsAsync(Create<ExamIntentionsDto>());
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IExamIntentions, ExamIntentions>()
                .Build();
        
            var factory = new ExamIntentionsFactory();
            var sut = await factory.GetByExamIdAsync(expectedExamId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByExamId_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
            var expectedExamId = Create<int>();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IExamIntentionsDal>();
            mockDal.Setup(m => m.GetByExamIdAsync(
                expectedUserId,
                expectedExamId))
                .ReturnsAsync(dto);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IExamIntentions, ExamIntentions>()
                .Build();
        
            var factory = new ExamIntentionsFactory();
            var sut = await factory.GetByExamIdAsync(expectedExamId);
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers()
                .Excluding(m => m.UserId)
                .Excluding(m => m.ExamId));
        }
        
        #endregion

        #region Create
        
        [Test]
        public async Task Create_CallsDalCorrectly()
        {
            var dto = CreateValidDto();
            ExamIntentionsDto passedDto = null;
        
            var mockDal = new Mock<IExamIntentionsDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<ExamIntentionsDto>()))
                .Callback<ExamIntentionsDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IExamIntentions, ExamIntentions>()
                .Build();
        
            var factory = new ExamIntentionsFactory();
            var sut = factory.Create();

            sut.UserId = dto.UserId;
            sut.ExamId = dto.ExamId;
            sut.ExamName = dto.ExamName;
            sut.ExamDate = dto.ExamDate;
            sut.DateReceived = dto.DateReceived;
            sut.Intention = dto.Intention;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null);
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                .Excluding(m => m.ExamName)
                .Excluding(m => m.ExamDate)
                .Excluding(m => m.DateReceived)
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
        
            var mockDal = new Mock<IExamIntentionsDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<ExamIntentionsDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IExamIntentions, ExamIntentions>()
                .Build();
        
            var factory = new ExamIntentionsFactory();
            var sut = factory.Create();
            sut.UserId = dto.UserId;
            sut.ExamId = dto.ExamId;
            sut.ExamName = dto.ExamName;
            sut.ExamDate = dto.ExamDate;
            sut.DateReceived = dto.DateReceived;
            sut.Intention = dto.Intention;
        
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
