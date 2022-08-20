using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Business.Utilities.ContextAccessor
{
    internal interface IContextAccessor
    {
        string Host { get; }
        string Scheme { get; }
        Guid UserId { get; }
    }
}
