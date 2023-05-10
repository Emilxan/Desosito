using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desosito.Domain.Enum
{
    public enum StatusCode
    {
        CarNotFound = 0,
        UserNotFound = 1,
        OK = 200,
        InternalServerError = 500
    }
}
