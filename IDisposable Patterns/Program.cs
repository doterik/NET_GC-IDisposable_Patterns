﻿// -------------------------------------------------------------
// <copyright file="Program.cs" company="DCOM Engineering, LLC">
//     Copyright (c) DCOM Engineering, LLC. All rights reserved.
// </copyright>
// -------------------------------------------------------------

namespace NET_GC;

using System;
using System.Collections.Generic;
using Basic_Dispose_Pattern;
using Dispose_Pattern_with_Finalizer;
using Dispose_Pattern_with_Inheritance;
using Memory_Leak;

#pragma warning disable MA0051  // Method is too long.
#pragma warning disable S2190   // Recursion should not be infinite.
#pragma warning disable S2479   // Whitespace and control characters in string literals should be explicit.

internal static class Program
{
    private static void Main()
    {
    start:
        Console.WriteLine("""
                .NET Garbage Collector (GC) Demo. Copyright © 2018 DCOM Engineering, LLC

                Available Commands:
                cls                         Clears the screen.
                clr                         Clears references to BitmapImages so they can be collected.
                leak_strategy               Performs allocation of BitmapImage and holds references to cause a memory leak.
                deterministic_strategy      Performs allocation of BitmapImage and explicitly disposes of them.
                indeterministic_strategy    Performs allocation of BitmapImage and lets the GC dispose of them.
                finalizer_strategy          Performs allocation of BitmapImage and frees the resources through finalization.
                inheritance_strategy        Performs allocation of BitmapImage / ImageBase and explicitly disposes of them.
                gc                          Forces garbage collection and waits for pending finalizers.
                exit                        Exits the application.
                """);

        while (true)
        {
            Console.Write(@"DCOM:\> "); string? input = Console.ReadLine();
            Console.WriteLine();

            Strategy? strategy = null;

            switch (input?.ToLowerInvariant())
            {
#pragma warning disable S907 // "goto" statement should not be used.
                case "cls":
                    Console.Clear();
                    goto start;
#pragma warning restore S907 // "goto" statement should not be used.
                case "clr":
                    MemoryLeakStrategy.ZeroRefCount();
                    break;
                case "leak_strategy":
                    strategy = new MemoryLeakStrategy();
                    break;
                case "deterministic_strategy":
                    strategy = new BasicStrategy(BasicStrategyMode.Deterministic);
                    break;
                case "indeterministic_strategy":
                    strategy = new BasicStrategy(BasicStrategyMode.Indeterministic);
                    break;
                case "finalizer_strategy":
                    strategy = new FinalizerStrategy();
                    break;
                case "inheritance_strategy":
                    strategy = new InheritanceStrategy();
                    break;
                case "gc":
                    strategy = new GCCollectStrategy();
                    break;
                case "exit":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Not a known strategy.");
                    Console.WriteLine();
                    continue;
            }

            IEnumerable<DebugAllocationData>? data = strategy?.Run();

            if (data is not null) Screen.Print(data);
        }
    }
}
