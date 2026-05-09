namespace semesterWork1;

public class Generation
{
    private const int INF = 1_000_000_000;
    private const int EDGE_PROBABILITY_PERCENT = 30;
    
    public static void GenerateGraphs(string folder, int min, int max, int perSize)
    {
        Directory.CreateDirectory(folder);
        var rnd = new Random(42);

        for (int size = min; size <= max; size += 10)
        {
            for (int f = 0; f < perSize; f++)
            {
                string path = Path.Combine(folder, $"graph_{size}_{f}.csv");
                using var sw = new StreamWriter(path);
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        int val = (i == j) ? 0 : 
                            (rnd.Next(100) < EDGE_PROBABILITY_PERCENT ? rnd.Next(1, 101) : INF);
                        sw.Write(val);
                        if (j < size - 1) sw.Write(",");
                    }
                    sw.WriteLine();
                }
            }
        }
    }
}