using DddGym.Framework.BaseTypes.V2;
using LanguageExt;
using LanguageExt.Common;

using static LanguageExt.Prelude;

namespace DddGym.Framework.Tests.Unit.BaseTypes;

public class ValueObjectTests
{
    [Fact]
    public void FistNameTest()
    {
        // https://paullouth.com/higher-kinds-in-c-with-language-ext-part-5-validation/
        Validation<Error, int> x = 1;
        Validation<Error, int> y = Error.New("xxx");
        Validation<Error, int> z = new ManyErrors([Error.New("1"), Error.New("2")]);

        //ManyErrors s = [Fail(Error.New("1")), Fail(Error.New("2"))];
        Validation<Error, FirstName> x1 = FirstName.Create("113");
        Validation<Error, FirstName> x2 = FirstName.Create("xxxxx");
    }
}
