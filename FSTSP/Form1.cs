using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSTSP
{
    public partial class MainWindow : Form
    {
        public Map FormMap { get; set; }
        public Graph[] nodes;
        public Graph[,,] map;
        public SquareGrid grid;
        //Drones[] drones;
        public List<Location> ThePath;
        Location Depot;
        public static int DroneTime = 28800;
        public static int TruckTime = 28800;

        public MainWindow()
        {
            InitializeComponent();
        }
        public void Form1_Load(object sender, EventArgs e)
        {
            LoadData loader = new LoadData();
            string filepath = @"e:\data source\addressbook.map"; //addressbook file path
            string addressBook = null, routes = null;
            int numLines = loader.fileDecrypt(filepath, ref addressBook);

            nodes = new Graph[(numLines - 1) * 4];

            string[] addresses = new string[numLines - 1];
            loader.loadNodes(addressBook, numLines, ref nodes);
            filepath = @"e:\data source\routes.map"; //routes file path
            loader.fileDecrypt(filepath, ref routes);
            loader.loadRoutes(routes, ref nodes);//загрузка соседей в каждый нод

            for (int i = 0; i < numLines - 1; i++)
            {
                addresses[i] = nodes[i].street + " " + nodes[i].house;
            }

            routing route = new routing();
            route.MapMaker(ref map, nodes, 41, 41, 4, 55.041513, 82.916838);
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            var areaSize = Int16.Parse(areaSizeUpDown.Value.ToString()) * 1000 / BaseConstants.PolygonSize; //sets size in nodes
            var areaHeight = 10; //150 metres total height
            Depot = new Location(areaSize / 2, areaSize / 2, 0);
            short ordersCount;
            if (!Int16.TryParse(numberOfCustomersTextBox.Text, out ordersCount))
                ordersCount = 15;
            
            grid = new SquareGrid(areaSize, areaSize, areaHeight);
            GridGeneration.fillGrid(grid, areaSize, areaHeight); //20+ mins for 5km
            outputTextBox.Text += $"Space of {areaSize* BaseConstants.PolygonSize / 1000} km2 ({areaSize*areaSize*areaHeight} polygons) generated successfully\n";
            
            List<Order> orders = new List<Order>();
            orders = Order.generateOrders(grid, Depot, ordersCount, areaSize);
            outputTextBox.Text += $"{ordersCount} orders generated successfully\n";

            var truckOrders = orders.Where(x => x.isDroneFriendly == false);
            var droneOrders = orders.Where(x => x.isDroneFriendly == true);

            doDrone(droneOrders.ToList());
            doTruck(truckOrders.ToList());
        }

        private void doDrone(List<Order> droneOrders)
        {
            List<List<Location>> dronePaths = new List<List<Location>>();
            foreach(var order in droneOrders)
            {
                List<Location> path = new List<Location>();
                AStarSearch astar = new AStarSearch(grid, Depot, new Location(order.x, order.y, 0));
                path = astar.ReconstructPath(Depot, new Location(order.x, order.y, 0), astar.cameFrom);
                var returnPath = path;
                returnPath.Reverse();
                path.AddRange(returnPath);
                dronePaths.Add(path);
            }

            droneStatusUpdate(dronePaths);
        }
        private void doTruck(List<Order> truckOrders)
        {
            List<List<Location>> truckPaths = new List<List<Location>>();
            List<Location> path = new List<Location>();

            AStarSearch astar = new AStarSearch(grid, Depot, new Location(truckOrders.First().x, truckOrders.First().y, 0));
            path = astar.ReconstructPath(Depot, new Location(truckOrders.First().x, truckOrders.First().y, 0), astar.cameFrom);
            truckPaths.Add(path);

            for (int i = 0; i < truckOrders.Count-1; i++)
            {
                
                astar = new AStarSearch(grid, 
                                        new Location(truckOrders[i].x, truckOrders[i].y, 0), 
                                        new Location(truckOrders[i+1].x, truckOrders[i+1].y, 0));
                path = astar.ReconstructPath(new Location(truckOrders[i].x, truckOrders[i].y, 0),
                                             new Location(truckOrders[i+1].x, truckOrders[i+1].y, 0), 
                                             astar.cameFrom);
                truckPaths.Add(path);
            }

            astar = new AStarSearch(grid, new Location(truckOrders.Last().x, truckOrders.Last().y, 0), Depot);
            path = astar.ReconstructPath(new Location(truckOrders.Last().x, truckOrders.Last().y, 0), Depot, astar.cameFrom);
            truckPaths.Add(path);

            truckStatusUpdate(truckPaths);
        }

        private void droneStatusUpdate(List<List<Location>> dronePaths)
        {
            
            foreach(var path in dronePaths)
            {
                var currentTime = TimeSpan.FromSeconds(DroneTime);
                outputTextBox.Text += $"[{currentTime.ToString(@"hh\:mm\:ss\:fff")}] Drone picked parcel and left the depot\n";

                DroneTime += (path.Count / 2) * BaseConstants.PolygonSize / BaseConstants.DroneSpeed;
                currentTime = TimeSpan.FromSeconds(DroneTime);
                outputTextBox.Text += $"[{currentTime.ToString(@"hh\:mm\:ss\:fff")}] Drone arrived to a client\n";

                DroneTime += BaseConstants.DroneDropDeliveryTime;
                currentTime = TimeSpan.FromSeconds(DroneTime);
                outputTextBox.Text += $"[{currentTime.ToString(@"hh\:mm\:ss\:fff")}] Drone dropped parcel and is returning to the depot\n";

                DroneTime += (path.Count / 2) * BaseConstants.PolygonSize / BaseConstants.DroneSpeed;
                currentTime = TimeSpan.FromSeconds(DroneTime);
                outputTextBox.Text += $"[{currentTime.ToString(@"hh\:mm\:ss\:fff")}] Drone arrived to the depot\n";

                DroneTime += BaseConstants.DroneDropDeliveryTime;
            }
            
        }

        private void truckStatusUpdate(List<List<Location>> truckPaths)
        {
            var currentTime = TimeSpan.FromSeconds(TruckTime);
            outputTextBox.Text += $"[{currentTime.ToString(@"hh\:mm\:ss\:fff")}] Truck picked parcels and left the depot\n";

            foreach (var path in truckPaths)
            {
                if(path == truckPaths.Last())
                {
                    TruckTime += path.Count * BaseConstants.PolygonSize / BaseConstants.TruckSpeed;
                    currentTime = TimeSpan.FromSeconds(TruckTime);
                    outputTextBox.Text += $"[{currentTime.ToString(@"hh\:mm\:ss\:fff")}] Truck arrived to the depot\n";
                    continue;
                }

                TruckTime += path.Count * BaseConstants.PolygonSize / BaseConstants.TruckSpeed;
                currentTime = TimeSpan.FromSeconds(TruckTime);
                outputTextBox.Text += $"[{currentTime.ToString(@"hh\:mm\:ss\:fff")}] Truck arrived to a client\n";

                TruckTime += BaseConstants.DroneDropDeliveryTime;
                currentTime = TimeSpan.FromSeconds(TruckTime);
                outputTextBox.Text += $"[{currentTime.ToString(@"hh\:mm\:ss\:fff")}] Truck dropped parcel and is heading to the next client\n";
            }
        }

    }
}
