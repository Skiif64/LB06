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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace kub
{
    
    public partial class Graph : Page
    {
        const int countDot = 30;

        List<double[]> dataList = new List<double[]>();

        DrawingGroup drawingGroup = new DrawingGroup();
        public Graph()
        {
            InitializeComponent();

            DataFill();
            Execute();

            image1.Source = new DrawingImage(drawingGroup);
        }

        private void DataFill()
        {
            double[] sin = new double[countDot + 1];
            double[] cos = new double[countDot + 1];

            for(int i=0;i<sin.Length;i++)
            {
                double angle = Math.PI * 2 / countDot * i;
                sin[i] = Math.Sin(angle);
                cos[i] = Math.Cos(angle);
            }

            dataList.Add(sin);
            dataList.Add(cos);
        }

        private void BackgroundFun()
        {
            GeometryDrawing geometryDrawing = new GeometryDrawing();

            RectangleGeometry rectGeometry = new RectangleGeometry();

            rectGeometry.Rect = new Rect(0, 0, 1, 1);
            geometryDrawing.Geometry = rectGeometry;

            geometryDrawing.Pen = new Pen(Brushes.Red, 0.005);
            geometryDrawing.Brush = Brushes.Beige;

            drawingGroup.Children.Add(geometryDrawing);
        }

        private void GridFun()
        {
            GeometryGroup geometryGroup = new GeometryGroup();

            for(int i=1;i<10;i++)
            {
                LineGeometry line = new LineGeometry(new Point(1.0, i*0.1),
                    new Point(-0.1,i*0.1));
                geometryGroup.Children.Add(line);
            }

            GeometryDrawing geometryDrawing = new GeometryDrawing();

            geometryDrawing.Geometry = geometryGroup;

            geometryDrawing.Pen = new Pen(Brushes.Gray, 0.003);
            double[] dashes = { 1, 1, 1, 1, 1 };
            geometryDrawing.Pen.DashStyle = new DashStyle(dashes, -1);

            geometryDrawing.Brush = Brushes.Beige;

            drawingGroup.Children.Add(geometryDrawing);
        }

        private void SinFun()
        {
            GeometryGroup geometryGroup = new GeometryGroup();

            for(int i=0;i<dataList[0].Length-1;i++)
            {
                LineGeometry line = new LineGeometry(new Point((double)i / (double)countDot,
                    0.5 - (dataList[0][i] / 2.0)), new Point((double)(i + 1) / (double)countDot,
                    0.5 - (dataList[0][i + 1] / 2.0)));
                geometryGroup.Children.Add(line);
            }

            GeometryDrawing geometryDrawing = new GeometryDrawing();
            geometryDrawing.Geometry = geometryGroup;

            geometryDrawing.Pen = new Pen(Brushes.Blue, 0.005);

            drawingGroup.Children.Add(geometryDrawing);
        }

        private void CosFun()
        {
            GeometryGroup geometryGroup = new GeometryGroup();

            for(int i = 0; i < dataList[1].Length;i++)
            {
                EllipseGeometry ellips = new EllipseGeometry(new Point((double)i 
                    / (double)countDot,0.5 - (dataList[1][i]/2.0))
                    ,0.01,0.01);
                geometryGroup.Children.Add(ellips);
            }

            GeometryDrawing geometryDrawing = new GeometryDrawing();
            geometryDrawing.Geometry = geometryGroup;
            geometryDrawing.Pen = new Pen(Brushes.Green, 0.005);

            drawingGroup.Children.Add(geometryDrawing);
        }

        private void MarkerFun()
        {
            GeometryGroup geometryGroup = new GeometryGroup();

            for(int i = 0; i<=10;i++)
            {
                FormattedText formattedText = new FormattedText(
                    String.Format("{0,7:F}", 1 - i * 0.2),
                    System.Globalization.CultureInfo.InvariantCulture,
                    FlowDirection.LeftToRight,
                    new Typeface("Verdana"),
                    0.05,
                    Brushes.Black);

                formattedText.SetFontWeight(FontWeights.Bold);

                Geometry geometry = formattedText.BuildGeometry(new Point(-0.2, i * 0.1 - 0.03));
                geometryGroup.Children.Add(geometry);
            }

            GeometryDrawing geometryDrawing = new GeometryDrawing();
            geometryDrawing.Geometry = geometryGroup;

            geometryDrawing.Brush = Brushes.LightGray;
            geometryDrawing.Pen = new Pen(Brushes.Gray, 0.003);

            drawingGroup.Children.Add(geometryDrawing);
        }

        private void Execute()
        {
            BackgroundFun();
            GridFun();
            SinFun();
            CosFun();
            MarkerFun();
        }
    }
}
