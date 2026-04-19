gcloud api-gateway api-configs create crm-api-config-catalog-2 --api=crm-api-v1 --openapi-spec=openapi.yaml --backend-auth-service-account=204573236312-compute@developer.gserviceaccount.com --project=zapcrm-492101
gcloud api-gateway gateways update zap-ecosystem-gateway --api=crm-api-v1 --api-config=crm-api-config-catalog-2 --location=us-central1 --project=zapcrm-492101
