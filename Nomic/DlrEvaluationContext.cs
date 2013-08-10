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
    internal abstract class DlrEvaluationContext
    {
        public DlrEvaluationContext(ScriptEngine engine)
        {
            this._engine = engine;
            this._scope = this._engine.CreateScope();
        }

        protected void ExportGlobal(string name, object value)
        {
            this._scope.SetVariable(name, value);
        }

        protected dynamic Eval(string code)
        {
            ScriptSource src = this._engine.CreateScriptSourceFromString(code);
            return src.Execute(this._scope);
        }

        private ScriptEngine _engine;
        private ScriptScope _scope;
    }

    internal sealed class IronPythonEvaluationContext : DlrEvaluationContext, IEvaluationContext
    {
        public IronPythonEvaluationContext() : base(Python.CreateEngine()) { }

        void IEvaluationContext.ExportGlobal(string name, object value)
        {
            base.ExportGlobal(name, value);
        }

        dynamic IEvaluationContext.Eval(string code)
        {
            return base.Eval(code);
        }

        ExceptionType IEvaluationContext.ExceptionTypeForException(Exception e)
        {
            if (e is IronPython.Runtime.Exceptions.SystemExitException)
            {
                return ExceptionType.ExitRequested;
            }
            else if (e is IronPython.Runtime.UnboundNameException ||
                     e is IronPython.Runtime.Exceptions.ImportException ||
                     e is System.MissingMemberException)
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
