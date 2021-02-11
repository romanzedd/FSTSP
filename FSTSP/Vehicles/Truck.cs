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
        public Location currentPosition;
        public Location destination = new Location(-1, -1, -1);
        public int time = 28800;

        public Truck(string Id, Location Depot, List<Drone> Drones)
        {
            id = Id;
            drones = Drones;
            currentPosition = Depot; // construction is always in the Depot
        }
    }
}
