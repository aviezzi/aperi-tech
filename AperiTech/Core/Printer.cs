﻿// file-scoped namespaces: C# 10.0
// NEW: https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-10#file-scoped-namespace-declaration
// DOC: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-10.0/file-scoped-namespaces
// DOC: https://devblogs.microsoft.com/dotnet/welcome-to-csharp-10/#file-scoped-namespaces

namespace AperiTech.Core;

using Abstract;
using Domain;
using Microsoft.Extensions.Options;
using Options;

public class Printer : IPrinter
{
    private readonly AppOptions _options;

    public Printer(IOptions<AppOptions> options)
    {
        _options = options.Value;
    }

    public void Print(IEnumerable<Shape> shapes)
    {
        // local functions: C# 7.0
        // static local functions: C# 8.0
        // NEW: https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-8#static-local-functions
        // DOC: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/local-functions
        static void LocalPrint(string message)
        {
            Console.Write("    ");
            Console.Write($"MESSAGE: {message}");
            Console.WriteLine();
        }

        foreach (var shape in shapes)
        {
            shape.WriteToConsole("Printer", false);
            LocalPrint(GetMessage(shape));

            Console.WriteLine();

            Thread.Sleep(_options.Settings.Delay);
        }
    }

    private static string GetMessage(Shape shape)
    {
        // Pattern matching: C# 8.0
        // NEW: https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-8#more-patterns-in-more-places
        // DOC: https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/pattern-matching
        // DOC: https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-8#switch-expressions
        return shape.Angles switch
        {
            // string interpolation: C# 6.0
            // DOC: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated
            0 => string.Equals(shape.Color, "Red", StringComparison.OrdinalIgnoreCase) ? $"ID={shape.Id} CIRCLE is red" : $"ID={shape.Id} CIRCLE is {shape.Color.ToUpperInvariant()}",
            4 => string.Equals(shape.Color, "REd", StringComparison.OrdinalIgnoreCase) ? $"ID={shape.Id} SQUARE is red" : $"ID={shape.Id} SQUARE is {shape.Color.ToUpperInvariant()}",
            _ => throw new ArgumentOutOfRangeException(nameof(shape))
        };
    }
}