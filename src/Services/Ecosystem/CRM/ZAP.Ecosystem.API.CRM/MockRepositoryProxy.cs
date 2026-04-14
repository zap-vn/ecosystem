using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.API.CRM
{
    public class MockRepositoryProxy : DispatchProxy
    {
        protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
        {
            if (targetMethod == null) return null;

            var returnType = targetMethod.ReturnType;

            if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
            {
                var taskType = returnType.GetGenericArguments()[0];
                object? defaultResult = CreateSafeDefault(taskType);

                var method = typeof(Task).GetMethod(nameof(Task.FromResult))!.MakeGenericMethod(taskType);
                return method.Invoke(null, new[] { defaultResult });
            }

            if (returnType == typeof(Task)) 
                return Task.CompletedTask;

            return CreateSafeDefault(returnType);
        }

        private object? CreateSafeDefault(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition().Name == "PagedResult`1")
            {
                var entityType = type.GetGenericArguments()[0];
                var listType = typeof(List<>).MakeGenericType(entityType);
                var emptyList = Activator.CreateInstance(listType);

                var defaultResult = Activator.CreateInstance(type);
                var itemsProp = type.GetProperty("items", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (itemsProp != null) itemsProp.SetValue(defaultResult, emptyList);
                var totalProp = type.GetProperty("total_record", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (totalProp != null) totalProp.SetValue(defaultResult, 0);
                return defaultResult;
            }
            if (type.IsValueType && type.Name.StartsWith("ValueTuple"))
            {
                var tupleArgs = type.GetGenericArguments();
                var argInstances = new object?[tupleArgs.Length];
                for (int i = 0; i < tupleArgs.Length; i++)
                {
                    argInstances[i] = CreateSafeDefault(tupleArgs[i]);
                }
                return Activator.CreateInstance(type, argInstances);
            }
            if (type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(IEnumerable<>) || type.GetGenericTypeDefinition() == typeof(IReadOnlyList<>)))
            {
                var entityType = type.GetGenericArguments()[0];
                var listType = typeof(List<>).MakeGenericType(entityType);
                return Activator.CreateInstance(listType);
            }
            if (type.IsClass && type != typeof(string))
            {
                try { return Activator.CreateInstance(type); } catch { return null; }
            }
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        public static T Create<T>()
        {
            return DispatchProxy.Create<T, MockRepositoryProxy>();
        }
    }
}
