using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSTSP
{
    class FSTSPRouting
    {
        public static void buildUnitRoute(SquareGrid grid, List<Order> orders, Truck truck)
        {
            if (truck.status.Equals(Status.Ready))
            {
                var availableDrones = truck.drones.Where(drone => drone.status.Equals(Status.Available)).ToList();
            }
        }

        private void selectDronesOrders(List<Drone> availableDrones, List<Order> orders)
        {
            List<droneRouteSheet> routeSheets = new List<droneRouteSheet>();
            foreach(var drone in availableDrones)
            {
                var candidateOrders = orders.Where(order => order.weight < drone.maxWeight);
                if (candidateOrders.Count() == 0) continue;

                Order orderToDeliver = null;
                foreach(var order in candidateOrders)
                {
                    if (routeSheets.Where(x => x.deliveryPoint.Equals(new Location(order.x, order.y, 0))).Count() == 0)
                    {
                        orderToDeliver = order;
                        break;
                    }
                    else
                        continue;
                }
                if (!(orderToDeliver is null))
                {
                    var orderToDeliverIndex = orders.IndexOf(orderToDeliver);
                    var meetingPoint = orders[orderToDeliverIndex + 1];
                    var distance = Location.surfaceDistance(drone.currentPosition, new Location(orderToDeliver.x, orderToDeliver.y, 0));
                    distance += Location.surfaceDistance(new Location(orderToDeliver.x, orderToDeliver.y, 0), new Location(meetingPoint.x, meetingPoint.y, 0));

                    if(distance * BaseConstants.PolygonSize * 1.3 < drone.range)
                    {
                        var newRouteSheet = new droneRouteSheet(drone, drone.currentPosition,
                                                                new Location(orderToDeliver.x, orderToDeliver.y, 0),
                                                                new Location(meetingPoint.x, meetingPoint.y, 0));
                        routeSheets.Add(newRouteSheet);
                    }
                }
                
            }
        }

        private class droneRouteSheet
        {
            public Drone drone;
            public Location start;
            public Location deliveryPoint;
            public Location meetingPoint;

            public droneRouteSheet(Drone Drone, Location Start, Location DeliveryPoint, Location MeetingPoint)
            {
                drone = Drone;
                start = Start;
                deliveryPoint = DeliveryPoint;
                meetingPoint = MeetingPoint;
            }
        }
    }

}
