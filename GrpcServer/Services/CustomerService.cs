using Grpc.Core;

namespace GrpcServer.Services
{
    public class CustomerService : Customer.CustomerBase
    {
        private readonly ILogger<CustomerService> _logger;
        public CustomerService(ILogger<CustomerService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            if (request.UserId == 1)
            {
                output.FirstName = "a";
                output.LastName = "a";
            }
            else if (request.UserId == 2)
            {
                output.FirstName = "b";
                output.LastName = "b";
            }
            else
            {
                output.FirstName = "c";
                output.LastName = "c";
            }

            return Task.FromResult(output);
        }

        public override async Task GetNewCustomer(
            NewCustomerRequest request,
            IServerStreamWriter<CustomerModel> responseStream,
            ServerCallContext context)
        {
            List<CustomerModel> cus = new List<CustomerModel>
            {
                new CustomerModel
                {
                    FirstName = "a",
                    LastName = "a",
                    EmailAddress = "aaa",
                    Age = 1,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "b",
                    LastName = "b",
                    EmailAddress = "bbb",
                    Age = 2,
                    IsAlive = false
                }
            };

            // no return value
            // because the repsonse is stream

            foreach (var c in cus)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(c);
            }
        }
    }
}
