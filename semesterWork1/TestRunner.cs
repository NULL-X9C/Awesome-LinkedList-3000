using System;
using System.IO;
using System.Text;

namespace semesterWork1;

public static class TestRunner
{
    private const int INF = 1_000_000_000;

    /// <summary>
    /// Генерирует тесты, запускает алгоритм, сохраняет файлы и выводит подробный отчёт в консоль.
    /// </summary>
    public static void GenerateAndRunTests(string folder)
    {
        Directory.CreateDirectory(folder);

        var testCases = new[]
        {
            new {
                Name = "test1_simple_3nodes",
                Input = new int[,] { {0, 3, INF}, {INF, 0, 1}, {INF, INF, 0} },
                Expected = new int[,] { {0, 3, 4}, {INF, 0, 1}, {INF, INF, 0} }
            },
            new {
                Name = "test2_negative_edge_4nodes",
                Input = new int[,] { {0, 2, INF, INF}, {INF, 0, -1, INF}, {INF, INF, 0, 3}, {INF, INF, INF, 0} },
                Expected = new int[,] { {0, 2, 1, 4}, {INF, 0, -1, 2}, {INF, INF, 0, 3}, {INF, INF, INF, 0} }
            },
            new {
                Name = "test3_disconnected_4nodes",
                Input = new int[,] { {0, 1, INF, INF}, {INF, 0, INF, INF}, {INF, INF, 0, 2}, {INF, INF, INF, 0} },
                Expected = new int[,] { {0, 1, INF, INF}, {INF, 0, INF, INF}, {INF, INF, 0, 2}, {INF, INF, INF, 0} }
            }
        };

        Console.WriteLine("\n" + new string('=', 60));
        Console.WriteLine(" ЗАПУСК ТЕСТОВ АЛГОРИТМА ФЛОЙДА-УОРШЕЛЛА");
        Console.WriteLine(new string('=', 60) + "\n");

        int passed = 0, failed = 0;

        foreach (var test in testCases)
        {
            int n = test.Input.GetLength(0);
            string inputPath  = Path.Combine(folder, $"{test.Name}_input.csv");
            string outputPath = Path.Combine(folder, $"{test.Name}_output.csv");

            SaveMatrix(inputPath, test.Input, n);

            long iterations;
            int[,] result = FloydArrayForTests(test.Input, n, out iterations);

            SaveMatrix(outputPath, result, n);

            Console.WriteLine($" Тест: {test.Name}");
            Console.WriteLine($"   Вершин: {n} | Итераций: {iterations}\n");

            Console.WriteLine("ВХОДНАЯ матрица:");
            PrintMatrix(test.Input, n);

            Console.WriteLine("ОЖИДАЕМЫЙ результат:");
            PrintMatrix(test.Expected, n);

            Console.WriteLine("ПОЛУЧЕННЫЙ результат:");
            PrintMatrix(result, n);

            // 5. Проверка
            bool isPassed = AreMatricesEqual(result, test.Expected, n);
            if (isPassed)
            {
                Console.WriteLine("СТАТУС: PASSED");
                passed++;
            }
            else
            {
                Console.WriteLine("СТАТУС:  FAILED");
                Console.WriteLine(" Несоответствия (ожидалось → получено):");
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (test.Expected[i, j] != result[i, j])
                            Console.Write($"   [{i},{j}]: {test.Expected[i, j]} → {result[i, j]}  ");
                    }
                }
                Console.WriteLine();
                failed++;
            }
            Console.WriteLine("\n" + new string('-', 60) + "\n");
        }

        // Итог
        Console.WriteLine(new string('=', 60));
        Console.WriteLine($"📊 ИТОГО: {passed} Passed | {failed} Failed | Всего: {testCases.Length}");
        Console.WriteLine(new string('=', 60) + "\n");
    }

    private static void SaveMatrix(string path, int[,] matrix, int n)
    {
        using var sw = new StreamWriter(path, false, Encoding.UTF8);
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                sw.Write(matrix[i, j]);
                if (j < n - 1) sw.Write(",");
            }
            sw.WriteLine();
        }
    }

    private static int[,] FloydArrayForTests(int[,] graph, int n, out long iterations)
    {
        int[,] dist = (int[,])graph.Clone();
        iterations = 0;
        for (int k = 0; k < n; k++)
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    iterations++;
                    if (dist[i, k] != INF && dist[k, j] != INF && dist[i, k] + dist[k, j] < dist[i, j])
                        dist[i, j] = dist[i, k] + dist[k, j];
                }
        return dist;
    }

    private static bool AreMatricesEqual(int[,] a, int[,] b, int n)
    {
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                if (a[i, j] != b[i, j]) return false;
        return true;
    }

    /// <summary>
    /// Красивый вывод матрицы в консоль
    /// </summary>
    private static void PrintMatrix(int[,] matrix, int n)
    {
        Console.Write("   ");
        for (int j = 0; j < n; j++) Console.Write($"[{j,2}] ");
        Console.WriteLine();

        for (int i = 0; i < n; i++)
        {
            Console.Write($"[{i,2}] ");
            for (int j = 0; j < n; j++)
            {
                string val = matrix[i, j] == INF ? " ♾️" : matrix[i, j].ToString("4");
                Console.Write($"{val, 3} ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}