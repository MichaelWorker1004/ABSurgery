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
    public class RegistrationCompleteCommandTests : TestBase<int>
    {
        #region ExecuteCommand

        public async Task ExecuteCommand_CallsDalCorrectly()
        {
            var expectedUserId = 1234;
            var expectedExamHeaderId = Create<int>();

            var mockDal = new Mock<IRegistrationCompleteCommandDal>();
            mockDal.Setup(m => m.CompleteRegistrationAsync(
                expectedUserId,
                expectedExamHeaderId));

            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IRegistrationCompleteCommand, RegistrationCompleteCommand>()
                .Build();

            var factory = new RegistrationCompleteCommandFactory();
            var sut = await factory.CompleteRegistrationAsync(expectedExamHeaderId);

            mockDal.VerifyAll();
        }

        #endregion
    }
}
