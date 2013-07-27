using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nomic.Test
{
    [TestClass]
    public class LocalConsoleReplViewTest
    {
        [TestInitialize]
        public void Initialize()
        {
            _view = new ConsoleReplView();
        }

        [TestMethod]
        public void TestNullPrint()
        {
            try
            {
                ((IReplView)_view).Print(null).Wait();
            }
            catch (Exception)
            {
                Assert.Fail("view threw exception when attempting to print null");
            }
        }

        ConsoleReplView _view;
    }
}
