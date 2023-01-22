using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SjecišteDvaPravca
{

    /// <summary>
    /// Struct that holds coordinates of a point in two dimensional space.
    /// </summary>
    public struct Point {
        public double? x, y;
    }

    /// <summary>
    /// Class that is used for line object and instersection calculation.
    /// </summary>
    public class Line
    {
        public double gradient;
        public double ordinate_intersection;
        
        public Line(double gradient, double ordinate_intersection) { 
            this.gradient = gradient;
            this.ordinate_intersection = ordinate_intersection;
        }

        /// <summary>
        /// Returns value of y for given x;
        /// </summary>
        /// <param name="x">Value of x</param>
        /// <returns>Value of y</returns>
        public double GetY(double x) {
            return gradient * x + ordinate_intersection;
        }

        /// <summary>
        /// Static function that calculates intesection of two lines. It
        /// returns Point struc that holds intesection coordinates.
        /// </summary>
        /// <param name="line1">First line</param>
        /// <param name="line2">Second line</param>
        /// <returns>Returns struPoint that intersection coordinates.</returns>
        public static Point Intersection(Line line1, Line line2) {
            Point intersection = new Point();
            
            if (line1.gradient == line2.gradient)
            {
                intersection.x = null;
                intersection.y = null;
            }
            else
            {
                intersection.x = (line1.ordinate_intersection - line2.ordinate_intersection) / (line2.gradient - line1.gradient);
                intersection.y = line1.gradient * intersection.x + line1.ordinate_intersection;
            }
            return intersection;
        }
    }
}
