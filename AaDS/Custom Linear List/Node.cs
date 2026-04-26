using System.Collections;

namespace AaDS.Custom_Linear_List;

public class MyLinkedList<T>
{
    public class Node<T>
    {
        public T Data;
        public Node<T> Next;
            
        public Node(T data)
        {
            Data = data;
        }
    }
    public Node<T> Head;
    public Node<T> Tail;
    public int Count;
    
    // Добавление в начало
    public void AddFirst(T data) {
        Node<T> newNode = new Node<T>(data);
        
        if (Head == null)
        {
            Head = newNode;
            Tail = newNode;
        }
        else {
            newNode.Next = Head;
            Head = newNode;
        }
        Count++;
    }
    
    // Добавление в конец
    public void AddLast(T data) {
        Node<T> newNode = new Node<T>(data);
        
        if (Head == null) 
        {
            Head = newNode;
            Tail = newNode;
        }
        else 
        {
            Tail.Next = newNode;
            Tail = newNode;
        }
        Count++;
    }
    // размер и пустота
    public int Size() => Count;
    public bool IsEmpty() => Count == 0;

    public bool Search(T item)
    {
        Node<T> current = Head;
        while (current != null)
        {
            if (Equals(current.Data, item))
                return true;
            current = current.Next;
        }
        return false;
    }
    public void Reverse()
    {
        if (Head == null || Head.Next == null)
            return; 
    
        Node<T> previous = null;
        Node<T> current = Head;
        Tail = Head; 
    
        while (current != null)
        {
            Node<T> next = current.Next; // сохраняем следующий
            current.Next = previous;      // разворачиваем ссылку
            previous = current;           // двигаем previous вперед
            current = next;               // двигаем current вперед
        }
    
        Head = previous; 
    }
    
    public List<int> GetIndexes(T item)
    {
        List<int> indexes = new List<int>();
        Node<T> current = Head;
        int index = 0;

        while (current != null)
        {
            if (Equals(current.Data, item))
            {
                indexes.Add(index);
            }
            current = current.Next;
            index++;
        }
        return indexes;
    }
    
    /// <summary>
    /// 
    /// </summary>
    public bool RemoveAt(int position)
    {
        // Проверка корректности позиции
        if (position < 1 || position > Count)
        {
            Console.WriteLine($"Ошибка: позиция {position} вне диапазона (1-{Count})");
            return false;
        }
    
        // Удаление первого элемента
        if (position == 1)
        {
            Head = Head.Next;
            if (Head == null) 
                Tail = null;
            Count--;
            return true;
        }
    
        // Поиск элемента 
        Node<T> current = Head;
        for (int i = 1; i < position - 1; i++)
        {
            current = current.Next;
        }
    
        Node<T> toDelete = current.Next; 
        current.Next = toDelete.Next; 
        
        if (toDelete == Tail)
            Tail = current;
    
        Count--;
        return true;
    }
    public bool InsertAt(T data, int position)
    {
        // Проверка корректности позиции (можно вставлять и после последнего)
        if (position < 1 || position > Count + 1)
        {
            Console.WriteLine($"Ошибка: позиция {position} вне диапазона (1-{Count + 1})");
            return false;
        }
    
        Node<T> newNode = new Node<T>(data);
    
        // Вставка в начало
        if (position == 1)
        {
            newNode.Next = Head;
            Head = newNode;
            if (Tail == null) // если список был пуст
                Tail = newNode;
            Count++;
            return true;
        }
    
        // Поиск позиции для вставки
        Node<T> current = Head;
        for (int i = 1; i < position - 1; i++)
        {
            current = current.Next;
        }
        // current указывает на элемент после которого вставляем
    
        newNode.Next = current.Next;
        current.Next = newNode;
    
        // Если вставляем в конец, обновляем tail
        if (newNode.Next == null)
            Tail = newNode;
    
        Count++;
        return true;
    }
    
    // вывод
    public void Print()
    {
        Node<T> current = Head;
        while (current != null) {
            Console.Write(current.Data + " -> ");
            current = current.Next;
        }
        Console.WriteLine("null");
    }
}

