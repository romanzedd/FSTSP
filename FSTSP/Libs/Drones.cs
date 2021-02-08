using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Drones
    {
        public string name;
        public int maxDistance;
        public int maxAlt;
        public int maxWeight;

        public Drones(string _name, int _mD, int _mA, int _mW)
        {
            name = _name;
            maxDistance = _mD;
            maxAlt = _mA;
            maxWeight = _mW;
        }
    }
}
