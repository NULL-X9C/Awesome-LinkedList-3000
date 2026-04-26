namespace AaDS.Forest.Tree;

public class TreeProcessor
{
    private TreeNode NodesMaker()
    {
                TreeNode node1 = new TreeNode(7);
                TreeNode node2 = new TreeNode(5);
                TreeNode node3 = new TreeNode(17);
                TreeNode node4 = new TreeNode(2);
                TreeNode node5 = new TreeNode(1);
                TreeNode node6 = new TreeNode(3);
                TreeNode node7 = new TreeNode(43);
                TreeNode node8 = new TreeNode(13);
                TreeNode node9 = new TreeNode(57);
                TreeNode node10 = new TreeNode(67);
                TreeNode node11 = new TreeNode(52);
                TreeNode node12 = new TreeNode(100);
                TreeNode node13 = new TreeNode(-1);
                
                node1.Children.AddRange([node2, node3, node4]);
                node2.Children.AddRange([node5, node6, node7]);
                node4.Children.AddRange([ node8, node9 ]);
                node7.Children.AddRange([ node10 ]);
                node8.Children.AddRange([ node11, node12 ]);
                node12.Children.AddRange([ node13 ]);
                return node1;
    }
    public void ProcessTree()
    {
        TreeNode root = NodesMaker();
        var tree = new Tree();
        
       // root.TraverseLevel([root]);
       tree.TraverseBreadthFirst2(root);
       root.TraverseBreadthFirst2();
    }
}