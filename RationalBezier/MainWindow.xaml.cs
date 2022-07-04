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


public class Point3D
{
    public double X;
    public double Y;
    public double Z;

    public Point3D(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public Point3D(Point P, double w)
    {
        X = P.X * w;
        Y = P.Y * w;
        Z = w;
    }

    public Point P2D()
    {
        if(Z != 0) return new Point(X / Z, Y / Z);
        return new Point(X, Y);
    }

    public static Point3D operator *(double a, Point3D P) => new Point3D(a * P.X, a * P.Y, a * P.Z);
    public static Point3D operator +(Point3D P, Point3D Q) => new Point3D(P.X + Q.X, P.Y + Q.Y, P.Z + Q.Z);
    public static Vector3D operator -(Point3D P, Point3D Q) => new Vector3D(P.X - Q.X, P.Y - Q.Y, P.Z - Q.Z);
}

public class Vector3D
{
    public double X;
    public double Y;
    public double Z;

    public Vector3D(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public Vector3D(Point3D A, Point3D B)
    {
        X = B.X - A.X;
        Y = B.Y - A.Y;
        Z = B.Z - A.Z;
    }

    public Vector V2D()
    {
        if(Z != 0) return new Vector(X / Z, Y / Z);

        return new Vector(X, Y);
    }

    public double Length()
    {
        return Math.Sqrt(X * X + Y * Y + Z * Z);
    }

    public static Vector3D operator *(double a, Vector3D v) => new Vector3D(a * v.X, a * v.Y, a * v.Z);
}

public class MyEllipse
{
    public Point S;
    Point V;
    double a;
    double b;

    public MyEllipse(Point _S,  Canvas _g)
    {
        S = _S;
        DrawMid(_g);
    }

    public MyEllipse()
    {

    }

    public void Update(Point _V, Canvas _g)
    {
        V = _V;
        a = Math.Abs(S.X - V.X);
        b = Math.Abs(S.Y - V.Y);
        DrawMid(_g);
        DrawLine(_g);
    }

    public void DrawMid(Canvas _g)
    {
        Ellipse E = new Ellipse
        {
            Fill = new SolidColorBrush(Colors.Black),
            Width = 4,
            Height = 4
        };

        Canvas.SetLeft(E, S.X - 2);
        Canvas.SetTop(E, S.Y - 2);
        Canvas.SetZIndex(E,50);
        _g.Children.Add(E);
    }

    public void DrawLine(Canvas _g)
    {
        Line L = new Line
        {
            Stroke = new SolidColorBrush(Colors.Gray),
            X1 = S.X,
            X2 = V.X,
            Y1 = S.Y,
            Y2 = V.Y,
            StrokeThickness = 1,
        };
        Canvas.SetZIndex(L, 20);
        _g.Children.Add(L);

        Ellipse E = new Ellipse
        {
            Fill = new SolidColorBrush(Colors.Gray),
            Width = 2,
            Height = 2
        };
        
        Canvas.SetLeft(E, V.X - 1);
        Canvas.SetTop(E, V.Y - 1);
        Canvas.SetZIndex(E, 10);
        _g.Children.Add(E);

        
    }


    public List<Point> QuadrantPoints(int k)
    {
        List<Point> L = new List<Point>();

        if (k == 1)
        {
            L.Add(new Point(S.X, S.Y + b));
            L.Add(new Point(S.X + a, S.Y + b));
            L.Add(new Point(S.X + a, S.Y));
            
            return L;
        }

        if (k == 2)
        {
            L.Add(new Point(S.X - a, S.Y));
            L.Add(new Point(S.X - a, S.Y + b));
            L.Add(new Point(S.X, S.Y + b));
            
            return L;
        }

        if (k == 3)
        {
            L.Add(new Point(S.X, S.Y - b));
            L.Add(new Point(S.X - a, S.Y - b));
            L.Add(new Point(S.X - a, S.Y));
            
            return L;
        }

        L.Add(new Point(S.X + a, S.Y));
        L.Add(new Point(S.X + a, S.Y - b));
        L.Add(new Point(S.X, S.Y - b));
        
        return L;
    }

