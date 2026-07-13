using System;
using System.Windows.Forms;
using ProcedGenV2.Configuration;
using ProcedGenV2.Generation;

namespace ProcedGenV2.UI;

/// <summary>Главное окно: ползунки параметров генерации и область отрисовки уровня.</summary>
public partial class MainForm : Form
{
    private readonly GenerationSettings _settings = new GenerationSettings();
    private readonly LevelGenerator _generator;
    private LevelCanvas _canvas;

    public MainForm()
    {
        _generator = new LevelGenerator(_settings);

        InitializeComponent();
        BuildLayout();
        Regenerate();
    }

    private void BuildLayout()
    {
        _canvas = new LevelCanvas { Dock = DockStyle.Fill };

        var controlPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Left,
            Width = 230,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            AutoScroll = true,
            Padding = new Padding(12),
        };

        AddSlider(controlPanel, "Ширина (тайлов)", 10, 60, _settings.TilesWide, value => _settings.TilesWide = value);
        AddSlider(controlPanel, "Высота (тайлов)", 5, 40, _settings.TilesHeight, value => _settings.TilesHeight = value);
        AddSlider(controlPanel, "Ветвистость", 2, 100, _settings.Branching, value => _settings.Branching = value);
        AddSlider(controlPanel, "Макс. тайлов", 10, 400, _settings.MaxTileCount, value => _settings.MaxTileCount = value);
        AddSlider(controlPanel, "Шанс поворота (1 из N)", 2, 12, _settings.BoolChance, value => _settings.BoolChance = value);

        controlPanel.Controls.Add(new Label
        {
            AutoSize = true,
            Margin = new Padding(3, 16, 3, 0),
            Text = "R — перегенерировать\nEsc — выход",
        });

        // Fill-панель добавляем первой, чтобы она заняла место, оставшееся от левой панели.
        Controls.Add(_canvas);
        Controls.Add(controlPanel);
    }

    // Создаёт подписанный ползунок; при изменении применяет значение и перегенерирует уровень.
    private void AddSlider(Control parent, string caption, int min, int max, int value, Action<int> apply)
    {
        var label = new Label { AutoSize = true, Margin = new Padding(3, 8, 3, 0) };
        var slider = new TrackBar
        {
            Minimum = min,
            Maximum = max,
            Value = Math.Clamp(value, min, max),
            Width = 200,
            TickStyle = TickStyle.None,
        };

        void UpdateCaption() => label.Text = $"{caption}: {slider.Value}";

        slider.ValueChanged += (_, _) =>
        {
            UpdateCaption();
            apply(slider.Value);
            Regenerate();
        };

        UpdateCaption();
        parent.Controls.Add(label);
        parent.Controls.Add(slider);
    }

    private void Regenerate()
    {
        _generator.Generate();
        _canvas.SetLevel(_generator.Map);
    }

    private void MainForm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.R)
            Regenerate();
        else if (e.KeyCode == Keys.Escape)
            Application.Exit();
    }
}
