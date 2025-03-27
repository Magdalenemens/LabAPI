using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Common
{
    public enum ResultsStatusEnum
    {
        RS = 1, // Resulted
        VR = 2, // Verified
        VD = 3, // Validated
        RD = 4, // Released
        AD = 5, // Amend
        QD = 6, // Queued for Validation ( Future Function)
        AV = 7  // Auto-Verified ( Future Function)
    }
}
