using DiffEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalDdd.SourceGenerator.Tests.Unit.Abstractions;

static class VerifyInitializer
{
    // https://github.com/VerifyTests/Verify/blob/main/docs/naming.md#derivepathinfo
    [ModuleInitializer]
    public static void Init()
    {
        // .verified 파일이 저장될 전역 폴더 지정
        //
        // {ProjectDirectory}\Snapshots\{클래스}.{메서드}.verified.txt
        DerivePathInfo((sourceFile, projectDirectory, type, method) => new(
            directory: Path.Combine(projectDirectory, "Snapshots"),
            typeName: type.Name,
            methodName: method.Name));

        //if (Environment.GetEnvironmentVariable("CI") == "true")
        //{
        //    DiffRunner.Disabled = true;
        //}
    }
}
