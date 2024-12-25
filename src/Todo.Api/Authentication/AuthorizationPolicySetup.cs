using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Todo.Api.Authentication;

public class AuthorizationPolicySetup: IConfigureNamedOptions<AuthorizationOptions>
{
    public void Configure(AuthorizationOptions options)
    {
        throw new NotImplementedException();
    }

    public void Configure(string? name, AuthorizationOptions options)
    {
        options.AddPolicy("AuthenticatedUser", policy =>
        {
            policy.RequireAuthenticatedUser();
        });

        options.AddPolicy("DeletePolicy", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireClaim("delete", "true");
        });

        options.AddPolicy("AdminUser", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireRole("admin");
        });
    }
}
