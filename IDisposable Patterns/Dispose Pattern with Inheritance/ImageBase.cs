// ---------------------------------------------------------------
// <copyright file="ImageBase.cs" company="DCOM Engineering, LLC">
//     Copyright (c) DCOM Engineering, LLC. All rights reserved.
// </copyright>
// ---------------------------------------------------------------

namespace NET_GC.Dispose_Pattern_with_Inheritance;

using System;
using System.Diagnostics;
using System.Drawing;

public abstract class ImageBase : IDisposable
{
#pragma warning disable SA1401 // Fields should be private
    protected readonly Image image;
#pragma warning restore SA1401 // Fields should be private
    protected ImageBase(Image image) // public
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
        Debug.WriteLine($"{nameof(ImageBase)}.{nameof(Dispose)}({disposing}) by {(disposing ? "user code" : "garbage collection.")}");

        if (disposed) return;

        if (disposing)
        {
            image.Dispose();
        }

        disposed = true;
    }
}
