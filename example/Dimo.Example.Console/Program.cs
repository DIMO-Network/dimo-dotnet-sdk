// See https://aka.ms/new-console-template for more information

using System.Text.Json;
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
    address: "<your address>"
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
    );
*/
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


#region Create Verifiable Credentials
// // Create verifiable vin vc and get the latest vin vc
// var tokenId = 0;
//
// var vehicleToken = "";
//
// var vinVc = await dimoClient.AttestationService.CreateVinVcAsync(tokenId, vehicleToken);
//
// Console.WriteLine(vinVc);
//
// var vinVcLatest = await dimoClient.TelemetryService.GetVehicleVinVcLatestAsync(tokenId, vehicleToken);
//
// Console.WriteLine(vinVcLatest);
#endregion

#region Vehicle Events Service
/*
// First, get the developer token
var auth = await dimoClient.AuthenticationService.GetTokenAsync(
    clientId: "<clientId>",
    domain: "https://<domain>",
    privateKey: "<privateKey>",
    address: "<clientId>"
);

// List all webhooks
var webhooks = await dimoClient.VehicleEventsService.ListWebhooksAsync(auth.AccessToken);
foreach (var webhook in webhooks)
{
    Console.WriteLine(JsonSerializer.Serialize(webhook));
}

// Get available signal names
var signalNames = await dimoClient.VehicleEventsService.GetWebhookSignalNamesAsync(auth.AccessToken);
Console.WriteLine("\nAvailable signal names:");
// foreach (var signal in signalNames)
// {
//     Console.WriteLine(JsonSerializer.Serialize(signal));
// }

// Create a new webhook
var createWebhook = new WebhookDefinitionRequest
{
    Service = WebhookService.Telemetry,
    Data = "powertrainTransmissionTravelledDistance",
    Trigger = "valueNumber > 1000",
    Setup = WebhookSetup.Realtime,
    Description = "Trigger when travelled distance exceeds 1000",
    TargetUri = "https://constantly-sweet-cardinal.ngrok-free.app",
    Status = WebhookStatus.Active,
    VerificationToken = "token"
};

var createdWebhook = await dimoClient.VehicleEventsService.CreateWebhookAsync(auth.AccessToken, createWebhook);
Console.WriteLine(JsonSerializer.Serialize(createdWebhook));

// Subscribe a vehicle to the webhook
var tokenId = 178893; // Replace with actual vehicle token ID
await dimoClient.VehicleEventsService.SubscribeVehicleAsync(auth.AccessToken, createdWebhook.Id, tokenId);
Console.WriteLine($"Subscribed vehicle {tokenId} to webhook {createdWebhook.Service}");

// List subscribed vehicles
var subscribedVehicles = await dimoClient.VehicleEventsService.ListSubscribedVehiclesAsync(auth.AccessToken, createdWebhook.Id);
Console.WriteLine("\nSubscribed vehicles:");
foreach (var vehicleId in subscribedVehicles)
{
    Console.WriteLine($"- Vehicle ID: {vehicleId}");
}

// List vehicle subscriptions
var vehicleSubscriptions = await dimoClient.VehicleEventsService.ListVehicleSubscriptionsAsync(auth.AccessToken, tokenId);
Console.WriteLine($"\nSubscriptions for vehicle {tokenId}:");
foreach (var subscription in vehicleSubscriptions)
{
    Console.WriteLine(JsonSerializer.Serialize(subscription));
}

// Update webhook configuration
var updatedWebhook = new WebhookDefinitionRequest
{
    Service = WebhookService.Telemetry,
    Data = "powertrainTransmissionTravelledDistance",
    Trigger = "valueNumber > 10000",
    Setup = WebhookSetup.Realtime,
    Description = "Trigger when travelled distance exceeds 10000",
    TargetUri = "https://constantly-sweet-cardinal.ngrok-free.app",
    Status = WebhookStatus.Active,
    VerificationToken = "token"
};

var updated = await dimoClient.VehicleEventsService.UpdateWebhookAsync(auth.AccessToken, createdWebhook.Id, updatedWebhook);
Console.WriteLine($"\nUpdated webhook description: {updated.Description}");

// Unsubscribe vehicle
await dimoClient.VehicleEventsService.UnsubscribeVehicleAsync(auth.AccessToken, createdWebhook.Id, tokenId);
Console.WriteLine($"\nUnsubscribed vehicle {tokenId} from webhook {createdWebhook.Service}");

// Delete webhook
await dimoClient.VehicleEventsService.DeleteWebhookAsync(auth.AccessToken, createdWebhook.Id);
Console.WriteLine($"\nDeleted webhook {createdWebhook.Service}");

// Example of subscribing all vehicles

var allVehiclesHook = await dimoClient.VehicleEventsService.CreateWebhookAsync(auth.AccessToken, createWebhook);
await dimoClient.VehicleEventsService.SubscribeAllVehiclesAsync(auth.AccessToken, allVehiclesHook.Id);
Console.WriteLine($"\nSubscribed all vehicles to webhook {allVehiclesHook.Service}");

// Clean up - unsubscribe all vehicles and delete webhook
await dimoClient.VehicleEventsService.UnsubscribeAllVehiclesAsync(auth.AccessToken, allVehiclesHook.Id);
await dimoClient.VehicleEventsService.DeleteWebhookAsync(auth.AccessToken, allVehiclesHook.Id);
Console.WriteLine($"\nCleaned up webhook {allVehiclesHook.Id}");
*/
#endregion