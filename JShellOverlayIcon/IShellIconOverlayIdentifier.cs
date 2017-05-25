using System;
using System.Runtime.InteropServices;

namespace JShellOverlayIcon
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
        void GetOverlayInfo(IntPtr iconFileBuffer, int iconFileBufferSize, out int iconIndex, [MarshalAs(UnmanagedType.U4)] out ISIOI flags);

        [PreserveSig]
        void GetPriority(out int priority);
    }

    [Flags]
    public enum ISIOI
    {
        ISIOI_ICONFILE = 1,
        ISIOI_ICONINDEX = 2
    }
}