using LanguageExt;
using LanguageExt.Common;
using static FunctionalDdd.Framework.Tests.Unit.Abstractions.Constants.Constants;

namespace FunctionalDdd.Framework.Tests.Unit.BaseTypes;

[Trait(nameof(UnitTest), UnitTest.Framework)]
public class ValueObjectTests
{
    [Fact]
    public void FistNameTest()
    {
        // Functional approach to try-catch-finally
        // https://github.com/louthy/language-ext/issues/1108

        // Higher Kinds in C# with language-ext [Part 5 - validation]
        // https://paullouth.com/higher-kinds-in-c-with-language-ext-part-5-validation/
        Validation<Error, int> x1 = 1;
        Validation<Error, int> x2 = Error.New("xxx");
        Validation<Error, int> x3 = new ManyErrors([Error.New("1"), Error.New("2")]);

        Fin<int> y1 = 1;
        Fin<int> y2 = Error.New("xxx");
        Fin<int> y3 = new ManyErrors([Error.New("1"), Error.New("2")]);
    }
}
