// -----------------------------------------------------------------------
// <copyright file="FinalizerStrategy.cs" company="DCOM Engineering, LLC">
//     Copyright (c) DCOM Engineering, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace NET_GC.Dispose_Pattern_with_Finalizer;

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

public class FinalizerStrategy : FileStrategy
{
    private bool isInitialized;
    private IntPtr initToken;

    public override IEnumerable<DebugAllocationData> Run()
    {
        if (!isInitialized)
        {
            StartupInput input = StartupInput.GetDefault();

            _ = GdiplusStartup(out initToken, ref input, out StartupOutput output);

            AppDomain.CurrentDomain.ProcessExit += (sender, e)
                => GdiplusShutdown(new HandleRef(wrapper: null, initToken));

            isInitialized = true;
        }

        for (int i = 0; i < 10; i++)
        {
            long beforeAlloc = Environment.WorkingSet;

#pragma warning disable S1481 // Unused local variables should be removed
            BitmapImage image = new(FilePath); // using
#pragma warning restore S1481 // Unused local variables should be removed

            long afterAlloc = Environment.WorkingSet;

            long afterDispose = Environment.WorkingSet;

            yield return new DebugAllocationData(beforeAlloc, afterAlloc, afterDispose);
        }
    }

#pragma warning disable SYSLIB1054 // Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time.

    [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
    [ResourceExposure(ResourceScope.Process)]
    private static extern int GdiplusStartup(out IntPtr token, ref StartupInput input, out StartupOutput output);

    [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
    [ResourceExposure(ResourceScope.None)]
    private static extern void GdiplusShutdown(HandleRef token);

#pragma warning disable S1144 // Unused private types or members should be removed.

    [StructLayout(LayoutKind.Sequential)]
    private struct StartupInput
    {
        public int GdiplusVersion;
        public IntPtr DebugEventCallback;
        public bool SuppressBackgroundThread;
        public bool SuppressExternalCodecs;

        public static StartupInput GetDefault()
        {
            return new StartupInput
            {
                GdiplusVersion = 1,
                SuppressBackgroundThread = false,
                SuppressExternalCodecs = false
            };
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct StartupOutput
    {
        public IntPtr Hook;
        public IntPtr Unhook;
    }
}
