using System;
using ProcedGenV2.Configuration;

namespace ProcedGenV2.Generation;

/// <summary>Одна ячейка карты уровня с координатами на сетке.</summary>
public class Tile
{
    private static readonly Random Random = new Random();

    public int X { get; }
    public int Y { get; }

    public Tile(int x, int y)
    {
        X = x;
        Y = y;
    }

    /// <summary>Создаёт тайл на случайных координатах в пределах карты.</summary>
    public Tile()
    {
        X = Random.Next(0, GenerationSettings.TilesWide);
        Y = Random.Next(0, GenerationSettings.TilesHeight);
    }

    public override string ToString() => $"{X},{Y}";
}
