using Microsoft.Scripting.Hosting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nomic
{
    internal sealed class Repl
    {
        internal Repl(IReplView view, ScriptEngine engine)
        {
            this._view = view;
            this._engine = engine;
            this._scope = _engine.CreateScope();
        }

        internal async Task Main()
        {
            while (!_exitRequested)
            {
                // read
                string next = await _view.Read();

                // evaluate
                ScriptSource src = _engine.CreateScriptSourceFromString(next);
                dynamic result = src.Execute(_scope);

                // print
                await _view.Print(result);
            }
        }

        private bool _exitRequested;
        private IReplView _view;

        private ScriptEngine _engine;
        private ScriptScope _scope;
    }
}
