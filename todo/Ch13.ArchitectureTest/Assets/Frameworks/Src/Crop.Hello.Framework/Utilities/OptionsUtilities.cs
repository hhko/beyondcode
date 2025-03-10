﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Crop.Hello.Framework.Utilities;

public static class OptionsUtilities
{
    public static TOptions GetOptions<TOptions>(this IServiceCollection services)
        where TOptions : class, new()
    {
        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider.GetRequiredService<IOptions<TOptions>>().Value;
    }
}
