namespace NetworkService.Core
{
    using System.Xml.Linq;

    public class Network
    {
        public Node? Root { get; set; }

        List<int> nodes;
        public Network()
        {
                nodes = new List<int>();
        }
        public void AddBranch(int StartNodeValue, int EndNodeValue)
        {
            var startNetworkNode = new Node(StartNodeValue);
            var endNetworkNode = new Node(EndNodeValue);

            //New network add root
            if (Root == null)
            {
                Root = startNetworkNode;
                Root.EndNodes.Add(endNetworkNode);
                return;
            }

            Validate(startNetworkNode, endNetworkNode);

            // Find if start node exists. If not exists then it is possible a new network
            var existedStartNode = FindNode(Root, startNetworkNode.Value);

            if (existedStartNode == null)
            {
                return;
            }

            //Check if end node exists.
            var existedEndNode = FindNode(Root, endNetworkNode.Value);
            if (existedEndNode != null)
            {
                existedStartNode.EndNodes.Add(existedEndNode);
            }
            else
            {
                existedStartNode.EndNodes.Add(endNetworkNode);
            }
        }

        public Node? FindNode(int value)
        {
            return FindNode(Root, value);
        }

        public Node? FindNode(Node node, int value)
        {
            if (node == null)
            {
                return null;
            }

            if (node.Value == value)
            {
                return node;
            }

            foreach (Node endNode in node.EndNodes)
            {
                var existingNode = FindNode(endNode, value);
                if (existingNode != null)
                {
                    return existingNode;
                }
            }

            return null;
        }

        public List<int> TraverseDownStream(int selectedNodeValue)
        {
            nodes = new List<int>();
            var node = FindNode(Root, selectedNodeValue);

            if (node is null)
            {
                return nodes;
            }

            nodes = TraverseDownStream(node);
            return nodes;
        }

        private List<int> TraverseDownStream(Node node)
        {
            nodes.Add(node.Value);
            foreach (var item in node.EndNodes)
            {
                TraverseDownStream(item);
            }

            return nodes;
        }

        // Validation
        private void Validate(Node startNetworkNode, Node endNetworkNode)
        {
            if (Root.Value == endNetworkNode.Value || startNetworkNode.Value == endNetworkNode.Value)
            {
                throw new ArgumentException("Validation failed");
            }
        }
    }
}
