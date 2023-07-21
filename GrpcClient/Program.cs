using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;




//var input = new HelloRequest { Name = "a" };

//var channel = GrpcChannel.ForAddress("https://localhost:7285/");

//var client = new Greeter.GreeterClient(channel);

//var reply = await client.SayHelloAsync(input);

//Console.WriteLine(reply.Message);


var input = new CustomerLookupModel { UserId = 1 };

var channel = GrpcChannel.ForAddress("https://localhost:7285/");

var client = new Customer.CustomerClient(channel);

var reply = await client.GetCustomerInfoAsync(input);

Console.WriteLine("{0} {1}", reply.FirstName, reply.LastName);


using (var call = client.GetNewCustomer(new NewCustomerRequest()))
{
    while(await call.ResponseStream.MoveNext())
    {
        var currentCustomer = call.ResponseStream.Current;

        Console.WriteLine($"{ currentCustomer.FirstName } { currentCustomer.LastName } {currentCustomer.Age} {currentCustomer.EmailAddress} {currentCustomer.IsAlive}");   
    }
}



Console.ReadLine();


