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
        public Status status = Status.Depot;
        public Location destination = new Location(-1,-1,-1);

        public Drone(string Id, int Range, int MaxWeight)
        {
            id = Id;
            range = Range;
            maxWeight = MaxWeight;
        }
    }
}
