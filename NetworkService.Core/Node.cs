namespace NetworkService.Core
{
    using System.Collections.Generic;

    public class Node
    {
        public int Value { get; set; }

        public List<Node> EndNodes { get; } = new List<Node>();

        public Node(int value)
        {
            Value = value;
        }
    }
}
