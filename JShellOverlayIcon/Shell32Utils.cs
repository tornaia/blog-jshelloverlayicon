using System;
using System.Runtime.InteropServices;

namespace JShellOverlayIcon
{
    public class Shell32Utils
    {
        private const int SHCNE_ASSOCCHANGED = 0x08000000;
        private const int SHCNF_IDLIST = 0x0;

        [DllImport("shell32.dll")]
        private static extern void SHChangeNotify(int eventID, uint flags, IntPtr item1, IntPtr item2);

        public static void FileAssociationsChanged()
        {
            SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_IDLIST, IntPtr.Zero, IntPtr.Zero);
        }
    }
}
