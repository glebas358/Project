using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain.Enums
{
    public enum PackageStatus
    {
        Expected = 0,
        AwaitingPickup = 1,
        Issued = 2,
        Returned = 3,
        Expired = 4
    }
}
