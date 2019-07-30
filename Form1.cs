using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcedGenV2
{
    public partial class Form1 : Form
    {
        //Параметры области отображения тайлов
        public static int TileSize => 32;
        public int TilesWide => Settings.TilesWide;
        public int TilesHeight => Settings.TilesHeight;

        private Generator generator = new Generator();

        public Form1()
        {

            InitializeComponent();

            //Устранение мерцания
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint, true);
            UpdateStyles();

            //Запрет разворачивания окна
            MaximizeBox = false;

            //Установка размера игровой области
            ClientSize = new Size(TileSize * TilesWide, TileSize * TilesHeight);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            generator.Generate();            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Create string to draw.
            String drawString = "";

            foreach (Tile tile in generator)
            {
                
                e.Graphics.DrawImage(
                    (tile == generator.map.tiles[0] || tile == generator.map.tiles[generator.map.TilesQuantity - 1] ? 
                        Textures.startend : Textures.tile),
                    new Rectangle(tile.X*TileSize, tile.Y*TileSize, TileSize, TileSize));
            }

            int i = 0;
            foreach (Tile tile in generator)
            {
                i++;
                drawString = i + "\n" + String.Format("({0})", tile.ToString());

                // Create font and brush.
                Font drawFont = new Font("Arial", TileSize / 4);
                SolidBrush drawBrush = new SolidBrush(Color.Black);

                // Create point for upper-left corner of drawing.
                float x = tile.X * TileSize + TileSize / 2;
                float y = tile.Y * TileSize + TileSize / 6;

                // Set format of string.
                StringFormat drawFormat = new StringFormat();
                drawFormat.Alignment = StringAlignment.Center;

                e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R)
            {
                generator.Generate();
                Refresh();
            }
            else if (e.KeyCode == Keys.Escape)
                Application.Exit();

        }
    }
}
