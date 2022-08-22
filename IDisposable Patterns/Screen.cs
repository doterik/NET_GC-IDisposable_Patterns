// ------------------------------------------------------------
// <copyright file="Screen.cs" company="DCOM Engineering, LLC">
//     Copyright (c) DCOM Engineering, LLC. All rights reserved.
// </copyright>
// ------------------------------------------------------------

namespace NET_GC;

using System;
using System.Collections.Generic;

public static class Screen
{
    public static void Print(IEnumerable<DebugAllocationData> data)
    {
        // int cursorLeft = Console.CursorLeft, cursorTop = Console.CursorTop;

        ConsoleColor oldFgColor = Console.ForegroundColor;
        Console.WriteLine($"Before Allocation (Bytes)    After Allocation (Bytes)    After Dispose (Bytes){Environment.NewLine}");

        DebugAllocationData? previousData = null;

        foreach (DebugAllocationData row in data)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"{row.BeforeAllocBytes,-29}{row.AfterAllocBytes,-28}{row.AfterDisposeBytes,-21}");

            if (previousData is not null)
            {
                bool isGreater = row.AfterDisposeBytes >= previousData.AfterDisposeBytes;

                long change = isGreater
                    ? previousData.AfterDisposeBytes - row.AfterDisposeBytes
                    : row.AfterDisposeBytes - previousData.AfterDisposeBytes;

                change = Math.Abs(change) / 1024;

                Console.ForegroundColor = isGreater ? ConsoleColor.Red : ConsoleColor.Green;
                Console.Write($"{(isGreater ? "-" : "+")} {change} kb");
                Console.ForegroundColor = oldFgColor;
            }

            previousData = row;

            Console.WriteLine();
        }

        Console.WriteLine();
    }
}
