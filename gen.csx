using System;
using System.IO;

string[] routes = { "promotions", "collections", "locations", "categories", "customers", "customergroups", "menus", "diningoptions", "brands", "products", "modifiergroups", "paymenttypes", "paymentterms", "management/prices" };
string yaml = "";
foreach (var r in routes) {
    string id = r.Replace("/", "_");
    yaml += $@"
  /api/{r}/list:
    post:
      operationId: list_{id}
      x-google-backend:
        address: https://crm-api-768989935757.us-central1.run.app/api/{r}/list
      responses:
        '200':
          description: OK";
}
File.AppendAllText("openapi.yaml", yaml);
