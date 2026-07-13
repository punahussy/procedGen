using System;
using System.Drawing;
using System.Windows.Forms;
using ProcedGenV2.Generation;
using ProcedGenV2.Rendering;

namespace ProcedGenV2.UI;

/// <summary>Панель, отрисовывающая сгенерированный уровень с автоматическим масштабом.</summary>
public class LevelCanvas : Panel
{
    // Минимальный размер тайла, при котором номера ещё читаемы.
    private const int MinTileSizeForLabels = 18;

    private TileMap _map;

    public LevelCanvas()
    {
        SetStyle(
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.UserPaint |
            ControlStyles.ResizeRedraw,
            true);
        UpdateStyles();

        BackColor = Color.FromArgb(32, 32, 32);
    }

    /// <summary>Задаёт карту для отображения и перерисовывает панель.</summary>
    public void SetLevel(TileMap map)
    {
        _map = map;
        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (_map is null || _map.Count == 0)
            return;

        int tileSize = Math.Max(1, Math.Min(
            ClientSize.Width / _map.Width,
            ClientSize.Height / _map.Height));

        // Центрируем сетку в доступной области.
        int offsetX = (ClientSize.Width - tileSize * _map.Width) / 2;
        int offsetY = (ClientSize.Height - tileSize * _map.Height) / 2;

        DrawTiles(e.Graphics, tileSize, offsetX, offsetY);
        DrawTileNumbers(e.Graphics, tileSize, offsetX, offsetY);
    }

    private void DrawTiles(Graphics graphics, int tileSize, int offsetX, int offsetY)
    {
        Tile firstTile = _map.Tiles[0];
        Tile lastTile = _map.Tiles[_map.Count - 1];

        foreach (Tile tile in _map.Tiles)
        {
            bool isEndpoint = tile == firstTile || tile == lastTile;
            Bitmap texture = isEndpoint ? Textures.StartEnd : Textures.Tile;

            graphics.DrawImage(
                texture,
                new Rectangle(offsetX + tile.X * tileSize, offsetY + tile.Y * tileSize, tileSize, tileSize));
        }
    }

    private void DrawTileNumbers(Graphics graphics, int tileSize, int offsetX, int offsetY)
    {
        if (tileSize < MinTileSizeForLabels)
            return;

        using Font font = new Font("Arial", tileSize / 4f);
        using StringFormat format = new StringFormat { Alignment = StringAlignment.Center };

        int number = 0;
        foreach (Tile tile in _map.Tiles)
        {
            number++;
            string text = $"{number}\n({tile})";
            float x = offsetX + tile.X * tileSize + tileSize / 2f;
            float y = offsetY + tile.Y * tileSize + tileSize / 6f;

            graphics.DrawString(text, font, Brushes.Black, x, y, format);
        }
    }
}
