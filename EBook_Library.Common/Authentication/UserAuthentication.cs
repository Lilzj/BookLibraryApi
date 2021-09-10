using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBook_Library.Common
{
    public static class UserAuthentication
    {
        public static UserAuth Authenticate(IHeaderDictionary header)
        {
            var userAuth = new UserAuth();
            var adminCheck = (string)header["Admin"];//.TryGetValue("Admin", out var headerValueAdmin);
            var userCheck = (string)header["Username"];

            if ((adminCheck == null && userCheck == null) || (adminCheck != "1" && userCheck != "user"))
                return userAuth;

            userAuth.IsAuthenticated = true;
            if (adminCheck == "1")
            {
                userAuth.IsAdmin = true;
            }

            return userAuth;
        }
    }
}