    public List<double> QuadrantPointsWeights(int k)
    {
        List<double> w = new List<double>();

        if (k == 1)
        {
            w.Add(2.0);
            w.Add(1.0);
            w.Add(1.0);
            
            return w;
        }

        if (k == 2)
        {
            
            w.Add(1.0);
            w.Add(1.0);
            w.Add(2.0);
            return w;
        }

        if (k == 3)
        {
            w.Add(2.0);
            w.Add(1.0);
            w.Add(1.0);

            return w;
        }

        w.Add(1.0);
        w.Add(1.0);
        w.Add(2.0);

        return w;
    }
}

public class MyTriangle
{
    public List<Point> V;
    public List<double> d;
    public List<double> sinV;

    public Point S;
    public double r;
    public List<Point> T;


    public MyTriangle(Point P)
    {
        V = new List<Point>();
        T = new List<Point>();
        sinV = new List<double>();
        d = new List<double>();

        V.Add(P);
    }

    void CountInfo()
    {
        d.Clear();
        sinV.Clear();
        T.Clear();

        for (int i = 0; i < 3; i++)
        {
            d.Add(Length(V[(i + 1) % 3], V[(i + 2) % 3]));
        }

        S = new Point((d[0] * V[0].X + d[1] * V[1].X + d[2] * V[2].X) / (d[0] + d[1] + d[2]), 
                      (d[0] * V[0].Y + d[1] * V[1].Y + d[2] * V[2].Y) / (d[0] + d[1] + d[2]));

        double tmp = (d[0] + d[1] + d[2]) / 2;
        r = Math.Sqrt((tmp - d[0]) * (tmp - d[1]) * (tmp - d[2]) / tmp);
        

        for (int i = 0; i < 3; i++)
        {
            sinV.Add(sinP(S, V[i], V[(i + 1) % 3]));

            Vector N = V[(i + 1) % 3] - V[(i + 2) % 3];
            N = new Vector(-N.Y, N.X);
            N.Normalize();

            Vector Tmp = V[i] - S;
            if (Tmp.X * N.X + Tmp.Y * N.Y > 0) N = -N;

            T.Add(S + r * N);
        }
    }

    public void Update(Point P)
    {
        if (V.Count == 3)
        {
            V.Clear();
            d.Clear();
            sinV.Clear();
            T.Clear();
            V.Add(P);
        }
        else if (V.Count == 2)
        {
            V.Add(P);
            CountInfo();
        }
        else
        {
            V.Add(P);
        }
    }


    public void DrawTriangle(Canvas _g)
    {
        for (int i = 0; i < V.Count; i++)
        {

            Ellipse E = new Ellipse
            {
                Fill = new SolidColorBrush(Colors.Black),
                Width = 6,
                Height = 6
            };

            Canvas.SetLeft(E, V[i].X - 3);
            Canvas.SetTop(E, V[i].Y - 3);
            Canvas.SetZIndex(E, 10);
            _g.Children.Add(E);

            if (i > 0)
            {
                Line L = new Line
                {
                    Stroke = new SolidColorBrush(Colors.Black),
                    X1 = V[i].X,
                    X2 = V[i - 1].X,
                    Y1 = V[i].Y,
                    Y2 = V[i - 1].Y,
                    StrokeThickness = 1,
                };
                Canvas.SetZIndex(L, 20);
                _g.Children.Add(L);

                if (i == 2)
                {
                    Line K = new Line
                    {
                        Stroke = new SolidColorBrush(Colors.Black),
                        X1 = V[i].X,
                        X2 = V[0].X,
                        Y1 = V[i].Y,
                        Y2 = V[0].Y,
                        StrokeThickness = 1,
                    };
                    Canvas.SetZIndex(K, 20);
                    _g.Children.Add(K);

                    for (int j = 0; j < 3; j++)
                    {
                        Ellipse F = new Ellipse
                        {
                            Fill = new SolidColorBrush(Colors.Magenta),
                            Width = 6,
                            Height = 6
                        };

                        Canvas.SetLeft(F, T[j].X - 3);
                        Canvas.SetTop(F, T[j].Y - 3);
                        Canvas.SetZIndex(F, 10);
                        _g.Children.Add(F);
                    }

                    Ellipse G = new Ellipse
                    {
                        Fill = new SolidColorBrush(Colors.Magenta),
                        Width = 6,
                        Height = 6
                    };

                    Canvas.SetLeft(G, S.X - 3);
                    Canvas.SetTop(G, S.Y - 3);
                    Canvas.SetZIndex(G, 10);
                    _g.Children.Add(G);
                }
            }
        }
    }

