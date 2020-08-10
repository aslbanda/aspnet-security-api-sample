using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetrieveDeviceScoreApp.Models
{
    public class DeviceScoreResponse : TableEntity
    {
        public DeviceScoreResponse()
        {
            PartitionKey = Guid.NewGuid().ToString();
            RowKey = Guid.NewGuid().ToString();
            CreatedDateTime = DateTime.Now;
        }

        public double DeviceScore { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}
