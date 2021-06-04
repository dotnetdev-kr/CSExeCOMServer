$obj = New-Object -ComObject 'CSExeCOMServer.SimpleObject'

Write-Output 'A CSExeCOMServer.SimpleObject object is created'

# call the HelloWorld method that returns a string
Write-Output "The HelloWorld method returns $($obj.HelloWorld())"

# Set the FloatProperty property
Write-Output 'Set the FloatProperty property to 1.2'
$obj.FloatProperty = [float]1.2

# Get the FloatProperty property
Write-Output "The FloatProperty property returns $($obj.FloatProperty())"

# Get Process Id
$processId = 0
$threadId = 0
$obj.GetProcessThreadId([ref] $processId, [ref] $threadId)
Write-Output "Out-of-process COM Server - Process ID: #$processId, Thread ID: #$threadId"

$obj = $null