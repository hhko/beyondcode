using Ardalis.SmartEnum;

namespace DddGym.Domain.Subscriptions.Enumerations;

public sealed class Grade : SmartEnum<Grade>
{
    public static readonly Grade Free = new(nameof(Free), 0);
    public static readonly Grade Starter = new(nameof(Starter), 0);
    public static readonly Grade Pro = new(nameof(Pro), 0);

    public Grade(string name, int value) : base(name, value)
    {
    }
}
