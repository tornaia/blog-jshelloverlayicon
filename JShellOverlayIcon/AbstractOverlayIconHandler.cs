using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;
using System.IO;
using System.Reflection;

namespace JShellOverlayIconHandler
{
    [ComVisible(false)]
    public abstract class AbstractOverlayIconHandler : IShellIconOverlayIdentifier
    {
        private const int HIGHEST_PRIORITY = 0;
        private const int ISIOI_ICONFILE = 1;

        private const int S_OK = 0;
        private const int S_FALSE = 1;

        public string IconFilePath { get; set; }


        protected AbstractOverlayIconHandler(string iconFileName)
        {
            var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var iconFilePath = Path.GetFullPath(Path.Combine(assemblyDirectory, iconFileName));
            IconFilePath = iconFilePath;
        }

        protected virtual Boolean IsHandled(string absolutePath, int attributes)
        {
            return false;
        }

        #region Registry
        [ComRegisterFunction]
        public static void Register(Type type)
        {
            RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\ShellIconOverlayIdentifiers\    " + type.Name);
            registryKey.SetValue(string.Empty, type.GUID.ToString("B").ToUpper());
            registryKey.Close();
            Shell32Utils.FileAssociationsChanged();
        }

        [ComUnregisterFunction]
        public static void Unregister(Type type)
        {
            Registry.LocalMachine.DeleteSubKeyTree(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\ShellIconOverlayIdentifiers\    " + type.Name);
            Shell32Utils.FileAssociationsChanged();
        }
        #endregion

        #region IShellIconOverlayIdentifier Members
        int IShellIconOverlayIdentifier.IsMemberOf(string absolutePath, int attributes)
        {
            return IsHandled(absolutePath, attributes) ? S_OK : S_FALSE;
        }

        int IShellIconOverlayIdentifier.GetOverlayInfo(IntPtr iconFileBuffer, int iconFileBufferSize, out int iconIndex, out uint flags)
        {
            WriteToIntPtr(IconFilePath, iconFileBuffer, iconFileBufferSize * 2);
            iconIndex = 0;
            flags = ISIOI_ICONFILE;
            return S_OK;
        }

        int IShellIconOverlayIdentifier.GetPriority(out int priority)
        {
            priority = HIGHEST_PRIORITY;
            return S_OK;
        }

        private static void WriteToIntPtr(string value, IntPtr destination, int bufferSize)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(value);
            int length = bytes.Length;
            if (length + 2 > bufferSize)
            {
                length = bufferSize - 2;
            }
            Marshal.Copy(bytes, 0, destination, length);
            Marshal.WriteByte(destination, length, 0);
            Marshal.WriteByte(destination, length + 1, 0);
        }
        #endregion
    }
}
