param (
    [Parameter(Mandatory=$true, HelpMessage="Nhap Google Cloud Project ID (VD: zap-company-1234)")]
    [string]$ProjectId,

    [Parameter(Mandatory=$true, HelpMessage="Nhap ten GitHub Repo (VD: tranjai/zap-ecosystem)")]
    [string]$RepoName,

    [string]$ServiceAccountName = "gh-actions-deployer",
    [string]$PoolName = "github-actions-pool",
    [string]$ProviderName = "github-provider"
)

Write-Host "=========================================================="
Write-Host "KHOI TAO GCP WORKLOAD IDENTITY FEDERATION CHO GITHUB ACTION"
Write-Host "=========================================================="
Write-Host "Project ID: $ProjectId"
Write-Host "Repository: $RepoName"

# 1. Kich hoat APIs mang tinh bat buoc
Write-Host "`n[1/6] Dang bat cac API yeu cau..."
gcloud services enable iamcredentials.googleapis.com --project=$ProjectId
gcloud services enable run.googleapis.com --project=$ProjectId

# 2. Tao Workload Identity Pool
Write-Host "`n[2/6] Tao Workload Identity Pool ($PoolName)..."
gcloud iam workload-identity-pools create $PoolName `
  --project=$ProjectId `
  --location="global" `
  --display-name="GitHub Actions Pool for ZAP"

# Lay ra ID tuyet doi cua Pool de config buoc sau
$PoolId = (gcloud iam workload-identity-pools describe $PoolName --project=$ProjectId --location="global" --format="value(name)")

# 3. Tao Workload Identity Provider
Write-Host "`n[3/6] Tao Provider lien ket GitHub OIDC ($ProviderName)..."
gcloud iam workload-identity-pools providers create-oidc $ProviderName `
  --project=$ProjectId `
  --location="global" `
  --workload-identity-pool=$PoolName `
  --display-name="GitHub Actions Provider" `
  --attribute-mapping="google.subject=assertion.sub,attribute.actor=assertion.actor,attribute.repository=assertion.repository" `
  --issuer-uri="https://token.actions.githubusercontent.com"

# 4. Tao Service Account chuyen dung luu Deploy
Write-Host "`n[4/6] Tao IAM Service Account ($ServiceAccountName)..."
gcloud iam service-accounts create $ServiceAccountName `
  --project=$ProjectId `
  --display-name="Service Account for GitHub Actions Deploy"

$ServiceAccountEmail = "$ServiceAccountName@$ProjectId.iam.gserviceaccount.com"

# 5. Cap quyen phan nhom "Ai" duoc uy quyen vao Service Account
Write-Host "`n[5/6] Lien ket Service Account voi repo GitHub: $RepoName ..."
gcloud iam service-accounts add-iam-policy-binding $ServiceAccountEmail `
  --project=$ProjectId `
  --role="roles/iam.workloadIdentityUser" `
  --member="principalSet://iam.googleapis.com/$PoolId/attribute.repository/$RepoName"

# 6. Phan quyen cho Service Account quan tri Cloud Run
Write-Host "`n[6/6] Phan quyen Cloud Run Admin va Service Account User..."
gcloud projects add-iam-policy-binding $ProjectId `
  --member="serviceAccount:$ServiceAccountEmail" `
  --role="roles/run.admin"

gcloud projects add-iam-policy-binding $ProjectId `
  --member="serviceAccount:$ServiceAccountEmail" `
  --role="roles/iam.serviceAccountUser"


$ProviderId = (gcloud iam workload-identity-pools providers describe $ProviderName --project=$ProjectId --location="global" --workload-identity-pool=$PoolName --format="value(name)")

Write-Host "=========================================================="
Write-Host "HOAN TAT! QUAN TRONG: VUI LONG COPY NHUNG GIA TRI SAU VAO GITHUB SECRETS"
Write-Host "=========================================================="
Write-Host ">> GCP_SERVICE_ACCOUNT            = $ServiceAccountEmail"
Write-Host ">> GCP_WORKLOAD_IDENTITY_PROVIDER = $ProviderId"
Write-Host "=========================================================="
