using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSTSP
{
    public class Drone
    {
        public readonly string id;
        public readonly int range; //metres
        public readonly int maxWeight;
        public Status status = Status.Ready;
        public Location currentPosition;
        public Location destination = new Location(-1,-1,-1);
        public int time = 28800;
        public List<Order> fulfilledOrders = new List<Order>();

        public Drone(string Id, int Range, int MaxWeight, Location Depot)
        {
            id = Id;
            range = Range;
            maxWeight = MaxWeight;
            currentPosition = Depot; // construction is always in the Depot
        }
    }
}
