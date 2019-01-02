using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

// Author: Brian Clayton (brianpclayton@gmail.com)
namespace RPSQuestion3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected double DegreesMinutesSecondsToDegrees(int degrees, int minutes, int seconds)
        {
            return (double)degrees + ((double)minutes + ((double)seconds / 60)) / 60;
        }

        public MainWindow()
        {
            InitializeComponent();
            // Given the following information of a map window, find the pixel location of   
            // 71° 20’ 13” W, 41° 33’ 24” N.  
            // Lower left map window coordinate: 71° 48’ 56” W, 41° 16’ 0” N  
            // Upper right map window coordinate: 70° 13’ 3” W, 41° 56’ 39” N  
            // Map window size in pixels: 1585 px width x 895 px height
            drawingCanvas.Background = Brushes.LightBlue;
            TextBlock locationLabel = new TextBlock
            {
                Foreground = Brushes.Black
            };
            Ellipse locationCircle = new Ellipse
            {
                Fill = Brushes.DarkBlue,
                Height = 3,
                Width = 3
            };
            double leftLon = DegreesMinutesSecondsToDegrees(71, 48, 56);
            double lowerLat = DegreesMinutesSecondsToDegrees(41, 16, 0);
            double rightLon = DegreesMinutesSecondsToDegrees(70, 13, 3);
            double upperLat = DegreesMinutesSecondsToDegrees(41, 56, 39);
            double locationLon = DegreesMinutesSecondsToDegrees(71, 20, 13);
            double locationLat = DegreesMinutesSecondsToDegrees(41, 33, 24);
            double leftOffset = Math.Round((locationLon - leftLon) / (rightLon - leftLon) * drawingCanvas.Width);
            double topOffset = Math.Round((locationLat - upperLat) / (lowerLat - upperLat) * drawingCanvas.Height);
            locationLabel.Text = String.Format("plot left: {0}, plot top: {1}", leftOffset, topOffset);
            Canvas.SetLeft(locationLabel, leftOffset + 5);
            Canvas.SetTop(locationLabel, topOffset + 5);
            drawingCanvas.Children.Add(locationLabel);
            Canvas.SetLeft(locationCircle, leftOffset - 1); // To account for 3 pixel width
            Canvas.SetTop(locationCircle, topOffset - 1); // To account for 3 pixel height
            drawingCanvas.Children.Add(locationCircle);
        }
    }
}
