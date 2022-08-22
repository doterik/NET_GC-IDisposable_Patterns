// -----------------------------------------------------------------
// <copyright file="BitmapImage.cs" company="DCOM Engineering, LLC">
//     Copyright (c) DCOM Engineering, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------

namespace NET_GC.Dispose_Pattern_with_Inheritance;

using System.Diagnostics;
using System.Drawing;

public class BitmapImage : ImageBase
{
    public BitmapImage(Bitmap image) : base(image) { }

    // public void Dispose() /* base */
    // {
    //     Dispose(true);
    //     GC.SuppressFinalize(this);
    // }

    private bool disposed;
    protected override void Dispose(bool disposing)
    {
        Debug.WriteLine($"{nameof(BitmapImage)}.{nameof(Dispose)}({disposing}) by {(disposing ? "user code" : "garbage collection.")}");

        if (disposed) return;

        if (disposing)
        {
            image.Dispose();
        }

        disposed = true;

        base.Dispose(disposing);
    }
}
