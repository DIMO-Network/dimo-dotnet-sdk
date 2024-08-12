# DIMO Client for .NET

This is a .NET client for the DIMO API. It is a simple wrapper around the DIMO API, which allows you to interact with the DIMO API using .NET.

## Installation

You can install the DIMO Client for .NET using NuGet. To install the DIMO Client for .NET, run the following command in the Package Manager Console:

For Windows Users:
```
Install-Package Dimo.Client
```

For Linux/Mac Users:
```
dotnet add package Dimo.Client
```

## API Documentation

Please visit the DIMO [Developer Documentation](https://docs.Dimo.zone/developer-platform) to learn more about building on DIMO and detailed information on the API.

### Developer License

As part of the authentication process, you will need to register a set of `client_id` and `redirect_uri` (aka `domain`) on the DIMO Network. The [DIMO Developer License](https://docs.Dimo.zone/developer-platform/getting-started/developer-license) is our approach and design to a more secured, decentralized access control. As a developer, you will need to perform the following steps:

1. [Approving the Dev License to use of $DIMO](https://docs.Dimo.zone/developer-platform/getting-started/developer-license/licensing-process#step-1-approving-the-dev-license-to-use-of-usddimo)
2. [Issue the Dev License](https://docs.Dimo.zone/developer-platform/getting-started/developer-license/licensing-process#step-2-issue-the-dev-license) (Get a `client_id` assigned to you)
3. [Configuring the Dev License](https://docs.Dimo.zone/developer-platform/getting-started/developer-license/licensing-process#step-3-configuring-the-dev-license) (Set `redirect_uri` aka `domain`)
4. [Enable Signer(s)](https://docs.Dimo.zone/developer-platform/getting-started/developer-license/licensing-process#step-4-enable-signer-s), the `private_key` of this signer will be required for API access

## Usage

DIMO Client for .NET is can be used with dependency injection or without it. Below are examples of how to use the DIMO Client for .NET with and without dependency injection.

### Without Dependency Injection

You can add each service individually to the DIMO Client. Below is an example of how to add the core services, GraphQL services, and Streamr services to the DIMO Client.
```csharp
using Dimo.Client;

var dimoClient = new DimoClientBuilder()
    .WithEnvironment(DimoEnvironment.Production)
    .AddCoreServices()
    .AddGraphQLServices()
    .AddStreamr()
    .Build(); 
```
Or you can add all services at once.
```csharp
var dimoClient = new DimoClientBuilder().AddAllServices().Build();
```

### With Dependency Injection

You can also use the DIMO Client for .NET with dependency injection. Below is an example of how to add the DIMO Client to the service collection.
```csharp
using Dimo.Client;

services.AddDimoClient(options =>
{
    options.Environment = DimoEnvironment.Production;
});
```
___
**_NOTE:_** Using dependency injection grants you access to all individual interfaces that make up the DIMO Client. You can inject them into your services as needed.
___
### Configuration

You can configure the DIMO Client using the `DimoClientOptions` class. Below is an example of how to configure the DIMO Client.
```csharp
using Dimo.Client;

services.AddDimoClient(options =>
{
    options.Environment = DimoEnvironment.Production;
});
```
___
**_NOTE:_** by default, the DIMO Client for .NET uses the `Production` environment. You can change the environment by setting the `Environment` property of the `DimoClientOptions` class.
___

### Services

#### REST Services

The core services provide the following functionality:

- Authentication
- Device Data
- Device Definitions
- Events
- Token Exchange
- Trips
- Users
- Valuations
- Vehicle Signal Decoding

#### GraphQL Services

The GraphQL services provide the following functionality:
- Identity API
- Telemetry API

#### Streamr Services
Coming soon...

### Examples

Below are examples of how to use the DIMO Client for .NET.

#### Authentication

In order to authenticate and access private API data, you will need to [authenticate with the DIMO Auth Server](https://docs.Dimo.zone/developer-platform/getting-started/authentication). The SDK provides you with all the steps needed in the [Wallet-based Authentication Flow](https://docs.Dimo.zone/developer-platform/getting-started/authentication/wallet-based-authentication-flow) in case you need it to build a wallet integration around it. We also offer expedited functions to streamline the multiple calls needed.

#### Prerequisites for Authentication

1. A valid Developer License.
2. Access to a signer wallet and its private keys. Best practice is to rotate this frequently for security purposes.

> At its core, a Web3 wallet is a software program that stores private keys, which are necessary for accessing blockchain networks and conducting transactions. Unlike traditional wallets, which store physical currency, Web3 wallets store digital assets such as Bitcoin, Ethereum, and NFTs.
___
**_NOTE:_** The signer wallet here is recommended to be different from the spender or holder wallet for your [DIMO Developer License](https://github.com/DIMO-Network/developer-license-donotus).
___
There three ways to authenticate with the DIMO Auth Server based on the steps listed in [Wallet-based Authentication Flow](https://docs.Dimo.zone/developer-platform/getting-started/authentication/wallet-based-authentication-flow):

1. Using `GenerateChallengeAsync`, `SignChallengeAsync`, and `SubmitChallengeAsync` methods.

```csharp
using Dimo.Client;

var dimoClient = new DimoClientBuilder().AddAllServices()
    .WithEnvironment(DimoEnvironment.Production)
    .Build();
    
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
    );
```

2. Using the `GetTokenAsync` method in the `AuthenticationService` class.

```csharp
using Dimo.Client;

var dimoClient = new DimoClientBuilder().AddAllServices()
    .WithEnvironment(DimoEnvironment.Production)
    .Build();
    
 var auth = await dimoClient.AuthenticationService.GetTokenAsync(
    clientId: "<your client id>",
    domain: "<your domain>",
    privateKey: "<your private key>",
    address: "<your address>"
    );
    
Console.WriteLine(auth.AccessToken);
```

3. Using the `GetTokenAsync` method in the `AuthenticationService` class with a `ClientCredentials` object.

```csharp
using Dimo.Client;

var dimoClient = new DimoClientBuilder()
    .AddAllServices()
    .WithEnvironment(DimoEnvironment.Production)
    .WithCredentials(new ClientCredentials
    {
        Address = "<your address>",
        ClientId = "<your client id>",
        Domain = "<your domain>",
        PrivateKey = "<your private key>"
    })
    .Build();

var auth = await dimoClient.AuthenticationService.GetTokenAsync();

Console.WriteLine(auth.AccessToken);
```
___
**_NOTE:_** 
- The `GetTokenAsync` method in the `AuthenticationService` will expect you to set `ClientCredentials` objects on build configuration.
- When using dependency injection, you can set the `ClientCredentials` object in the `AddDimoClient` method or use Options Pattern to add it configuring an `IOptions<ClientCrendentials>`.
- If you call this method and neither is set it will throw an exception
___

#### Device Data API

```csharp
using Dimo.Client;

var dimoClient = new DimoClientBuilder().AddAllServices().
    .WithEnvironment(DimoEnvironment.Production)
    .Build();

var tokenId = 123456; // The token id of the device you want to get the data for

var auth = await dimoClient.AuthenticationService.GetTokenAsync(
    clientId: "<your client id>",
    domain: "<your domain>",
    privateKey: "<your private key>",
    address: "<your address>"
    );

var privilegeToken = await dimoClient.TokenExchangeService.GetPrivilegeTokenAsync(
    accessToken: auth.AccessToken,
    tokenId: tokenId,
    privileges: [1, 2, 3, 4]); // The privileges you want to get for the device

var vehicleStatus = await dimoClient.DeviceDataService.GetVehicleStatusAsync(tokenId, privilegeToken.Token);

Console.WriteLine(vehicleStatus);
```
___
**_NOTE:_**  for the `privileges` parameter you can check the [DIMO API documentation](https://docs.Dimo.zone/developer-platform/rest-api-references/dimo-protocol/token-exchange-api/token-exchange-api-endpoints#references-for-privilege-sharing) for the available privileges.
___
#### Querying the GraphQL API

The SDK accepts any type of valid custom GraphQL queries, but we've also included a few sample queries to help you understand the DIMO GraphQL APIs.

```csharp
using Dimo.Client;

var dimoClient = new DimoClientBuilder().AddAllServices().
    .WithEnvironment(DimoEnvironment.Production)
    .Build();

// Code to authenticate with the DIMO Auth Server and get the privilege token
// ...

var query = @"
{
  some_valid_GraphQL_query
}
";

var variables = new
{
    VariableName = "VariableValue"
};

var result = await dimoClient.TelemetryService.ExecuteQueryAsync<TResponse>(query, variables, privilegeToken.Token);

Console.WriteLine(result);
```
___
**_NOTE:_**  The `ExecuteQueryAsync` method accepts a generic type `TResponse` which is the type of the response you expect from the GraphQL query.
___
