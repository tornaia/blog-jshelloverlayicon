package com.github.tornaia.jshelloverlayiconapp;

import com.sun.jna.Pointer;
import com.sun.jna.platform.win32.WinDef;
import org.springframework.stereotype.Component;

import java.nio.file.Path;

@Component
public class FileStatusService {

    private static final long SHCNE_UPDATEITEM = 0x00002000;
    private static final int SHCNF_PATHW = 0x0005;

    public FileStatus getFileStatus(Path path) {
        String absolutePath = path.toAbsolutePath().toString();

        if (!absolutePath.contains("SampleSyncDirectory")) {
            return FileStatus.SKIP;
        } else if (absolutePath.contains("syncing")) {
            return FileStatus.SYNCING;
        } else if (absolutePath.contains("ignored")) {
            return FileStatus.IGNORED;
        } else if (absolutePath.contains("unsynced")) {
            return FileStatus.UNSYNCED;
        }

        return FileStatus.SYNCED;
    }

    public void refreshOverlayIcon(Path path) {
        String absolutePath = path.toFile().getAbsolutePath();
        Pointer filePointer = new NativeString(absolutePath).getPointer();
        Shell32.INSTANCE.SHChangeNotify(new WinDef.LONG(SHCNE_UPDATEITEM), new WinDef.UINT(SHCNF_PATHW), new WinDef.LPVOID(filePointer), new WinDef.LPVOID(Pointer.NULL));
    }
}
