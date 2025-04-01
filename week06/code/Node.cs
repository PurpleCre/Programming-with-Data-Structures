public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        // Problem 1

        if (value < Data)
        {
            // Insert to the left
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else if (value == Data)
        {
            return;
        }
        else
        {
            // Insert to the right
            if (Right is null)
                Right = new Node(value);
            else
                Right.Insert(value);
        }
    }

    public bool Contains(int value)
    {
        // TODO Start Problem 2
        if (value < Data)
        {
            // Insert to the left
            if (Left is null)
                return false;
            else
                return Left.Contains(value);
        }
        else if (value == Data)
        {
            return true;
        }
        else
        {
            // Insert to the right
            if (Right is null)
                return false;
            else
                return Right.Contains(value);
        }
    }

    public int GetHeight()
    {
        // Problem 4
        // Base case: if the node is null, return -1 (height of an empty tree)
        if (this == null)
            return -1;

        // Recursively calculate the height of the left and right subtrees
        int leftHeight = Left?.GetHeight() ?? 0; // Use null-conditional operator to handle null case 
        int rightHeight = Right?.GetHeight() ?? 0; // Use null-conditional operator to handle null case

        // Return the maximum height of the two subtrees plus 1 for the current node
        return Math.Max(leftHeight, rightHeight) + 1;             
    }
}