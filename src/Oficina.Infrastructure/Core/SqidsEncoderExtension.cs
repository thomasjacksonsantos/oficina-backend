
using System.Numerics;
using Sqids;

namespace Oficina.Infrastructure.Core;

public static class SqidsExtensions
{
    public static int DecodeWithSqids(this string valor)
        => DecodeWithSqids<int>(valor);
    
    public static T DecodeWithSqids<T>(this string valor) 
        where T : unmanaged, IBinaryInteger<T>, IMinMaxValue<T>
    {
        var sqIds = new SqidsEncoder<T>();
        var decoded = sqIds.Decode(valor);

        if (decoded == null || !decoded.Any())
            throw new InvalidOperationException("The DecodeWithSqids value is null or empty");

        return decoded.Single();
    }

    public static string EncodeWithSqids<T>(this T valor)
        where T : unmanaged, IBinaryInteger<T>, IMinMaxValue<T>
        => new SqidsEncoder<T>().Encode(valor);
}