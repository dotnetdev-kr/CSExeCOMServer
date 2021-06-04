using System;
using System.Runtime.InteropServices;

namespace CSExeCOMServer
{
    [ComVisible(true)]
    [Guid("014C067E-660D-4d20-9952-CD973CE50436")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface ISimpleObjectEvents
    {
        [DispId(1)]
        void FloatPropertyChanging(float NewValue, ref bool Cancel);
    }
}
