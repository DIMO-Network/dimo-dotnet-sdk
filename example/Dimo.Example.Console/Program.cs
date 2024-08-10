// See https://aka.ms/new-console-template for more information

using Dimo.Client;
using Dimo.Client.Core;

var dimoClient = new DimoClientBuilder()
    .WithEnvironment(DimoEnvironment.Production)
    .AddCoreServices()
    .AddGraphQLServices()
    .AddStreamr()
    .Build();

// or you can add all services at once
//var dimoClient = new DimoClientBuilder().AddAllServices().Build();

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
    privileges: [1, 2, 3, 4]);

var vehicleStatus = await dimoClient.DeviceDataService.GetVehicleStatusAsync(tokenId, privilegeToken.Token);

Console.WriteLine(vehicleStatus);*/

// uncomment the following code to get data from the graphql services.
// var vehicleCount = await dimoClient.IdentityApi.CountDimoVehiclesAsync();
//
// Console.WriteLine(vehicleCount);
