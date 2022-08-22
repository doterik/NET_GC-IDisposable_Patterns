// -----------------------------------------------------------------
// <copyright file="BitmapImage.cs" company="DCOM Engineering, LLC">
//     Copyright (c) DCOM Engineering, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------

namespace NET_GC.Dispose_Pattern_with_SafeHandle;

using System;
using System.Drawing;

public class BitmapImage : IDisposable
{
    private readonly Bitmap image;
    public BitmapImage(Bitmap image)
    {
        this.image = image;
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
        }

        disposed = true;
    }
}
