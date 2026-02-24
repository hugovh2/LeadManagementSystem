# Run API Script
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Starting Leads Manager API" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

Set-Location LeadsManager.API

Write-Host "Starting API server..." -ForegroundColor Yellow
Write-Host ""

dotnet run

Set-Location ..
