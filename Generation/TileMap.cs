using System.Collections;
using System.Collections.Generic;

namespace ProcedGenV2.Generation;

/// <summary>Карта уровня: сетка занятых ячеек и упорядоченный список размещённых тайлов.</summary>
public class TileMap : IEnumerable<Tile>
{
    private readonly bool[,] _occupied;
    private readonly List<Tile> _tiles = new List<Tile>();

    public TileMap(int width, int height)
    {
        Width = width;
        Height = height;
        _occupied = new bool[width, height];
    }

    /// <summary>Ширина карты в тайлах.</summary>
    public int Width { get; }

    /// <summary>Высота карты в тайлах.</summary>
    public int Height { get; }

    /// <summary>Размещённые тайлы в порядке добавления.</summary>
    public IReadOnlyList<Tile> Tiles => _tiles;

    /// <summary>Количество размещённых тайлов.</summary>
    public int Count => _tiles.Count;

    /// <summary>Размещает тайл на карте по его координатам.</summary>
    public void Place(Tile tile)
    {
        _occupied[tile.X, tile.Y] = true;
        _tiles.Add(tile);
    }

    /// <summary>Возвращает true, если координаты в пределах карты и ещё не заняты.</summary>
    public bool CanPlace(int x, int y)
    {
        if (x < 0 || y < 0 || x >= Width || y >= Height)
            return false;

        return !_occupied[x, y];
    }

    public IEnumerator<Tile> GetEnumerator() => _tiles.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
