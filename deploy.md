# Deploy umbraTV to Google Cloud Run

## Prerequisites
1. Google Cloud Project with billing enabled
2. Google Cloud SDK installed (`gcloud`)
3. Docker installed locally (for testing)

## Setup Google Cloud

```powershell
# Login to Google Cloud
gcloud auth login

# Set your project (replace with your project ID)
gcloud config set project YOUR_PROJECT_ID

# Enable required APIs
gcloud services enable run.googleapis.com
gcloud services enable cloudbuild.googleapis.com
gcloud services enable containerregistry.googleapis.com
```

## Test Docker Build Locally (Optional)

```powershell
# Build the Docker image
cd c:\dev\dotNet\umbrMCPTV
docker build -t umbratv:local .

# Test locally
docker run -p 8080:8080 umbratv:local

# Visit http://localhost:8080
```

## Deploy to Cloud Run

### Option 1: Using Cloud Build (Automated)

```powershell
cd c:\dev\dotNet\umbrMCPTV

# Submit build to Cloud Build
gcloud builds submit --config=cloudbuild.yaml

# Your app will be automatically deployed!
```

### Option 2: Manual Deployment

```powershell
cd c:\dev\dotNet\umbrMCPTV

# Build and push to Container Registry
gcloud builds submit --tag gcr.io/YOUR_PROJECT_ID/umbratv

# Deploy to Cloud Run
gcloud run deploy umbratv \
  --image gcr.io/YOUR_PROJECT_ID/umbratv \
  --platform managed \
  --region us-central1 \
  --allow-unauthenticated \
  --memory 512Mi \
  --port 8080

# Get the service URL
gcloud run services describe umbratv --region us-central1 --format 'value(status.url)'
```

## Important Notes

### SQLite in Production
- ✅ Your SQLite database is **baked into the Docker image**
- ✅ Read-only in production (perfect for your use case)
- ⚠️ Any changes in Umbraco backoffice will be **lost on restart**
- 💡 Make all content changes locally, then redeploy

### To Update Content:
1. Make changes in localhost Umbraco backoffice
2. Test locally
3. Rebuild and redeploy:
   ```powershell
   gcloud builds submit --config=cloudbuild.yaml
   ```

### Costs
- Cloud Run charges for:
  - Request time (free tier: 2 million requests/month)
  - Container instance time (free tier: 180,000 vCPU-seconds/month)
  - Network egress
- Estimate: $5-20/month for low traffic

### Custom Domain
```powershell
# Map custom domain
gcloud run domain-mappings create \
  --service umbratv \
  --domain your-domain.com \
  --region us-central1
```

### Environment Variables (if needed)
```powershell
gcloud run services update umbratv \
  --set-env-vars="ASPNETCORE_ENVIRONMENT=Production" \
  --region us-central1
```

## Troubleshooting

### View logs
```powershell
gcloud run services logs read umbratv --region us-central1 --limit 50
```

### Check service status
```powershell
gcloud run services describe umbratv --region us-central1
```

### Increase memory if needed
```powershell
gcloud run services update umbratv --memory 1Gi --region us-central1
```
