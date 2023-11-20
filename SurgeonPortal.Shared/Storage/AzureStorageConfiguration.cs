using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurgeonPortal.Shared.Storage
{
    [Serializable]
    public class AzureStorageConfiguration
    {
        public string? ConnectionString { get; set; }
        public string? ContainerName { get; set; }
    }
}
