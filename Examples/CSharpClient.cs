// CSExeCOMServer C# Client Example
// This example demonstrates how to use the CSExeCOMServer COM object from C#
//
// To compile this example:
// csc /reference:CSExeCOMServer.tlb CSharpClient.cs
//
// Or add this file to a C# Console Application project and add a COM reference
// to the registered CSExeCOMServer.

using System;
using System.Runtime.InteropServices;
using CSExeCOMServer;

namespace CSExeCOMServerClientExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== CSExeCOMServer C# Client Example ===\n");

            try
            {
                // Create an instance of the COM object
                Console.WriteLine("Creating SimpleObject instance...");
                SimpleObject simpleObject = new SimpleObject();
                Console.WriteLine("SimpleObject instance created successfully.\n");

                // Subscribe to the FloatPropertyChanging event
                simpleObject.FloatPropertyChanging += SimpleObject_FloatPropertyChanging;

                // Test 1: HelloWorld method
                Console.WriteLine("Test 1: Calling HelloWorld()");
                string helloMessage = simpleObject.HelloWorld();
                Console.WriteLine("Result: " + helloMessage + "\n");

                // Test 2: Get process and thread IDs
                Console.WriteLine("Test 2: Getting Process and Thread IDs");
                int processId, threadId;
                simpleObject.GetProcessThreadId(out processId, out threadId);
                Console.WriteLine("Server Process ID: " + processId);
                Console.WriteLine("Server Thread ID: " + threadId);
                Console.WriteLine("Client Process ID: " + System.Diagnostics.Process.GetCurrentProcess().Id);
                Console.WriteLine("Note: Server and Client process IDs should be different (out-of-process COM)\n");

                // Test 3: FloatProperty - Get and Set
                Console.WriteLine("Test 3: Testing FloatProperty");
                Console.WriteLine("Initial value: " + simpleObject.FloatProperty);

                Console.WriteLine("\nSetting FloatProperty to 3.14...");
                simpleObject.FloatProperty = 3.14f;
                Console.WriteLine("Current value: " + simpleObject.FloatProperty);

                Console.WriteLine("\nSetting FloatProperty to 2.71...");
                simpleObject.FloatProperty = 2.71f;
                Console.WriteLine("Current value: " + simpleObject.FloatProperty + "\n");

                // Test 4: Demonstrating event cancellation
                Console.WriteLine("Test 4: Demonstrating event cancellation");
                Console.WriteLine("Setting FloatProperty to 999.0 (this will be cancelled in the event handler)...");
                simpleObject.FloatProperty = 999.0f;
                Console.WriteLine("Current value after attempted change: " + simpleObject.FloatProperty);
                Console.WriteLine("Note: Value should remain unchanged due to event cancellation.\n");

                // Clean up
                Console.WriteLine("Releasing COM object...");
                Marshal.ReleaseComObject(simpleObject);
                simpleObject = null;

                Console.WriteLine("\nAll tests completed successfully!");
            }
            catch (COMException comEx)
            {
                Console.WriteLine("COM Error occurred:");
                Console.WriteLine("Error Code: 0x" + comEx.ErrorCode.ToString("X"));
                Console.WriteLine("Message: " + comEx.Message);
                Console.WriteLine("\nMake sure CSExeCOMServer.exe is registered using:");
                Console.WriteLine("Regasm.exe CSExeCOMServer.exe");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
                Console.WriteLine("Stack trace: " + ex.StackTrace);
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        // Event handler for FloatPropertyChanging event
        private static void SimpleObject_FloatPropertyChanging(float NewValue, ref bool Cancel)
        {
            Console.WriteLine("  [Event] FloatPropertyChanging fired!");
            Console.WriteLine("  [Event] New value: " + NewValue);

            // Cancel the change if the new value is greater than 100
            if (NewValue > 100.0f)
            {
                Console.WriteLine("  [Event] Cancelling the change (value > 100)");
                Cancel = true;
            }
            else
            {
                Console.WriteLine("  [Event] Accepting the change");
                Cancel = false;
            }
        }
    }
}
