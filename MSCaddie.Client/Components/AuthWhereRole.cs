//using MSCaddie.Shared.Dtos;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using System.Net.Http;

//namespace MSCaddie.Client.Components
//{
//    public class AuthWhereRole : AuthorizeAttribute
//    {
//        /// <summary>
//        /// Add the allowed roles to this property.
//        /// </summary>
//        public PlayerRole Is;

//        /// <summary>
//        /// Checks to see if the user is authenticated and has the
//        /// correct role to access a particular view.
//        /// </summary>
//        /// <param name="httpContext"></param>
//        /// <returns></returns>
//        protected override bool AuthorizeCore(HttpContext httpContext)
//        {
//            if (httpContext == null)
//                throw new ArgumentNullException("httpContext");

//            // Make sure the user is authenticated.
//            if (!httpContext.User.Identity.IsAuthenticated)
//                return false;

//            PlayerRole role = httpContext.User.Identity.Role; // Load the user's role here

//            // Perform a bitwise operation to see if the user's role
//            // is in the passed in role values.
//            if (Is != 0 && ((Is & role) != role))
//                return false;

//            return true;
//        }
//    }
//}
