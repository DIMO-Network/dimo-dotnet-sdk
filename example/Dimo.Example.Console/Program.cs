// See https://aka.ms/new-console-template for more information

using Dimo.Client;
using Dimo.Client.Core;

var dimoClient = new DimoClientBuilder()
    .WithEnvironment(DimoEnvironment.Production)
    .AddCoreServices()
    .Build();

var tokenId = 12345;

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

var data = await dimoClient.DeviceDataService.GetVehicleStatusAsync(tokenId, privilegeToken.Token);

Console.WriteLine(data);