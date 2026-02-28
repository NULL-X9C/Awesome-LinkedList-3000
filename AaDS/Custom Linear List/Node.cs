using System.Collections;

namespace AaDS.Custom_Linear_List;

public class myLinkedList<T>
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
    public Node<T> head;
    public Node<T> tail;
    public int count;
    
    // Добавление в начало
    public void AddFirst(T data) {
        Node<T> newNode = new Node<T>(data);
        
        if (head == null)
        {
            head = newNode;
            tail = newNode;
        }
        else {
            newNode.Next = head;
            head = newNode;
        }
        count++;
    }
    
    // Добавление в конец
    public void AddLast(T data) {
        Node<T> newNode = new Node<T>(data);
        
        if (head == null) 
        {
            head = newNode;
            tail = newNode;
        }
        else 
        {
            tail.Next = newNode;
            tail = newNode;
        }
        count++;
    }
    // размер и пустота
    public int Size() => count;
    public bool IsEmpty() => count == 0;

    public bool Search(T item)
    {
        Node<T> current = head;
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
        if (head == null || head.Next == null)
            return; 
    
        Node<T> previous = null;
        Node<T> current = head;
        tail = head; 
    
        while (current != null)
        {
            Node<T> next = current.Next; // сохраняем следующий
            current.Next = previous;      // разворачиваем ссылку
            previous = current;           // двигаем previous вперед
            current = next;               // двигаем current вперед
        }
    
        head = previous; 
    }
    
    public List<int> GetIndexes(T item)
    {
        List<int> indexes = new List<int>();
        Node<T> current = head;
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
        if (position < 1 || position > count)
        {
            Console.WriteLine($"Ошибка: позиция {position} вне диапазона (1-{count})");
            return false;
        }
    
        // Удаление первого элемента
        if (position == 1)
        {
            head = head.Next;
            if (head == null) 
                tail = null;
            count--;
            return true;
        }
    
        // Поиск элемента 
        Node<T> current = head;
        for (int i = 1; i < position - 1; i++)
        {
            current = current.Next;
        }
    
        Node<T> toDelete = current.Next; 
        current.Next = toDelete.Next; 
        
        if (toDelete == tail)
            tail = current;
    
        count--;
        return true;
    }
    public bool InsertAt(T data, int position)
    {
        // Проверка корректности позиции (можно вставлять и после последнего)
        if (position < 1 || position > count + 1)
        {
            Console.WriteLine($"Ошибка: позиция {position} вне диапазона (1-{count + 1})");
            return false;
        }
    
        Node<T> newNode = new Node<T>(data);
    
        // Вставка в начало
        if (position == 1)
        {
            newNode.Next = head;
            head = newNode;
            if (tail == null) // если список был пуст
                tail = newNode;
            count++;
            return true;
        }
    
        // Поиск позиции для вставки
        Node<T> current = head;
        for (int i = 1; i < position - 1; i++)
        {
            current = current.Next;
        }
        // current указывает на элемент после которого вставляем
    
        newNode.Next = current.Next;
        current.Next = newNode;
    
        // Если вставляем в конец, обновляем tail
        if (newNode.Next == null)
            tail = newNode;
    
        count++;
        return true;
    }
    
    // вывод
    public void Print()
    {
        Node<T> current = head;
        while (current != null) {
            Console.Write(current.Data + " -> ");
            current = current.Next;
        }
        Console.WriteLine("null");
    }
}

