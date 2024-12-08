using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crop.Hello.Api;

public class Class1
{
    private ILogger<Class1> _logger;

    public Class1(ILogger<Class1> logger)
    {
        _logger = logger;

        _logger.LogInformation("{Msg} is", "Class1");
    }
}
