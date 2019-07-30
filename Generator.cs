using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcedGenV2
{
    //Делегат, создающий новый тайл
    public delegate Tile CreateTile(Tile tile);

    public class Generator : IEnumerable
    {
        //Количество итераций 
        private int count;
        private int totalCount;
        //Текущее направление
        private Directions currentDirection;

        //Карта тайлов 
        public Map map { get; private set; }

        //Словарь с делегатами, создающими новый тайл в выбранном направлении от входного
        private readonly Dictionary<Directions, CreateTile> walk
            = new Dictionary<Directions, CreateTile>
        {
            {Directions.left, tile => new Tile(tile.X-1, tile.Y) },
            {Directions.right, tile => new Tile(tile.X+1, tile.Y) },
            {Directions.up, tile => new Tile(tile.X, tile.Y-1) },
            {Directions.down, tile => new Tile(tile.X, tile.Y+1) }
        };

        //Enum содержащий направления для упрощения случайной генерации
        private enum Directions
        {
            left,
            right,
            up,
            down,
        }

        //Количество значений в enum Directions
        readonly int valuesCount = Enum.GetValues(typeof(Directions)).Cast<Directions>().Distinct().Count();

        #region Случайная генерация
        //Необходимые для генерации случаных чисел объекты
        private static readonly Random rnd = new Random();
        private static readonly object syncLock = new object();

        //Выбирает случайное направление
        private void ChooseDirection()
        {
            //Thread.Sleep(1);
            lock (syncLock)
            {
                currentDirection = (Directions)rnd.Next(valuesCount);
            }
        }

        //Возвращает случайный bool
        private bool RandomBool()
        {
            bool output;
            lock (syncLock)
            {
                output = rnd.Next(0, Settings.BoolChance) == 1;
            }
            return output;
        }

        //Возвращает случайный номер тайла
        private int RandomTile()
        {
            int output;
            lock (syncLock)
            {
                output = rnd.Next(0, map.TilesQuantity - 1);
            }
            return output;
        }

        #endregion

        //Основной метод, запускающий генерацию уровня
        public void Generate()
        {
            if (Settings.MaxTotalCount > Settings.TilesHeight * Settings.TilesWide)
                throw new Exception("Затребованное количество тайлов для генерации превышает размеры поля генерации");

            map = new Map();
            Tile startTile = new Tile();
            map.Place(startTile);
            ChooseDirection();
            count = 0;
            totalCount = 0;
            ProcessTile(startTile);
        }

        //Рекурсивный метод постройки случайного маршрута
        private void ProcessTile(Tile tile)
        {
            count++;
            totalCount++;

            //При достижении максимального числа генераций останавливает генерацию
            if (totalCount >= Settings.MaxTotalCount)
                return;

            //Управляет ветвистостью лабиринта
            if (count >= Settings.Branching)
            {
                count = 0;
                ProcessRandomTile();
                return;
            }

            //Определяет нужно ли менять направление, меняет если нужно
            if (RandomBool())
                ChooseDirection();

            Tile nextTile = walk[currentDirection](tile);
            while (!map.CanPlace(nextTile.X, nextTile.Y))
            {
                if (RandomBool())
                {
                    ProcessRandomTile();
                    return;
                }
                else
                    ChooseDirection();
                nextTile = walk[currentDirection](tile);
            }

            map.Place(nextTile);
            ProcessTile(nextTile);
        }

        private void ProcessRandomTile()
        {
            ChooseDirection();
            ProcessTile(map.tiles[RandomTile()]);
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var tile in map)
            {
                yield return tile;
            }
        }
    }
}
