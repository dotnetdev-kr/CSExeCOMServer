/****************************** Module Header ******************************\
* Module Name:  SimpleObject.cs
* Project:      CSExeCOMServer
* Copyright (c) Microsoft Corporation.
* 
* The definition of the COM class, SimpleObject, and its ClassFactory, 
* SimpleObjectClassFactory.
* 
* (Please generate new GUIDs when you are writing your own COM server) 
* Program ID: CSExeCOMServer.SimpleObject
* CLSID_SimpleObject: DB9935C1-19C5-4ed2-ADD2-9A57E19F53A3
* IID_ISimpleObject: 941D219B-7601-4375-B68A-61E23A4C8425
* DIID_ISimpleObjectEvents: 014C067E-660D-4d20-9952-CD973CE50436
* 
* Properties:
* // With both get and set accessor methods
* float FloatProperty
* 
* Methods:
* // HelloWorld returns a string "HelloWorld"
* string HelloWorld();
* // GetProcessThreadID outputs the running process ID and thread ID
* void GetProcessThreadID(out int processId, out int threadId);
* 
* Events:
* // FloatPropertyChanging is fired before new value is set to the 
* // FloatProperty property. The Cancel parameter allows the client to cancel 
* // the change of FloatProperty.
* void FloatPropertyChanging(float NewValue, ref bool Cancel);
* 
* This source is subject to the Microsoft Public License.
* See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
* All other rights reserved.
* 
* THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
* EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
* WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace CSExeCOMServer
{
    [ComVisible(true)]
    [Guid("DB9935C1-19C5-4ed2-ADD2-9A57E19F53A3")]
    [ClassInterface(ClassInterfaceType.None)] // No ClassInterface
    [ComSourceInterfaces(typeof(ISimpleObjectEvents))]
    public class SimpleObject : ReferenceCountedObject, ISimpleObject
    {
        // These routines perform the additional COM registration needed by 
        // the service.

        [ComRegisterFunction]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Register(Type t)
        {
            try
            {
                HelperMethods.RegasmRegisterLocalServer(t);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log the error
                throw ex; // Re-throw the exception
            }
        }

        [ComUnregisterFunction]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Unregister(Type t)
        {
            try
            {
                HelperMethods.RegasmUnregisterLocalServer(t);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log the error
                throw ex; // Re-throw the exception
            }
        }

        private float fField = 0f;

        public float FloatProperty
        {
            get { return this.fField; }
            set
            {
                bool cancel = false;
                // Raise the event FloatPropertyChanging
                if (null != FloatPropertyChanging)
                    FloatPropertyChanging(value, ref cancel);
                if (!cancel)
                    this.fField = value;
            }
        }

        public string HelloWorld()
        {
            return "HelloWorld";
        }

        public void GetProcessThreadId(out int processId, out int threadId)
        {
            processId = NativeMethods.GetCurrentProcessId();
            threadId = NativeMethods.GetCurrentThreadId();
        }

        [ComVisible(false)]
        public delegate void FloatPropertyChangingEventHandler(float NewValue, ref bool Cancel);

        public event FloatPropertyChangingEventHandler FloatPropertyChanging;
    }
}
