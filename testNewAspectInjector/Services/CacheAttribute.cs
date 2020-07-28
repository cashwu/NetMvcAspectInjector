using System;
using System.Runtime.Caching;
using AspectInjector.Broker;

namespace testNewAspectInjector.Services
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    [Injection(typeof(CacheAspect), Inherited = true)]
    public abstract class CacheAttribute : Attribute
    {
        public abstract ObjectCache Cache { get; }

        public abstract CacheItemPolicy Policy { get; }
    }
}