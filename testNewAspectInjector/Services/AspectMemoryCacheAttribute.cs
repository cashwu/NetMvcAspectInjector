using System;
using System.Runtime.Caching;

namespace testNewAspectInjector.Services
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AspectMemoryCacheAttribute : CacheAttribute
    {
        private static readonly MemoryCache _cache = new MemoryCache("aspect_builtin_memory_cache");
        private readonly uint _seconds;

        public AspectMemoryCacheAttribute(uint seconds)
        {
            _seconds = seconds;
        }

        public override ObjectCache Cache => _cache;
        public override CacheItemPolicy Policy => new CacheItemPolicy() { AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(_seconds) };
    }
}