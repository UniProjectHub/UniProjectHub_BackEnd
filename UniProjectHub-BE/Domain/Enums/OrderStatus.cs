using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum OrderStatus
    {
        Pending = 1,
        Processing = 2,
        Success = 3,
        Canceled = 4,
    }
}
