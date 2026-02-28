// See https://aka.ms/new-console-template for more information
using AaDS.Custom_Linear_List;

// Console.WriteLine("Hello, World!");
// var MyList = new LinkedList<T>()
// LinkedList<int> list = new LinkedList<int>();
        
myLinkedList<int> list = new myLinkedList<int>();
        
// Добавляем элементы
list.AddLast(10);
list.AddLast(20);
list.AddLast(30);
list.AddLast(40);
        
Console.WriteLine("Исходный список:");
list.Print(); // 10 -> 20 -> 30 -> 40 -> null
        
// Вставка на позицию 3
list.InsertAt(25, 3);
Console.WriteLine("\nПосле вставки 25 на позицию 3:");
list.Print(); // 10 -> 20 -> 25 -> 30 -> 40 -> null
        
// Переворот списка
list.Reverse();
Console.WriteLine("\nПосле переворота:");
list.Print(); 
// Удаление с позиции 2
list.RemoveAt(2);
Console.WriteLine("\nПосле удаления элемента с позиции 2:");
list.Print(); // 40 -> 25 -> 20 -> 10 -> null