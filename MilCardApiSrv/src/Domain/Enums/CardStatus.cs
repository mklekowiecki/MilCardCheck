using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilCardApiSrv.Domain.Enums;

public enum CardStatus
{
    Ordered = 0,
    Inactive = 1,
    Active = 2,
    Restricted = 3,
    Blocked = 4,
    Expired = 5,
    Closed = 6,
    Any = 7
}
