using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurgeonPortal.Models.MedicalTraining
{
    public class UserCertificateFileModel
    {
        public int CertificateTypeId { get; set; }
        public IFormFile File { get; set; }
    }
}
