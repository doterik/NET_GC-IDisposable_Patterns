global using System.Diagnostics;
global using System.Drawing;
global using System.Runtime.InteropServices;
global using System.Runtime.Versioning;
global using Microsoft.Win32.SafeHandles;
global using NET_GC.Basic_Dispose_Pattern;
global using NET_GC.Dispose_Pattern_with_Finalizer;
global using NET_GC.Dispose_Pattern_with_Inheritance;
global using NET_GC.Memory_Leak;

// -------------------------------------------------------------
// <copyright file="Program.cs" company="DCOM Engineering, LLC">
//     Copyright (c) DCOM Engineering, LLC. All rights reserved.
// </copyright>
// -------------------------------------------------------------

namespace NET_GC;

using ConsoleMenu.Light; /* 'Color' is else ambiguous between 'System.Drawing' and 'ConsoleMenu.Light'. */

internal static class Program
{
    private static void Main()
    {
        string[] menuItems = new[] {
            /*0*/ "cls                        Clears the screen.",
            /*1*/ "clr                        Clears references to BitmapImages so they can be collected.",
            /*2*/ "leak_strategy              Performs allocation of BitmapImage and holds references to cause a memory leak.",
            /*3*/ "deterministic_strategy     Performs allocation of BitmapImage and explicitly disposes of them.",
            /*4*/ "indeterministic_strategy   Performs allocation of BitmapImage and lets the GC dispose of them.",
            /*5*/ "finalizer_strategy         Performs allocation of BitmapImage and frees the resources through finalization.",
            /*6*/ "inheritance_strategy       Performs allocation of BitmapImage / ImageBase and explicitly disposes of them.",
            /*7*/ "gc                         Forces garbage collection and waits for pending finalizers.",
            /*8*/ "exit                       Exits the application." };

        Action?[] actions = new[] {
            /*0*/ () => Console.Clear(),
            /*1*/ () => MemoryLeakStrategy.ZeroRefCount(),
            /*2*/ () => Screen.Print(new MemoryLeakStrategy()?.Run()),
            /*3*/ () => Screen.Print(new BasicStrategy(BasicStrategyMode.Deterministic)?.Run()),
            /*4*/ () => Screen.Print(new BasicStrategy(BasicStrategyMode.Indeterministic)?.Run()),
            /*5*/ () => Screen.Print(new FinalizerStrategy()?.Run()),
            /*6*/ () => Screen.Print(new InheritanceStrategy()?.Run()),
            /*7*/ () => Screen.Print(new GCCollectStrategy()?.Run()),
            /*8*/ () => Environment.Exit(0) };

        Menu menu = new(
             menuTitle: ".NET Garbage Collector (GC) Demo. Copyright © 2018 DCOM Engineering, LLC",
             menuItems,
             actions,           // optional
             clearScreen: true, // optional
             menuColor: new Color(ConsoleColor.Blue, ConsoleColor.White),      // optional
             itemsColor: new Color(ConsoleColor.Yellow, ConsoleColor.Black),   // optional
             selectedColor: new Color(ConsoleColor.White, ConsoleColor.Red));  // optional

        int selection = 0;

        while (menu.DisplayMenu(ref selection) is >= 0 and < 8) { /* empty */ }

        Environment.Exit(-1); // = Esc

        // while (true) /* Without predefined actions. */
        // {
        //     switch (menu.DisplayMenu())
        //     {
        //         case 0: Console.Clear(); break;
        //         case 1: MemoryLeakStrategy.ZeroRefCount(); break;
        //         /* ... */
        //         default: Console.WriteLine("Try again..."); break;
        //     }
        // }
    }
}
