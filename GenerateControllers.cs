using System;
using System.IO;
using System.Linq;

class Program {
    static void Main() {
        string[] modules = { "Sales", "Finance", "HRM", "Inventory" };
        string baseDir = @"d:\01-Working\PROJECTS\ZAP\Product\ecosystem\src\Modules";

        foreach (var module in modules) {
            string appDir = Path.Combine(baseDir, module, $"ZAP.Ecosystem.{module}.Application", "Features");
            string apiDir = Path.Combine(baseDir, module, $"ZAP.Ecosystem.{module}.API", "Controllers");
            
            if (!Directory.Exists(apiDir)) Directory.CreateDirectory(apiDir);
            
            if (Directory.Exists(appDir)) {
                var featureDirs = Directory.GetDirectories(appDir).Select(Path.GetFileName);
                if (Path.GetFileName(appDir) == "Features" && Directory.GetDirectories(appDir).Any(d => Path.GetFileName(d) == "v1")) {
                     featureDirs = Directory.GetDirectories(Path.Combine(appDir, "v1")).Select(Path.GetFileName);
                }
                var v1Dirs = Directory.GetDirectories(appDir, "v1", SearchOption.AllDirectories);
                var allFeatures = v1Dirs.Select(d => Directory.GetParent(d).Name).Where(n => n != "Features").Distinct().ToList();
                if (allFeatures.Count == 0) {
                   allFeatures = Directory.GetDirectories(appDir).Select(Path.GetFileName).Where(n => n != "v1").ToList();
                }

                foreach (var featureName in allFeatures) {
                    string controllerName = featureName;
                    string routeName = featureName.ToLower();

                    string content = $@"
using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.{module}.API.Controllers
{{
    [ApiController]
    [Route(""api/{module.ToLower()}/{routeName}"")]
    public class {controllerName}Controller : ControllerBase
    {{
        private readonly IMediator _mediator;
        public {controllerName}Controller(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> Get()
        {{
            return Ok(""Endpoint {controllerName} GET ready"");
        }}

        [HttpPost]
        public async Task<IActionResult> Post()
        {{
            return Ok(""Endpoint {controllerName} POST ready"");
        }}
    }}
}}";
                    string filePath = Path.Combine(apiDir, $"{controllerName}Controller.cs");
                    if (!File.Exists(filePath)) {
                        File.WriteAllText(filePath, content.Trim());
                        Console.WriteLine($"Generated {filePath}");
                    }
                }
            }
        }
    }
}