    public List<Point> QuadrantPoints(int k)
    {
        List<Point> L = new List<Point>();
        L.Add(T[(k + 2) % 3]);
        L.Add(V[k]);
        L.Add(T[(k + 1) % 3]);
        return L;
    }


        public List<double> QuadrantPointsWeights(int k)
    {
        List<double> w = new List<double>();

        w.Add(1.0);
        w.Add(sinV[k]);
        w.Add(1.0);
        return w;
    }

        double Length(Point P, Point Q)
    {
        Vector U = P - Q;
        return U.Length;
    }

    double sinP (Point O, Point P, Point R)
    {
        Vector3D U = new Vector3D(new Point3D(P, 1), new Point3D(O, 1));
        Vector3D V = new Vector3D(new Point3D(P, 1), new Point3D(R, 1));

        Vector3D W = new Vector3D(U.Y - V.Y, V.X - U.X, U.X * V.Y - U.Y * V.X);
        return W.Length() / (U.Length() * V.Length());
    }

}








namespace RacionalBezier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool AllowedWeightsChange;

        List<Point> Vert;

        bool activePoint = false;
        int activePointIndex;

        bool drawRationalBezier = true;
        bool drawEllipse = false;
        bool drawIncircle = false;

        MyEllipse E;
        int IterEllipse = 0;

        MyTriangle Tri;
        int IterTriangle = 0;
        

        bool parameterVisualize = false;
        double parameter = 0.0;

        bool paramFlowVisualize = false;


        List<double> Weights;
        Vector Tangent;
        Vector Normal;

        public MainWindow()
        {
            InitializeComponent();
            Vert = new List<Point>();
            Weights = new List<double>();
            
            Tangent = new Vector();
            Normal = new Vector();

            if (Control != null)
            {
                Control.Content = "Add point - left button" + "\n" +
                                  "Delete point - right button" + "\n" +
                                  "Move point - hold left button";
            }

        }




        //do vah vieme vpisovat len desatinne cisla
        private void Weights_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            AllowedWeightsChange = true;

            //nie cislo z klavesnice
            if ((e.Key < Key.D0 || e.Key > Key.D9))
            {
                //nie cislo z numpadu
                if ((e.Key < Key.NumPad0 || e.Key > Key.NumPad9))
                {
                    //nie backspace, enter, medzera, minus alebo desatinna bodka/ciarka
                    if (!(e.Key == Key.Back || e.Key == Key.Enter || e.Key == Key.Space || e.Key == Key.Subtract || e.Key == Key.OemMinus 
                        || e.Key == Key.OemComma || e.Key == Key.Decimal))
                    {
                        AllowedWeightsChange = false;
                    }
                }
            }
        }

        private void Weights_KeyDown(object sender, KeyEventArgs e)
        {
            if (!AllowedWeightsChange)
            {
                e.Handled = true;
            }
        }

        //zmena vah
        private void Weights_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string[] indstr = WeightsTextBox.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                List<double> ind = new List<double>();

                for (int i = 0; i < indstr.Length; i++)
                {
                    ind.Add(Convert.ToDouble(indstr[i].Replace(".", ",")));
                }

