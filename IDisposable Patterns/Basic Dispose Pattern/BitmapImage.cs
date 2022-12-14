// -----------------------------------------------------------------
// <copyright file="BitmapImage.cs" company="DCOM Engineering, LLC">
//     Copyright (c) DCOM Engineering, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------

namespace NET_GC.Basic_Dispose_Pattern;

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
        Debug.WriteLine($"{nameof(BitmapImage)}.{nameof(Dispose)}({disposing}) by {(disposing ? "user code" : "garbage collection.")}");

        if (disposed) return;

        if (disposing)
        {
            image.Dispose();
        }

        disposed = true;
    }
}
