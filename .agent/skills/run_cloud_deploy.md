# ZAP Cloud Deployment Skill (GCP Cloud Run & Gateway)

Đây là tài liệu hướng dẫn nhanh (Skill) dành cho AI Assistant hoặc Developer để deploy các mảnh Microservice của dự án **ZAP Ecosystem** (ví dụ: `CRM API`, `Identity API`) lên Google Cloud GCP.

## 1. Yêu cầu hệ thống (Prerequisites)
- Google Cloud CLI (`gcloud`) đã được cài đặt và thiết lập.
- `Dockerfile` đã được cấu hình tại thư mục gốc của dự án (ví dụ `d:/PROJECTS/4_CRM_API/Dockerfile`) đóng gói đúng Service mục tiêu (Ví dụ: `.NET 10.0 SDK & ASPNET`).
- Tài khoản GCP phải có quyền tạo Artifact Registry và deploy Cloud Run.

## 2. Các Bước Deploy Cloud Run

### 2.1 Chuẩn bị Login & Project
Đăng nhập tài khoản và chọn Project cần Deploy (ví dụ `zapcrmv1`):
```powershell
gcloud auth login
gcloud config set project zapcrmv1
```

### 2.2 Thực thi lệnh Deploy
Tại thư mục gốc codebase (cùng cấp với `Dockerfile`, ví dụ `D:/PROJECTS/4_CRM_API/`), sử dụng cờ `--source .` để Cloud Build tự động nhận diện Dockerfile, build container, đưa lên Artifact Registry và tiến hành Deploy vào Cloud Run.
```powershell
# Bật APIs nền tảng (chỉ cần chạy lần đầu cho project mới)
gcloud services enable run.googleapis.com cloudbuild.googleapis.com artifactregistry.googleapis.com

# Lệnh Deploy chính thức (thay crm-api thành tên service mong muốn)
gcloud run deploy crm-api `
  --source . `
  --region us-central1 `
  --allow-unauthenticated `
  --project zapcrmv1
```
*Ghi chú: Lệnh trên sẽ tự động Build dựa vào Dockerfile hiện hành.*

## 3. Cấu hình API Gateway sau khi Deploy (Tùy chọn)

Thông thường các service như CRM hay Identity sẽ được hứng qua Google Cloud API Gateway.

### 3.1 Mapping File OpenAPI (openapi.yaml)
Cập nhật file `openapi.yaml` của Gateway trỏ tới target Cloud Run URL bạn vừa nhận được từ Bước 2.2:
```yaml
paths:
  /api/auth/login:
    post:
      summary: Customer Login
      operationId: loginCustomer
      x-google-backend:
        address: https://crm-api-768989935757.us-central1.run.dev 
```

### 3.2 Update Config API Gateway
Thực thi lệnh để gắn file openapi.yaml lên Gateway:
```powershell
# 1. Tạo bản Config lưu nháp mới
gcloud api-gateway api-configs create crm-config-v2 `
  --api=crm-gateway `
  --openapi-spec=openapi.yaml `
  --project=zapcrmv1

# 2. Bắn bản Config nháp này áp dụng vào Gateway Live
gcloud api-gateway gateways update crm-gateway-v1 `
  --api=crm-gateway `
  --api-config=crm-config-v2 `
  --location=us-central1 `
  --project=zapcrmv1
```

## 4. Chẩn đoán lỗi thường gặp (Troubleshooting)

- **Lỗi .NET SDK version (NETSDK1045):** Xảy ra khi Google Cloud Buildpack nhận diện sai Verison .NET. Luôn luôn phải push một file `Dockerfile` rành mạch từ bản `mcr.microsoft.com/dotnet/sdk:10.0` (tuỳ vào version csproj).
- **Lỗi `AUTH_002` sau Gateway:** Thường là do DB production bị sai Password_hash Plain text chặn ở BCrypt. Đảm bảo logic identity kiểm tra Hash và cấu hình Gateway đang forward chuẩn xác tới Gateway Service thay vì rớt qua service khác.
