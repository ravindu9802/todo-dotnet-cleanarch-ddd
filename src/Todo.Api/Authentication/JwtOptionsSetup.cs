using Microsoft.Extensions.Options;
using Todo.Infrastructure.Authentication;

namespace Todo.Api.Authentication;

public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
    private readonly IConfiguration _configuration;
    private const string JwtOptionsSection = "JwtConfig";

    public JwtOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(JwtOptionsSection).Bind(options);
    }
}
