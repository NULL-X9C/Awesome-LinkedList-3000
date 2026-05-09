using  semesterWork1;

public class Program
{
    const string DATA_FOLDER = "graphs_data2";
    const string RESULT_FILE = "results2.csv";
    const int MIN_SIZE = 10;
    const int MAX_SIZE = 1000; 
    const int FILES_PER_SIZE = 3; 

    static void Main()
     {
         Console.WriteLine("1. Генерация входных данных...");
        // Generation.GenerateGraphs(DATA_FOLDER, MIN_SIZE, MAX_SIZE, FILES_PER_SIZE);
     
         Console.WriteLine("2. Запуск бенчмарков...");
         var results = Benchmarks.RunBenchmarks(DATA_FOLDER, MIN_SIZE, MAX_SIZE, FILES_PER_SIZE);
     
         Console.WriteLine("3. Сохранение результатов...");
         SaveResults(results, RESULT_FILE);
     
         Console.WriteLine("Готово. Откройте results.csv в Excel для построения графиков.");
     }
    
    static void SaveResults(List<string> data, string file)
    {
        File.WriteAllLines(file, data);
    }
}

