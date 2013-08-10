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

    internal interface IEvaluationContext
    {
        void ExportGlobal(string name, object value);

        dynamic Eval(string code);
        ExceptionType ExceptionTypeForException(Exception e);
    }
}
