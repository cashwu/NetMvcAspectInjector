using System;

namespace testNewAspectInjector.Services
{
    public class PersonService : IPersonService
    {
        [LoggingAspect]
        [AspectMemoryCache(5)]
        public Person Get()
        {
            return new Person
            {
                Id = 1,
                Name = "test",
                Date = DateTime.Now
            };
        }
    }
}