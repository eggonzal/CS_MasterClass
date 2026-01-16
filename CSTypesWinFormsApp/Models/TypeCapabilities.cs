using System.Numerics;

namespace FirstWinFormsApp.Models;

public record TypeCapabilities(
    BigInteger? MaxValue,
    BigInteger? MinValue,
    bool IsSigned,
    bool IsIntegral,
    bool HasPrecision,
    byte? SizeInBytes,
    Type Type)
{
    /// <summary>
    /// Gets the C# keyword name for primitive types, or the type name for others.
    /// </summary>
    public string Name => Type switch
    {
        _ when Type == typeof(sbyte) => "sbyte",
        _ when Type == typeof(byte) => "byte",
        _ when Type == typeof(short) => "short",
        _ when Type == typeof(ushort) => "ushort",
        _ when Type == typeof(int) => "int",
        _ when Type == typeof(uint) => "uint",
        _ when Type == typeof(long) => "long",
        _ when Type == typeof(ulong) => "ulong",
        _ when Type == typeof(float) => "float",
        _ when Type == typeof(double) => "double",
        _ when Type == typeof(decimal) => "decimal",
        _ => Type.Name
    };

    public bool CanFit(BigInteger minValue, BigInteger maxValue) =>
        (MinValue is null || minValue >= MinValue) && (MaxValue is null || maxValue <= MaxValue);

    public static IReadOnlyList<TypeCapabilities> AllNumericTypes { get; } =
    [
        new((BigInteger)sbyte.MaxValue, (BigInteger)sbyte.MinValue, IsSigned: true, IsIntegral: true, HasPrecision: false, SizeInBytes: 1, Type: typeof(sbyte)),
        new((BigInteger)byte.MaxValue, (BigInteger)byte.MinValue, IsSigned: false, IsIntegral: true, HasPrecision: false, SizeInBytes: 1, Type: typeof(byte)),
        new((BigInteger)short.MaxValue, (BigInteger)short.MinValue, IsSigned: true, IsIntegral: true, HasPrecision: false, SizeInBytes: 2, Type: typeof(short)),
        new((BigInteger)ushort.MaxValue, (BigInteger)ushort.MinValue, IsSigned: false, IsIntegral: true, HasPrecision: false, SizeInBytes: 2, Type: typeof(ushort)),
        new((BigInteger)int.MaxValue, (BigInteger)int.MinValue, IsSigned: true, IsIntegral: true, HasPrecision: false, SizeInBytes: 4, Type: typeof(int)),
        new((BigInteger)uint.MaxValue, (BigInteger)uint.MinValue, IsSigned: false, IsIntegral: true, HasPrecision: false, SizeInBytes: 4, Type: typeof(uint)),
        new((BigInteger)long.MaxValue, (BigInteger)long.MinValue, IsSigned: true, IsIntegral: true, HasPrecision: false, SizeInBytes: 8, Type: typeof(long)),
        new((BigInteger)ulong.MaxValue, (BigInteger)ulong.MinValue, IsSigned: false, IsIntegral: true, HasPrecision: false, SizeInBytes: 8, Type: typeof(ulong)),
        new((BigInteger)float.MaxValue, (BigInteger)float.MinValue, IsSigned: true, IsIntegral: false, HasPrecision: false, SizeInBytes: 4, Type: typeof(float)),
        new((BigInteger)double.MaxValue, (BigInteger)double.MinValue, IsSigned: true, IsIntegral: false, HasPrecision: false, SizeInBytes: 8, Type: typeof(double)),
        new((BigInteger)decimal.MaxValue, (BigInteger)decimal.MinValue, IsSigned: true, IsIntegral: false, HasPrecision: true, SizeInBytes: 16, Type: typeof(decimal)),
        new(null, null, IsSigned: true, IsIntegral: true, HasPrecision: false, SizeInBytes: null, Type: typeof(BigInteger)),
    ];
}
