namespace GymManagement.Tests.Unit.Abstractions.Constants;

public static partial class Constants
{
    public static class NamingConvention
    {
        // CQRS
        public const string Command = nameof(Command);
        public const string CommandUsecase = nameof(CommandUsecase);
        public const string Query = nameof(Query);
        public const string QueryUsecase = nameof(QueryUsecase);

        public const string ValidatorSuffix = ".*(Command|Query|Options)Validator$";
    }
}