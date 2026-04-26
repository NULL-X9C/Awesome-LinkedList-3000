namespace AaDS.Forest.Tree;

public class TreeNode
{
    public int Info;
    public List<TreeNode> Children;

    public TreeNode(int value)
    {
        Info = value;
        Children = new List<TreeNode>();
    }
}

public class Tree
{

    public void TraverseLevel(List<TreeNode> nodes)
    {
        if (nodes.Count == 0)
        {
            Console.WriteLine("Empty");
            return;
        }
        
        var newNodes = new List<TreeNode>();
        foreach (var node in nodes)
        {
            Console.Write($"{node.Info} ");
            if (node.Children.Count == 0)
                continue;
            foreach (var child in node.Children)
            {
                newNodes.Add(child);
            }
        }

        if (newNodes.Count > 0)
        {
            Console.WriteLine("");
            TraverseLevel(newNodes);
        }
    }
    public void TraverseBreadthFirst2(TreeNode root)
    {
        if (root == null)
            throw new ArgumentNullException(nameof(root));
        
        TraverseLevel(new List<TreeNode> { root });
    }
}

public static class TreeExtensions
{
    public static void TraverseBreadthFirst2(this TreeNode root)
    {
        if (root == null)
            throw new ArgumentNullException(nameof(root));
        var tree = new Tree();
        tree.TraverseLevel(new List<TreeNode> { root });
    }
}