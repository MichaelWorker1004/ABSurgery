using Csla;
using Csla.Core;
using Csla.Rules;
using Moq;
using NUnit.Framework;
using SurgeonPortal.Library.Contracts.ProfessionalStanding;
using SurgeonPortal.Library.ProfessionalStanding;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.ProfessionalStanding
{
	internal class TestableClinicallyActiveRequiresRule : ClinicallyActiveRequiresRule
	{
		public TestableClinicallyActiveRequiresRule(IGetClinicallyActiveCommandFactory getClinicallyActiveCommandFactory, IPropertyInfo primaryProperty, int priority) : base(getClinicallyActiveCommandFactory, primaryProperty, priority)
		{
		}

		public void TestableExecute(IRuleContext context)
		{
			Execute(context);
		}
	}

	public class ClinicallyActiveRequiresRuleTests : TestBase<int>
	{
		[Test]
		public void Execute_ShouldNotAddError_WhenClinicalActiveWithRequiredFields()
		{
			var mockCommandFactory = GetMockedCommandFactory(true);

			var mockUserProfessionalStanding = new Mock<IUserProfessionalStanding>();
			mockUserProfessionalStanding.SetupGet(x => x.PrimaryPracticeId).Returns(1);
			mockUserProfessionalStanding.SetupGet(x => x.OrganizationTypeId).Returns(1);

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(x => x.Target).Returns(mockUserProfessionalStanding.Object);

			var rule = new TestableClinicallyActiveRequiresRule(mockCommandFactory.Object, null, 1);

			rule.TestableExecute(mockContext.Object);

			mockContext.Verify(x => x.AddErrorResult(It.IsAny<string>()), Times.Never);
		}

		[Test]
		public void Execute_ShouldAddError_WhenClinicalActiveWithoutRequiredFields()
		{
			var mockCommandFactory = GetMockedCommandFactory(true);

			var mockUserProfessionalStanding = new Mock<IUserProfessionalStanding>();
			mockUserProfessionalStanding.SetupGet(x => x.PrimaryPracticeId).Returns((int?)null);
			mockUserProfessionalStanding.SetupGet(x => x.OrganizationTypeId).Returns((int?)null);

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(x => x.Target).Returns(mockUserProfessionalStanding.Object);

			var primaryProperty = new PropertyInfo<string>("ClinicallyActive");

			var rule = new TestableClinicallyActiveRequiresRule(mockCommandFactory.Object, primaryProperty, 1);

			rule.TestableExecute(mockContext.Object);

			mockContext.Verify(x => x.AddErrorResult(primaryProperty, "Primary practice is required when clinically active is true"), Times.Once);
			mockContext.Verify(x => x.AddErrorResult(primaryProperty, "Organization type is required when clinically active is true"), Times.Once);
		}

		[Test]
		public void Execute_ShouldNotAddError_WhenNotClinicalActiveWithRequiredFields()
		{
			var mockCommandFactory = GetMockedCommandFactory(false);

			var mockUserProfessionalStanding = new Mock<IUserProfessionalStanding>();
			mockUserProfessionalStanding.SetupGet(x => x.ExplanationOfNonPrivileges).Returns("value");
			mockUserProfessionalStanding.SetupGet(x => x.ExplanationOfNonClinicalActivities).Returns("string");

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(x => x.Target).Returns(mockUserProfessionalStanding.Object);

			var rule = new TestableClinicallyActiveRequiresRule(mockCommandFactory.Object, null, 1);

			rule.TestableExecute(mockContext.Object);

			mockContext.Verify(x => x.AddErrorResult(It.IsAny<string>()), Times.Never);
		}

		[Test]
		public void Execute_ShouldAddError_WhenNotClinicalActiveWithoutRequiredFields()
		{
			var mockCommandFactory = GetMockedCommandFactory(false);

			var mockUserProfessionalStanding = new Mock<IUserProfessionalStanding>();
			mockUserProfessionalStanding.SetupGet(x => x.ExplanationOfNonPrivileges).Returns((string)null);
			mockUserProfessionalStanding.SetupGet(x => x.ExplanationOfNonClinicalActivities).Returns((string)null);

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(x => x.Target).Returns(mockUserProfessionalStanding.Object);

			var primaryProperty = new PropertyInfo<string>("ClinicallyActive");

			var rule = new TestableClinicallyActiveRequiresRule(mockCommandFactory.Object, primaryProperty, 1);

			rule.TestableExecute(mockContext.Object);

			mockContext.Verify(x => x.AddErrorResult(primaryProperty, "Explanation of non-privaleges is required when clinically active is false"), Times.Once);
			mockContext.Verify(x => x.AddErrorResult(primaryProperty, "Explanation of non-clinical activities is required when clinically active is false"), Times.Once);
		}

		static Mock<IGetClinicallyActiveCommandFactory> GetMockedCommandFactory(bool clinicallyActive)
		{
			var mockCommand = new Mock<IGetClinicallyActiveCommand>();
			mockCommand.SetupGet(x => x.ClinicallyActive).Returns(clinicallyActive);

			var mockCommandFactory = new Mock<IGetClinicallyActiveCommandFactory>();
			mockCommandFactory.Setup(x => x.GetClinicallyActiveByUserId()).Returns(mockCommand.Object);

			return mockCommandFactory;
		}
	}
}
