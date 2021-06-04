using System;
using System.Runtime.InteropServices;

namespace CSExeCOMServer
{
    /// <summary>
    /// Class factory for the class SimpleObject.
    /// </summary>
    internal class SimpleObjectClassFactory : IClassFactory
    {
        public int CreateInstance(IntPtr pUnkOuter, ref Guid riid, 
            out IntPtr ppvObject)
        {
            ppvObject = IntPtr.Zero;

            if (pUnkOuter != IntPtr.Zero)
            {
                // The pUnkOuter parameter was non-NULL and the object does 
                // not support aggregation.
                Marshal.ThrowExceptionForHR(NativeMethods.CLASS_E_NOAGGREGATION);
            }

            if (riid == HelperMethods.GetGuidFromType(typeof(SimpleObject)) ||
                riid == new Guid(NativeMethods.IID_IDispatch) ||
                riid == new Guid(NativeMethods.IID_IUnknown))
            {
                // Create the instance of the .NET object
                ppvObject = Marshal.GetComInterfaceForObject(
                    new SimpleObject(), typeof(ISimpleObject));
            }
            else
            {
                // The object that ppvObject points to does not support the 
                // interface identified by riid.
                Marshal.ThrowExceptionForHR(NativeMethods.E_NOINTERFACE);
            }

            return 0;   // S_OK
        }

        public int LockServer(bool fLock)
        {
            return 0;   // S_OK
        }
    }
}
