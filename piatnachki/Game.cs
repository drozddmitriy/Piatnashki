using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piatnachki
{
    class Game
    {
        int size;
        int[,] map;
        int sx, sy;
        static Random rand = new Random();
        public Game(int size)
        {
            this.size = size;
            map = new int[size, size];
        }

        private int cords_to_position(int x, int y) // perevod cordinat v position
        {
            if (x < 0) x = 0;
            if (x > size - 1) x = size - 1;
            if (y < 0) y = 0;
            if (y > size - 1) y = size - 1;
            return y * size + x;
        }

        private void position_to_cords(int position, out int x, out int y) // perevod position v cordinatu
        {
            if (position < 0) position = 0;
            if (position > size * size - 1) position = size * size - 1;
            x = position % size;
            y = position / size;
        }

        public void shift(int position)                         // peremeshenie
        {
            int x, y;
            position_to_cords(position, out x, out y);
            if (Math.Abs(sx - x) + Math.Abs(sy - y) != 1) 
                return;
            map[sx, sy] = map[x, y];
            map[x, y] = 0;
            sx = x;
            sy = y;
        }

        public void start()                         // zapoln pole
        {
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    map[x, y] = cords_to_position(x, y) + 1;
            sx = size -1;
            sy = size -1;
            map [sx,sy] = 0;
        }

        public void shift_random()             // peremechka
        {
            int a = rand.Next(0, 4);
            int x = sx;
            int y = sy;
            switch (a)
            {
                case 0: x--; break;
                case 1: x++; break;
                case 2: y--; break;
                case 3: y++; break;
            }
            shift(cords_to_position(x, y));
        }

        public int get_number(int position)
        {
            int x, y;
            position_to_cords(position, out x, out y);
            if (x < 0 || x >= size) return 0;
            if (y < 0 || y >= size) return 0;
            return map[x, y];
        }

        public bool check()                                 // proverka na konec igru
        {
            if(!(sx == size-1 && sy == size-1))
                return false;
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    if (!(x == size - 1 && y == size - 1))
                    {
                        if (map[x, y] != cords_to_position(x, y) + 1)
                        {
                            return false;
                        }
                    }
            return true;
        }
    }
}
