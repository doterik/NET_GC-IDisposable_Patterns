// -----------------------------------------------------------------
// <copyright file="BitmapImage.cs" company="DCOM Engineering, LLC">
//     Copyright (c) DCOM Engineering, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------

namespace NET_GC.Dispose_Pattern_with_Finalizer;

public class BitmapImage : IDisposable
{
    private readonly IntPtr image;
    public BitmapImage(string filename)
    {
        _ = GdipLoadImageFromFile(filename, out image);
        _ = GdipImageForceValidation(new HandleRef(wrapper: null, image));
    }

#pragma warning disable MA0055 // Do not use finalizer.

    ~BitmapImage()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    private bool disposed;
    protected virtual void Dispose(bool disposing)
    {
        Debug.WriteLine($"{nameof(BitmapImage)}.{nameof(Dispose)}({disposing}) by {(disposing ? "user code" : "garbage collection.")}");

        if (disposed) return;

        _ = IntGdipDisposeImage(new HandleRef(wrapper: null, image));

        disposed = true;
    }

#pragma warning disable SYSLIB1054 // Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time.

    [DllImport("gdiplus.dll", CharSet = CharSet.Unicode)]
    private static extern int GdipLoadImageFromFile(string filename, out IntPtr image);

    [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
    [ResourceExposure(ResourceScope.None)]
    private static extern int GdipImageForceValidation(HandleRef image);

    [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, EntryPoint = "GdipDisposeImage", CharSet = CharSet.Unicode)] // 3 = Unicode
    [ResourceExposure(ResourceScope.None)]
    private static extern int IntGdipDisposeImage(HandleRef image);
}
