using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string[] routes = { "promotions", "collections", "locations", "categories", "customers", "customergroups", "menus", "diningoptions", "brands", "products", "modifiergroups", "paymenttypes", "paymentterms", "management/prices" };

        string yaml = @"swagger: '2.0'
info:
  title: CRM Gateway Config
  description: API Gateway mapping for CRM API
  version: 1.0.0
schemes:
  - https
paths:
  /api/v1/auth/login:
    post:
      summary: ""Login Customer/Employee""
      operationId: loginAuth
      x-google-backend:
        address: https://identity-api-768989935757.us-central1.run.app/api/v1/auth/login
      responses:
        '200':
          description: A successful response
";

        foreach (var r in routes)
        {
            string id = r.Replace("/", "_");

            yaml += $@"
  /api/{r}/list:
    post:
      operationId: list_{id}
      x-google-backend:
        address: https://crm-api-768989935757.us-central1.run.app/api/{r}/list
      responses:
        '200':
          description: OK

  /api/{r}:
    get:
      operationId: get_{id}
      x-google-backend:
        address: https://crm-api-768989935757.us-central1.run.app/api/{r}
      responses:
        '200':
          description: OK
    post:
      operationId: post_{id}
      x-google-backend:
        address: https://crm-api-768989935757.us-central1.run.app/api/{r}
      responses:
        '200':
          description: OK
    put:
      operationId: put_{id}
      x-google-backend:
        address: https://crm-api-768989935757.us-central1.run.app/api/{r}
      responses:
        '200':
          description: OK

  /api/{r}/{{entityId}}:
    get:
      operationId: get_id_{id}
      parameters:
        - name: entityId
          in: path
          required: true
          type: string
      x-google-backend:
        address: https://crm-api-768989935757.us-central1.run.app/api/{r}/{{entityId}}
        path_translation: APPEND_PATH_TO_ADDRESS
      responses:
        '200':
          description: OK
    post:
      operationId: post_id_{id}
      parameters:
        - name: entityId
          in: path
          required: true
          type: string
      x-google-backend:
        address: https://crm-api-768989935757.us-central1.run.app/api/{r}/{{entityId}}
        path_translation: APPEND_PATH_TO_ADDRESS
      responses:
        '200':
          description: OK
    put:
      operationId: put_id_{id}
      parameters:
        - name: entityId
          in: path
          required: true
          type: string
      x-google-backend:
        address: https://crm-api-768989935757.us-central1.run.app/api/{r}/{{entityId}}
        path_translation: APPEND_PATH_TO_ADDRESS
      responses:
        '200':
          description: OK
    delete:
      operationId: del_id_{id}
      parameters:
        - name: entityId
          in: path
          required: true
          type: string
      x-google-backend:
        address: https://crm-api-768989935757.us-central1.run.app/api/{r}/{{entityId}}
        path_translation: APPEND_PATH_TO_ADDRESS
      responses:
        '200':
          description: OK
";
        }

        File.WriteAllText("openapi.yaml", yaml);
    }
}
