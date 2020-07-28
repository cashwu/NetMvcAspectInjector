using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AspectInjector.Broker;

namespace testNewAspectInjector.Services
{
    [Aspect(Scope.Global)]
    public class CacheAspect
    {
        private static readonly Type VoidTaskResult = Task.FromException(new Exception()).GetType();
        private static readonly object NullMarker = new object();

        [Advice(Kind.Around)]
        public object Handle(
            [Argument(Source.Target)] Func<object[], object> target,
            [Argument(Source.Arguments)] object[] args,
            [Argument(Source.Instance)] object instance,
            [Argument(Source.ReturnType)] Type retType,
            [Argument(Source.Triggers)] Attribute[] triggers
        )
        {
            object result = null;
            var resultFound = false;

            var cacheTriggers = triggers.OfType<CacheAttribute>().ToList();
            var key = GetKey(target, args);

            foreach (var cache in cacheTriggers.Select(ct => ct.Cache).Distinct())
            {
                var ci = cache.GetCacheItem(key);
                if (ci != null)
                {
                    result = ci.Value;

                    if (result == NullMarker)
                    {
                        result = null;
                    }

                    resultFound = true;
                    break;
                }
            }

            if (!resultFound)
            {
                result = target(args);

                if (result == null)
                {
                    result = NullMarker;
                }

                foreach (var cache in cacheTriggers)
                {
                    cache.Cache.Set(key, result, cache.Policy);
                }
            }

            return result;
        }

        private string GetKey(Func<object[], object> target, object[] args)
        {
            return $"{target.Target.GetType().Name}-{target.Method.Name}-{string.Join("-", args.Select(a => (a ?? "").ToString()).ToArray())}";
        }

        private string GetKey(MethodInfo method, object[] args)
        {
            return $"{method.GetHashCode()}{args.Select(a => a.GetHashCode()).Sum()}";
        }

        private bool IsVoid(Type type) => type == typeof(void) || type == typeof(Task) || type == VoidTaskResult;
    }
}