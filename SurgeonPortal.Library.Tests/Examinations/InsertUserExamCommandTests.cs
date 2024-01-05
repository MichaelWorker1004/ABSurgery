using Moq;
using SurgeonPortal.DataAccess.Contracts.Examinations;
using SurgeonPortal.Library.Contracts.Examinations;
using SurgeonPortal.Library.Examinations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Examinations
{
    public class InsertUserExamCommandTests : TestBase<int>
    {
        #region ExecuteCommand

        public async Task ExecuteCommand_CallsDalCorrectly()
        {
            var expectedExamHeaderId = Create<int>();
            var expectedUserId = 1234;

            var mockDal = new Mock<IInsertUserExamCommandDal>();
            mockDal.Setup(m => m.InsertUserExamAsync(
                expectedUserId,
                expectedExamHeaderId));

            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IInsertUserExamCommand, InsertUserExamCommand>()
                .Build();

            var factory = new InsertUserExamCommandFactory();
            var sut = await factory.InsertUserExamAsync(expectedExamHeaderId);

            mockDal.VerifyAll();
        }

        #endregion
    }
}
