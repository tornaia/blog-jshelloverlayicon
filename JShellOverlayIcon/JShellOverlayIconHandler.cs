using System.Runtime.InteropServices;
using System;

namespace JShellOverlayIconHandler
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("19841221-F0EE-4A04-8E8C-0D8698CD0001")]
    public class MyCloud1SyncedOverlayIcon : AbstractOverlayIconHandler
    {
        public MyCloud1SyncedOverlayIcon() : base(@"icon_synced.ico")
        {
        }

        protected override bool IsHandled(string absolutePath, int attributes)
        {
            var fileStatus = JavaRestClient.GetFileStatus(absolutePath);
            return fileStatus == FileStatus.SYNCED;
        }
    }

    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("19841221-F0EE-4A04-8E8C-0D8698CD0002")]
    public class MyCloud2SyncingOverlayIcon : AbstractOverlayIconHandler
    {
        public MyCloud2SyncingOverlayIcon() : base(@"icon_syncing.ico")
        {
        }

        protected override bool IsHandled(string absolutePath, int attributes)
        {
            var fileStatus = JavaRestClient.GetFileStatus(absolutePath);
            return fileStatus == FileStatus.SYNCING;
        }
    }

    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("19841221-F0EE-4A04-8E8C-0D8698CD0003")]
    public class MyCloud3IgnoredOverlayIcon : AbstractOverlayIconHandler
    {
        public MyCloud3IgnoredOverlayIcon() : base(@"icon_ignored.ico")
        {
        }

        protected override bool IsHandled(string absolutePath, int attributes)
        {
            var fileStatus = JavaRestClient.GetFileStatus(absolutePath);
            return fileStatus == FileStatus.IGNORED;
        }

    }
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("19841221-F0EE-4A04-8E8C-0D8698CD0004")]
    public class MyCloud4UnsyncedOverlayIcon : AbstractOverlayIconHandler
    {
        public MyCloud4UnsyncedOverlayIcon() : base(@"icon_unsynced.ico")
        {
        }

        protected override bool IsHandled(string absolutePath, int attributes)
        {
            var fileStatus = JavaRestClient.GetFileStatus(absolutePath);
            return fileStatus == FileStatus.UNSYNCED;
        }
    }
}
