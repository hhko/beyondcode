using LanguageExt;
using LanguageExt.Common;

namespace DddGym.Framework.Tests.Unit.BaseTypes;

public class ValueObjectTests
{
    [Fact]
    public void FistNameTest()
    {
        // https://paullouth.com/higher-kinds-in-c-with-language-ext-part-5-validation/
        Validation<Error, int> x1 = 1;
        Validation<Error, int> x2 = Error.New("xxx");
        Validation<Error, int> x3 = new ManyErrors([Error.New("1"), Error.New("2")]);

        Fin<int> y1 = 1;
        Fin<int> y2 = Error.New("xxx");
        Fin<int> y3 = new ManyErrors([Error.New("1"), Error.New("2")]);
    }
}
