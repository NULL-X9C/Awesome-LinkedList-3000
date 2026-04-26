namespace AaDS.Custom_Linear_List;

public class LinkedListWithoutTail<T>
{
    public class Node<T>
    {
        public T Data;
        public Node<T>? Next;

        public Node(T data) 
        {
            Data = data;
        } 
    }

    public Node<T> Head;

    public void AddFirst(T data)
    {
        Node<T> newNode = new Node<T>(data);
        if (Head == null)
        {
            Head = newNode;
        }
        else
        {
            newNode.Next = Head;
            Head = newNode;
        }
    }
    
    public void AddLast(T data)
    {
        Node<T> newNode = new Node<T>(data);
        if (Head is null)
        {
            Head = newNode;
        }
        else
        {
            Node<T> current = Head;
            while (current.Next != null)
            {
                current = current.Next;
            }

            current.Next = newNode;
        }
    }

    public int GetSize()
    {
        if (Head is null)
            return 0;
        int count = 1;
        Node<T> current =  Head;
        while (current.Next is not null)
        {
            count++;
            current = current.Next;
        }
        return count;
    }

    public bool IsEmpty() => Head is null;

    public void AddToIndex(T data, int position)
    {
        if (position <= 1 || position > GetSize())
        {
            Console.WriteLine($"Ошибка: позиция {position} вне диапазона (1-{GetSize()})");
            return;
        }
        
        Node<T> newNode = new Node<T>(data);
        Node<T> current = Head;

        for (int i = 1; i < position - 1; i++)
        {
            current = current.Next;
        }

        newNode.Next = current.Next;
        current.Next = newNode;
        

    }
    
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