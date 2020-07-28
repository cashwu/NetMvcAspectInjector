using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using testNewAspectInjector.Services;

namespace testNewAspectInjector.Application
{
    public class TestRequestHandler : IRequestHandler<TestRequest, TestResponse>
    {
        [LoggingAspect]
        [AspectMemoryCache(5)]
        public Task<TestResponse> Handle(TestRequest request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"{nameof(TestRequestHandler)}");
            
            return Task.FromResult(new TestResponse
            {
                Person = new Person
                {
                    Id = 1,
                    Name = "abc",
                    Date = DateTime.Now
                }
            });
        }
    }
}