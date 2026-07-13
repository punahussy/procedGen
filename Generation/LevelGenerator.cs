using System;
using System.Collections;
using System.Collections.Generic;
using ProcedGenV2.Configuration;

namespace ProcedGenV2.Generation;

/// <summary>Строит случайный маршрут тайлов по карте уровня согласно заданным настройкам.</summary>
public class LevelGenerator : IEnumerable<Tile>
{
    private static readonly Random Random = new Random();
    private static readonly int DirectionCount = Enum.GetValues<Direction>().Length;

    // Функции, создающие соседний тайл в заданном направлении от исходного.
    private readonly IReadOnlyDictionary<Direction, Func<Tile, Tile>> _walk =
        new Dictionary<Direction, Func<Tile, Tile>>
        {
            { Direction.Left,  tile => new Tile(tile.X - 1, tile.Y) },
            { Direction.Right, tile => new Tile(tile.X + 1, tile.Y) },
            { Direction.Up,    tile => new Tile(tile.X, tile.Y - 1) },
            { Direction.Down,  tile => new Tile(tile.X, tile.Y + 1) },
        };

    private readonly GenerationSettings _settings;

    private int _stepsSinceBranch;
    private int _totalSteps;
    private int _targetCount;
    private Direction _currentDirection;

    public LevelGenerator(GenerationSettings settings)
    {
        _settings = settings;
    }

    /// <summary>Карта, построенная последним вызовом <see cref="Generate"/>.</summary>
    public TileMap Map { get; private set; }

    /// <summary>Запускает генерацию нового уровня по текущим настройкам.</summary>
    public void Generate()
    {
        int width = _settings.TilesWide;
        int height = _settings.TilesHeight;

        // Больше тайлов, чем ячеек на карте, разместить нельзя — ограничиваем цель.
        _targetCount = Math.Min(_settings.MaxTileCount, width * height);

        Map = new TileMap(width, height);
        _stepsSinceBranch = 0;
        _totalSteps = 0;

        Tile startTile = new Tile(Random.Next(0, width), Random.Next(0, height));
        Map.Place(startTile);
        ChooseDirection();
        ProcessTile(startTile);
    }

    // Рекурсивно строит случайный маршрут, начиная с указанного тайла.
    private void ProcessTile(Tile tile)
    {
        _stepsSinceBranch++;
        _totalSteps++;

        // Останавливаемся при достижении целевого числа тайлов.
        if (_totalSteps >= _targetCount)
            return;

        // Управляем ветвистостью: периодически стартуем от случайного тайла.
        if (_stepsSinceBranch >= _settings.Branching)
        {
            _stepsSinceBranch = 0;
            ProcessRandomTile();
            return;
        }

        if (NextBool())
            ChooseDirection();

        Tile nextTile = _walk[_currentDirection](tile);
        while (!Map.CanPlace(nextTile.X, nextTile.Y))
        {
            if (NextBool())
            {
                ProcessRandomTile();
                return;
            }

            ChooseDirection();
            nextTile = _walk[_currentDirection](tile);
        }

        Map.Place(nextTile);
        ProcessTile(nextTile);
    }

    private void ProcessRandomTile()
    {
        ChooseDirection();
        ProcessTile(Map.Tiles[NextTileIndex()]);
    }

    private void ChooseDirection() => _currentDirection = (Direction)Random.Next(DirectionCount);

    private bool NextBool() => Random.Next(0, _settings.BoolChance) == 1;

    private int NextTileIndex() => Random.Next(0, Map.Count - 1);

    public IEnumerator<Tile> GetEnumerator() => Map.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
