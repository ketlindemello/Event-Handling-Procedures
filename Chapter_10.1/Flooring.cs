using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter_10._1
{
    public class Flooring
    {
        public string Name { get; set; }
        public double PricePerSquareFoot { get; set; }
        public string Description { get; set; }
        public string ItemNumber { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="floorAreaInSquareFeet">Accepts a value represented in Square Feet</param>
        public double CalculatePrice(double floorAreaInSquareFeet)
        {
            return floorAreaInSquareFeet * this.PricePerSquareFoot;
        }
    }
}
