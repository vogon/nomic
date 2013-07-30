using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Dynamic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nomic;
using System.Diagnostics;

namespace Nomic.Test
{
    [TestClass]
    public class ReplTest
    {
        private class View : IReplView
        {
            public string NextRead { get; set; }
            public Action<dynamic> NextPrintExpect { get; set; }

            System.Threading.Tasks.Task<string> IReplView.Read()
            {
                Assert.IsNotNull(this.NextRead);
                Task<string> result = Task.FromResult(this.NextRead);

                this.NextRead = null;
                return result;
            }

            async System.Threading.Tasks.Task IReplView.Print(dynamic result)
            {
                Assert.IsNotNull(this.NextPrintExpect);
                this.NextPrintExpect(result);

                this.NextPrintExpect = null;
                await Task.Yield();
            }
        }

        [TestInitialize]
        public void Initialize()
        {
            this._view = new View();

            IronPythonReplLanguage lang = new IronPythonReplLanguage();

            this._repl = new Repl(this._view, lang);
        }

        [TestMethod]
        public void TestExecutionException()
        {
            this._view.NextRead = "x";
            this._view.NextPrintExpect = (o) => { Assert.IsTrue(o is Exception); };
            this._repl.RepOnce().Wait();
        }

        [TestMethod]
        public void SmokeTestConstant()
        {
            this._view.NextRead = "3";
            this._view.NextPrintExpect = (o) => { Assert.AreEqual(o, 3); };
            this._repl.RepOnce().Wait();
        }

        Repl _repl;
        View _view;
    }
}
