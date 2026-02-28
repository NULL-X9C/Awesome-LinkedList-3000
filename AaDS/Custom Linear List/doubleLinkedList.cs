namespace AaDS.Custom_Linear_List;

public class doubleLinkedList
{
    public class LinkedNode
    {
        public int Data;
        public LinkedNode Next { get; set; }
        public LinkedNode Previous { get; set; }
        public LinkedNode(int data)
        {
            Data = data;
            Next = null;
            Previous = null;
        }
    }
    
}