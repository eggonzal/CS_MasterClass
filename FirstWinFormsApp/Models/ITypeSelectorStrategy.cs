using System.Numerics;

namespace FirstWinFormsApp.Models;

public interface ITypeSelectorStrategy
{
    TypeCapabilities? SelectBestType(
        IEnumerable<TypeCapabilities> candidates,
        BigInteger minValue,
        BigInteger maxValue,
        bool integralOnly,
        bool requiresPrecision);
}
