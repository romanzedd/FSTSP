using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSTSP
{
    class GridGeneration
    {
        public static void fillGrid(SquareGrid grid, int areaSize, int areaHeight)
        {
            bool[,,] obstacles = new bool[areaSize, areaSize, areaHeight];
            Random rnd = new Random();
            int threshold = 65;

            for (int x = 0; x < areaSize; x++)
            {
                for (int y = 0; y < areaSize; y++)
                {
                    obstacles[x, y, 0] = true;
                }
            }

            for (int x = 0; x < areaSize; x++)
            {
                if (rnd.Next(100) > threshold)
                {
                    for (int y = 0; y < areaSize; y++)
                    {
                        obstacles[x, y, 0] = false;
                    }
                }
            }

            for (int y = 0; y < areaSize; y++)
            {
                if (rnd.Next(100) > threshold)
                {
                    for (int x = 0; x < areaSize; x++)
                    {
                        obstacles[x, y, 0] = false;
                    }
                }
            }

            for (int x = 0; x < areaSize; x++)
            {
                for (int y = 0; y < areaSize; y++)
                {
                    if (obstacles[x, y, 0])
                        grid.walls.Add(new Location(x, y, 0));
                }
            }


            for (int z = 1; z < areaHeight; z++)
            {
                for (int x = 0; x < areaSize; x++)
                {
                    for (int y = 0; y < areaSize; y++)
                    {
                        if ((rnd.Next(100) > threshold || rnd.Next(100) > 20) && obstacles[x, y, z - 1])
                        {
                            obstacles[x, y, z] = true;
                            grid.walls.Add(new Location(x, y, z));
                        }
                        else
                        {
                            obstacles[x, y, z] = false;
                        }
                    }
                }
                threshold -= 5;
            }
        }
    }
}
