using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSTSP
{
    public class BaseConstants
    {
        public static int DroneRange = 7; // kilometers
        public static int DroneSpeed = 20; //meters per second
        public static int DroneDropDeliveryTime = 120; // seconds required to drop delivery at customer

        public static int TruckSpeed = 5;//meters per second
         
        public static int PolygonSize = 20; //in paper it says 5, but generation for 5 is taking way too long
        public static int areaHeight = 10;
    }
}