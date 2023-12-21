using Styria.MVVM.Model;
using Styria.MVVM.ViewModel;
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
using Styria.MVVM.View.Effects;
using Styria.MVVM.View.Types;

namespace Styria.MVVM.View
{
    /// <summary>
    /// Interaction logic for TabControl.xaml
    /// </summary>
    public partial class TabControl : UserControl
    {
        WrapPanel tab = new WrapPanel();

        Dictionary<char, int> stringHeights = new Dictionary<char, int>
        {
            {'e',  32}, {'B', 52}, {'G', 72}, {'D', 92}, {'A', 112}, {'E', 132}
        };

        int tab_width = 312;
        int typeHeight = 18;

        EffectViewThemes effectViewThemes;
        TypeViewThemes typeViewThemes;

        public TabControl()
        {
            InitializeComponent();

            this.DataContext = new MainViewModel();

            MainViewModel viewModel = (MainViewModel)this.DataContext;

            effectViewThemes = new EffectViewThemes();
            typeViewThemes = new TypeViewThemes();

            // tab
            tab.Background = new BrushConverter().ConvertFromString("#1c1c1c") as SolidColorBrush;
            CreateTabBlock(viewModel.tb1);
            CreateTabBlock(viewModel.tb2);
            CreateTabBlock(viewModel.tb1);
            CreateTabBlock(viewModel.tb1);
            CreateTabBlock(viewModel.tb2);

            Content = tab;
        }

