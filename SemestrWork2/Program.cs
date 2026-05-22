using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace FenwickBenchmark
{
    class Program
    {
        const string DATA_DIR = "fenwick_data";
        const string RESULT_CSV = "results.csv";
        
        private static readonly int[] SIZES = GenerateSizes().ToArray();

        static IEnumerable<int> GenerateSizes()
        {
            for (int i = 100; i <= 100000; i += 100)
                yield return i;
        }
        const int MAX_VAL = 1_000;

        static void Main()
        {
            Console.WriteLine(" 1. Проверка корректности алгоритма...");
            RunTests();

            Console.WriteLine("\n 2. Генерация входных данных...");
            GenerateData(DATA_DIR);

            Console.WriteLine(" 3. Запуск бенчмарков...");
            var results = RunBenchmark(DATA_DIR);

            Console.WriteLine(" 4. Сохранение результатов...");
            File.WriteAllLines(RESULT_CSV, results);
            Console.WriteLine("✅ Готово. Сохранено в results.csv в Excel.");
        }

        #region 1. Тесты корректности
        static void RunTests()
        {
            var ft = new FenwickTree(5);
            ft.Insert(1, 3); ft.Insert(2, 1); ft.Insert(3, 4); ft.Insert(4, 2); ft.Insert(5, 5);

            bool passed = true;
            passed &= ft.Search(3) == 8;          // 3+1+4
            passed &= ft.RangeSum(2, 4) == 7;     // 1+4+2
            passed &= ft.Search(5) == 15;         // 3+1+4+2+5

            ft.Delete(3); // удаляем значение в индексе 3 (было 4)
            passed &= ft.Search(3) == 4;          // 3+1+0

            Console.WriteLine(passed ? "✅ Все тесты пройдены." : "❌ Ошибка в логике!");
        }
        #endregion

        #region 2. Генерация данных
        static void GenerateData(string dir)
        {
            Directory.CreateDirectory(dir);
            var rnd = new Random(42);

            foreach (int size in SIZES)
            {
                // Случайные данные
                int[] random = Enumerable.Range(0, size).Select(_ => rnd.Next(1, MAX_VAL)).ToArray();
                File.WriteAllLines(Path.Combine(dir, $"data_{size}_random.csv"), 
                    new[] { string.Join(",", random) });

                // Отсортированные данные
                Array.Sort(random);
                File.WriteAllLines(Path.Combine(dir, $"data_{size}_sorted.csv"), 
                    new[] { string.Join(",", random) });
            }
            Console.WriteLine($"Сгенерировано {SIZES.Length * 2} файлов в папке {dir}");
        }
        #endregion

        #region 3. Бенчмарк
        static List<string> RunBenchmark(string dir)
        {
            var outData = new List<string> { "Size,DataType,Operation,TimeMs,Iterations" };
            var queryRng = new Random(123); // Детерминированный генератор для RangeSum запросов

            foreach (int size in SIZES)
            {
                foreach (var type in new[] { "random", "sorted" })
                {
                    string path = Path.Combine(dir, $"data_{size}_{type}.csv");
                    
                    // Чтение данных 
                    int[] raw = Array.ConvertAll(File.ReadAllLines(path)[0].Split(','), int.Parse);

                    var ft = new FenwickTree(size);

                    BenchmarkOp(ft, size, type, "Insert", outData, 
                        () => { for (int i = 0; i < size; i++) ft.Insert(i + 1, raw[i]); });
                    ft.ResetIterations();

                    BenchmarkOp(ft, size, type, "Search", outData, 
                        () => { for (int i = 1; i <= size; i++) ft.Search(i); });
                    ft.ResetIterations();

                    BenchmarkOp(ft, size, type, "RangeSum", outData, () => {
                        int queries = Math.Min(50, size / 2);
                        for (int q = 0; q < queries; q++) {
                            int l = queryRng.Next(1, size / 2 + 1);
                            int r = queryRng.Next(l, size + 1);
                            ft.RangeSum(l, r);
                        }
                    });
                    ft.ResetIterations();

                    BenchmarkOp(ft, size, type, "Delete", outData, 
                        () => { for (int i = 1; i <= size; i++) ft.Delete(i); });
                    ft.ResetIterations();

                    GC.Collect(); GC.WaitForPendingFinalizers();
                    Console.Write($"\rОбработано: {size} ({type})");
                }
            }
            Console.WriteLine();
            return outData;
        }

        static void BenchmarkOp(FenwickTree ft, int size, string type, string opName, List<string> outData, Action action)
        {
            var sw = Stopwatch.StartNew();
            action();
            sw.Stop();
            outData.Add($"{size},{type},{opName},{sw.ElapsedMilliseconds},{ft.Iterations}");
        }
        #endregion
    }

    #region Реализация дерева Фенвика 
    public class FenwickTree
    {
        public int Size { get; }
        private int[] tree;
        private int[] currentValues; // Хранит актуальные значения для корректного Update/Delete
        public long Iterations { get; private set; } = 0;

        public FenwickTree(int size)
        {
            Size = size;
            tree = new int[size + 1];
            currentValues = new int[size + 1];
        }

        public void ResetIterations() => Iterations = 0;

        /// <summary>Вставка/обновление значения по индексу (1-based)</summary>
        public void Insert(int idx, int val)
        {
            int delta = val - currentValues[idx];
            Update(idx, delta);
            currentValues[idx] = val;
        }

        /// <summary>Удаление элемента (обнуление значения)</summary>
        public void Delete(int idx)
        {
            Update(idx, -currentValues[idx]);
            currentValues[idx] = 0;
        }

        /// <summary>Префиксная сумма от 1 до idx</summary>
        public int Search(int idx) => PrefixSum(idx);

        /// <summary>Сумма на отрезке [l, r]</summary>
        public int RangeSum(int l, int r) => PrefixSum(r) - PrefixSum(l - 1);

        private void Update(int idx, int delta)
        {
            for (; idx <= Size; idx += idx & (-idx))
            {
                tree[idx] += delta;
                Iterations++;
            }
        }

        private int PrefixSum(int idx)
        {
            int sum = 0;
            for (; idx > 0; idx -= idx & (-idx))
            {
                sum += tree[idx];
                Iterations++; 
            }
            return sum;
        }
    }
    #endregion
}