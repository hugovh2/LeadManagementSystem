# Run Frontend Script
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Starting Leads Manager Frontend" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

Set-Location leads-front-web

Write-Host "Starting React development server..." -ForegroundColor Yellow
Write-Host ""

npm start

Set-Location ..
