using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSTSP
{
    public class Order
    {
        public int x;
        public int y;
        public bool isDroneFriendly;

        public Order(int _x, int _y, bool droneFriendly)
        {
            x = _x;
            y = _y;
            isDroneFriendly = droneFriendly;
        }

        public static List<Order> generateOrders(SquareGrid grid, Location Depot, int ordersCount, int areaSize)
        {
            Random rnd = new Random();
            List<Order> ordersList = new List<Order>();
            while (ordersCount > 0)
            {
                var x = rnd.Next(areaSize);
                var y = rnd.Next(areaSize);
                var isWall = true;
                while (isWall)
                {
                    if (grid.walls.Contains(new Location(x, y, 0)))
                    {
                        x = rnd.Next(areaSize);
                        y = rnd.Next(areaSize);
                    }
                    else
                        isWall = false;
                }

                var distanceFromDepot = Math.Sqrt(Math.Pow((x - Depot.x), 2.0) + Math.Pow((y - Depot.y), 2.0)) * BaseConstants.PolygonSize / 1000;

                bool isDroneFriendly = false;
                if (rnd.Next(2) == 0)
                {

                    if (distanceFromDepot < BaseConstants.DroneRange)
                        isDroneFriendly = true;
                }

                ordersList.Add(new Order(x, y, isDroneFriendly));
                ordersCount--;
            }
            return ordersList;
        }
    }
}
