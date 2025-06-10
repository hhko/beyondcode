using GymDdd.Framework.BaseTypes.Errors;
using LanguageExt.Common;

namespace GymManagement.Adapters.Infrastructure.Authentication.PasswordHasher.Errors;

internal static partial class Infrastructure
{
    internal static partial class PasswordHasherErrors
    {
        public static Error PasswordWeak(string password) =>
            ErrorCodeFactory.Create(
                $"{nameof(Infrastructure)}.{nameof(PasswordHasherErrors)}.{nameof(PasswordWeak)}",
                $"Passworkd '{password}' too weak");
    }
}
