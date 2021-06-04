using System;
using System.Runtime.InteropServices;

namespace CSExeCOMServer
{
    [ComVisible(true)]
    [Guid("941D219B-7601-4375-B68A-61E23A4C8425")]
    public interface ISimpleObject
    {
        float FloatProperty { get; set; }

        string HelloWorld();

        void GetProcessThreadId(out int processId, out int threadId);
    }
}
