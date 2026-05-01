namespace algorithms;

/// <summary>
/// Предмет для рюкзака
/// </summary>
public class Item
{
    public int Id { get; set; }
    public int Weight { get; set; }
    public int Value { get; set; }
        
    /// <summary>
    /// Удельная стоимость (ценность на единицу веса)
    /// </summary>
    public double ValuePerWeight => Weight == 0 ? double.MaxValue : (double)Value / Weight;
}

class Program
{
    static void Main()
    {
        var items = new List<Item>
        {
            new Item { Id = 1, Weight = 10, Value = 60 },  // V/W = 6.00
            new Item { Id = 2, Weight = 20, Value = 100 }, // V/W = 5.00
            new Item { Id = 3, Weight = 30, Value = 120 }, // V/W = 4.00
            new Item { Id = 4, Weight = 15, Value = 90  }  // V/W = 6.00
        };
        const int capacity = 40;

        Console.WriteLine("Жадный алгоритм");
        Console.WriteLine(new string('═', 60));

        // Сортировка по убыванию удельной стоимости
        var sorted = items.OrderByDescending(i => i.ValuePerWeight).ToList();

        Console.WriteLine("\n Порядок жадного выбора (по V/W):");
        foreach (var item in sorted)
        {
            Console.WriteLine($"   Предмет #{item.Id}: Вес={item.Weight,2}, " +
                              $"Ценность={item.Value,3}, V/W={item.ValuePerWeight:F2}");
        }

        // Жадное заполнение
        int currentWeight = 0;
        int totalValue = 0;
        var taken = new List<Item>();

        Console.WriteLine("\n Процесс принятия решений:");
        for (int i = 0; i < sorted.Count; i++)
        {
            var item = sorted[i];
            if (currentWeight + item.Weight <= capacity)
            {
                // Помещается - берём целиком
                currentWeight += item.Weight;
                totalValue += item.Value;
                taken.Add(item);
                Console.WriteLine($" [+] Взят предмет #{item.Id}" +
                                  $" | Вес: {currentWeight}/{capacity} | Ценность: {totalValue}");
            }
            else
            {
                // Не помещается - пропускаем 
                Console.WriteLine($" [-] Пропущен предмет #{item.Id} " +
                                  $"| Не влезает (свободно: {capacity - currentWeight}, нужно: {item.Weight})");
            }
        }
        
        Console.WriteLine("\n" + new string('═', 60));
        Console.WriteLine("✅ РЕЗУЛЬТАТ ЖАДНОГО ПОДХОДА:");
        Console.WriteLine($"   Взятые предметы: {string.Join(", ", taken.Select(i => $"#{i.Id}"))}");
        Console.WriteLine($"   Итоговый вес:    {currentWeight} / {capacity}");
        Console.WriteLine($"   Итоговая ценность: {totalValue}");
        Console.WriteLine($"   Остаток места:   {capacity - currentWeight}");

        Console.WriteLine("   Жадный алгоритм по удельной стоимости является эвристикой для 0/1 рюкзака.");
        Console.WriteLine("   Он НЕ гарантирует глобальный оптимум, так как не рассматривает комбинации предметов.");
        Console.WriteLine("   Точное решение даёт динамическое программирование за O(n·W).");
        Console.WriteLine($"   Сложность данного подхода: O(n log n) по времени, O(n) по памяти.");
    }
}