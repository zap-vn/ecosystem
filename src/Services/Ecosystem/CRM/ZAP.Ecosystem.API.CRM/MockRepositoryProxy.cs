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
                object? defaultResult = null;

                // Handle PagedResult<T> gracefully to avoid crashes in handlers
                if (taskType.IsGenericType && taskType.GetGenericTypeDefinition().Name == "PagedResult`1")
                {
                    var entityType = taskType.GetGenericArguments()[0];
                    var listType = typeof(List<>).MakeGenericType(entityType);
                    var emptyList = Activator.CreateInstance(listType);

                    defaultResult = Activator.CreateInstance(taskType);
                    var itemsProp = taskType.GetProperty("items", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (itemsProp != null)
                    {
                        itemsProp.SetValue(defaultResult, emptyList);
                    }
                    var totalProp = taskType.GetProperty("total_record", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (totalProp != null)
                    {
                        totalProp.SetValue(defaultResult, 0);
                    }
                }
                else if (taskType.IsClass && taskType != typeof(string))
                {
                    try { defaultResult = Activator.CreateInstance(taskType); } catch { }
                }
                else if (taskType.IsValueType)
                {
                    defaultResult = Activator.CreateInstance(taskType);
                }

                var method = typeof(Task).GetMethod(nameof(Task.FromResult))!.MakeGenericMethod(taskType);
                return method.Invoke(null, new[] { defaultResult });
            }

            return returnType.IsValueType ? Activator.CreateInstance(returnType) : null;
        }

        public static T Create<T>()
        {
            return DispatchProxy.Create<T, MockRepositoryProxy>();
        }
    }
}
