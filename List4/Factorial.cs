using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using List4.Exceptions.Factorial;

namespace List4;
internal class Factorial
{
    public static long Calculate(long count)
    {
        return count switch
        {
            < 0 => throw new NegativeValueException($"Factorial cannot be calculated, because input was lesser than 0, actual value: {count}"),
            <= 1 => 1,
            _ => count * Calculate(count - 1)
        };
    }
}
