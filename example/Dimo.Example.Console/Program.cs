// See https://aka.ms/new-console-template for more information

using Dimo.Client;
using Dimo.Client.Models;

var dimoClient = new DimoClientBuilder()
    .WithEnvironment(DimoEnvironment.Production)
    // .WithCredentials(new ClientCredentials
    // {
    //     Address = "<your address>",
    //     ClientId = "<your client id>",
    //     Domain = "<your domain>",
    //     PrivateKey = "<your private key>"
    // })
    .AddRestServices()
    .AddGraphQLServices()
    .Build();

// or you can add all services at once
//var dimoClient = new DimoClientBuilder().AddAllServices().Build();


/*
var challenge = await dimoClient.AuthenticationService.GenerateChallengeAsync(
    clientId: "<your client id>",
    domain: "<your domain>",
    address: "<your address>",
    );

var signedChallenge = await dimoClient.AuthenticationService.SignChallengeAsync(
    message: challenge.Challenge,
    privateKey: "<your private key>"
    );

var auth = await dimoClient.AuthenticationService.SubmitChallengeAsync(
    clientId: "<your client id>",
    domain: "<your domain>",
    state: challenge.State,
    signature: signedChallenge
    );*/

// uncomment the following code to get data from the rest services.
/*var tokenId = 12345; // token id from the device you want to get data from

var auth = await dimoClient.AuthenticationService.GetTokenAsync(
    clientId: "<your client id>",
    domain: "<your domain>",
    privateKey: "<your private key>",
    address: "<your address>"
    );

var privilegeToken = await dimoClient.TokenExchangeService.GetPrivilegeTokenAsync(
    accessToken: auth.AccessToken,
    tokenId: tokenId,
    privileges: [ 
        PrivilegeSharing.AllTimeNoLocationData,
        PrivilegeSharing.Commands, 
        PrivilegeSharing.CurrentLocation, 
        PrivilegeSharing.AllTimeLocation 
    ]);

var vehicleStatus = await dimoClient.DeviceDataService.GetVehicleStatusAsync(tokenId, privilegeToken.Token);

Console.WriteLine(vehicleStatus);*/

// uncomment the following code to get data from the graphql services.

/*var response = await dimoClient.IdentityService.ListVehiclesDefinitionsPerAddressAsync("<your 0x address>", 10);

foreach (var node in response.Vehicles.Nodes)
{
    Console.WriteLine("Aftermarket Device:");
    Console.WriteLine($"TokenId: {node.AftermarketDevice.TokenId}, Address: {node.AftermarketDevice.Address}");
    Console.WriteLine("Synthetic Device:");
    Console.WriteLine($"TokenId: {node.SyntheticDevice.TokenId}, Address: {node.SyntheticDevice.Address}");
    Console.WriteLine("Definition:");
    Console.WriteLine($"Make: {node.Definition.Make}, Model: {node.Definition.Model}, Year: {node.Definition.Year}");
}
*/

// graphql custom query
// remember: Parameters should be in the correct gql type
/*
var query = @"
query ListVehiclesDefinitionsPerDeviceDefinitionId($deviceDefinitionId: String!, $limit: Int!) {
    vehicles(filterBy: {deviceDefinitionId: $deviceDefinitionId}, first: $limit) {
      nodes {
        aftermarketDevice {
            tokenId
            address
          	mintedAt
        }
          syntheticDevice {
            address
            tokenId
            mintedAt
        }
        definition {
          make
          model
          year
          id
        }
      }
    }
}
";
            
var variables = new
{
    deviceDefinitionId = "cadillac_ct6_2019",
    limit = 10
};

            
var response = await dimoClient.IdentityService.ExecuteQueryAsync<VehicleSchemeResponse<VehicleDefinition>>(
    query, 
    variables, 
    queryName: "ListVehiclesDefinitionsPerDeviceDefinitionId");

foreach (var node in response.Vehicles.Nodes)
{
    Console.WriteLine("====================================");
    if (node.AftermarketDevice != null)
    {
        Console.WriteLine("Aftermarket Device:");
        Console.WriteLine($"TokenId: {node.AftermarketDevice.TokenId}, Address: {node.AftermarketDevice.Address}");
    }

    if (node.SyntheticDevice != null)
    {
        Console.WriteLine("Synthetic Device:");
        Console.WriteLine($"TokenId: {node.SyntheticDevice.TokenId}, Address: {node.SyntheticDevice.Address}");
    }
    
    if (node.Definition != null)
    {
        Console.WriteLine("Definition:");
        Console.WriteLine($"Make: {node.Definition.Make}, Model: {node.Definition.Model}, Year: {node.Definition.Year}");
    }
}
*/