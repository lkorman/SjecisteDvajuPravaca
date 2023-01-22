using InteractiveDataDisplay.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Windows.Forms.LinkLabel;

namespace SjecišteDvaPravca
{
    /// <summary>
    /// Interaction logic for Graph.xaml
    /// </summary>
    public partial class Graph : Window
    {
        double xscale = 0.5;
        LineGraph LineGraph1;
        LineGraph LineGraph2;
        LineGraph abscissa; 
        LineGraph ordinate;


        public Graph()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Function that draws coordinate system and provided lines close 
        /// to their intersection (+/-intersection.x*(1+xscale)).
        /// </summary>
        /// <param name="line1">First line to draw</param>
        /// <param name="line2">Second line to draw</param>
        public void PlotLinesIntersection(Line line1, Line line2)
        {

            // Izmijeniti crtanje grafa na način da se umjesto računjanja niza točaka izračunaju samo početne i kranje točke
            // i da se koordinatni sustav prikazuje od do +/- maks abs(y) i +/- maks abs(x) i da apscisa i ordinata budu u tom rangu iscratne

            var intersection = Line.Intersection(line1, line2);
            if (intersection.x == null && intersection.y == null) {
                return;
            }
            
            double[] x = { -(double)intersection.x * (1 + xscale), (double)intersection.x * (1 + xscale) };
            //in case if intesrction.x is zero
            if (x[0] == 0)
            {
                x[0] = -xscale;
                x[1] = xscale;
            }
            
            if (lines.Children.Count > 0) {
                lines.Children.Remove(LineGraph1);
                lines.Children.Remove(LineGraph2);
                lines.Children.Remove(abscissa);
                lines.Children.Remove(ordinate);
            }
            
            var y1 = x.Select(v => line1.GetY(v)).ToArray();
            var y2 = x.Select(v => line2.GetY(v)).ToArray();
            

            var ymin = y1.Min() >= y2.Min() ? y2.Min() : y1.Min();
            var ymax = y1.Max() <= y2.Max() ? y2.Max() : y1.Max();
            
            //Ordinate and Abscissa
            ordinate = new LineGraph();
            lines.Children.Add(ordinate);
            ordinate.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            ordinate.Description = String.Format("y");
            ordinate.StrokeThickness = 3;
            double[] x_ordinate = { 0, 0 };
            double[] y_ordinate = { ymin, ymax };
            ordinate.Plot(x_ordinate, y_ordinate);

            abscissa = new LineGraph();
            lines.Children.Add(abscissa);
            abscissa.Stroke = new SolidColorBrush(Color.FromRgb(128, 128, 128));
            abscissa.Description = String.Format("x");
            abscissa.StrokeThickness = 3;
            double[] x_abscissa = { x[0], x[x.Length - 1] };
            double[] y_abscissa = { 0, 0 };
            abscissa.Plot(x_abscissa, y_abscissa);


            //Lines
            LineGraph1 = new LineGraph();
            lines.Children.Add(LineGraph1);
            LineGraph1.Stroke = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            LineGraph1.Description = String.Format("Pravac 1");
            LineGraph1.StrokeThickness = 2;
            LineGraph1.Plot(x, y1);

            LineGraph2 = new LineGraph();
            lines.Children.Add(LineGraph2);
            LineGraph2.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 255));
            LineGraph2.Description = String.Format("Pravac 2");
            LineGraph2.StrokeThickness = 2;
            LineGraph2.Plot(x, y2);
            
            CartesianGraph.PlotOriginX = x[0];
            CartesianGraph.PlotWidth = x[x.Length - 1] - x[0];

            CartesianGraph.PlotOriginY = ymin;
            CartesianGraph.PlotHeight = ymax - ymin;
        }
    }
}
