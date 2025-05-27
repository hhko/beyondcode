using ArchUnitNET.Domain;

namespace GymDdd.Tests.Architecture.Abstractions.ArchitectureRules.Musts;

public static partial class Must
{
    public static bool IsNestedPublicSealed(Type t) =>
        t.IsNestedPublic
        && t.IsSealed;

    public static bool IsNestedInternalSealed(Type t) =>
        t.IsNestedAssembly
        && t.IsSealed;

    public static bool IsImplementsInterface(Type type, Type interfaceType)
    {
        // i.GenericTypeArguments[0]   -> 인터페이스<T> : T값 얻기
        return type.GetInterfaces()
                   .Any(i =>
                        (i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType)
                        || i == interfaceType);
    }

    public static bool IsPrivateConstructor(MethodMember ctor) =>
        ctor.Visibility == Visibility.Private;

    public static bool IsPrivateParameterlessConstructor(MethodMember ctor) =>
        ctor.Visibility == Visibility.Private
        && !ctor.Parameters.Any();

    public static bool IsPublicStaticMethod(MethodMember method) =>
        method.Visibility == Visibility.Public
        && method.IsStatic == true;

    public static bool IsPublicStaticField(FieldMember field, Type type)
    {
        return field.Type.FullName == type.FullName
               && field.Visibility == Visibility.Public
               && field.IsStatic == true;
    }
}
