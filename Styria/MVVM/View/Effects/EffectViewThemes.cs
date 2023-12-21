using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Styria.MVVM.View.Effects
{
    public class EffectViewThemes
    {
        public System.Windows.Shapes.Path ConnectNotesCurve(System.Windows.Point startPoint, System.Windows.Size size, System.Windows.Point endPoint)
        {
            System.Windows.Shapes.Path path = new System.Windows.Shapes.Path();
            path.Stroke = Brushes.White;
            path.StrokeThickness = 1;


            PathGeometry pathGeometry = new PathGeometry();
            PathFigureCollection pathFigureCollection = new PathFigureCollection();
            PathFigure pathFigure = new PathFigure
            {
                StartPoint = startPoint
            };

            PathSegmentCollection pathSegmentCollection = new PathSegmentCollection();

            ArcSegment arcSegment = new ArcSegment
            {
                Size = size,
                RotationAngle = 0,
                IsLargeArc = false,
                SweepDirection = System.Windows.Media.SweepDirection.Clockwise,
                Point = endPoint
            };

            pathSegmentCollection.Add(arcSegment);
            pathFigure.Segments = pathSegmentCollection;
            pathFigureCollection.Add(pathFigure);
            pathGeometry.Figures = pathFigureCollection;
            path.Data = pathGeometry;

            return path;
        }

        public Line ConnectNotesSlide(System.Windows.Point startPoint, System.Windows.Point endPoint)
        {
            Line line = new Line();
            line.Stroke = Brushes.White;
            line.StrokeThickness = 2;
            line.X1 = startPoint.X; line.Y1 = startPoint.Y;
            line.X2 = endPoint.X; line.Y2 = endPoint.Y;
            return line;
        }

    }
}
