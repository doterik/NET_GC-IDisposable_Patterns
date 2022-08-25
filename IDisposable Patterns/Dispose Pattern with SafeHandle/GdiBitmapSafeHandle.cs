// -------------------------------------------------------------------------
// <copyright file="GdiBitmapSafeHandle.cs" company="DCOM Engineering, LLC">
//     Copyright (c) DCOM Engineering, LLC. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------

namespace NET_GC;

public class GdiBitmapSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    public GdiBitmapSafeHandle(IntPtr handle) : base(ownsHandle: true)
    {
        SetHandle(handle);
    }

    protected override bool ReleaseHandle()
    {
        return DeleteObject(handle);
    }

#pragma warning disable SYSLIB1054 // Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time.

    [DllImport("gdi32.dll")]
    private static extern bool DeleteObject(IntPtr hObject);
}
