namespace AaDS.Forest.Binary_Search_Tree;

public class BtNode
{
    public int Key;
    public string? Value;
    public BtNode? Parent;
    public BtNode? Left;
    public BtNode? Right;
    public int DescendantCount { get; set; }
    public long SubtreeKeySum { get; set; }
    public int SubtreeHeight { get; set; }
    
    public BtNode(int key, string value)
    {
        Key = key;
        Value = value;
    }
}