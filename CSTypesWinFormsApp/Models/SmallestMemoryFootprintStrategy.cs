using System.Numerics;

namespace FirstWinFormsApp.Models;

public class SmallestMemoryFootprintStrategy : ITypeSelectorStrategy
{
    public TypeCapabilities? SelectBestType(
        IEnumerable<TypeCapabilities> candidates,
        BigInteger minValue,
        BigInteger maxValue,
        bool integralOnly,
        bool requiresPrecision)
    {
        var requiresSigned = minValue < 0;

        return candidates
            .Where(t => t.CanFit(minValue, maxValue))
            .Where(t => integralOnly ? t.IsIntegral : !t.IsIntegral)
            .Where(t => !requiresPrecision || t.HasPrecision)
            .Where(t => !requiresSigned || t.IsSigned)
            .OrderBy(t => t.SizeInBytes ?? byte.MaxValue)
            .ThenBy(t => t.IsSigned == requiresSigned ? 0 : 1)
            .FirstOrDefault();
    }
}
