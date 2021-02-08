﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Map1
    {
        /*public static Map Randomize(int nodeCount, int branching, int seed, bool randomWeights)
        {
            var rnd = new Random(seed);
            var map = new Map();

            for (int i = 0; i < nodeCount; i++)
            {
                var newNode = Node.GetRandom(rnd, i.ToString());
                if (!newNode.ToCloseToAny(map.Nodes))
                    map.Nodes.Add(newNode);
            }

            foreach (var node in map.Nodes)
                node.ConnectClosestNodes(map.Nodes, branching, rnd, randomWeights);
            //map.StartNode = map.Nodes.OrderBy(n => n.Point.X + n.Point.Y).First();
            //map.EndNode = map.Nodes.OrderBy(n => n.Point.X + n.Point.Y).Last();
            map.EndNode = map.Nodes[rnd.Next(map.Nodes.Count - 1)];
            map.StartNode = map.Nodes[rnd.Next(map.Nodes.Count - 1)];

            foreach (var node in map.Nodes)
            {
                Debug.WriteLine($"{node}");
                foreach (var cnn in node.Connections)
                {
                    Debug.WriteLine($"{cnn}");
                }
            }
            return map;
        }*/

        public List<Node> Nodes { get; set; } = new List<Node>();

        public Node StartNode { get; set; }

        public Node EndNode { get; set; }

        public List<Node> ShortestPath { get; set; } = new List<Node>();
    }

    public class Node
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Point Point { get; set; }
        public List<Edge> Connections { get; set; } = new List<Edge>();

        public double? MinCostToStart { get; set; }
        public Node NearestToStart { get; set; }
        public bool Visited { get; set; }
        public double StraightLineDistanceToEnd { get; set; }

        /*internal static Node GetRandom(Random rnd, string name)
        {
            return new Node
            {
                Point = new Point
                {
                    X = rnd.NextDouble(),
                    Y = rnd.NextDouble()
                },
                Id = Guid.NewGuid(),
                Name = name
            };
        }*/

        internal void ConnectClosestNodes(List<Node> nodes, int branching, Random rnd, bool randomWeight)
        {
            var connections = new List<Edge>();
            foreach (var node in nodes)
            {
                if (node.Id == this.Id)
                    continue;

                var dist = Math.Sqrt(Math.Pow(Point.X - node.Point.X, 2) + Math.Pow(Point.Y - node.Point.Y, 2));
                connections.Add(new Edge
                {
                    ConnectedNode = node,
                    Length = dist,
                    Cost = randomWeight ? rnd.NextDouble() : dist,
                });
            }
            connections = connections.OrderBy(x => x.Length).ToList();
            var count = 0;
            foreach (var cnn in connections)
            {
                //Connect three closes nodes that are not connected.
                if (!Connections.Any(c => c.ConnectedNode == cnn.ConnectedNode))
                    Connections.Add(cnn);
                count++;

                //Make it a two way connection if not already connected
                if (!cnn.ConnectedNode.Connections.Any(cc => cc.ConnectedNode == this))
                {
                    var backConnection = new Edge { ConnectedNode = this, Length = cnn.Length };
                    cnn.ConnectedNode.Connections.Add(backConnection);
                }
                if (count == branching)
                    return;
            }
        }

        public double StraightLineDistanceTo(Node end)
        {
            return Math.Sqrt(Math.Pow(Point.X - end.Point.X, 2) + Math.Pow(Point.Y - end.Point.Y, 2));
        }

        internal bool ToCloseToAny(List<Node> nodes)
        {
            foreach (var node in nodes)
            {
                var d = Math.Sqrt(Math.Pow(Point.X - node.Point.X, 2) + Math.Pow(Point.Y - node.Point.Y, 2));
                if (d < 0.01)
                    return true;
            }
            return false;
        }
        public override string ToString()
        {
            return Name;
        }
    }

    public class Edge
    {
        public double Length { get; set; }
        public double Cost { get; set; }
        public Node ConnectedNode { get; set; }

        public override string ToString()
        {
            return "-> " + ConnectedNode.ToString();
        }
    }

    /*public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Point(double _x, double _y, double _z)
        {
            X = _x;
            Y = _y;
            Z = _z;
        }
    }*/
}
