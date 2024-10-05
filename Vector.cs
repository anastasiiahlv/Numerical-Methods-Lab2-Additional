using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerical_Methods_Lab2
{
    internal class Vector
    {
        double[] elements;

        public Vector(double[] elements)
        {
            this.elements = elements;
        }

        // Ітераційний процес для методу Зейделя
        public static Vector NextIterationVector(Vector x_k)
        {
            double[] newElements = new double[4];
            newElements[0] = 0.5 * (-1 * x_k.elements[1] + x_k.elements[2] + 1);
            newElements[1] = 0.33 * (-1 * newElements[0] - x_k.elements[3] - 3);
            newElements[2] = 0.5 * (newElements[0] - x_k.elements[3] - 2);
            newElements[3] = 0.25 * (-1 * newElements[1] - newElements[2] - 5);

            return new Vector(newElements);
        }

        // Віднімання векторів
        public static Vector VectorSubtraction(Vector x_k, Vector x_k_next)
        {
            double[] result = new double[4];
            for (int i = 0; i < 4; i++)
            {
                result[i] = x_k_next.elements[i] - x_k.elements[i];
            }
            return new Vector(result);
        }

        // Шукаємо норму вектора - максимальне  по модулю значення 
        public static double VectorNorm(Vector x)
        {
            double max = Math.Abs(x.elements[0]);
            for (int i = 1; i < 4; i++)
            {
                if (Math.Abs(x.elements[i]) > max)
                {
                    max = Math.Abs(x.elements[i]);
                }
            }
            return max;
        }

        // Виводимо вектор
        public static void PrintVector(Vector x)
        {
            Console.WriteLine("[" + string.Join(", ", x.elements) + "]");
        }

        // Знаходимо обернену матрицю
        public static double[,] InvertMatrix(double[,] matrix)
        {
            int n = matrix.GetLength(0);
            double[,] extendedMatrix = new double[n, 2 * n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    extendedMatrix[i, j] = matrix[i, j];
                }
                extendedMatrix[i, i + n] = 1;
            }

            for (int i = 0; i < n; i++)
            {
                // Знаходимо провідний елемент
                double conductiveElement = extendedMatrix[i, i];
                for (int j = 0; j < 2 * n; j++)
                {
                    extendedMatrix[i, j] /= conductiveElement;
                }

                // Зануляємо елементи стовпця
                for (int j = 0; j < n; j++)
                {
                    if (j != i)
                    {
                        double factor = extendedMatrix[j, i];
                        for (int k = 0; k < 2 * n; k++)
                        {
                            extendedMatrix[j, k] -= factor * extendedMatrix[i, k];
                        }
                    }
                }
            }

            // Виділення оберненої матриці
            double[,] inverseMatrix = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    inverseMatrix[i, j] = extendedMatrix[i, j + n];
                }
            }

            return inverseMatrix;
        }

        // Рахуємо число обумовленості
        public static double CalculateConditionNumber(double a, double b)
        {
            return a * b;
        }

        // Знаходимо норму матриці - максимальне значення черед сум елементів стовпців по модулю
        public static double CalculateMatrixNorm(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            double maxColumnSum = 0;

            for (int j = 0; j < cols; j++)
            {
                double columnSum = 0;

                for (int i = 0; i < rows; i++)
                {
                    columnSum += Math.Abs(matrix[i, j]);
                }

                maxColumnSum = Math.Max(maxColumnSum, columnSum);
            }

            return maxColumnSum;
        }
    }
}
