using System;
using System.Drawing;
using System.Windows.Forms;
using ProcedGenV2.Configuration;
using ProcedGenV2.Generation;
using ProcedGenV2.Rendering;

namespace ProcedGenV2.UI;

/// <summary>Главное окно: отрисовывает сгенерированный уровень и обрабатывает ввод.</summary>
public partial class MainForm : Form
{
    private const int TileSize = 32;

    private readonly LevelGenerator _generator = new LevelGenerator();

    public MainForm()
    {
        InitializeComponent();

        // Двойная буферизация устраняет мерцание при перерисовке.
        SetStyle(
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.UserPaint,
            true);
        UpdateStyles();

        MaximizeBox = false;
        ClientSize = new Size(
            TileSize * GenerationSettings.TilesWide,
            TileSize * GenerationSettings.TilesHeight);
    }

    private void MainForm_Load(object sender, EventArgs e) => _generator.Generate();

    private void MainForm_Paint(object sender, PaintEventArgs e)
    {
        DrawTiles(e.Graphics);
        DrawTileNumbers(e.Graphics);
    }

    private void DrawTiles(Graphics graphics)
    {
        Tile firstTile = _generator.Map.Tiles[0];
        Tile lastTile = _generator.Map.Tiles[_generator.Map.Count - 1];

        foreach (Tile tile in _generator)
        {
            bool isEndpoint = tile == firstTile || tile == lastTile;
            Bitmap texture = isEndpoint ? Textures.StartEnd : Textures.Tile;

            graphics.DrawImage(
                texture,
                new Rectangle(tile.X * TileSize, tile.Y * TileSize, TileSize, TileSize));
        }
    }

    private void DrawTileNumbers(Graphics graphics)
    {
        using Font font = new Font("Arial", TileSize / 4f);
        using StringFormat format = new StringFormat { Alignment = StringAlignment.Center };

        int number = 0;
        foreach (Tile tile in _generator)
        {
            number++;
            string text = $"{number}\n({tile})";
            float x = tile.X * TileSize + TileSize / 2f;
            float y = tile.Y * TileSize + TileSize / 6f;

            graphics.DrawString(text, font, Brushes.Black, x, y, format);
        }
    }

    private void MainForm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.R)
        {
            _generator.Generate();
            Refresh();
        }
        else if (e.KeyCode == Keys.Escape)
        {
            Application.Exit();
        }
    }
}
