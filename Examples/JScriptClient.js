// CSExeCOMServer JScript Client Example
// This example demonstrates how to use the CSExeCOMServer COM object from JScript
//
// To run this example:
// cscript JScriptClient.js
// or
// wscript JScriptClient.js
//
// Make sure CSExeCOMServer.exe is registered before running:
// Regasm.exe CSExeCOMServer.exe

var simpleObject;
var helloMessage;
var processId = { Value: 0 };
var threadId = { Value: 0 };

WScript.Echo("=== CSExeCOMServer JScript Client Example ===");
WScript.Echo("");

try {
    // Create an instance of the COM object
    WScript.Echo("Creating SimpleObject instance...");
    simpleObject = new ActiveXObject("CSExeCOMServer.SimpleObject");
    WScript.Echo("SimpleObject instance created successfully.");
    WScript.Echo("");

    // Test 1: HelloWorld method
    WScript.Echo("Test 1: Calling HelloWorld()");
    helloMessage = simpleObject.HelloWorld();
    WScript.Echo("Result: " + helloMessage);
    WScript.Echo("");

    // Test 2: Get process and thread IDs
    WScript.Echo("Test 2: Getting Process and Thread IDs");
    // Note: JScript/ActiveX doesn't support out parameters well,
    // so we'll skip this test or use a workaround
    WScript.Echo("(Process/Thread ID retrieval requires special handling in JScript)");
    WScript.Echo("");

    // Test 3: FloatProperty - Get and Set
    WScript.Echo("Test 3: Testing FloatProperty");
    var initialValue = simpleObject.FloatProperty;
    WScript.Echo("Initial value: " + initialValue);

    WScript.Echo("");
    WScript.Echo("Setting FloatProperty to 3.14...");
    simpleObject.FloatProperty = 3.14;
    var currentValue = simpleObject.FloatProperty;
    WScript.Echo("Current value: " + currentValue);

    WScript.Echo("");
    WScript.Echo("Setting FloatProperty to 2.71...");
    simpleObject.FloatProperty = 2.71;
    currentValue = simpleObject.FloatProperty;
    WScript.Echo("Current value: " + currentValue);
    WScript.Echo("");

    // Test 4: Multiple property changes
    WScript.Echo("Test 4: Multiple property changes");
    for (var i = 1; i <= 5; i++) {
        var newValue = i * 10.5;
        simpleObject.FloatProperty = newValue;
        currentValue = simpleObject.FloatProperty;
        WScript.Echo("Set to " + newValue + ", current value: " + currentValue);
    }
    WScript.Echo("");

    // Test 5: Property verification
    WScript.Echo("Test 5: Property verification");
    simpleObject.FloatProperty = 99.99;
    var verifyValue = simpleObject.FloatProperty;

    // Note: Floating point comparison needs tolerance
    var tolerance = 0.01;
    if (Math.abs(verifyValue - 99.99) < tolerance) {
        WScript.Echo("Property set/get verification: PASSED");
    } else {
        WScript.Echo("Property set/get verification: FAILED");
        WScript.Echo("Expected: 99.99, Got: " + verifyValue);
    }
    WScript.Echo("");

    // Clean up
    WScript.Echo("Releasing COM object...");
    simpleObject = null;

    WScript.Echo("");
    WScript.Echo("All tests completed successfully!");

} catch (e) {
    WScript.Echo("Error occurred:");
    WScript.Echo("Error Number: 0x" + (e.number >>> 0).toString(16));
    WScript.Echo("Description: " + e.description);
    WScript.Echo("");
    WScript.Echo("Make sure CSExeCOMServer.exe is registered using:");
    WScript.Echo("Regasm.exe CSExeCOMServer.exe");
    WScript.Quit(1);
}

WScript.Echo("");
WScript.Echo("Press Enter to exit...");
WScript.StdIn.ReadLine();
