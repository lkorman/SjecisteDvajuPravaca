using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SjecišteDvaPravca
{

    public struct Point {
        public double? x, y;
    }
    public class Line
    {
        public double gradient;
        public double ordinate_intersection;
        public Line(double gradient, double ordinate_intersection) { 
            this.gradient = gradient;
            this.ordinate_intersection = ordinate_intersection;
        }

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
