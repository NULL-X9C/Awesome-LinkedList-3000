namespace AaDS.Forest.Binary_Search_Tree;

public class BsTree
{
    private BtNode? _root;

    public BsTree(int key, string value)
    {
        _root = new(key, value);
    }

    public void Insert(int key, string value)
    {
        if (_root == null)
        {
            _root = new BtNode(key, value);
            return;
        }
        Insert(_root, key, value);
    }

    private void Insert(BtNode current, int key, string value)
    {
        if (key > current.Key)
        {
            if (current.Right is null)
            {
                current.Right = new(key, value);
            }
            else
            {
                Insert(current.Right, key, value);
            }
        }
        else if (key < current.Key)
        {
            if (current.Left is null)
            {
                current.Left = new(key, value);
            }
            else
            {
                 Insert(current.Left, key, value);
            }
        }
        else
        {
            current.Value = value;
        }
    }    
    
    public int GetHeight() => GetHeight(_root);

    private int GetHeight(BtNode? current)
    {
        if (current == null)
        {
            return -1;
        }
        return 1 + Math.Max(GetHeight(current.Left), GetHeight(current.Right));
    }

    // Задание 2: Обходы дерева
    
    /// <summary>
    /// Обход в ширину (линейный обход) - без рекурсии, используя очередь
    /// Берем list, по нему проходимся в цикле, и каждый раз добавляем в конец потомков
    /// </summary>
    public void BreadthFirstTraverse()
    {
        if (_root == null)
        {
            Console.WriteLine("Дерево пусто");
            return;
        }

        var queue = new List<BtNode> { _root };
        int index = 0;

        Console.Write("Обход в ширину: ");
        while (index < queue.Count)
        {
            BtNode current = queue[index];
            Console.Write($"({current.Key}:{current.Value}) ");

            if (current.Left != null)
                queue.Add(current.Left);
            if (current.Right != null)
                queue.Add(current.Right);

            index++;
        }
        Console.WriteLine();
    }

    /// <summary>
    /// INFIX_TRAVERSE (In-order) - обход по порядку: левое поддерево, вершина, правое поддерево
    /// Элементы выводятся по возрастанию ключей
    /// </summary>
    public void InfixTraverse()
    {
        Console.Write("INFIX_TRAVERSE (In-order): ");
        InfixTraverse(_root);
        Console.WriteLine();
    }

    private void InfixTraverse(BtNode? current)
    {
        if (current == null)
            return;

        InfixTraverse(current.Left);
        Console.Write($"({current.Key}:{current.Value}) ");
        InfixTraverse(current.Right);
    }

    /// <summary>
    /// PREFIX_TRAVERSE (Pre-order) - обход по порядку: вершина, левое поддерево, правое поддерево
    /// Элементы выводятся как они расположены в дереве
    /// </summary>
    public void PrefixTraverse()
    {
        Console.Write("PREFIX_TRAVERSE (Pre-order): ");
        PrefixTraverse(_root);
        Console.WriteLine();
    }

    private void PrefixTraverse(BtNode? current)
    {
        if (current == null)
            return;

        Console.Write($"({current.Key}:{current.Value}) ");
        PrefixTraverse(current.Left);
        PrefixTraverse(current.Right);
    }

    /// <summary>
    /// POSTFIX_TRAVERSE (Post-order) - обход по порядку: левое поддерево, правое поддерево, вершина
    /// Элементы выводятся в обратном порядке относительно структуры дерева
    /// </summary>
    public void PostfixTraverse()
    {
        Console.Write("POSTFIX_TRAVERSE (Post-order): ");
        PostfixTraverse(_root);
        Console.WriteLine();
    }

    private void PostfixTraverse(BtNode? current)
    {
        if (current == null)
            return;

        PostfixTraverse(current.Left);
        PostfixTraverse(current.Right);
        Console.Write($"({current.Key}:{current.Value}) ");
    }
    
    public BtNode? Find(int key)
    {
        return Find(_root, key);
    }

    private BtNode? Find(BtNode? current, int key)
    {
        if (current == null)
        {
            return null;
        }
        if (key == current.Key)
        {
            return current;
        }
        if (key > current.Key)
        {
            return Find(current.Right, key);
        }
        else
        {
            return Find(current.Left, key);
        }
    }
}