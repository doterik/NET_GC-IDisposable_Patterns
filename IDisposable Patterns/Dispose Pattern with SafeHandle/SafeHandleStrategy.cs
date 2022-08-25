// ------------------------------------------------------------------------
// <copyright file="SafeHandleStrategy.cs" company="DCOM Engineering, LLC">
//     Copyright (c) DCOM Engineering, LLC. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace NET_GC.Dispose_Pattern_with_SafeHandle;

public class SafeHandleStrategy : FileStrategy
{
    public override IEnumerable<DebugAllocationData> Run()
    {
#pragma warning disable MA0025  // Implement the functionality instead of throwing NotImplementedException.
        throw new NotImplementedException();
    }
}
