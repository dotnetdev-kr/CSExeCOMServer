# CSExeCOMServer PowerShell Client Example
# This example demonstrates how to use the CSExeCOMServer COM object from PowerShell
#
# To run this example:
# powershell -ExecutionPolicy Bypass -File PowerShellClient.ps1
#
# Make sure CSExeCOMServer.exe is registered before running:
# Regasm.exe CSExeCOMServer.exe

Write-Host "=== CSExeCOMServer PowerShell Client Example ===" -ForegroundColor Cyan
Write-Host ""

try {
    # Create an instance of the COM object
    Write-Host "Creating SimpleObject instance..." -ForegroundColor Yellow
    $simpleObject = New-Object -ComObject "CSExeCOMServer.SimpleObject"
    Write-Host "SimpleObject instance created successfully." -ForegroundColor Green
    Write-Host ""

    # Test 1: HelloWorld method
    Write-Host "Test 1: Calling HelloWorld()" -ForegroundColor Yellow
    $helloMessage = $simpleObject.HelloWorld()
    Write-Host "Result: $helloMessage" -ForegroundColor Green
    Write-Host ""

    # Test 2: Get process and thread IDs
    Write-Host "Test 2: Getting Process and Thread IDs" -ForegroundColor Yellow
    $processId = 0
    $threadId = 0
    $simpleObject.GetProcessThreadId([ref]$processId, [ref]$threadId)
    Write-Host "Server Process ID: $processId" -ForegroundColor Green
    Write-Host "Server Thread ID: $threadId" -ForegroundColor Green
    Write-Host "Client Process ID: $PID" -ForegroundColor Green
    Write-Host "Note: Server and Client process IDs should be different (out-of-process COM)" -ForegroundColor Cyan
    Write-Host ""

    # Test 3: FloatProperty - Get and Set
    Write-Host "Test 3: Testing FloatProperty" -ForegroundColor Yellow
    $initialValue = $simpleObject.FloatProperty
    Write-Host "Initial value: $initialValue" -ForegroundColor Green

    Write-Host ""
    Write-Host "Setting FloatProperty to 3.14..." -ForegroundColor Yellow
    $simpleObject.FloatProperty = 3.14
    $currentValue = $simpleObject.FloatProperty
    Write-Host "Current value: $currentValue" -ForegroundColor Green

    Write-Host ""
    Write-Host "Setting FloatProperty to 2.71..." -ForegroundColor Yellow
    $simpleObject.FloatProperty = 2.71
    $currentValue = $simpleObject.FloatProperty
    Write-Host "Current value: $currentValue" -ForegroundColor Green
    Write-Host ""

    # Test 4: Multiple property changes
    Write-Host "Test 4: Multiple property changes" -ForegroundColor Yellow
    for ($i = 1; $i -le 5; $i++) {
        $newValue = $i * 10.5
        $simpleObject.FloatProperty = $newValue
        $currentValue = $simpleObject.FloatProperty
        Write-Host "Set to $newValue, current value: $currentValue" -ForegroundColor Green
    }
    Write-Host ""

    # Test 5: Property verification
    Write-Host "Test 5: Property verification" -ForegroundColor Yellow
    $simpleObject.FloatProperty = 99.99
    $verifyValue = $simpleObject.FloatProperty
    if ($verifyValue -eq 99.99) {
        Write-Host "Property set/get verification: PASSED" -ForegroundColor Green
    } else {
        Write-Host "Property set/get verification: FAILED" -ForegroundColor Red
        Write-Host "Expected: 99.99, Got: $verifyValue" -ForegroundColor Red
    }
    Write-Host ""

    # Clean up
    Write-Host "Releasing COM object..." -ForegroundColor Yellow
    [System.Runtime.Interopservices.Marshal]::ReleaseComObject($simpleObject) | Out-Null
    $simpleObject = $null
    [System.GC]::Collect()
    [System.GC]::WaitForPendingFinalizers()

    Write-Host ""
    Write-Host "All tests completed successfully!" -ForegroundColor Green

} catch [System.Runtime.InteropServices.COMException] {
    Write-Host "COM Error occurred:" -ForegroundColor Red
    Write-Host "Error Code: 0x$($_.Exception.ErrorCode.ToString('X'))" -ForegroundColor Red
    Write-Host "Message: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host ""
    Write-Host "Make sure CSExeCOMServer.exe is registered using:" -ForegroundColor Yellow
    Write-Host "Regasm.exe CSExeCOMServer.exe" -ForegroundColor Yellow
    exit 1
} catch {
    Write-Host "Error occurred: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host "Stack trace: $($_.Exception.StackTrace)" -ForegroundColor Red
    exit 1
}

Write-Host ""
Write-Host "Press Enter to exit..." -ForegroundColor Cyan
Read-Host
