using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ProcedGenV2.Rendering;

/// <summary>Растровые текстуры тайлов, встроенные в сборку.</summary>
internal static class Textures
{
    public static Bitmap Tile { get; } = Load("tile.png");

    public static Bitmap StartEnd { get; } = Load("startend.png");

    private static Bitmap Load(string fileName)
    {
        Assembly assembly = typeof(Textures).Assembly;
        string resourceName = assembly
            .GetManifestResourceNames()
            .Single(name => name.EndsWith(fileName, StringComparison.OrdinalIgnoreCase));

        using Stream stream = assembly.GetManifestResourceStream(resourceName)
            ?? throw new InvalidOperationException($"Встроенный ресурс не найден: {fileName}");

        return new Bitmap(stream);
    }
}
