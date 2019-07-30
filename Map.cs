using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcedGenV2
{
    //Карта уровня
    public class Map : IEnumerable
    {
        //Карта уровня
        private bool[,] map;
        //Список тайлов
        public List<Tile> tiles { get; }
        //Количество тайлов
        public int TilesQuantity { get => tiles.Count; }

        //Конструктор, берущий размеры из класса Settings 
        public Map()
        {
            tiles = new List<Tile>();
            map = new bool[Settings.TilesWide, Settings.TilesHeight];
        }

        //Устанавливает тайл на карту по координатам
        public void Place(Tile tile)
        {
            map[tile.X, tile.Y] = true;
            tiles.Add(tile);
        }

        //Проверяет заняты ли координаты 
        public bool CanPlace(int x, int y)
        {
            bool can = true;
            if (x >= Settings.TilesWide || y >= Settings.TilesHeight
                || x < 0 || y < 0)
                can = false;
            else
                can = !map[x, y];
            return can;
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)tiles).GetEnumerator();
        }
    }
}
