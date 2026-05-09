namespace semesterWork1;

public class FloydSAlgorithm
{
    private const int INF = 1_000_000_000;
    #region Алгоритмы
    public static void FloydArray(int[,] graph, int n, out long iterations)
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
    }

    public static void FloydList(List<List<int>> graph, int n, out long iterations)
    {
        
        // Глубокое копирование входной матрицы
        var dist = new List<List<int>>(n);
        for (int i = 0; i < n; i++)
            dist.Add(new List<int>(graph[i]));

        iterations = 0;

        for (int k = 0; k < n; k++)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    iterations++;

                    int dik = dist[i][k];
                    int dkj = dist[k][j];
                    
                    if (dik != INF && dkj != INF && dik + dkj < dist[i][j])
                    {
                        dist[i][j] = dik + dkj;
                    }
                }
            }
        }
    }
    #endregion
}