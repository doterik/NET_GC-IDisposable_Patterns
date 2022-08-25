// ---------------------------------------------------------------------
// <copyright file="UnmanagedBitmap.cs" company="DCOM Engineering, LLC">
//     Copyright (c) DCOM Engineering, LLC. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------

namespace NET_GC;

public class UnmanagedBitmap : IDisposable
{
    private readonly Bitmap image;
    private readonly GdiBitmapSafeHandle handle;
    public UnmanagedBitmap(string file)
    {
        image = (Bitmap)Image.FromFile(file);
        handle = new GdiBitmapSafeHandle(image.GetHbitmap());
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    private bool disposed;
    protected virtual void Dispose(bool disposing)
    {
        if (disposed) return;

        if (disposing)
        {
            image.Dispose();

            if (!handle.IsInvalid) handle.Dispose();
        }

        disposed = true;
    }
}
