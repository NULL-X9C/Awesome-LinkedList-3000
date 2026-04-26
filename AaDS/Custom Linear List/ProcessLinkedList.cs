namespace AaDS.Custom_Linear_List;

public class ProcessLinkedList
{
    public static void Run()
    {
        // Console.WriteLine("Hello, World!");
        //var myList = new LinkedList<int>();
            LinkedList<int> list = new LinkedList<int>();
            
            MyLinkedList<int> linkedList = new MyLinkedList<int>();
                
        // Добавляем элементы
            linkedList.AddLast(10);
            linkedList.AddLast(20);
            linkedList.AddLast(30);
            linkedList.AddLast(40);
                
            Console.WriteLine("Исходный список:");
            linkedList.Print(); // 10 -> 20 -> 30 -> 40 -> null
                
        // Вставка на позицию 3
            linkedList.InsertAt(25, 3);
            Console.WriteLine("\nПосле вставки 25 на позицию 3:");
            linkedList.Print(); // 10 -> 20 -> 25 -> 30 -> 40 -> null
                
        // Переворот списка
            linkedList.Reverse();
            Console.WriteLine("\nПосле переворота:");
            linkedList.Print(); 
        // Удаление с позиции 2
            linkedList.RemoveAt(2);
            Console.WriteLine("\nПосле удаления элемента с позиции 2:");
            linkedList.Print(); // 40 -> 25 -> 20 -> 10 -> null
        
            var List2 = new LinkedListWithoutTail<int>();
            List2.AddLast(10);
            List2.AddLast(20);
            List2.AddLast(30);
            List2.AddLast(40);
            int size = List2.GetSize();
            Console.WriteLine(size);
        
            List2.AddToIndex(99,2);
            List2.Print();
    }
}