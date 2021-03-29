using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter_10._1
{
    class FloorInstaller : AddOns
    {
        public double CalculateAddOnWorkCost(double floorArea)
        {
            return floorArea * 30;
        }
    }
}
