using Csla.Rules;
using Moq;
using NUnit.Framework;
using SurgeonPortal.Library.Contracts.ProfessionalStanding;
using SurgeonPortal.Library.ProfessionalStanding;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.ProfessionalStanding
{
	internal class TestableUpdateOnlySelfReportedRule : UpdateOnlySelfReportedRule
	{
		public TestableUpdateOnlySelfReportedRule(int priority) : base(priority)
		{
		}

		public void TestableExecute(IRuleContext context)
		{
			Execute(context);
		}
	}

	public class UpdateOnlySelfReportedRuleTests : TestBase<int>
	{
		[Test]
		public void Execute_ShouldAddError_WhenNotNewAndNotSelfReported()
		{
			var mockMedicalLicense = new Mock<IMedicalLicense>();
			mockMedicalLicense.SetupGet(ml => ml.IsNew).Returns(false);
			mockMedicalLicense.SetupGet(ml => ml.ReportingOrganization).Returns("Other");

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(c => c.Target).Returns(mockMedicalLicense.Object);

			var sut = new TestableUpdateOnlySelfReportedRule(1);

			sut.TestableExecute(mockContext.Object);

			mockContext.Verify(c => c.AddErrorResult(It.IsAny<string>()), Times.Once);
		}

		[Test]
		public void Execute_ShouldNotAddError_WhenNewAndNotSelfReported()
		{
			var mockMedicalLicense = new Mock<IMedicalLicense>();
			mockMedicalLicense.SetupGet(ml => ml.IsNew).Returns(true);
			mockMedicalLicense.SetupGet(ml => ml.ReportingOrganization).Returns("Other");

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(c => c.Target).Returns(mockMedicalLicense.Object);

			var sut = new TestableUpdateOnlySelfReportedRule(1);

			sut.TestableExecute(mockContext.Object);

			mockContext.Verify(c => c.AddErrorResult(It.IsAny<string>()), Times.Never);
		}

		[Test]
		public void Execute_ShouldNotAddError_WhenNotNewAndSelfReported()
		{
			var mockMedicalLicense = new Mock<IMedicalLicense>();
			mockMedicalLicense.SetupGet(ml => ml.IsNew).Returns(false);
			mockMedicalLicense.SetupGet(ml => ml.ReportingOrganization).Returns("Self");

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(c => c.Target).Returns(mockMedicalLicense.Object);

			var sut = new TestableUpdateOnlySelfReportedRule(1);

			sut.TestableExecute(mockContext.Object);

			mockContext.Verify(c => c.AddErrorResult(It.IsAny<string>()), Times.Never);
		}

		[Test]
		public void Execute_ShouldNotAddError_WhenNewAndSelfReported()
		{
			var mockMedicalLicense = new Mock<IMedicalLicense>();
			mockMedicalLicense.SetupGet(ml => ml.IsNew).Returns(true);
			mockMedicalLicense.SetupGet(ml => ml.ReportingOrganization).Returns("Self");

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(c => c.Target).Returns(mockMedicalLicense.Object);

			var sut = new TestableUpdateOnlySelfReportedRule(1);

			sut.TestableExecute(mockContext.Object);

			mockContext.Verify(c => c.AddErrorResult(It.IsAny<string>()), Times.Never);
		}
	}
}
