using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSTSP
{
    class Truck
    {
        public readonly string id;
        public readonly List<Drone> drones;
        public Status status = Status.Depot;
        public Location destination = new Location(-1, -1, -1);

        public Truck(string Id, List<Drone> Drones)
        {
            id = Id;
            drones = Drones;
        }
    }
}
