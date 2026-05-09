namespace semesterWork1;

public class Reading
{
    public static int[,] ReadArray(string path, int size)
    {
        int[,] matrix = new int[size, size];
        var lines = File.ReadAllLines(path);
        for (int i = 0; i < size; i++)
        {
            var parts = lines[i].Split(',');
            for (int j = 0; j < size; j++)
                matrix[i, j] = int.Parse(parts[j]);
        }
        return matrix;
    }
    
    public static List<List<int>> ReadList(string path, int size)
    {
        var matrix = new List<List<int>>(size);
        var lines = File.ReadAllLines(path);
        foreach (var line in lines)
        {
            var row = line.Split(',').Select(int.Parse).ToList();
            matrix.Add(row);
        }
        return matrix;
    }
}