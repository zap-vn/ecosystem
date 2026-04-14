using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        var outDir = "../../src/Services/Ecosystem/ZAP.Ecosystem.Infrastructure/Data/Repositories/CRM";
        var assembly = Assembly.LoadFrom("../../src/Services/Ecosystem/CRM/ZAP.Ecosystem.Domain.CRM/bin/Debug/net10.0/ZAP.Ecosystem.Domain.CRM.dll");
        
        var missingRepos = new[] { "ICollectionRepository", "ICustomerGroupRepository", "ICustomerRepository", "IDiningOptionRepository", "IEmployeeRepository", "IMenuRepository", "IOrderRepository", "IOrganizationRepository", "IPaymentMethodRepository", "IPaymentTermsRepository", "IPaymentTypeRepository", "IPromotionRepository", "IReportRepository" };
        
        foreach (var type in assembly.GetTypes())
        {
            if (type.IsInterface && missingRepos.Contains(type.Name))
            {
                var sb = new StringBuilder();
                sb.AppendLine("using System;");
                sb.AppendLine("using System.Collections.Generic;");
                sb.AppendLine("using System.Threading.Tasks;");
                sb.AppendLine("using ZAP.Ecosystem.Domain.CRM;");
                sb.AppendLine("using ZAP.Ecosystem.Infrastructure.Data;");
                sb.AppendLine();
                sb.AppendLine("namespace ZAP.Ecosystem.Infrastructure.Data.Repositories.CRM");
                sb.AppendLine("{");
                var className = type.Name.Substring(1);
                sb.AppendLine($"    public class {className} : {type.Name}");
                sb.AppendLine("    {");
                sb.AppendLine("        private readonly EcosystemDbContext _context;");
                sb.AppendLine($"        public {className}(EcosystemDbContext context) => _context = context;");
                sb.AppendLine();
                
                foreach (var method in type.GetMethods())
                {
                    var returnStr = GetTypeName(method.ReturnType);
                    var parameters = string.Join(", ", method.GetParameters().Select(p => $"{GetTypeName(p.ParameterType)} {p.Name}{GetDefaultValue(p)}"));
                    sb.AppendLine($"        public {returnStr} {method.Name}({parameters}) => throw new NotImplementedException();");
                }
                
                sb.AppendLine("    }");
                sb.AppendLine("}");
                
                File.WriteAllText(Path.Combine(outDir, className + ".cs"), sb.ToString());
                Console.WriteLine($"Generated: {className}.cs");
            }
        }
    }
    
    static string GetTypeName(Type t)
    {
        if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Task<>))
        {
            var inner = t.GetGenericArguments()[0];
            if (inner.IsValueType && inner.Name.StartsWith("ValueTuple"))
            {
                var args = inner.GetGenericArguments();
                return $"Task<({GetTypeName(args[0])} Items, {GetTypeName(args[1])} TotalCount)>";
            }
            return $"Task<{GetTypeName(inner)}>";
        }
        if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>))
        {
            return $"IEnumerable<{GetTypeName(t.GetGenericArguments()[0])}>";
        }
        if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IList<>))
        {
            return $"IList<{GetTypeName(t.GetGenericArguments()[0])}>";
        }
        if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            return GetTypeName(t.GetGenericArguments()[0]) + "?["; // simple hack
        }
        if (t.Name == "Guid") return "Guid";
        if (t.Name == "String") return "string";
        if (t.Name == "Int32") return "int";
        if (t.Name == "Boolean") return "bool";
        
        // FullName check for outside types
        if (t.FullName != null && t.FullName.StartsWith("ZAP.Ecosystem.Domain.CRM.Interfaces.")) return t.FullName;
        
        return t.Name; // simple fallback
    }
    
    static string GetDefaultValue(ParameterInfo p)
    {
        if (p.HasDefaultValue)
        {
            if (p.DefaultValue == null) return " = null";
            if (p.DefaultValue is string s) return $" = \"{s}\"";
            if (p.DefaultValue is bool b) return b ? " = true" : " = false";
            return $" = {p.DefaultValue}";
        }
        return "";
    }
}
