using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerical_Methods_Lab2
{
    internal class SeidelMethod
    {
        public static void Method(double epsilon, double[] initialElements)
        {
            int n = 4;

            Vector x_k = new Vector(initialElements);
            Vector x_k_next;
            Vector vectorSubtraction;
            Vector result = new Vector(new double[n]);

            double norm;
            int iterationCount = 0;
            bool isResult = false;

            for (int i = 1; i <= 100; i++)
            {
                x_k_next = Vector.NextIterationVector(x_k);
                vectorSubtraction = Vector.VectorSubtraction(x_k, x_k_next);
                norm = Vector.VectorNorm(vectorSubtraction);

                Console.WriteLine(" ----------------------------------------------------------");
                Console.WriteLine($"{i} iteration");
                Console.Write("x_k: ");
                Vector.PrintVector(x_k_next);
                Console.Write("x_k_prev: ");
                Vector.PrintVector(x_k);
                Console.WriteLine();

                Console.WriteLine("x_k_next - x_k");
                Vector.PrintVector(vectorSubtraction);
                Console.WriteLine();

                Console.WriteLine($"Vector norm: {norm}");
                Console.WriteLine(" ----------------------------------------------------------");

                x_k = x_k_next;

                if (norm <= epsilon && !isResult)
                {
                    iterationCount = i;
                    isResult = true;
                    result = x_k_next;
                    break;
                }
            }

            if (isResult)
            {
                Console.WriteLine($"The solution of the system with an epsilon of {epsilon} is:");
                Vector.PrintVector(result);
                Console.WriteLine($"Result is found during {iterationCount} iteration");
            }
            else
            {
                Console.WriteLine("Solution is not found within 100 iterations.");
            }
        }
    }
}
