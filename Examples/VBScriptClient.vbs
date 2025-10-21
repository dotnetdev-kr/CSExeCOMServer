' CSExeCOMServer VBScript Client Example
' This example demonstrates how to use the CSExeCOMServer COM object from VBScript
'
' To run this example:
' cscript VBScriptClient.vbs
' or
' wscript VBScriptClient.vbs
'
' Make sure CSExeCOMServer.exe is registered before running:
' Regasm.exe CSExeCOMServer.exe

Option Explicit

Dim simpleObject
Dim helloMessage
Dim processId, threadId
Dim initialValue, newValue

WScript.Echo "=== CSExeCOMServer VBScript Client Example ==="
WScript.Echo ""

On Error Resume Next

' Create an instance of the COM object
WScript.Echo "Creating SimpleObject instance..."
Set simpleObject = CreateObject("CSExeCOMServer.SimpleObject")

If Err.Number <> 0 Then
    WScript.Echo "Error creating COM object:"
    WScript.Echo "Error Code: 0x" & Hex(Err.Number)
    WScript.Echo "Description: " & Err.Description
    WScript.Echo ""
    WScript.Echo "Make sure CSExeCOMServer.exe is registered using:"
    WScript.Echo "Regasm.exe CSExeCOMServer.exe"
    WScript.Quit 1
End If

WScript.Echo "SimpleObject instance created successfully."
WScript.Echo ""

' Test 1: HelloWorld method
WScript.Echo "Test 1: Calling HelloWorld()"
helloMessage = simpleObject.HelloWorld()
WScript.Echo "Result: " & helloMessage
WScript.Echo ""

' Test 2: Get process and thread IDs
WScript.Echo "Test 2: Getting Process and Thread IDs"
simpleObject.GetProcessThreadId processId, threadId
WScript.Echo "Server Process ID: " & processId
WScript.Echo "Server Thread ID: " & threadId
WScript.Echo "Note: This is running in a separate process (out-of-process COM)"
WScript.Echo ""

' Test 3: FloatProperty - Get and Set
WScript.Echo "Test 3: Testing FloatProperty"
initialValue = simpleObject.FloatProperty
WScript.Echo "Initial value: " & initialValue

WScript.Echo ""
WScript.Echo "Setting FloatProperty to 3.14..."
simpleObject.FloatProperty = 3.14
newValue = simpleObject.FloatProperty
WScript.Echo "Current value: " & newValue

WScript.Echo ""
WScript.Echo "Setting FloatProperty to 2.71..."
simpleObject.FloatProperty = 2.71
newValue = simpleObject.FloatProperty
WScript.Echo "Current value: " & newValue
WScript.Echo ""

' Test 4: Multiple property changes
WScript.Echo "Test 4: Multiple property changes"
Dim i
For i = 1 To 5
    simpleObject.FloatProperty = i * 10.5
    WScript.Echo "Set to " & (i * 10.5) & ", current value: " & simpleObject.FloatProperty
Next
WScript.Echo ""

' Clean up
WScript.Echo "Releasing COM object..."
Set simpleObject = Nothing

WScript.Echo ""
WScript.Echo "All tests completed successfully!"
WScript.Echo ""
WScript.Echo "Press Enter to exit..."
WScript.StdIn.ReadLine
