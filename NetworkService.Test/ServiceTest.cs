namespace NetworkService.Test
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.HttpResults;
    using NetworkService.Api.Reponses;
    using NetworkService.Api.Requests;
    using NetworkService.Api.Services;
    using NetworkService.Core.Providers;
    using Newtonsoft.Json;

    [TestClass]
    public class ServiceTest
    {

        IService TestService;

        [TestInitialize]
        public void Setup()
        {
            TestService = new Service(new NetworkProvider());
        }

        [TestMethod]
        public async Task When_Selected_Node_Is_50_Then_Total_Customers_Must_Be_10()
        {
            string networkRequestJson = @"{
                                            ""network"": {
                                             ""branches"": [
                                            {""startNode"": 10,""endNode"": 20},
                                            {""startNode"": 20,""endNode"": 30},
                                            {""startNode"": 30,""endNode"": 50},
                                            {""startNode"": 50,""endNode"": 60},
                                            {""startNode"": 50,""endNode"": 90},
                                            {""startNode"": 60,""endNode"": 40},
                                            {""startNode"": 70,""endNode"": 80}
                                            ],
                                            ""customers"": [
                                            {""node"": 30,""numberOfCustomers"": 8},
                                            {""node"": 40,""numberOfCustomers"": 3},
                                            {""node"": 60,""numberOfCustomers"": 2},
                                            {""node"": 70,""numberOfCustomers"": 1},
                                            {""node"": 80,""numberOfCustomers"": 3},
                                            {""node"": 90,""numberOfCustomers"": 5}
                                            ]
                                            },
                                            ""selectedNode"": 50
                                          }";

            var testNetworkRequest = JsonConvert.DeserializeObject<NetworkRequest>(networkRequestJson);
            Assert.IsNotNull(testNetworkRequest);

            IResult results = await TestService.ProcessRequest(testNetworkRequest) ;
            Assert.IsNotNull(results);

            var response = (Ok<NetworkResponse>)results;
            Assert.AreEqual(StatusCodes.Status200OK, (response.StatusCode));


            Assert.AreEqual(10, response.Value.TotalNumberOfCustomers);
            Assert.AreEqual(50, response.Value.SelectedNode);
        }

        [TestMethod]
        public async Task When_Selected_Node_Is_70_Then_Total_Customers_Must_Be_4()
        {
            string networkRequestJson = @"{
                                            ""network"": {
                                             ""branches"": [
                                            {""startNode"": 10,""endNode"": 20},
                                            {""startNode"": 20,""endNode"": 30},
                                            {""startNode"": 30,""endNode"": 50},
                                            {""startNode"": 50,""endNode"": 60},
                                            {""startNode"": 50,""endNode"": 90},
                                            {""startNode"": 60,""endNode"": 40},
                                            {""startNode"": 70,""endNode"": 80}
                                            ],
                                            ""customers"": [
                                            {""node"": 30,""numberOfCustomers"": 8},
                                            {""node"": 40,""numberOfCustomers"": 3},
                                            {""node"": 60,""numberOfCustomers"": 2},
                                            {""node"": 70,""numberOfCustomers"": 1},
                                            {""node"": 80,""numberOfCustomers"": 3},
                                            {""node"": 90,""numberOfCustomers"": 5}
                                            ]
                                            },
                                            ""selectedNode"": 70
                                          }";

            var testNetworkRequest = JsonConvert.DeserializeObject<NetworkRequest>(networkRequestJson);
            Assert.IsNotNull(testNetworkRequest);

            IResult results = await TestService.ProcessRequest(testNetworkRequest);
            Assert.IsNotNull(results);

            var response = (Ok<NetworkResponse>)results;
            Assert.AreEqual(StatusCodes.Status200OK, (response.StatusCode));
            Assert.AreEqual(4, response.Value.TotalNumberOfCustomers);
            Assert.AreEqual(70, response.Value.SelectedNode);
        }

        [TestMethod]
        public async Task When_StartNode_Is_Same_As_EndNode()
        {
            string networkRequestJson = @"{
                                            ""network"": {
                                             ""branches"": [
                                            {""startNode"": 10,""endNode"": 10}
                                            ],
                                            ""customers"": [
                                            {""node"": 10,""numberOfCustomers"": 8}
                                            ]
                                            },
                                            ""selectedNode"": 10
                                          }";

            var testNetworkRequest = JsonConvert.DeserializeObject<NetworkRequest>(networkRequestJson);
            Assert.IsNotNull(testNetworkRequest);

           
            Assert.ThrowsExceptionAsync<System.ArgumentException>(async () => await TestService.ProcessRequest(testNetworkRequest));
        }

        [TestMethod]
        public async Task When_RootNode_Is_Same_As_EndNode()
        {
            string networkRequestJson = @"{
                                            ""network"": {
                                             ""branches"": [
                                            {""startNode"": 10,""endNode"": 10},
                                            {""startNode"": 20,""endNode"": 10}
                                            ],
                                            ""customers"": [
                                            {""node"": 10,""numberOfCustomers"": 8}
                                            ]
                                            },
                                            ""selectedNode"": 10
                                          }";

            var testNetworkRequest = JsonConvert.DeserializeObject<NetworkRequest>(networkRequestJson);
            Assert.IsNotNull(testNetworkRequest);


            Assert.ThrowsExceptionAsync<System.ArgumentException>(async () => await TestService.ProcessRequest(testNetworkRequest));
        }

        [TestMethod]
        public async Task When_Request_Is_Empty()
        {
            string networkRequestJson = string.Empty;

            var testNetworkRequest = JsonConvert.DeserializeObject<NetworkRequest>(networkRequestJson);
            Assert.IsNull(testNetworkRequest);

            Assert.ThrowsExceptionAsync<NullReferenceException>(async () => await TestService.ProcessRequest(testNetworkRequest));
        }
    }
}
