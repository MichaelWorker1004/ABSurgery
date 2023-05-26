using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Storage
{
    public interface IStorageDal
    {
        Task SaveAsync(Stream fileStream, string name);
        Task<Stream> LoadAsync(string name);
    }
}
