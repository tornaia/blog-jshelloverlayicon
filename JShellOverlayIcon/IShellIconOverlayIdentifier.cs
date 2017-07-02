using System;
using System.Runtime.InteropServices;

namespace JShellOverlayIconHandler
{
    [ComVisible(false)]
    [ComImport]
    [Guid("0C6C4200-C589-11D0-999A-00C04FD655E1")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IShellIconOverlayIdentifier
    {
        [PreserveSig]
        int IsMemberOf([MarshalAs(UnmanagedType.LPWStr)] string absolutePath, [MarshalAs(UnmanagedType.U4)] int attributes);

        [PreserveSig]
        int GetOverlayInfo(IntPtr iconFileBuffer, int iconFileBufferSize, out int iconIndex, out uint flags);

        [PreserveSig]
        int GetPriority(out int priority);
    }
}