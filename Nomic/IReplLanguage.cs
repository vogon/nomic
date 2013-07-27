using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nomic
{
    enum ExceptionType
    {
        UserError,
        InternalError,
        ExitRequested
    };

    internal interface IReplLanguage
    {
        dynamic Eval(string code);
        ExceptionType ExceptionTypeForException(Exception e);
    }
}
