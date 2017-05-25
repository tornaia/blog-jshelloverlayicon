package com.github.tornaia.jshelloverlayiconapp;

import com.sun.jna.Library;
import com.sun.jna.Native;
import com.sun.jna.platform.win32.WinDef;
import com.sun.jna.win32.W32APIFunctionMapper;
import com.sun.jna.win32.W32APITypeMapper;

import java.util.HashMap;
import java.util.Map;

public interface Shell32 extends com.sun.jna.platform.win32.Shell32 {

    Map<String, Object> WIN32API_OPTIONS = new HashMap<String, Object>() {
        private static final long serialVersionUID = 1L;

        {
            put(Library.OPTION_FUNCTION_MAPPER, W32APIFunctionMapper.UNICODE);
            put(Library.OPTION_TYPE_MAPPER, W32APITypeMapper.UNICODE);
        }
    };

    Shell32 INSTANCE = (Shell32) Native.loadLibrary("Shell32", Shell32.class, WIN32API_OPTIONS);

    void SHChangeNotify(WinDef.LONG wEventId, WinDef.UINT uFlags, WinDef.LPVOID dwItem1, WinDef.LPVOID dwItem2);
}
