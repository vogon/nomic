using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nomic
{
    internal sealed class Repl
    {
        internal Repl(IReplView view, IEvaluationContext language)
        {
            this._view = view;
            this._language = language;
        }

        internal void RequestExit()
        {
            this._exitRequested = true;
        }

        internal void LoadScripts(IEnumerable<Script> scripts)
        {
            foreach (Script s in scripts)
            {
                this._language.Eval(s.Code);
            }
        }

        internal async Task Main()
        {
            this._language.ExportGlobal("server", new ServerProxy(this));

            while (!_exitRequested)
            {
                await RepOnce();
            }
        }

        internal async Task RepOnce()
        {
            // read
            string next = await _view.Read();

            // evaluate
            dynamic result = null;

            try
            {
                result = this._language.Eval(next);
            }
            catch (Exception e)
            {
                switch (this._language.ExceptionTypeForException(e))
                {
                case ExceptionType.ExitRequested:
                    this._exitRequested = true;
                    break;
                case ExceptionType.InternalError:
                    throw e;
                case ExceptionType.UserError:
                    result = e;
                    break;
                }
            }

            // print
            await _view.Print(result);
        }

        private bool _exitRequested;
        private IReplView _view;
        private IEvaluationContext _language;
    }
}
