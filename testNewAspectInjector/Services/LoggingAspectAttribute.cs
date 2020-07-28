using System;
using AspectInjector.Broker;

namespace testNewAspectInjector.Services
{
    [Aspect(Scope.Global)]
    [Injection(typeof(LoggingAspectAttribute))]
    public class LoggingAspectAttribute : Attribute
    {
        [Advice(Kind.Before, Targets = Target.Method)]
        public void Before([Argument(Source.Name)] string name,
                           [Argument(Source.Arguments)] object[] arguments)
        {
            Console.WriteLine($"{name} - On Before");
        }

        [Advice(Kind.After, Targets = Target.Method)]
        public void After([Argument(Source.Name)] string name,
                          [Argument(Source.Arguments)] object[] arguments,
                          [Argument(Source.ReturnValue)] object returnValue)
        {
            Console.WriteLine($"{name} - On After");
        }

        [Advice(Kind.Around, Targets = Target.Method)]
        public object Around([Argument(Source.Name)] string name,
                             [Argument(Source.Arguments)] object[] arguments,
                             [Argument(Source.Target)] Func<object[], object> target)
        {
            Console.WriteLine($"{name} - On Around Before");

            var result = target(arguments);

            Console.WriteLine($"{name} - On Around After");

            return result;
        }
    }
}