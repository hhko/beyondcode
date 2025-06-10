using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

Console.WriteLine("Hello, World!");

var result = from _ in Divide_Pure(2025, 0)
             select _;

int Divide_Impure(int x, int y)
{
    if (y == 0)
        throw new ArgumentException();

    return x / y;
}

Fin<int> Divide_Pure(int x, int y)
{
    if (y == 0)
        return Error.New("분모가 0입니다");

    return x / y;
}