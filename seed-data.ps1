# Seed Data Script
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Seeding Sample Leads" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

$apiUrl = "http://localhost:5000/api/leads"

$leads = @(
    @{
        firstName = "Bill"
        lastName = "Johnson"
        email = "bill@example.com"
        phoneNumber = "0412345678"
        suburb = "Yanderra 2574"
        category = "Painters"
        description = "Need to paint 2 aluminum windows and a sliding glass door"
        price = 62.00
    },
    @{
        firstName = "Craig"
        lastName = "Smith"
        email = "craig@example.com"
        phoneNumber = "0423456789"
        suburb = "Woolooware 2230"
        category = "Interior Painters"
        description = "Internal walls 3 colours"
        price = 49.00
    },
    @{
        firstName = "Pete"
        lastName = "Anderson"
        email = "pete@example.com"
        phoneNumber = "0412345680"
        suburb = "Carramar 6031"
        category = "General Building Work"
        description = "Plaster exposed brick walls (see photos), square off 2 archways (see photos), and expand pantry (see photos)."
        price = 26.00
    },
    @{
        firstName = "Chris"
        lastName = "Sanderson"
        email = "chris.sanderson@example.com"
        phoneNumber = "0498765432"
        suburb = "Quinns Rocks 6030"
        category = "Home Renovations"
        description = "There is a two story building at the front of the main house that's about 10x5 that would like to convert into and contained living area"
        price = 32.00
    },
    @{
        firstName = "Sarah"
        lastName = "Williams"
        email = "sarah.williams@example.com"
        phoneNumber = "0487654321"
        suburb = "Bondi Beach 2026"
        category = "Plumbing"
        description = "Fix leaking kitchen sink and replace bathroom faucet"
        price = 650.00
    },
    @{
        firstName = "Michael"
        lastName = "Brown"
        email = "michael.brown@example.com"
        phoneNumber = "0476543210"
        suburb = "Melbourne CBD 3000"
        category = "Electrical"
        description = "Install new lighting fixtures in living room and dining area"
        price = 420.00
    }
)

Write-Host "Creating sample leads..." -ForegroundColor Yellow
Write-Host ""

foreach ($lead in $leads) {
    $json = $lead | ConvertTo-Json
    
    try {
        $response = Invoke-RestMethod -Uri $apiUrl -Method Post -Body $json -ContentType "application/json"
        Write-Host "✓ Created lead for $($lead.firstName) $($lead.lastName)" -ForegroundColor Green
    } catch {
        Write-Host "✗ Failed to create lead for $($lead.firstName) $($lead.lastName): $_" -ForegroundColor Red
    }
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Sample data seeding complete!" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "You can now view the leads at: http://localhost:3000" -ForegroundColor Yellow
Write-Host ""
