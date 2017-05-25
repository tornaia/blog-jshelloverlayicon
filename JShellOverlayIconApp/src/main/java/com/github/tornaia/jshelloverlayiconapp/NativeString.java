package com.github.tornaia.jshelloverlayiconapp;

import com.sun.jna.Memory;
import com.sun.jna.Native;
import com.sun.jna.Pointer;

import java.nio.CharBuffer;

/**
 * Provides a temporary allocation of an immutable C string
 * (<code>const char*</code> or <code>const wchar_t*</code>) for use when
 * converting a Java String into a native memory function argument.
 */
public class NativeString implements CharSequence, Comparable {

    private Pointer pointer;

    /**
     * Create a native string as a NUL-terminated array of <code>wchar_t</code>.
     * <p>
     * If the system property <code>jna.encoding</code> is set, its value will
     * be used to encode the native <code>char</code>string.
     * If not set or if the encoding is unavailable, the default platform
     * encoding will be used.
     *
     * @param string value to write to native memory
     */
    public NativeString(String string) {
        if (string == null) {
            throw new IllegalArgumentException("String must not be null");
        }

        // Allocate the memory to hold the string.  Note, we have to
        // make this 1 element longer in order to accommodate the terminating
        // NUL (which is generated in Pointer.setString()).
        int len = (string.length() + 1) * Native.WCHAR_SIZE;
        pointer = new Memory(len);
        pointer.setWideString(0, string);
    }

    public Pointer getPointer() {
        return pointer;
    }

    @Override
    public char charAt(int index) {
        return toString().charAt(index);
    }

    @Override
    public int length() {
        return toString().length();
    }

    @Override
    public CharSequence subSequence(int start, int end) {
        return CharBuffer.wrap(toString()).subSequence(start, end);
    }

    @Override
    public int compareTo(Object other) {
        if (other == null) {
            return 1;
        }

        return toString().compareTo(other.toString());
    }

    @Override
    public int hashCode() {
        return toString().hashCode();
    }

    @Override
    public boolean equals(Object other) {
        if (other instanceof CharSequence) {
            return compareTo(other) == 0;
        }

        return false;
    }

    @Override
    public String toString() {
        return "const wchar_t*(" + pointer.getString(0) + ")";
    }
}