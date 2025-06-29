# Database Setup Script for Utility Billing API
Write-Host "Setting up database for Utility Billing API..." -ForegroundColor Green

# Check if PostgreSQL is running
Write-Host "Checking PostgreSQL connection..." -ForegroundColor Yellow

try {
    # Try to connect to PostgreSQL using the connection string from appsettings
    $connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=example"
    
    # You can test the connection here if you have psql installed
    Write-Host "Connection string: $connectionString" -ForegroundColor Cyan
    
    Write-Host "To test the connection manually, you can:" -ForegroundColor Yellow
    Write-Host "1. Install PostgreSQL client tools" -ForegroundColor White
    Write-Host "2. Run: psql -h localhost -p 5432 -U postgres -d postgres" -ForegroundColor White
    Write-Host "3. Enter password: example" -ForegroundColor White
    
} catch {
    Write-Host "Error checking PostgreSQL: $($_.Exception.Message)" -ForegroundColor Red
}

Write-Host "`nNext steps:" -ForegroundColor Green
Write-Host "1. Ensure PostgreSQL is running on localhost:5432" -ForegroundColor White
Write-Host "2. Verify the database 'postgres' exists" -ForegroundColor White
Write-Host "3. Ensure user 'postgres' has proper permissions" -ForegroundColor White
Write-Host "4. Run the application: dotnet run --project src/UtilityBilling.Api" -ForegroundColor White

Write-Host "`nIf using Docker:" -ForegroundColor Cyan
Write-Host "docker-compose -f docker/docker-compose.yml up -d postgres" -ForegroundColor White 