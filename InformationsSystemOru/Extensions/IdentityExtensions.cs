using Data_Access_Layer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace InformationsSystemOru.Extensions
{
    public static class IdentityExtensions
    {
        private static AccessRepository _accessRep = new AccessRepository();
        private static AccountRepository _accountRep = new AccountRepository();
        private static UserRepository _userRep = new UserRepository();

        public static bool IsAdmin (this IIdentity identity)
        {
            var user = _userRep.GetUserFromId(_accountRep.GetIdFromUsername(identity.Name));
            return _accessRep.IsInformaticsAdmin(user) || _accessRep.IsResearchAdmin(user);
        }
    }
}