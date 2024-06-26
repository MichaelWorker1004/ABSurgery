﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Identity
{
    /// <summary>
    /// Backing GUID for role claims used in BO authorization rules
    /// </summary>
    public class SurgeonPortalClaims
    {
        public const string UserClaim = "2AA5D2F2-0AA3-4B64-B340-26900EDF7CC2";
        public const string SurgeonClaim = "1BDACD5A-2F92-4FC3-81D6-9B292E27702C";
        public const string TraineeClaim = "8E508896-0442-443E-BFF5-29EDD11C7463";
        public const string ExaminerClaim = "22A5DC1E-9C24-48FE-86CE-741C25A7E21D";
	}
}
