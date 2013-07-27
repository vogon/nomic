using IronPython.Runtime;
using IronPython.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Scripting.Hosting;

namespace Nomic
{
    internal abstract class DlrReplLanguage
    {
        public DlrReplLanguage(ScriptEngine engine)
        {
            this._engine = engine;
            this._replScope = this._engine.CreateScope();
        }

        protected dynamic Eval(string code)
        {
            ScriptSource src = this._engine.CreateScriptSourceFromString(code);
            return src.Execute(this._replScope);
        }

        private ScriptEngine _engine;
        private ScriptScope _replScope;
    }

    internal sealed class IronPythonReplLanguage : DlrReplLanguage, IReplLanguage
    {
        public IronPythonReplLanguage() : base(Python.CreateEngine()) { }

        dynamic IReplLanguage.Eval(string code)
        {
            return base.Eval(code);
        }

        ExceptionType IReplLanguage.ExceptionTypeForException(Exception e)
        {
            if (e is IronPython.Runtime.Exceptions.SystemExitException)
            {
                return ExceptionType.ExitRequested;
            }
            else if (e is IronPython.Runtime.UnboundNameException)
            {
                return ExceptionType.UserError;
            }
            else
            {
                return ExceptionType.InternalError;
            }
        }
    }
}
