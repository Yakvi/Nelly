using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nelly;

namespace Nelly.Tests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void Test_Answer()
        {
            Assert.AreEqual(42, Program.Answer());
        }
        
    }
}
