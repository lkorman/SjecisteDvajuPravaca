using InteractiveDataDisplay.WPF;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace SjecišteDvaPravca
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static double InitialValue = 0;
        Line line1 = new Line(InitialValue, InitialValue);
        Line line2 = new Line(InitialValue, InitialValue);
        Point LinesIntersection;

        String TextBoxK1_Text;
        String TextBoxK2_Text;
        String TextBoxOrdinate1_Text;
        String TextBoxOrdinate2_Text;

        Graph GraphWindow;

        public MainWindow()
        {
            InitializeComponent();
            TextBoxK1.Text = InitialValue.ToString();
            TextBoxK2.Text = InitialValue.ToString();
            TextBoxOrdinate1.Text = InitialValue.ToString();
            TextBoxOrdinate2.Text = InitialValue.ToString();
            LinesIntersection = Line.Intersection(line1, line2);
            CalculateInstersectionPointAndDrawGraph();
        }

        /// <summary>
        /// Event function that is called when text is chnaged in textbox TextBoxK1. It checks 
        /// if new text can be parsed to a double. If can be parsed it calls DisplayLinesIntersectionPoint 
        /// function. If not, it will set text to the last value and the function 
        /// DisplayLinesIntersectionPoint will not be called.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChangedTextBoxK1(object sender, TextChangedEventArgs e)
        {
            if (TextBoxK1.Text == "-") {
                line1.gradient = 0;
            }
            else if (!Double.TryParse(TextBoxK1.Text, out line1.gradient))
            {
                TextBoxK1.Text = TextBoxK1_Text;
                return;
            }
            CalculateInstersectionPointAndDrawGraph();
        }

        /// <summary>
        /// Event function that is called to preview the text. This function stores
        /// the current text of the TextBoxK1 before the text is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveCurrentValueTextBoxBoxK1(object sender, TextCompositionEventArgs e)
        {
            TextBoxK1_Text = TextBoxK1.Text;
        }

        /// <summary>
        /// Event function that is called when text is chnaged in textbox TextBoxK2. It checks 
        /// if new text can be parsed to a double. If can be parsed it calls DisplayLinesIntersectionPoint 
        /// function. If not, it will set text to the last value and the function 
        /// DisplayLinesIntersectionPoint will not be called.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChangedTextBoxK2(object sender, TextChangedEventArgs e)
        {
            if (TextBoxK2.Text == "-")
            {
                line2.gradient = 0;
            }
            else if (!Double.TryParse(TextBoxK2.Text, out line2.gradient))
            {
                TextBoxK2.Text = TextBoxK2_Text;
                return;
            }
            CalculateInstersectionPointAndDrawGraph();
        }


        /// <summary>
        /// Event function that is called to preview the text. This function stores
        /// the current text of the TextBoxK2 before the text is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveCurrentValueTextBoxBoxK2(object sender, TextCompositionEventArgs e)
        {
            TextBoxK2_Text = TextBoxK2.Text;
        }


        /// <summary>
        /// Event function that is called when text is chnaged in textbox TextBoxOrdinate1. It checks 
        /// if new text can be parsed to a double. If can be parsed it calls DisplayLinesIntersectionPoint 
        /// function. If not, it will set text to the last value and the function 
        /// DisplayLinesIntersectionPoint will not be called.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChangedTextBoxOrdinate1(object sender, TextChangedEventArgs e)
        {
            if (TextBoxOrdinate1.Text == "-")
            {
                line1.ordinate_intersection = 0;
            }
            else if (!Double.TryParse(TextBoxOrdinate1.Text, out line1.ordinate_intersection))
            {
                TextBoxOrdinate1.Text = TextBoxOrdinate1_Text;
                return;
            }
            CalculateInstersectionPointAndDrawGraph();
        }

        /// <summary>
        /// Event function that is called to preview the text. This function stores
        /// the current text of the TextBoxOrdinate1 before the text is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveCurrentValueTextBoxOrdinate1(object sender, TextCompositionEventArgs e)
        {
            TextBoxOrdinate1_Text = TextBoxOrdinate1.Text;
        }


        /// <summary>
        /// Event function that is called when text is chnaged in textbox TextBoxOrdinate2. It checks 
        /// if new text can be parsed to a double. If can be parsed it calls DisplayLinesIntersectionPoint 
        /// function. If not, it will set text to the last value and the function 
        /// DisplayLinesIntersectionPoint will not be called.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChangedTextBoxOrdinate2(object sender, TextChangedEventArgs e)
        {
            if (TextBoxOrdinate2.Text == "-")
            {
                line2.ordinate_intersection = 0;
            }
            else if(!Double.TryParse(TextBoxOrdinate2.Text, out line2.ordinate_intersection))
            {
                TextBoxOrdinate2.Text = TextBoxOrdinate2_Text;
                return;
            }
            CalculateInstersectionPointAndDrawGraph();
        }

        /// <summary>
        /// Event function that is called to preview the text. This function stores
        /// the current text of the TextBoxOrdinate2 before the text is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveCurrentValueTextBoxOrdinate2(object sender, TextCompositionEventArgs e)
        {
            TextBoxOrdinate2_Text = TextBoxOrdinate2.Text;
        }

        /// <summary>
        /// Button event instantiate new Graph window if needed and brings it to the front.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (GraphWindow == null || !GraphWindow.IsLoaded)
            {
                GraphWindow = new Graph();
                GraphWindow.Show();
            }
            GraphWindow.Activate();
            CalculateInstersectionPointAndDrawGraph();
        }

        /// <summary>
        /// Function calculats intersection of Line1 and Line 2.
        /// If GraphWindow is oppened it draws Line1 and Line2 on graph.
        /// </summary>
        private void CalculateInstersectionPointAndDrawGraph()
        {
            LinesIntersection = Line.Intersection(line1, line2);

            if (LinesIntersection.x == null && LinesIntersection.y == null)
            {
                LabelParallel.Visibility = Visibility.Visible;
                Xlabel.Content = "";
                Ylabel.Content = "";
                LabelTextX.Visibility = Visibility.Hidden;
                LabelTextY.Visibility = Visibility.Hidden;
                ButtonGraph.Visibility = Visibility.Hidden;
            }
            else
            {
                LabelParallel.Visibility = Visibility.Hidden;
                LabelTextX.Visibility = Visibility.Visible;
                LabelTextY.Visibility = Visibility.Visible;
                ButtonGraph.Visibility = Visibility.Visible;
                Xlabel.Content = LinesIntersection.x.ToString();
                Ylabel.Content = LinesIntersection.y.ToString();
            }

            if (GraphWindow != null && GraphWindow.IsLoaded)
            {
                GraphWindow.PlotLinesIntersection(line1, line2);
            }
        }

        /// <summary>
        /// Event function that calls select text in the text box when text box is
        /// double clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectEverythignInTextBoxOnDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectText(sender);
        }

        /// <summary>
        /// Event function that calls select text in the text box when text box is
        /// selected with keyboard keys (e.g. Tab key).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectEverythignInTextBoxKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            SelectText(sender);
        }

        /// <summary>
        /// Function that selects whole text in text box if object sender is instance of 
        /// TextBox.
        /// </summary>
        /// <param name="sender">Object which is expected to instance of textbox</param>
        void SelectText(object sender)
        {
            TextBox tb = (sender as TextBox);

            if (tb != null)
            {
                tb.SelectAll();
            }
        }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (GraphWindow != null && GraphWindow.IsLoaded)
            {
                GraphWindow.Close();
            }
        }
    }
}
