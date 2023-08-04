using System;
using System.Collections.Generic;

namespace Services
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> ServiceContainer = new();

        public static void SetService<T>(T value) where T : class
        {
            var typeValue = typeof(T);
            ServiceContainer.TryAdd(typeValue, value);
        }
        
        public static T Get<T>() where T : class
        {
            var key = typeof(T);
            return ServiceContainer[key] as T;
        }
    }
}