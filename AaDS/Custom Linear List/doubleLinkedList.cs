namespace AaDS.Custom_Linear_List;

public class DoubleLinkedList
{
    public class DoubleNode
    {
        public int Data;
        public DoubleNode Next { get; set; }
        public DoubleNode Previous { get; set; }

        public DoubleNode(int data)
        {
            Data = data;
            Next = null;
            Previous = null;
        }
    }

    public DoubleNode FirstNode;
        public void AddToStart(int value)
        {
            if (FirstNode == null)
            {
                FirstNode = new DoubleNode(value);
                
            }
            else
            {
                DoubleNode newNode = new DoubleNode(value);
                newNode.Next = FirstNode;
                FirstNode.Previous = newNode;
                FirstNode = newNode;
            }
        }

        public void AddToEnd(int value)
        {
            
        }

        public void RemoveFromStart()
        {
            if (FirstNode == null)
            {
                throw new Exception("The first node is null");
            }
            FirstNode = FirstNode.Next;
            FirstNode.Previous = null;
        }

        public void RemoveFromEnd()
        {
            if (FirstNode == null)
            {
                throw new Exception("The first node is null");
            }
            // FirstNode = FirstNode.Previous;
            // FirstNode.Next = null;
            //FirstNode.Previous = 
            if (FirstNode.Next == null)
            {
                FirstNode = null;
            }
        }
        public void RemoveFromIndex(int index)
        {
            if (FirstNode == null)
            {
                throw new Exception("The first node is null");
            }

            if (FirstNode.Next == null || index == 0)
            {
                RemoveFromStart();
            }
        }
        
        
}