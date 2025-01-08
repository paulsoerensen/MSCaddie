using MSCaddie.Shared.Models;
using Microsoft.AspNetCore.Authorization;

namespace MSCaddie.Client.Components;

public class AuthorizeRolesAttribute : AuthorizeAttribute
{
    public AuthorizeRolesAttribute(params string[] roles) : base()
    {
        Roles = string.Join(",", roles);
    }
}
