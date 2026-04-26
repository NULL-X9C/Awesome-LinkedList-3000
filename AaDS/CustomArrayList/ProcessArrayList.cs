namespace AaDS.CustomArrayList;

public class ProcessArrayList
{
    public static void Run()
    {
        Console.WriteLine("============================================ CustomLinkedList ============================================");
        var list = new CustomArrayList<int>(new[] { 10, 20, 30 });
        Console.Write("Исходный список: ");
        list.Print();

        Console.WriteLine(" AddLast ");
        list.AddLast(40);
        list.AddLast(50);
        Console.Write("После AddLast(40, 50): ");
        list.Print();

        Console.WriteLine(" AddFirst ");
        list.AddFirst(5);
        Console.Write("После AddFirst(5): ");
        list.Print();

        Console.WriteLine(" Insert ");
        list.Insert(15, 2);
        Console.Write("После Insert(15, 2): ");
        list.Print();

        Console.WriteLine(" RemoveAt ");
        list.RemoveAt(3);
        Console.Write("После RemoveAt(3): ");
        list.Print();

        Console.WriteLine(" Reverse ");
        list.Reverse();
        Console.Write("После Reverse(): ");
        list.Print();

        Console.WriteLine(" RemoveRange ");
        list.RemoveRange(3);
        Console.Write("После RemoveRange(3): ");
        list.Print();

        Console.WriteLine(" Удаление всех элементов ");
        list.RemoveRange(0);
        Console.Write("После очистки: ");
        list.Print();

        Console.WriteLine(" Добавление в пустой список ");
        list.AddLast(100);
        Console.Write("После AddLast(100): ");
        list.Print();

        Console.WriteLine("--- Тест с строками ---");
        var stringList = new CustomArrayList<string>(new[] { "A", "B", "C" });
        Console.Write("Строковый список: ");
        stringList.Print();
        stringList.AddFirst("Start");
        stringList.AddLast("End");
        Console.Write("После добавления: ");
        stringList.Print();
        stringList.Reverse();
        Console.Write("После Reverse(): ");
        stringList.Print();

        Console.WriteLine(" Тест завершен ");
    }
}