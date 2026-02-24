# Leads Manager Setup Script
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Leads Manager System - Setup Script" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Check prerequisites
Write-Host "Checking prerequisites..." -ForegroundColor Yellow

# Check .NET SDK
try {
    $dotnetVersion = dotnet --version
    Write-Host "✓ .NET SDK installed: $dotnetVersion" -ForegroundColor Green
} catch {
    Write-Host "✗ .NET SDK not found. Please install .NET 6 SDK" -ForegroundColor Red
    exit 1
}

# Check Node.js
try {
    $nodeVersion = node --version
    Write-Host "✓ Node.js installed: $nodeVersion" -ForegroundColor Green
} catch {
    Write-Host "✗ Node.js not found. Please install Node.js 18+" -ForegroundColor Red
    exit 1
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Setting up Backend..." -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan

# Restore .NET packages
Write-Host "Restoring .NET packages..." -ForegroundColor Yellow
dotnet restore LeadsManager.sln

if ($LASTEXITCODE -ne 0) {
    Write-Host "✗ Failed to restore .NET packages" -ForegroundColor Red
    exit 1
}
Write-Host "✓ .NET packages restored" -ForegroundColor Green

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Setting up Frontend..." -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan

# Install npm packages
Write-Host "Installing npm packages..." -ForegroundColor Yellow
Set-Location leads-front-web
npm install

if ($LASTEXITCODE -ne 0) {
    Write-Host "✗ Failed to install npm packages" -ForegroundColor Red
    exit 1
}
Write-Host "✓ npm packages installed" -ForegroundColor Green

# Create .env file if it doesn't exist
if (-not (Test-Path ".env")) {
    Write-Host "Creating .env file..." -ForegroundColor Yellow
    Copy-Item ".env.example" ".env"
    Write-Host "✓ .env file created" -ForegroundColor Green
    Write-Host "  Please update the .env file with your configuration" -ForegroundColor Yellow
}

Set-Location ..

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Setup Complete!" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Next steps:" -ForegroundColor Yellow
Write-Host "1. Update connection string in LeadsManager.API/appsettings.json" -ForegroundColor White
Write-Host "2. Update .env file in leads-front-web folder" -ForegroundColor White
Write-Host "3. Run the API: cd LeadsManager.API && dotnet run" -ForegroundColor White
Write-Host "4. Run the Frontend: cd leads-front-web && npm start" -ForegroundColor White
Write-Host ""
Write-Host "Or use Docker: docker-compose up --build" -ForegroundColor White
Write-Host ""
