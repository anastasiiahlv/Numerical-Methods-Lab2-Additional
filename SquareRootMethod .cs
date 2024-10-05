using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerical_Methods_Lab2
{
    public class SquareRootMethod
    {
        public double[,] S { get; private set; }
        public double[,] D { get; private set; }
        public double[,] A { get; private set; }
        public double[,] Multiplications { get; private set; }

        // ініціалізація
        public SquareRootMethod(double[,] matrix)
        {
            A = matrix;
            int n = A.GetLength(0);
            S = new double[n, n];
            D = new double[n, n];
        }

        // Знаходимо обернену матрицю А^-1
        public void TransposedMatrix()
        {
            int n = S.GetLength(0);
            double[,] result = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = S[j, i];
                }
            }
            S = result;
        }

        // Для множення матриці S^T та D
        public void MatrixMultiplication()
        {
            double sum;
            int n = A.GetLength(0);
            Multiplications = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    sum = 0;
                    for (int k = 0; k < n; k++)
                    {
                        sum += S[i, k] * D[k, j];
                    }
                    Multiplications[i, j] = sum;
                }
            }
        }

        // Знаходимо елементи головної діагоналі для матриці D
        public void ValueOfMatrixD(int index)
        {
            double d = A[index, index];
            for (int j = 0; j < index; j++)
            {
                d -= D[j, j] * Math.Pow(S[j, index], 2);
            }

            D[index, index] = d > 0 ? 1 : d == 0 ? 0 : -1;
        }

        // Знаходимо вектор y 
        public double[] YResults(double[] b)
        {
            int n = b.Length;
            double[] result = new double[n];
            for (int i = 0; i < n; i++)
            {
                double sum = 0.0;
                for (int j = 0; j < i; j++)
                {
                    sum += S[i, j] * result[j];
                }
                result[i] = (b[i] - sum) / S[i, i];
            }
            return result;
        }

        // Знаходимо вектор х - результат системи
        public double[] XResults(double[] y)
        {
            double[,] _S = {
            { Math.Pow(2, 0.5) , 1 / Math.Pow(2, 0.5), -1 / Math.Pow(2, 0.5), 0 },
            { 0, 1.58, 0.32, 0.63 },
            { 0, 0, 1.18, 0.68 },
            { 0, 0, 0, 1.77 }
            };

            int n = y.Length;
            double[] result = new double[n];
            for (int i = n - 1; i >= 0; i--)
            {
                double sum = 0.0;
                for (int j = i + 1; j < n; j++)
                {
                    sum += _S[i, j] * result[j]; 
                }

                result[i] = (y[i] - sum) / _S[i, i];
            }
            return result;
        }

        // Декомпозиція матриці А на S*D*S^T
        public void TriangleMatrix()
        {
            int n = A.GetLength(0);
            double valueS;

            for (int i = 0; i < n; i++)
            {
                ValueOfMatrixD(i);
                for (int j = i; j < n; j++)
                {
                    valueS = A[i, j];
                    for (int k = 0; k < i; k++)
                    {
                        if (i == j)
                        {
                            valueS -= D[k, k] * S[k, i] * S[k, i];
                        }
                        else
                        {
                            valueS -= S[k, i] * S[k, j] * D[k, k];
                        }
                    }
                    if (i == j)
                    {
                        valueS = Math.Sqrt(Math.Abs(valueS));
                    }
                    else
                    {
                        valueS = valueS / (D[i, i] * S[i, i]);
                    }
                    S[i, j] = valueS;
                }
            }
        }

        // Рахуємо визначник матриці
        public double Determinant()
        {
            double result = 1;
            for (int i = 0; i < A.GetLength(0); i++)
            {
                result *= D[i, i] * Math.Pow(S[i, i], 2);
            }
            return result;
        }

        // Виводимо матрицю 
        public static void PrintMatrix(double[,] F)
        {
            int rows = F.GetLength(0);
            int cols = F.GetLength(1);
            int maxLength = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    int elementLength = F[i, j].ToString("0.###").Length;
                    if (elementLength > maxLength)
                    {
                        maxLength = elementLength;
                    }
                }
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(F[i, j].ToString("F3").PadLeft(maxLength + 2));
                }
                Console.WriteLine();
            }
        }

        // Виводимо матрицю з цілочисельними елементами
        public static void DisplayIntMatrix(double[,] F)
        {
            int rows = F.GetLength(0);
            int cols = F.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"{(int)F[i, j],4:D} "); 
                }
                Console.WriteLine();
            }
        }
    }
}
