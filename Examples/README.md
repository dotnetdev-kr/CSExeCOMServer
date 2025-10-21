# CSExeCOMServer Usage Examples

This directory contains example programs demonstrating how to use the CSExeCOMServer COM object from different programming languages and scripting environments.

## Prerequisites

Before running any of these examples, you must:

1. Build the CSExeCOMServer project
2. Register the COM server using RegAsm:

```powershell
Regasm.exe CSExeCOMServer.exe
```

**Important**: If you're on a 64-bit operating system, make sure to build the project with the Platform target explicitly set to `x64` or `x86` (not "Any CPU"). See the main README.md for more details.

## Available Examples

### 1. C# Client Example

**File**: `CSharpClient.cs`

A comprehensive C# console application that demonstrates:
- Creating an instance of the COM object
- Calling the `HelloWorld()` method
- Getting and setting the `FloatProperty`
- Retrieving process and thread IDs
- Handling the `FloatPropertyChanging` event
- Event cancellation logic

#### How to Compile and Run:

**Option A: Using csc.exe (command line)**
```powershell
# First, generate a Type Library (TLB) from the registered COM server
# This is done automatically during registration, but if needed:
# Regasm.exe /tlb:CSExeCOMServer.tlb CSExeCOMServer.exe

# Compile with direct reference to the COM server assembly
csc /reference:..\CSExeCOMServer\bin\Debug\CSExeCOMServer.exe CSharpClient.cs

# Or compile with COM interop
csc /reference:..\CSExeCOMServer\bin\Release\CSExeCOMServer.exe CSharpClient.cs

# Run
CSharpClient.exe
```

**Option B: Using Visual Studio**
1. Create a new C# Console Application project
2. Add the `CSharpClient.cs` file to the project
3. Add a COM reference to CSExeCOMServer (Project > Add Reference > COM tab)
4. Build and run

---

### 2. VBScript Client Example

**File**: `VBScriptClient.vbs`

A simple VBScript that demonstrates:
- Creating the COM object using `CreateObject()`
- Calling methods and properties
- Error handling for COM errors

#### How to Run:

```powershell
# Using cscript (console output)
cscript VBScriptClient.vbs

# Using wscript (dialog boxes)
wscript VBScriptClient.vbs
```

**Note**: VBScript doesn't support COM events in the same way as C#, so event handling is not demonstrated in this example.

---

### 3. JScript Client Example

**File**: `JScriptClient.js`

A JScript example that demonstrates:
- Creating the COM object using `ActiveXObject()`
- Calling methods and properties
- Error handling for COM errors
- Property verification with floating-point tolerance

#### How to Run:

```powershell
# Using cscript (console output)
cscript JScriptClient.js

# Using wscript (dialog boxes)
wscript JScriptClient.js
```

**Note**: JScript/ActiveX doesn't handle out parameters in the traditional way, so the `GetProcessThreadId()` method call is noted but not fully demonstrated.

---

### 4. PowerShell Client Example

**File**: `PowerShellClient.ps1`

A PowerShell script that demonstrates:
- Creating the COM object using `New-Object -ComObject`
- Calling methods with out parameters using `[ref]`
- Colored console output for better readability
- Proper COM object cleanup

#### How to Run:

```powershell
# Run with execution policy bypass (if needed)
powershell -ExecutionPolicy Bypass -File PowerShellClient.ps1

# Or if execution policy allows
.\PowerShellClient.ps1
```

---

## What Each Example Demonstrates

All examples demonstrate the following core functionality:

### 1. Creating the COM Object

Each example shows how to create an instance of `CSExeCOMServer.SimpleObject`:

- **C#**: `new SimpleObject()` or via COM interop
- **VBScript**: `CreateObject("CSExeCOMServer.SimpleObject")`
- **PowerShell**: `New-Object -ComObject "CSExeCOMServer.SimpleObject"`

### 2. Calling Methods

#### HelloWorld()
Returns a simple "HelloWorld" string to verify the connection.

```csharp
string message = simpleObject.HelloWorld();  // Returns "HelloWorld"
```

#### GetProcessThreadId()
Returns the process ID and thread ID of the COM server, demonstrating that it runs in a separate process.

```csharp
int processId, threadId;
simpleObject.GetProcessThreadId(out processId, out threadId);
```

### 3. Using Properties

#### FloatProperty
A read/write property that stores a float value.

```csharp
// Get
float value = simpleObject.FloatProperty;

// Set
simpleObject.FloatProperty = 3.14f;
```

### 4. Handling Events (C# Example Only)

The `FloatPropertyChanging` event fires before the property value changes, allowing you to cancel the change.

```csharp
simpleObject.FloatPropertyChanging += (float NewValue, ref bool Cancel) =>
{
    if (NewValue > 100.0f)
    {
        Cancel = true;  // Cancel the change
    }
};
```

---

## Troubleshooting

### Error: "Retrieving the COM class factory for component with CLSID {...} failed"

**Possible causes:**
1. The COM server is not registered. Run: `Regasm.exe CSExeCOMServer.exe`
2. On 64-bit systems, the Platform target might be set to "Any CPU". Rebuild with explicit `x64` or `x86` target.
3. The build output path doesn't match the registered path. Re-register after building.

### Error: "0x80080005" or similar COM errors

This usually indicates a platform mismatch on 64-bit systems. Ensure:
- The COM server is built with Platform target set to `x64` or `x86` (not "Any CPU")
- The client application matches the same bitness (32-bit client for 32-bit server, etc.)

### VBScript or PowerShell can't create the object

1. Verify registration: Check the registry at `HKEY_CLASSES_ROOT\CSExeCOMServer.SimpleObject`
2. Run the script as Administrator if needed
3. Check Windows Event Viewer for detailed error messages

---

## Additional Notes

### Out-of-Process COM Server

CSExeCOMServer is an **out-of-process** (Local Server) COM server, which means:
- It runs in a separate process from the client
- The process IDs will be different between client and server
- Inter-process communication is handled by COM/DCOM
- The server process (CSExeCOMServer.exe) will start automatically when a client creates an object

### COM Server Lifetime

- The server increments a lock count when objects are created
- The lock count decrements when objects are released (garbage collected)
- When the lock count reaches zero, the server shuts down automatically
- The server triggers garbage collection every 5 seconds to ensure timely cleanup

---

## COM Object Interface Summary

### Properties
- `float FloatProperty` - Read/write property for storing a float value

### Methods
- `string HelloWorld()` - Returns "HelloWorld"
- `void GetProcessThreadId(out int processId, out int threadId)` - Gets the server's process and thread IDs

### Events
- `void FloatPropertyChanging(float NewValue, ref bool Cancel)` - Fires before FloatProperty changes, allows cancellation

---

## Program IDs and GUIDs

```
Program ID: CSExeCOMServer.SimpleObject
CLSID_SimpleObject: DB9935C1-19C5-4ed2-ADD2-9A57E19F53A3
IID_ISimpleObject: 941D219B-7601-4375-B68A-61E23A4C8425
DIID_ISimpleObjectEvents: 014C067E-660D-4d20-9952-CD973CE50436
```

**Note**: These GUIDs are specific to this sample. When creating your own COM server, generate new GUIDs.

---

## Further Reading

For more information about the CSExeCOMServer project, see the main [README.md](../README.md) in the root directory.
