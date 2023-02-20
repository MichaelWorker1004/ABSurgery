using Csla.Configuration;
using NUnit.Framework;

namespace SurgeonPortal.Library.Tests
{
    [SetUpFixture]
    public class GlobalLifecycle
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            new CslaConfiguration()
                .DataPortal().AutoCloneOnUpdate(false);
        }
    }
}
