using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nomic
{
    internal sealed class ConsoleReplView : IReplView
    {
        internal ConsoleReplView() : this(Console.In, Console.Out) { } 

        internal ConsoleReplView(TextReader input, TextWriter output)
        {
            this._input = input;
            this._output = output;
        }

        async Task<string> IReplView.Read()
        {
            await this._output.WriteAsync("nomic> ");
            await this._output.FlushAsync();

            string result = await this._input.ReadLineAsync();

            return result;
        }

        async Task IReplView.Print(dynamic result)
        {
            if (result == null)
            {
                await this._output.WriteLineAsync();
            }
            else
            {
                await this._output.WriteLineAsync(result.ToString());
            }
        }

        private TextReader _input;
        private TextWriter _output;
    }
}
