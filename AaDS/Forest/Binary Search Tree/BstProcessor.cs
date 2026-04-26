namespace AaDS.Forest.Binary_Search_Tree;

public class BstProcessor
{
    public static void Run()
    {
        // Создаем тестовое бинарное дерево поиска
        var tree = new BsTree(50, "корень");
        tree.Insert(30, "левое");
        tree.Insert(70, "правое");
        tree.Insert(20, "левый_левого");
        tree.Insert(40, "правый_левого");
        tree.Insert(60, "левый_правого");
        tree.Insert(80, "правый_правого");

        Console.WriteLine("=============== Задание 2: Обходы дерева ===============\n");

        // Обход в ширину (без рекурсии)
        tree.BreadthFirstTraverse();

        // Три вида обходов в глубину
        tree.PrefixTraverse();   // вершина, левое, правое
        tree.InfixTraverse();    // левое, вершина, правое (по возрастанию)
        tree.PostfixTraverse();  // левое, правое, вершина

        Console.WriteLine();
        Console.WriteLine($"Высота дерева: {tree.GetHeight()}");
    }
}

