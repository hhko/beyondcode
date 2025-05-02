namespace GymManagement.Tests.Unit.Abstractions.Constants;

public static partial class Constants
{
    public static class NamingConvention
    {
        // CQRS
        public const string Command = nameof(Command);
        public const string Query = nameof(Query);
        public const string Options = nameof(Options);

        //public const string CommandUsecase = nameof(CommandUsecase);

        //public const string QueryUsecase = nameof(QueryUsecase);

        //public const string ValidatorSuffix = ".*(Command|Query|Options)Validator$";
        public const string ValidatorSuffix = ".*Validator$";


        public const string SectionName = nameof(SectionName);

        public const string Request = nameof(Request);
        public const string Response = nameof(Response);
        public const string Validator = nameof(Validator);
    }
}