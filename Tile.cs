using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcedGenV2
{
    public class Tile
    {
        public int X { get; }
        public int Y { get; }

        public Tile(int x, int y)
        {
            X = x;
            Y = y;
        }

        private static readonly object syncLock = new object();
        //Создание тайла на случайных координатах
        public Tile()
        {
            int x;
            int y;
            Random rnd = new Random();
            lock (syncLock)
            {
                x = rnd.Next(0, Settings.TilesWide);
            }
            y = rnd.Next(0, Settings.TilesHeight);
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            string output = String.Format("{0},{1}", X,Y);
            return output;
        }
    }
}
