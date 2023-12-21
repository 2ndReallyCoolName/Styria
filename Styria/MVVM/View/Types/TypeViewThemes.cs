using System;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Styria.MVVM.View.Types
{
    public class TypeViewThemes
    {
        public TextBlock TextBlockType(string type)
        {

            TextBlock textBlock = new TextBlock
            {
                FontSize = 9,
                FontWeight = FontWeights.Bold,
                FontStyle = FontStyles.Italic,
                Text = type,
                Foreground = new SolidColorBrush(Colors.White),
            };

            return textBlock;
        }

        public Polyline GetVibratoType(int height, int startX, int endX, bool wide, int sz_x=4, int sz_y=4, float dist=0.25f)
        {
            Polyline polyline = new Polyline
            {
                Stroke = System.Windows.Media.Brushes.GhostWhite,
                StrokeThickness = wide ? 2 : 1,
                FillRule = FillRule.EvenOdd
            };

            PointCollection pointCollection = new PointCollection();

            int dx1 = (int)Math.Floor(sz_x * dist);

            for(int x = startX; x < endX; x+= sz_x)
            {
                pointCollection.Add(new System.Windows.Point { X = x, Y = height});
                pointCollection.Add(new System.Windows.Point { X = x+dx1, Y = height - sz_y});
            }

            polyline.Points = pointCollection;
            
            return polyline;
        }

    }
}
