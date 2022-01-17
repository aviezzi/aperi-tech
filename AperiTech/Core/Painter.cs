// file-scoped namespaces: C# 10.0
// NEW: https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-10#file-scoped-namespace-declaration
// DOC: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-10.0/file-scoped-namespaces
// DOC: https://devblogs.microsoft.com/dotnet/welcome-to-csharp-10/#file-scoped-namespaces

namespace AperiTech.Core;

using Abstract;
using Bogus;
using Domain;
using Microsoft.Extensions.Options;
using Options;

public class Painter : IPainter
{
    private readonly Faker _faker;
    private readonly AppOptions _options;

    public Painter(Faker faker, IOptions<AppOptions> options)
    {
        _faker = faker;
        _options = options.Value;
    }

    public IEnumerable<IShape> Paint(IEnumerable<IShape> shapes)
    {
        var acc = new List<IShape>();

        // using declaration: C# 8.0
        // NEW: https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-8#using-declarations
        // DOC: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-statement
        using var enumerator = shapes.GetEnumerator();
        while (enumerator.MoveNext())
        {
            var current = enumerator.Current as Shape;
            // with expression: C# 9.0
            // DOC: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/with-expression
            // nullable reference types (!): C# 8.0
            // NEW: https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-8#nullable-reference-types
            // DOC: https://docs.microsoft.com/en-us/dotnet/csharp/nullable-warnings#possible-dereference-of-null
            // DOC: https://docs.microsoft.com/en-us/dotnet/csharp/nullable-references#nullable-variable-annotations 
            IShape shape = current! with {Color = _faker.PickRandom(_options.Shapes.Colors).OrNull(_faker, 0.2f)};

            shape.WriteToConsole("Painter");
            acc.Add(shape);
            Thread.Sleep(_options.Settings.Delay);
        }

        return acc;
    }
}