using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurgeonPortal.Models.Users
{
    public class TokenResponseModel
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string token_type { get; set; }
        public string user_name { get; set; }
        public DateTime expiration { get; set; }
        public int expires_in_minutes { get; set; }

        public AppUserReadOnlyModel user { get; set; }
    }
}
