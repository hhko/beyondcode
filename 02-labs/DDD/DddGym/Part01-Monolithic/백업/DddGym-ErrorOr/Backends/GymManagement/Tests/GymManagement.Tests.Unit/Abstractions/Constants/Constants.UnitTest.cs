﻿namespace GymManagement.Tests.Unit.Abstractions.Constants;

public static partial class Constants
{
    public static class UnitTest
    {
        public const string Architecture = nameof(Architecture);

        // 레이어
        public const string Domain = nameof(Domain);
        public const string Application = nameof(Application);
        public const string Infrastructure = nameof(Infrastructure);
        //public const string Persistence = nameof(Persistence);
        //public const string Presentation = nameof(Presentation);
    }
}