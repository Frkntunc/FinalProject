using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)//ClaimsPrincipal: O an jwt ile gelen kişinin claimlerine ulaşmak için kullanılan bir sınıf 
        {
            var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList(); //?: null olabilir demek
            return result;
        }

        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.Claims(ClaimTypes.Role);
        }
    }
}
