namespace algorithms.DynamicProgramming;

public class KnapsackProblem
{
    public static void Run()
    {
        // Knapsack( [1, 5, 7, 8, 9, 6, 4, 9], [100, 234, 888, 66, 99, 60, 55, 66], 15);
        //
        // KnapsackGreedy([6, 5, 5], [7, 5, 5], 10);
        //
        // KnapsackGreedy(
        //     [10, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1], 
        //     [100, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9], 
        //     10
        // );
        // Knapsack([10, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
        //     [100, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9],
        //     10);
        
        // Жадный: 51 (берёт #0, ratio=51, вес 1)
        // ДП: 100 (берёт #1, вес 100, но capacity=100)
        // Разница: почти в 2 раза!
        KnapsackGreedy([4, 4, 3, 3], [40, 40, 30, 30], 8);
        Knapsack([4, 4, 3, 3], [40, 40, 30, 30], 8);
        Knapsack([4, 4, 2, 7, 4], [4, 4, 8, 6, 1], 16);
        KnapsackGreedy([4, 4, 2, 7, 4], [4, 4, 8, 6, 1], 16);
    }
    public static int Knapsack(int[] weights, int[] values, int W)
    {
        int n = weights.Length;
        int[,] dp = new int[n + 1, W + 1];
    
        // Инициализация (уже 0 по умолчанию)
        for (int i = 0; i <= n; i++)
        for (int w = 0; w <= W; w++)
            dp[i, w] = 0;
    
        // Заполнение таблицы
        for (int i = 1; i <= n; i++)
        {
            for (int w = 0; w <= W; w++)
            {
                if (weights[i - 1] <= w)
                {
                    dp[i, w] = Math.Max(
                        dp[i - 1, w],
                        values[i - 1] + dp[i - 1, w - weights[i - 1]]
                    );
                }
                else
                {
                    dp[i, w] = dp[i - 1, w];
                }
            }
        }

        Console.WriteLine("Обычная таблица:");
        PrintDp(dp);
        Console.WriteLine(string.Join(",", values));
        Console.WriteLine(string.Join(",", weights));
        Console.WriteLine("Rez: {0}", dp[n, W]);
        return dp[n, W];
    }

    public static int KnapsackGreedy(int[] weights, int[] values, int W)
    {
        int n = weights.Length;

        // Индексы, отсортированные по убыванию ценности на единицу веса
        int[] indices = Enumerable.Range(0, n).ToArray();
        Array.Sort(indices, (a, b) =>
            ((double)values[b] / weights[b]).CompareTo((double)values[a] / weights[a]));

        int[,] greedyDp = new int[n + 1, W + 1];
        
        for (int i = 1; i <= n; i++)
        {
            int idx = indices[i - 1];        // i-й предмет в жадном порядке
            int weight = weights[idx];
            int value = values[idx];

            for (int w = 0; w <= W; w++)
            {
                // Если предмет влезает – жадный алгоритм берёт его всегда
                if (weight <= w)
                {
                    // Ценность = ценность этого предмета + жадная ценность для оставшейся вместимости
                    greedyDp[i, w] = value + greedyDp[i - 1, w - weight];
                }
                else
                {
                    greedyDp[i, w] = greedyDp[i - 1, w];
                }
            }
        }

        Console.WriteLine("Жадная таблица (ценность для префиксов по убыванию удельной стоимости):");
        PrintDp(greedyDp);
        Console.WriteLine(string.Join(",", values));
        Console.WriteLine(string.Join(",", weights));
        Console.WriteLine("Rez: {0}", greedyDp[n, W]);

        // Возвращаем итоговое жадное решение
        return greedyDp[n, W];
    }
    
    static void PrintDp(int[,] dp)
    {
        int rows = dp.GetLength(0);
        int cols = dp.GetLength(1);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(dp[i, j].ToString().PadLeft(5));
            }
            Console.WriteLine();
        }

        Console.WriteLine();
    }
}