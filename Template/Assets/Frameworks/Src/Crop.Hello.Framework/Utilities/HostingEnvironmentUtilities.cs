using Microsoft.Extensions.Hosting;

namespace Crop.Hello.Framework.Utilities;

public static class HostingEnvironmentUtilities
{
    public static bool IsDevelopmentOrDocker(this IHostEnvironment environment)
    {
        return environment.IsDevelopment() ||
               string.Equals(environment.EnvironmentName, "Docker", StringComparison.OrdinalIgnoreCase);
    }
}

