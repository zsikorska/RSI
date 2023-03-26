using Grpc.Core;
using GrpcGreeter;
using System;
using System.Globalization;

namespace GrpcGreeter.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Witaj " + request.Name + "\n" + DateTime.Now.ToString("dd MMMM, HH:mm:ss", new CultureInfo("pl-PL"))
            });
        }
    }
}