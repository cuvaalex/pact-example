# C# Consumer example with PactNet
This example show how to use Pact Foundation with a consumer in C#

## Configuration
The example is configured as follow:
> csharp-client -> API Client
> csharp-pact-consumer -> pact test for the consumer

Before start, we need to create a project with dotnet as follow:

''''
> mkdir xxx-pact
> cd xxx-pact
> dotnet new xunit
> dotnet add package PactNet.Windows (1)

''''
(1) There is different PactNet librairies, depends if you will run the test under a windows system, or Mac or Linux

### Client Librairy
We will need for this example create a .Net Core Rest Client API, to have a solid base for our example

''''
> mkdir xxx-client
> cd xxx-client
> dotnet new classlib
> dotnet add package System.Text.Json

''''
We will add the following client class

''''

''''