                //zla dlzka vektora vah
                if (ind.Count != Vert.Count)
                {
                    MessageBox.Show("vektor vah je zlej dlzky");
                    WeightsTextBox.Text = "";
                    for (int i = 0; i < Weights.Count; i++)
                    {
                        WeightsTextBox.Text += Convert.ToString(Weights[i]) + " ";
                    }
                }
                else
                {
                    Weights = ind;
                    WeightsTextBox.Text = "";
                    for (int i = 0; i < Weights.Count; i++)
                    {
                        WeightsTextBox.Text += Convert.ToString(Weights[i]) + " ";
                    }

                    g.Children.Clear();
                    DrawRationalBezier();
                }
            }
        }


        //vykreslenie všetkých bodov hlavného riadiaceho polygónu
        public void DrawPoints()
        {
            for (int i = 0; i < Vert?.Count; i++)
            {

                Ellipse E = new Ellipse
                {
                    Fill = new SolidColorBrush(Colors.Black),
                    Width = 8,
                    Height = 8
                };

                E.MouseLeftButtonDown += new MouseButtonEventHandler(PointMouseDown);
                E.MouseRightButtonDown += new MouseButtonEventHandler(PointRightClick);

                Canvas.SetLeft(E, Vert[i].X - 4);
                Canvas.SetTop(E, Vert[i].Y - 4);


                Canvas.SetZIndex(E, i + 50);

                g.Children.Add(E);

            }
            
        }

        //označenie bodu pre jeho pohyb
        private void PointMouseDown(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < Vert.Count; i++)
            {
                if ((Vert[i].X - 5 < e.GetPosition(g).X) & (Vert[i].X + 5 > e.GetPosition(g).X) &
                    (Vert[i].Y - 5 < e.GetPosition(g).Y) & (Vert[i].Y + 5 > e.GetPosition(g).Y))
                {
                    activePoint = true;
                    activePointIndex = i;
                    break;
                }

            }

        }

        //odstránenie bodu
        private void PointRightClick(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < Vert.Count; i++)
            {
                if ((Vert[i].X - 5 < e.GetPosition(g).X) & (Vert[i].X + 5 > e.GetPosition(g).X) &
                    (Vert[i].Y - 5 < e.GetPosition(g).Y) & (Vert[i].Y + 5 > e.GetPosition(g).Y))
                {
                    Vert.RemoveAt(i);
                    Weights.RemoveAt(i);
                    WeightsTextBox.Text = "";
                    for (int j = 0; j < Weights.Count; j++)
                    {
                        WeightsTextBox.Text += Convert.ToString(Weights[j]) + " ";
                    }

                    g.Children.Clear();
                    DrawRationalBezier();
                }

            }
        }

        //pomocná metóda na vykreslenie úsečky
        public void DrawLine(Point V_1, Point V_2, SolidColorBrush b, int thck, int zInd)
        {
            Line L = new Line
            {
                Stroke = b,
                X1 = V_1.X,
                X2 = V_2.X,
                Y1 = V_1.Y,
                Y2 = V_2.Y,
                StrokeThickness = thck,
            };

            Canvas.SetZIndex(L, zInd);

            g.Children.Add(L);
        }

        //vykreslenie úsečiek hlavného riadiaceho polygónu
        public void DrawLines()
        {
            if (Vert?.Count > 1)
            {
                for (int i = 0; i < Vert.Count - 1; i++)
                {
                    DrawLine(Vert[i], Vert[i + 1], new SolidColorBrush(Colors.Black), 1, 0);
                }
            }
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (drawRationalBezier && !activePoint)
            {
                Vert.Add(e.GetPosition(g));
                Weights.Add(1.0);
                WeightsTextBox.Text += Convert.ToString(Weights.Last()) + " ";

                g.Children.Clear();
                DrawRationalBezier();
            }

            else if (drawEllipse)
            {
                if (IterEllipse % 2 == 0)
                {
                    IterEllipse++;
                    g.Children.Clear();
                    E = new MyEllipse(e.GetPosition(g), g);
                }
                else
                {
                    IterEllipse++;

                    g.Children.Clear();
                    E.Update(e.GetPosition(g), g);
                    DrawEllipse();
                }
            }
            else if(drawIncircle)
            {
                if (IterTriangle % 3 == 0)
                {
                    IterTriangle++;
                    g.Children.Clear();
                    Tri = new MyTriangle(e.GetPosition(g));
                    Tri.DrawTriangle(g);

                }
                else if(IterTriangle % 3 == 1)
                {
                    IterTriangle++;
                    Tri.Update(e.GetPosition(g));
                    Tri.DrawTriangle(g);
                }
                else
                {
                    IterTriangle++;
                    Tri.Update(e.GetPosition(g));
                    Tri.DrawTriangle(g);
                    DrawIncircle();
                }
            }
        }

        private void g_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawRationalBezier && activePoint)
            {
                switch (e.LeftButton)
                {
                    case MouseButtonState.Pressed:
                        Vert[activePointIndex] = e.GetPosition(g);
                        break;
                    case MouseButtonState.Released:
                        activePoint = false;
                        break;

                }

                g.Children.Clear();
                DrawRationalBezier();
            }
            
        }


        public Point3D Casteljau3D(double t, int level, int index, bool visualizeCasteljau)
        {
            if (level == 1)
            {
                Point3D V1 = new Point3D(Vert[index], Weights[index]);
                Point3D V2 = new Point3D(Vert[index + 1], Weights[index + 1]);
                return (1 - t) * V1 + t * V2;
            }


            Point3D W1 = Casteljau3D(t, level - 1, index, visualizeCasteljau);
            Point3D W2 = Casteljau3D(t, level - 1, index + 1, visualizeCasteljau);
            Point3D P = (1 - t) * W1 + t * W2;

            if (visualizeCasteljau)
            {
                DrawLine(W1.P2D(), W2.P2D(), new SolidColorBrush(Colors.Aquamarine), 1, 5);

                if (level == Vert.Count - 1)
                {
                    Tangent = W2.P2D() - W1.P2D();
                    Normal = new Vector(-Tangent.Y, Tangent.X);
                    Tangent.Normalize();
                    Normal.Normalize();

                    DrawLine(P.P2D(), P.P2D() + 20 * Tangent, new SolidColorBrush(Colors.Blue), 1, 6);
                    DrawLine(P.P2D(), P.P2D() + 20 * Normal, new SolidColorBrush(Colors.Red), 1, 6);
                }


            }

            if (paramFlowVisualize && level == Vert.Count - 1)
            {
                Tangent = W2.P2D() - W1.P2D();
                Normal = new Vector(-Tangent.Y, Tangent.X);
                Normal.Normalize();
                DrawLine(P.P2D() - 5 * Normal, P.P2D() + 5 * Normal, new SolidColorBrush(Colors.Magenta), 1, 15);
            }

            return P;

        }

        public void DrawRationalBezier()
        {
            if (Vert.Count > 1)
            {
                double step = 1.0 / 50;
                for (int i = 0; i < 50; i++)
                {
                    Point3D V1 = Casteljau3D(i * step, Vert.Count - 1, 0, false);
                    Point3D V2 = Casteljau3D((i + 1) * step, Vert.Count - 1, 0, false);

                    DrawLine(V1.P2D(), V2.P2D(), new SolidColorBrush(Colors.DarkMagenta), 2, 10);
                }

                if (parameterVisualize)
                {
                    Point3D C = Casteljau3D(parameter, Vert.Count - 1, 0, true);
                }
            }

            DrawPoints();
            DrawLines();
        }

        public void DrawEllipse()
        {
            if (E != null)
            {
                for (int k = 1; k < 5; k++)
                {
                    Vert.Clear();
                    Weights.Clear();
                    Vert = E.QuadrantPoints(k);
                    Weights = E.QuadrantPointsWeights(k);

                    double step = 1.0 / 50;
                    for (int i = 0; i < 50; i++)
                    {
                        Point3D V1 = Casteljau3D(i * step, Vert.Count - 1, 0, false);
                        Point3D V2 = Casteljau3D((i + 1) * step, Vert.Count - 1, 0, false);

                        DrawLine(V1.P2D(), V2.P2D(), new SolidColorBrush(Colors.DarkMagenta), 2, 10);
                    }

                    if (parameterVisualize)
                    {
                        Point3D C = Casteljau3D(parameter, Vert.Count - 1, 0, true);
                    }

                    DrawPoints();
                    DrawLines();
                }

                E.DrawLine(g);
                E.DrawMid(g);
            }
        }

        public void DrawIncircle()
        {
            if (Tri != null)
            {
                for (int k = 0; k < 3; k++)
                {
                    Vert.Clear();
                    Weights.Clear();
                    Vert = Tri.QuadrantPoints(k);
                    Weights = Tri.QuadrantPointsWeights(k);

                    double step = 1.0 / 50;
                    for (int i = 0; i < 50; i++)
                    {
                        Point3D V1 = Casteljau3D(i * step, Vert.Count - 1, 0, false);
                        Point3D V2 = Casteljau3D((i + 1) * step, Vert.Count - 1, 0, false);

                        DrawLine(V1.P2D(), V2.P2D(), new SolidColorBrush(Colors.DarkMagenta), 2, 10);
                    }

                    if (parameterVisualize)
                    {
                        Point3D C = Casteljau3D(parameter, Vert.Count - 1, 0, true);
                    }
                }
            }
        }

        private void GeneralCurve_Checked(object sender, RoutedEventArgs e)
        {
            drawRationalBezier = true;
            drawEllipse = false;
            drawIncircle = false;
            if (WeightsTextBox != null) WeightsTextBox.Text = "";

            g.Children.Clear();
            Vert?.Clear();
            Weights?.Clear();

            if (Control != null)
            {
                Control.Content = "Add point - left button" + "\n" +
                                  "Delete point - right button" + "\n" +
                                  "Move point - hold left button";
            }
                                

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            drawRationalBezier = false;
            drawEllipse = true;
            drawIncircle = false;
            WeightsTextBox.Text = "";

            g.Children.Clear();

            if (Control != null)
            {
                Control.Content = "Two leftclics on different places \nto draw ellipse, \ncannot be adjusted";
            }
        }

        private void Incircle_Checked(object sender, RoutedEventArgs e)
        {
            drawRationalBezier = false;
            drawEllipse = false;
            drawIncircle = true;
            WeightsTextBox.Text = "";

            g.Children.Clear();


            if (Control != null)
            {
                Control.Content = "Three leftclics on different places \nto draw triangle, \ncannot be adjusted";
            }
        }

        private void ParamCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            parameterVisualize = true;

            g.Children.Clear();
            if (drawRationalBezier) DrawRationalBezier();
            else if (drawEllipse) DrawEllipse();
            else if(drawIncircle)
            {
                Tri?.DrawTriangle(g);
                DrawIncircle();
            }
        }

        private void ParamCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            parameterVisualize = false;

            g.Children.Clear();
            if (drawRationalBezier) DrawRationalBezier();
            else if (drawEllipse) DrawEllipse();
            else if (drawIncircle)
            {
                Tri?.DrawTriangle(g);
                DrawIncircle();
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            parameter = Convert.ToDouble(Slider.Value);

            g.Children.Clear();
            if (drawRationalBezier) DrawRationalBezier();
            else if (drawEllipse) DrawEllipse();
            else if (drawIncircle)
            {
                Tri.DrawTriangle(g);
                DrawIncircle();
            }
        }

        private void FlowCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            paramFlowVisualize = true;

            g.Children.Clear();
            if (drawRationalBezier) DrawRationalBezier();
            else if (drawEllipse) DrawEllipse();
            else if (drawIncircle)
            {
                Tri?.DrawTriangle(g);
                DrawIncircle();
            }
        }

        private void FlowCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            paramFlowVisualize = false;

            g.Children.Clear();
            if (drawRationalBezier) DrawRationalBezier();
            else if (drawEllipse) DrawEllipse();
            else if (drawIncircle)
            {
                Tri?.DrawTriangle(g);
                DrawIncircle();
            }
        }

        private void DelBut_Click(object sender, RoutedEventArgs e)
        {
            Vert.Clear();
            Weights.Clear();
            g.Children.Clear();
            IterEllipse = 0;
            E = null;
            IterTriangle = 0;
            Tri = null;
            WeightsTextBox.Text = "";
        }
    }
}
