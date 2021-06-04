using System.Runtime.InteropServices;

namespace CSExeCOMServer
{
    /// <summary>
    /// Reference counted object base.
    /// </summary>
    [ComVisible(false)]
    public class ReferenceCountedObject
    {
        public ReferenceCountedObject()
        {
            // Increment the lock count of objects in the COM server.
            ExecutableComServer.Instance.Lock();
        }

        ~ReferenceCountedObject()
        {
            // Decrement the lock count of objects in the COM server.
            ExecutableComServer.Instance.Unlock();
        }
    }
}