        private void CreateTabBlock(Tab _tab)
        {
            int l_margin_width = tab_width + 20;
            // Canvas
            Canvas tabBlock = new Canvas();
            tabBlock.Background = new BrushConverter().ConvertFromString("#3c3c3c") as SolidColorBrush;
            tabBlock.HorizontalAlignment = HorizontalAlignment.Center;
            tabBlock.VerticalAlignment = VerticalAlignment.Center;
            tabBlock.Height = 170; tabBlock.Width = l_margin_width;


            // Strings
            Line eString = new Line();
            eString.Stroke = Brushes.White;
            eString.StrokeThickness = 1;
            eString.X1 = 0; eString.Y1 = 40;
            eString.X2 = l_margin_width; eString.Y2 = 40;
            tabBlock.Children.Add(eString);

            Line bstring = new Line();
            bstring.Stroke = Brushes.White;
            bstring.StrokeThickness = 1;
            bstring.X1 = 0; bstring.Y1 = 60;
            bstring.X2 = l_margin_width; bstring.Y2 = 60;
            tabBlock.Children.Add(bstring);

            Line gString = new Line();
            gString.Stroke = Brushes.White;
            gString.StrokeThickness = 1;
            gString.X1 = 0; gString.Y1 = 80;
            gString.X2 = l_margin_width; gString.Y2 = 80;
            tabBlock.Children.Add(gString);

            Line dString = new Line();
            dString.Stroke = Brushes.White;
            dString.StrokeThickness = 1;
            dString.X1 = 0; dString.Y1 = 100;
            dString.X2 = l_margin_width; dString.Y2 = 100;
            tabBlock.Children.Add(dString);

            Line aString = new Line();
            aString.Stroke = Brushes.White;
            aString.StrokeThickness = 1;
            aString.X1 = 0; aString.Y1 = 120;
            aString.X2 = l_margin_width; aString.Y2 = 120;
            tabBlock.Children.Add(aString);

            Line EString = new Line();
            EString.Stroke = Brushes.White;
            EString.StrokeThickness = 1;
            EString.X1 = 0; EString.Y1 = 140;
            EString.X2 = l_margin_width; EString.Y2 = 140;
            tabBlock.Children.Add(EString);


            //Borders
            Line leftBorder = new Line();
            leftBorder.Stroke = Brushes.White;
            leftBorder.StrokeThickness = 10;
            leftBorder.X1 = 0; leftBorder.Y1 = 40;
            leftBorder.X2 = 0; leftBorder.Y2 = 140;
            tabBlock.Children.Add(leftBorder);

            Line rightBorder = new Line();
            rightBorder.Stroke = Brushes.White;
            rightBorder.StrokeThickness = 2;
            rightBorder.X1 = l_margin_width; rightBorder.Y1 = 40;
            rightBorder.X2 = l_margin_width; rightBorder.Y2 = 140;
            tabBlock.Children.Add(rightBorder);


            //Tabs

            int x = 20;

            int div = l_margin_width / _tab.TimeSignature.beats;

            int delta = 0; int prev_delta;

            foreach (var t in _tab.tabNotes)
            {
                prev_delta = delta;
                delta = div / (t.Duration / _tab.TimeSignature.noteValue);

                foreach (var note in t.Notes)
                {
                    TextBlock _tb = new TextBlock();
                    _tb.Width = 14; _tb.Height = 14;
                    _tb.TextAlignment = TextAlignment.Center;
                    _tb.Background = new BrushConverter().ConvertFromString("#3c3c3c") as SolidColorBrush;
                    _tb.Foreground = Brushes.White; _tb.FontWeight = FontWeights.Bold;
                    _tb.Text = note.Fret.ToString();

                    int h = stringHeights[note.String];

                    Canvas.SetTop(_tb, h); Canvas.SetLeft(_tb, x);
                    tabBlock.Children.Add(_tb);

                    switch (t.EffectID)
                    {
                        case 1:
                            tabBlock.Children.Add(effectViewThemes.ConnectNotesCurve(new System.Windows.Point(x + 7, h), new System.Windows.Size(30, 15), new System.Windows.Point(x + delta + 7, h)));
                            break;
                        case 2:
                            tabBlock.Children.Add(effectViewThemes.ConnectNotesSlide(new System.Windows.Point(x + 14, h+4), new System.Windows.Point(x + delta, h+12)));
                            break;
                        case 3:
                            tabBlock.Children.Add(effectViewThemes.ConnectNotesCurve(new System.Windows.Point(x + 7, h), new System.Windows.Size(30, 15), new System.Windows.Point(x + delta + 7, h)));
                            tabBlock.Children.Add(effectViewThemes.ConnectNotesSlide(new System.Windows.Point(x + 14, h + 4), new System.Windows.Point(x + delta, h + 12)));
                            break;
                        case 4:
                            tabBlock.Children.Add(effectViewThemes.ConnectNotesSlide(new System.Windows.Point(x - (prev_delta / 2), h + 10), new System.Windows.Point(x, h+6)));
                            break;
                        case 5:
                            tabBlock.Children.Add(effectViewThemes.ConnectNotesSlide(new System.Windows.Point(x - (prev_delta / 2), h + 6), new System.Windows.Point(x, h + 10)));
                            break;
                        case 6:
                            tabBlock.Children.Add(effectViewThemes.ConnectNotesSlide(new System.Windows.Point(x+14, h + 10), new System.Windows.Point(x + 7 + (delta / 2), h + 6)));
                            break;
                        case 7:
                            tabBlock.Children.Add(effectViewThemes.ConnectNotesSlide(new System.Windows.Point(x+14, h + 6), new System.Windows.Point(x +  7 + (delta / 2), h + 10)));
                            break;
                        default: break;
                    }

                    switch (note.TypeID)
                    {
                        case 3:
                            TextBlock typeTB = typeViewThemes.TextBlockType("A.H.");
                            Canvas.SetTop(typeTB, typeHeight); Canvas.SetLeft(typeTB, x);
                            tabBlock.Children.Add(typeTB);
                            break;
                        case 4:
                            typeTB = typeViewThemes.TextBlockType("Harm.");
                            Canvas.SetTop(typeTB, typeHeight); Canvas.SetLeft(typeTB, x);
                            tabBlock.Children.Add(typeTB);
                            break;
                        case 5:
                            typeTB = typeViewThemes.TextBlockType("P.M.");
                            Canvas.SetTop(typeTB, typeHeight); Canvas.SetLeft(typeTB, x);
                            tabBlock.Children.Add(typeTB);
                            break;
                        case 6:
                            tabBlock.Children.Add(typeViewThemes.GetVibratoType(typeHeight+9, x, x + delta - 2, false));
                            break;
                        case 7:
                            tabBlock.Children.Add(typeViewThemes.GetVibratoType(typeHeight+9, x, x + delta - 2, true));
                            break;
                        case 8:
                            tabBlock.Children.Add(typeViewThemes.GetVibratoType(typeHeight + 9, x, x + delta - 2, false, 8, 8, 0.5f));
                            break;
                        case 9:
                            tabBlock.Children.Add(typeViewThemes.GetVibratoType(typeHeight + 9, x, x + delta - 2, true, 8, 8, 0.5f));
                            break;
                        default:
                            break;
                    }

                }

                x += delta;
            }


            tab.Children.Add(tabBlock);
        }


    }
}
