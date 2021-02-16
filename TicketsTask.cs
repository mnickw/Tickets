using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Tickets
{
    public class TicketsTask
    {
        public static BigInteger Solve(int halfLen, int totalSum)
        {
            if (totalSum % 2 == 1)
                return 0;
            int halfSum = totalSum / 2;
            var optimizationTable = FillOptimizationTable(halfLen, halfSum);
            return optimizationTable[halfLen - 1, halfSum] * optimizationTable[halfLen - 1, halfSum];
        }
        public static BigInteger[,] FillOptimizationTable(int halfLen, int halfSum)
        {
            BigInteger[,] optimizationTable = new BigInteger[halfLen, halfSum + 1];

            for (int i = 0; i < halfLen; i++) optimizationTable[i, 0] = 1;
            for (int j = 0; j <= halfSum && j < 10; j++) optimizationTable[0, j] = 1;

            for (int i = 1; i < halfLen; i++)
                for (int j = 1; j <= halfSum; j++)
                {
                    if (j < 10)
                    {
                        optimizationTable[i, j] = optimizationTable[i, j - 1] + optimizationTable[i - 1, j];
                        continue;
                    }
                    optimizationTable[i, j] = optimizationTable[i, j - 1] - optimizationTable[i - 1, j - 10] + optimizationTable[i - 1, j];
                    if (optimizationTable[i, j] > optimizationTable[i, j - 1])
                        continue;
                    if (optimizationTable[i, j] < optimizationTable[i, j - 1])
                    {
                        for (int k = 1; k <= j - 2 && j + k <= halfSum; k++)
                            optimizationTable[i, j + k] = optimizationTable[i, j - 2 - k];
                        break;
                    }
                    if (optimizationTable[i, j] == optimizationTable[i, j - 1])
                    {
                        for (int k = 1; k <= j - 1 && j + k <= halfSum; k++)
                            optimizationTable[i, j + k] = optimizationTable[i, j - 1 - k];
                        break;
                    }
                }
            return optimizationTable;
        }
    }
}
