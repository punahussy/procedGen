namespace ProcedGenV2.Generation;

/// <summary>Одна ячейка карты уровня с координатами на сетке.</summary>
public class Tile
{
    public int X { get; }
    public int Y { get; }

    public Tile(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString() => $"{X},{Y}";
}
