using System.Numerics;

namespace FirstWinFormsApp.Models;

public class TypeSelector(ITypeSelectorStrategy? strategy = null, IReadOnlyList<TypeCapabilities>? types = null)
{
    private readonly ITypeSelectorStrategy _strategy = strategy ?? new SmallestMemoryFootprintStrategy();
    private readonly IReadOnlyList<TypeCapabilities> _types = types ?? TypeCapabilities.AllNumericTypes;

    public TypeCapabilities? GetBestType(BigInteger minValue, BigInteger maxValue, bool integralOnly = false, bool requiresPrecision = false)
        => _strategy.SelectBestType(_types, minValue, maxValue, integralOnly, requiresPrecision);
}
