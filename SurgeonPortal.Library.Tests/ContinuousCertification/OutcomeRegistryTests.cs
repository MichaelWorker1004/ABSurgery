using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.ContinuousCertification;
using SurgeonPortal.Library.ContinuousCertification;
using SurgeonPortal.Library.Contracts.ContinuousCertification;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.ContinuousCertification
{
    [TestFixture] 
	public class OutcomeRegistryTests : TestBase<string>
    {
        private OutcomeRegistryDto CreateValidDto()
        {     
            var dto = Create<OutcomeRegistryDto>();

            dto.SurgeonSpecificRegistry = Create<bool>();
            dto.RegistryComments = Create<string>();
            dto.RegisteredWithACHQC = Create<bool>();
            dto.RegisteredWithCESQIP = Create<bool>();
            dto.RegisteredWithMBSAQIP = Create<bool>();
            dto.RegisteredWithABA = Create<bool>();
            dto.RegisteredWithASBS = Create<bool>();
            dto.RegisteredWithStatewideCollaboratives = Create<bool>();
            dto.RegisteredWithABMS = Create<bool>();
            dto.RegisteredWithNCDB = Create<bool>();
            dto.RegisteredWithRQRS = Create<bool>();
            dto.RegisteredWithNSQIP = Create<bool>();
            dto.RegisteredWithNTDB = Create<bool>();
            dto.RegisteredWithSTS = Create<bool>();
            dto.RegisteredWithTQIP = Create<bool>();
            dto.RegisteredWithUNOS = Create<bool>();
            dto.RegisteredWithNCDR = Create<bool>();
            dto.RegisteredWithSVS = Create<bool>();
            dto.RegisteredWithELSO = Create<bool>();
            dto.UserConfirmed = Create<bool>();
            dto.UserConfirmedDateUtc = Create<System.DateTime>();
            dto.UserId = Create<int>();
    
            return dto;
        }

            

        #region GetByUserIdAsync OutcomeRegistry
        
        [Test]
        public async Task GetByUserIdAsync_CallsDalCorrectly()
        {
            
            var mockDal = new Mock<IOutcomeRegistryDal>();
            mockDal.Setup(m => m.GetByUserIdAsync())
                .ReturnsAsync(Create<OutcomeRegistryDto>());
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IOutcomeRegistry, OutcomeRegistry>()
                .Build();
        
            var factory = new OutcomeRegistryFactory();
            var sut = await factory.GetByUserIdAsync();
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByUserId_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IOutcomeRegistryDal>();
            mockDal.Setup(m => m.GetByUserIdAsync())
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IOutcomeRegistry, OutcomeRegistry>()
                .Build();
        
            var factory = new OutcomeRegistryFactory();
            var sut = await factory.GetByUserIdAsync();
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region Create
        
        [Test]
        public async Task Create_CallsDalCorrectly()
        {
            var dto = CreateValidDto();
            OutcomeRegistryDto passedDto = null;
        
            var mockDal = new Mock<IOutcomeRegistryDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<OutcomeRegistryDto>()))
                .Callback<OutcomeRegistryDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IOutcomeRegistry, OutcomeRegistry>()
                .Build();
        
            var factory = new OutcomeRegistryFactory();
            var sut = factory.Create();
            
            sut.SurgeonSpecificRegistry = dto.SurgeonSpecificRegistry;
            sut.RegistryComments = dto.RegistryComments;
            sut.RegisteredWithACHQC = dto.RegisteredWithACHQC;
            sut.RegisteredWithCESQIP = dto.RegisteredWithCESQIP;
            sut.RegisteredWithMBSAQIP = dto.RegisteredWithMBSAQIP;
            sut.RegisteredWithABA = dto.RegisteredWithABA;
            sut.RegisteredWithASBS = dto.RegisteredWithASBS;
            sut.RegisteredWithStatewideCollaboratives = dto.RegisteredWithStatewideCollaboratives;
            sut.RegisteredWithABMS = dto.RegisteredWithABMS;
            sut.RegisteredWithNCDB = dto.RegisteredWithNCDB;
            sut.RegisteredWithRQRS = dto.RegisteredWithRQRS;
            sut.RegisteredWithNSQIP = dto.RegisteredWithNSQIP;
            sut.RegisteredWithNTDB = dto.RegisteredWithNTDB;
            sut.RegisteredWithSTS = dto.RegisteredWithSTS;
            sut.RegisteredWithTQIP = dto.RegisteredWithTQIP;
            sut.RegisteredWithUNOS = dto.RegisteredWithUNOS;
            sut.RegisteredWithNCDR = dto.RegisteredWithNCDR;
            sut.RegisteredWithSVS = dto.RegisteredWithSVS;
            sut.RegisteredWithELSO = dto.RegisteredWithELSO;
            sut.UserConfirmed = dto.UserConfirmed;
            sut.UserConfirmedDateUtc = dto.UserConfirmedDateUtc;
            sut.UserId = dto.UserId;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null);
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                .Excluding(m => m.CreatedAtUtc)
                .Excluding(m => m.CreatedByUserId)
                .Excluding(m => m.LastUpdatedAtUtc)
                .Excluding(m => m.LastUpdatedByUserId)
                .Excluding(m => m.UserId)
                .ExcludingMissingMembers());
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task Create_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IOutcomeRegistryDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<OutcomeRegistryDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IOutcomeRegistry, OutcomeRegistry>()
                .Build();
        
            var factory = new OutcomeRegistryFactory();
            var sut = factory.Create();
            sut.SurgeonSpecificRegistry = Create<bool>();
        
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
            
            var dto = Create<OutcomeRegistryDto>();
            OutcomeRegistryDto passedDto = null;
        
            var mockDal = new Mock<IOutcomeRegistryDal>();
            mockDal.Setup(m => m.GetByUserIdAsync())
                        .ReturnsAsync(dto);
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<OutcomeRegistryDto>()))
                .Callback<OutcomeRegistryDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IOutcomeRegistry, OutcomeRegistry>()
                .Build();
        
            var factory = new OutcomeRegistryFactory();
            var sut = await factory.GetByUserIdAsync();
            
            sut.SurgeonSpecificRegistry = dto.SurgeonSpecificRegistry;
            sut.RegistryComments = dto.RegistryComments;
            sut.RegisteredWithACHQC = dto.RegisteredWithACHQC;
            sut.RegisteredWithCESQIP = dto.RegisteredWithCESQIP;
            sut.RegisteredWithMBSAQIP = dto.RegisteredWithMBSAQIP;
            sut.RegisteredWithABA = dto.RegisteredWithABA;
            sut.RegisteredWithASBS = dto.RegisteredWithASBS;
            sut.RegisteredWithStatewideCollaboratives = dto.RegisteredWithStatewideCollaboratives;
            sut.RegisteredWithABMS = dto.RegisteredWithABMS;
            sut.RegisteredWithNCDB = dto.RegisteredWithNCDB;
            sut.RegisteredWithRQRS = dto.RegisteredWithRQRS;
            sut.RegisteredWithNSQIP = dto.RegisteredWithNSQIP;
            sut.RegisteredWithNTDB = dto.RegisteredWithNTDB;
            sut.RegisteredWithSTS = dto.RegisteredWithSTS;
            sut.RegisteredWithTQIP = dto.RegisteredWithTQIP;
            sut.RegisteredWithUNOS = dto.RegisteredWithUNOS;
            sut.RegisteredWithNCDR = dto.RegisteredWithNCDR;
            sut.RegisteredWithSVS = dto.RegisteredWithSVS;
            sut.RegisteredWithELSO = dto.RegisteredWithELSO;
            sut.UserConfirmed = dto.UserConfirmed;
            sut.UserConfirmedDateUtc = dto.UserConfirmedDateUtc;
            sut.UserId = dto.UserId;
        
            // We now change all properties on the SUT to make it Dirty
            // or the SaveAsync() will not be called. :)
            dto = CreateValidDto();
        
            sut.SurgeonSpecificRegistry = dto.SurgeonSpecificRegistry;
            sut.RegistryComments = dto.RegistryComments;
            sut.RegisteredWithACHQC = dto.RegisteredWithACHQC;
            sut.RegisteredWithCESQIP = dto.RegisteredWithCESQIP;
            sut.RegisteredWithMBSAQIP = dto.RegisteredWithMBSAQIP;
            sut.RegisteredWithABA = dto.RegisteredWithABA;
            sut.RegisteredWithASBS = dto.RegisteredWithASBS;
            sut.RegisteredWithStatewideCollaboratives = dto.RegisteredWithStatewideCollaboratives;
            sut.RegisteredWithABMS = dto.RegisteredWithABMS;
            sut.RegisteredWithNCDB = dto.RegisteredWithNCDB;
            sut.RegisteredWithRQRS = dto.RegisteredWithRQRS;
            sut.RegisteredWithNSQIP = dto.RegisteredWithNSQIP;
            sut.RegisteredWithNTDB = dto.RegisteredWithNTDB;
            sut.RegisteredWithSTS = dto.RegisteredWithSTS;
            sut.RegisteredWithTQIP = dto.RegisteredWithTQIP;
            sut.RegisteredWithUNOS = dto.RegisteredWithUNOS;
            sut.RegisteredWithNCDR = dto.RegisteredWithNCDR;
            sut.RegisteredWithSVS = dto.RegisteredWithSVS;
            sut.RegisteredWithELSO = dto.RegisteredWithELSO;
            sut.UserConfirmed = dto.UserConfirmed;
            sut.UserConfirmedDateUtc = dto.UserConfirmedDateUtc;
            sut.UserId = dto.UserId;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null, "The passedDto is null, which can mean that the DataPortal method was not called.");
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                    .Excluding(m => m.CreatedAtUtc)
                    .Excluding(m => m.CreatedByUserId)
                    .Excluding(m => m.LastUpdatedAtUtc)
                    .Excluding(m => m.LastUpdatedByUserId)
                    .Excluding(m => m.UserId)
                .ExcludingMissingMembers());
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task Update_YieldsCorrectResult()
        {
            
            var dto = Create<OutcomeRegistryDto>();
        
            var mockDal = new Mock<IOutcomeRegistryDal>();
            mockDal.Setup(m => m.GetByUserIdAsync())
                        .ReturnsAsync(Create<OutcomeRegistryDto>());
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<OutcomeRegistryDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IOutcomeRegistry, OutcomeRegistry>()
                .Build();
        
            var factory = new OutcomeRegistryFactory();
            var sut = await factory.GetByUserIdAsync();
            sut.SurgeonSpecificRegistry = Create<bool>();
        
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
