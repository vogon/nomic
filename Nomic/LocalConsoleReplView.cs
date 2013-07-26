using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nomic
{
    internal sealed class LocalConsoleReplView : IReplView
    {
        internal LocalConsoleReplView() { }

        async Task<string> IReplView.Read()
        {
            Console.Write("nomic> ");
            Console.Out.Flush();

            string result = await Console.In.ReadLineAsync();

            return result;
        }

        async Task IReplView.Print(dynamic result)
        {
            await Console.Out.WriteLineAsync(result.ToString());
        }
    }
}
