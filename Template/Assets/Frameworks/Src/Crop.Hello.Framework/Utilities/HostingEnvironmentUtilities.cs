using Microsoft.Extensions.Hosting;

namespace Crop.Hello.Framework.Utilities;

public static class HostingEnvironmentUtilities
{
    public static bool IsDevelopmentOrLocal(this IHostEnvironment environment)
    {
        return environment.IsDevelopment() ||
               environment.EnvironmentName.StartsWith("Local", StringComparison.OrdinalIgnoreCase);
    }
}

