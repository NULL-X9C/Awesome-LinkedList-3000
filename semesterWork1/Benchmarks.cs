using System.Diagnostics;

namespace semesterWork1;

public class Benchmarks
{
    public static List<string> RunBenchmarks(string folder, int min, int max, int perSize)
    {
        var results = new List<string> { "Size,Type,TimeMs,Iterations" };
            
        for (int size = min; size <= max; size += 10)
        {
            for (int f = 0; f < perSize; f++)
            {
                string path = Path.Combine(folder, $"graph_{size}_{f}.csv");
                    
                // Массив
                int[,] arr = Reading.ReadArray(path, size);
                long iterArr;
                var swArr = Stopwatch.StartNew();
                FloydSAlgorithm.FloydArray(arr, size, out iterArr);
                swArr.Stop();
                results.Add($"{size},Array,{swArr.ElapsedMilliseconds},{iterArr}");

                // List
                var lst = Reading.ReadList(path, size);
                long iterLst;
                var swLst = Stopwatch.StartNew();
                FloydSAlgorithm.FloydList(lst, size, out iterLst);
                swLst.Stop();
                results.Add($"{size},List,{swLst.ElapsedMilliseconds},{iterLst}");
                    
                GC.Collect(); GC.WaitForPendingFinalizers(); // Очистка памяти между проходами
                Console.Write($"\rОбработано: {size}x{size} (файл {f+1})");
            }
        }
        Console.WriteLine();
        return results;
    }
}