namespace NetworkService.Test
{
    using NetworkService.Core;

    [TestClass]
    public class BuildNetworkTests
    {
        [TestMethod]
        public void Add_RootNode_When_Node_NotExists()
        {
            var network = new Network();
            network.AddBranch(10, 20);
            Assert.IsNotNull(network.Root);
            Assert.AreEqual(10, network.Root.Value);
            Assert.AreEqual(20, network.Root.EndNodes[0].Value);
        }

        [TestMethod]
        public void When_EndNode_Is_Same_as_Root_Node_Throw_Argument_Exception_Test()
        {
            var network = new Network();
            network.AddBranch(10, 20);
            Assert.ThrowsException<ArgumentException>(() => { network.AddBranch(20, 10); });
        }

        [TestMethod]
        public void When_EndNode_Is_Same_as_Start_Node_Throw_Argument_Exception_Test()
        {
            var network = new Network();
            network.AddBranch(10, 20);
            Assert.ThrowsException<ArgumentException>(() => { network.AddBranch(20, 20); });
        }

        [TestMethod]
        public void Add_Branches_test()
        {
            var network = new Network();
            network.AddBranch(10, 20);
            network.AddBranch(20, 30);
            network.AddBranch(30, 50);
            network.AddBranch(50, 60);
            network.AddBranch(50, 90);
            network.AddBranch(60, 40);
            network.AddBranch(70, 80);
            Assert.IsNotNull(network.Root);
            Assert.AreEqual(10, network.Root.Value);
            Assert.AreEqual(20, network.Root.EndNodes[0].Value);
            Assert.AreEqual(30, network.FindNode(new Node(30), 30)?.Value);
        }

        [TestMethod]
        public void Traverse_DownStream_GetAddedValue_test()
        {
            var network = new Network();
            network.AddBranch(10, 20);
            network.AddBranch(20, 30);
            network.AddBranch(30, 50);
            network.AddBranch(50, 60);
            network.AddBranch(50, 90);
            network.AddBranch(60, 40);
            Assert.IsNotNull(network.Root);
            Assert.AreEqual(10, network.Root.Value);
            Assert.AreEqual(20, network.Root.EndNodes[0].Value);
            Assert.AreEqual(30, network.FindNode(new Node(30), 30)?.Value);
            Assert.AreEqual(4, network.TraverseDownStream(50).Count());
            Assert.AreEqual(2, network.TraverseDownStream(60).Count());
            Assert.AreEqual(0, network.TraverseDownStream(1000).Count());

        }
    }
}