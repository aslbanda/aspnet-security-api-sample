using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace MicrosoftGraph_Security_API_Sample.Models.Responses
{
    public class WindowsUpdateStatesResponse
    {
        public double PendingReboot { get; set; }

        public double UpToDate { get; set; }

        public double PendingInstallation { get; set; }

        public double Failed { get; set; }
    }
}
