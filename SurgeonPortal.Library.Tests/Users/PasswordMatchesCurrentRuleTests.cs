using Csla;
using Csla.Core;
using Csla.Rules;
using Moq;
using NUnit.Framework;
using SurgeonPortal.Library.Contracts.Users;
using SurgeonPortal.Library.Users;
using System.Collections.Generic;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Users
{
	internal class TestablePasswordMatchesCurrentRule : PasswordMatchesCurrentRule
	{
		public TestablePasswordMatchesCurrentRule(IPasswordValidationCommandFactory passwordValidationCommandFactory, IPropertyInfo primaryProperty, IPropertyInfo userIdProperty, int priority) 
			: base(passwordValidationCommandFactory, primaryProperty, userIdProperty, priority)
		{
		}

		public void TestableExecute(IRuleContext context)
		{
			Execute(context);
		}
	}

	public class PasswordMatchesCurrentRuleTests : TestBase<int>
	{
		private readonly PropertyInfo<string> primaryProperty = new("password");
		private readonly PropertyInfo<int> userIdProperty = new("userId");

		[Test]
		public void Should_NotAddError_IfPasswordNotAvailable()
		{
			var mockContext = GetMockContext(null, 1234);

			var mockCommandFactory = GetCommandFactory(true);

			var sut = new TestablePasswordMatchesCurrentRule(mockCommandFactory.Object, primaryProperty, userIdProperty, 1);

			sut.TestableExecute(mockContext.Object);

			mockContext.Verify(c => c.AddErrorResult(primaryProperty, It.IsAny<string>()), Times.Never);
		}

		[Test]
		public void Should_NotAddError_IfUserIdNotAvailable()
		{
			var mockContext = GetMockContext("password", null);

			var mockCommandFactory = GetCommandFactory(true);

			var sut = new TestablePasswordMatchesCurrentRule(mockCommandFactory.Object, primaryProperty, userIdProperty, 1);

			sut.TestableExecute(mockContext.Object);

			mockContext.Verify(c => c.AddErrorResult(primaryProperty, It.IsAny<string>()), Times.Never);
		}

		[Test]
		public void Should_NotAddError_IfUserIdNotInt()
		{
			var mockContext = GetMockContext("password", "invalid");

			var mockCommandFactory = GetCommandFactory(true);

			var sut = new TestablePasswordMatchesCurrentRule(mockCommandFactory.Object, primaryProperty, userIdProperty, 1);

			sut.TestableExecute(mockContext.Object);

			mockContext.Verify(c => c.AddErrorResult(primaryProperty, It.IsAny<string>()), Times.Never);
		}

		[Test]
		public void Should_NotAddError_IfPasswordEmpty()
		{
			var mockContext = GetMockContext("", 1234);

			var mockCommandFactory = GetCommandFactory(true);

			var sut = new TestablePasswordMatchesCurrentRule(mockCommandFactory.Object, primaryProperty, userIdProperty, 1);

			sut.TestableExecute(mockContext.Object);

			mockContext.Verify(c => c.AddErrorResult(primaryProperty, It.IsAny<string>()), Times.Never);
		}

		[Test]
		public void Should_NotAddError_IfPasswordsDontMatch()
		{
			var mockContext = GetMockContext("password", 1234);

			var mockCommandFactory = GetCommandFactory(false);

			var sut = new TestablePasswordMatchesCurrentRule(mockCommandFactory.Object, primaryProperty, userIdProperty, 1);

			sut.TestableExecute(mockContext.Object);

			mockContext.Verify(c => c.AddErrorResult(primaryProperty, It.IsAny<string>()), Times.Never);
		}

		[Test]
		public void ShouldAddError_IfPasswordsMatch()
		{
			var mockContext = GetMockContext("password", 1234);

			var mockCommandFactory = GetCommandFactory(true);

			var sut = new TestablePasswordMatchesCurrentRule(mockCommandFactory.Object, primaryProperty, userIdProperty, 1);

			sut.TestableExecute(mockContext.Object);

			mockContext.Verify(c => c.AddErrorResult(primaryProperty, It.IsAny<string>()), Times.Once);
		}

		Mock<IRuleContext> GetMockContext(object password, object userId)
		{
			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(c => c.InputPropertyValues).Returns(new Dictionary<IPropertyInfo, object>
			{
				{ primaryProperty, password },
				{ userIdProperty, userId }
			});

			return mockContext;
		}

		static Mock<IPasswordValidationCommandFactory> GetCommandFactory(bool passwordsMatch)
		{
			var mockCommand = new Mock<IPasswordValidationCommand>();
			mockCommand.SetupGet(c => c.PasswordsMatch).Returns(passwordsMatch);

			var mockCommandFactory = new Mock<IPasswordValidationCommandFactory>();
			mockCommandFactory.Setup(cf => cf.Validate(It.IsAny<int>(), It.IsAny<string>())).Returns(mockCommand.Object);

			return mockCommandFactory;
		}
	}
}
