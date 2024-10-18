using Numerical_Methods_Lab2;
using System;

public class Program
{
    // вивід результату методу квадратного кореня
    public static void SquareRootMethodOutput(double[,] A, double[] b)
    {
        var method = new SquareRootMethod(A);

        method.TriangleMatrix();

        Console.WriteLine("Matrix S:");
        SquareRootMethod.PrintMatrix(method.S);

        Console.WriteLine("\nMatrix D:");
        SquareRootMethod.DisplayIntMatrix(method.D);

        method.TransposedMatrix();

        Console.WriteLine("\nTransposed matrix S:");
        SquareRootMethod.PrintMatrix(method.S);

        method.MatrixMultiplication();

        Console.WriteLine("\nTransposed matrix S * matrix D:");
        SquareRootMethod.PrintMatrix(method.Multiplications);

        Console.WriteLine("\ny values:");
        double[] y = method.YResults(b);
        for (int i = 0; i < y.Length; i++)
        {
            Console.WriteLine($"y[{i + 1}] = {y[i]}");
        }

        Console.WriteLine();
        Console.WriteLine("\nResult:\n");

        double[] x = method.XResults(y);
        for (int i = 0; i < x.Length; i++)
        {
            Console.WriteLine($"x[{i + 1}] = {x[i]}");
        }
        Console.WriteLine();
    }

    // вивід матриці А, А^-1, визначника матриці А та числа обумовленості
    public static void ResultOutput(int n, double[,] matrix)
    {
        Console.WriteLine("Matrix A: ");
        SquareRootMethod.DisplayIntMatrix(matrix);

        Console.WriteLine();

        var method = new SquareRootMethod(matrix);
        method.TriangleMatrix();
        double det = method.Determinant();
        Console.WriteLine("Det A: " + det);

        Console.WriteLine();

        Console.WriteLine("Inverse matrix A^(-1): ");
        double[,] invertMatrix = SquareRootMethod.InverseMatrix();
        SquareRootMethod.PrintMatrix(invertMatrix);
        Console.WriteLine();

        // Виклик функції обчислення числа обумовленості
        double conditionNumber = Vector.CalculateConditionNumber(
            Vector.CalculateMatrixNorm(matrix),
            Vector.CalculateMatrixNorm(invertMatrix)
        );

        Console.WriteLine("Condition number: " + conditionNumber);
        Console.WriteLine();
    }

    static void Main(string[] args)
    {
        double[,] A = {
            { 2 , 1, -1, 0 },
            { 1, 3, 0, 1 },
            { -1, 0, 2, 1 },
            { 0, 1, 1, 4 }
        };

        double[] b = { 1, -3, -2, -5 };

        int n = -1;

        while(n != 0)
        {
            Console.WriteLine("Choose the method. Enter 1 or 2.");
            Console.WriteLine("1 - Square Root Method");
            Console.WriteLine("2 - Seidel Method");
            Console.Write("Enter: ");

            if (int.TryParse(Console.ReadLine(), out int selectedMethod))
            {
                switch (selectedMethod)
                {
                    case 1:
                        Console.WriteLine(" ----------------------------------------------------------");
                        SquareRootMethodOutput(A, b);
                        ResultOutput(4, A);
                        break;
                    case 2:
                        Console.WriteLine(" ----------------------------------------------------------");
                        Console.Write("Enter the precision: ");
                        double epsilon;

                        // перевірка на коректність введеної точності
                        while (!double.TryParse(Console.ReadLine(), out epsilon))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid number: ");
                        }

                        double[] initialElements = new double[4];
                        Console.WriteLine("Enter the initial approximation (4 elements): ");

                        // Введення початкових елементів
                        for (int i = 0; i < 4; i++)
                        {
                            while (true)
                            {
                                Console.Write($"Element {i + 1}: ");
                                if (double.TryParse(Console.ReadLine(), out initialElements[i]))
                                {
                                    break; // Вихід з циклу, якщо введення коректне
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Please enter a valid number: ");
                                }
                            }
                        }

                        SeidelMethod.Method(epsilon, initialElements);
                        Console.WriteLine();
                        ResultOutput(4, A);
                        Console.WriteLine();
                        break;
                    default:
                        Console.WriteLine("Invalid selection.");
                        continue;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number 1 or 2.");
                Console.WriteLine();
                continue;
            }

            Console.Write("Enter 1 to continue, 0 to exit: ");
            if (int.TryParse(Console.ReadLine(), out n))
            {
                continue;
            }
            else
            {
                Console.WriteLine("Invalid input. Exiting.");
                break;
            }
        }

        
    }
}


