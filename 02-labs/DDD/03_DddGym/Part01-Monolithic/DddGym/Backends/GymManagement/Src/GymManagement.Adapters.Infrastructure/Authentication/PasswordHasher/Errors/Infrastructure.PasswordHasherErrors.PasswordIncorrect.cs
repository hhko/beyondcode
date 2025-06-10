using GymDdd.Framework.BaseTypes.Errors;
using LanguageExt.Common;

namespace GymManagement.Adapters.Infrastructure.Authentication.PasswordHasher.Errors;

internal static partial class Infrastructure
{
    internal static partial class PasswordHasherErrors
    {
        public static Error PasswordIncorrect() =>
            ErrorCodeFactory.Create(
                $"{nameof(Infrastructure)}.{nameof(PasswordHasherErrors)}.{nameof(PasswordIncorrect)}",
                $"Passworkd is incorrect");
    }
}
