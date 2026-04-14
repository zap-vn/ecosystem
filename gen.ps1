 = ""
 += "swagger: '2.0'
"
 += "info:
"
 += "  title: CRM Gateway Config
"
 += "  description: API Gateway mapping for CRM API
"
 += "  version: 1.0.0
"
 += "schemes:
"
 += "  - https
"
 += "paths:
"
 += "  /api/v1/auth/login:
"
 += "    post:
"
 += "      summary: Login Customer/Employee
"
 += "      operationId: loginAuth
"
 += "      x-google-backend:
"
 += "        address: https://identity-api-768989935757.us-central1.run.app/api/v1/auth/login
"
 += "      responses:
"
 += "        '200':
"
 += "          description: OK
"

 = @('promotions', 'collections', 'locations', 'categories', 'customers', 'customergroups', 'menus', 'diningoptions', 'brands', 'products', 'modifiergroups', 'paymenttypes', 'paymentterms')

foreach ($r in $routes) {
     += "  /api/$r/list:
"
     += "    post:
"
     += "      operationId: list_$r
"
     += "      x-google-backend:
"
     += "        address: https://crm-api-768989935757.us-central1.run.app/api/$r/list
"
     += "      responses: { '200': { description: 'OK' } }
"

     += "  /api/$r:
"
     += "    get:
"
     += "      operationId: get_$r
"
     += "      x-google-backend:
"
     += "        address: https://crm-api-768989935757.us-central1.run.app/api/$r
"
     += "      responses: { '200': { description: 'OK' } }
"
     += "    post:
"
     += "      operationId: post_$r
"
     += "      x-google-backend:
"
     += "        address: https://crm-api-768989935757.us-central1.run.app/api/$r
"
     += "      responses: { '200': { description: 'OK' } }
"
     += "    put:
"
     += "      operationId: put_$r
"
     += "      x-google-backend:
"
     += "        address: https://crm-api-768989935757.us-central1.run.app/api/$r
"
     += "      responses: { '200': { description: 'OK' } }
"

     += "  /api/$r/{id}:
"
     += "    get:
"
     += "      operationId: get_id_$r
"
     += "      parameters:
"
     += "        - name: id
"
     += "          in: path
"
     += "          required: true
"
     += "          type: string
"
     += "      x-google-backend:
"
     += "        address: https://crm-api-768989935757.us-central1.run.app/api/$r/{id}
"
     += "        path_translation: APPEND_PATH_TO_ADDRESS
"
     += "      responses: { '200': { description: 'OK' } }
"
     += "    put:
"
     += "      operationId: put_id_$r
"
     += "      parameters:
"
     += "        - name: id
"
     += "          in: path
"
     += "          required: true
"
     += "          type: string
"
     += "      x-google-backend:
"
     += "        address: https://crm-api-768989935757.us-central1.run.app/api/$r/{id}
"
     += "        path_translation: APPEND_PATH_TO_ADDRESS
"
     += "      responses: { '200': { description: 'OK' } }
"
     += "    delete:
"
     += "      operationId: delete_id_$r
"
     += "      parameters:
"
     += "        - name: id
"
     += "          in: path
"
     += "          required: true
"
     += "          type: string
"
     += "      x-google-backend:
"
     += "        address: https://crm-api-768989935757.us-central1.run.app/api/$r/{id}
"
     += "        path_translation: APPEND_PATH_TO_ADDRESS
"
     += "      responses: { '200': { description: 'OK' } }
"
}

Out-File openapi.yaml -InputObject $yaml -Encoding UTF8
