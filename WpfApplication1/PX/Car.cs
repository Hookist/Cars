//#define TEST
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApplication1.PX
{
   public class Car
    {
      public Thickness margin { get; set; }
      public List<Point> XY { get; set; }
     public  Image img = new Image();
        public RotateTransform rt = new RotateTransform();

      //  public  double Angle { get; set; }
     public int Height { get; set; }
     public int Width { get; set; }

     public  int Hp { get; set; }
      public double maxSpeedUp { get; set; }
       double maxSpeedDown { get; set; }
      public double accelerationUp { get; set; }
       double accelerationDown { get; set; }
       double controllability { get; set; }
      public double NowSpeed { get; set; }

      public int IsCrashed;
       double CrashedAngular;
        public double CrSpeed;
        public Key Left =Key.Left;
        public Key Right = Key.Right;
        public Key Up = Key.Up;
        public Key Down = Key.Down;
        public Key Space = Key.Space;

       public int destroed = 0;
        public  Car()
       {
           
           XY = new List<Point>();
           rt.Angle = 0;
           Height = 90;
           Width = 50;

           Hp = 100;
           maxSpeedUp = 15;
           maxSpeedDown = -2;
           accelerationUp = 0.1;
           accelerationDown = -0.1;
           controllability = 2;
           NowSpeed = 0;

           IsCrashed = 0;
           CrashedAngular = 0;
            CrSpeed = 0;
            ImageSourceConverter imgs = new ImageSourceConverter();
            string pic = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.Length - 11);
            char s = pic[pic.Length - 1];
            pic += "Pictures" + s + "car2.png";
            img.SetValue(Image.SourceProperty, imgs.ConvertFromString(@pic));

            img.RenderTransformOrigin = new Point(0.5, 0.5);
            img.Width = Width;
            img.Height = Height;
            img.Margin = margin;
        }

       public void right()
       {
            if (destroed != 1)
            {
                if (NowSpeed <= 2 && NowSpeed > 0)
                {
                    rt.Angle += controllability / 2 * NowSpeed;
                    NowSpeed -= 0.02;
                }
                else if (NowSpeed >= 2)
                {
                    rt.Angle += controllability;
                    NowSpeed -= 0.05;
                }
                if (NowSpeed >= 10)
                {
                    rt.Angle += controllability;
                    NowSpeed -= 0.3;
                }
                if (NowSpeed < 0 && NowSpeed >= -2)
                    rt.Angle += controllability / 2 * NowSpeed;
                img.RenderTransform = rt;
            }
       }
       public void left()
       {
            if (destroed != 1)
            {
                if (NowSpeed <= 2 && NowSpeed > 0)
                {
                    rt.Angle -= controllability / 2 * NowSpeed;
                    NowSpeed -= 0.02;
                }
                else if (NowSpeed >= 2)
                {
                    rt.Angle -= controllability;
                    NowSpeed -= 0.05;
                }
                if (NowSpeed >= 10)
                {
                    rt.Angle -= controllability;
                    NowSpeed -= 0.3;
                }
                if (NowSpeed < 0 && NowSpeed >= -2)
                    rt.Angle -= controllability / 2 * NowSpeed;
                img.RenderTransform = rt;
            }
       }
       public void up()
       {
            if (destroed != 1)
            {
                if (NowSpeed < maxSpeedUp)
                    NowSpeed += accelerationUp;
            }
       }
       public void down()
       {
            if (destroed != 1)
            {
                if (NowSpeed > maxSpeedDown)
                    NowSpeed += accelerationDown;
            }
       }
       public void move()
       {
            if (rt.Angle > 360)
                rt.Angle -= 360;
            if (rt.Angle <= 0)
                rt.Angle += 360;

            if (destroed != 1)
            {
                
                
                    if (Keyboard.IsKeyDown(Right) == true) { right(); }
                    if (Keyboard.IsKeyDown(Left) == true) { left(); }
                    if (Keyboard.IsKeyDown(Up) == true) { up(); }
                    if (Keyboard.IsKeyDown(Down) == true) { down(); }
                    if (Keyboard.IsKeyDown(Space) == true) { Drag(); }

                
                    //if (NowSpeed > 0)
                    //    NowSpeed -= 0.02;
                    //if (NowSpeed < 0)
                    //    NowSpeed += 0.02;

                    if (NowSpeed != 0)
                    {
                        double x = Math.Sin(rt.Angle * Math.PI / 180) * -NowSpeed;
                        double y = Math.Cos(rt.Angle * Math.PI / 180) * -NowSpeed;
                        margin = new Thickness(margin.Left - x, margin.Top + y, 0, 0);


                    }

#if TEST
                try {
                    Console.WriteLine();
                    var res = TopLeft((int)margin.Left, (int)margin.Top, Width, Height, rt.Angle);
                    Console.WriteLine("x:{0} y:{1} w:{2} h:{3} a:{4}", (int)margin.Left, (int)margin.Top, Width, Height, rt.Angle);
                    Console.WriteLine("x:{0} y:{1}", res.X, res.Y);
                } catch (Exception ex) {}

#endif
                if (IsCrashed != 0)
                {
                    //if (NowSpeed > 0)
                    //    NowSpeed -= 0.1;
                    //if (NowSpeed < 0)
                    //    NowSpeed += 0.1;

                    //if (CrashedAngular > 0)
                    //{ rt.Angle += 0.5 * NowSpeed; }
                    //if (CrashedAngular < 0)
                    //{ rt.Angle += -0.5 * NowSpeed; }



                    double x = Math.Sin(CrashedAngular * Math.PI / 180) * -CrSpeed;
                    double y = Math.Cos(CrashedAngular * Math.PI / 180) * -CrSpeed;
                    margin = new Thickness(margin.Left - x, margin.Top + y, 0, 0);

                    //if(IsCrashed==1)
                    //    NowSpeed=0;
                    IsCrashed--;
                }
                img.Margin = margin;
                img.RenderTransform = rt;
                GeneratingCarPoints();
            }
       }
       public void Drag ()
       {
            if (destroed != 1)
            {
                if (NowSpeed > 0)
                    NowSpeed -= 0.5;
                if (NowSpeed < 0)
                    NowSpeed += 0.5;
            }
       }

       public void GeneratingCarPoints()
       {
            if (destroed != 1)
            {
                //  P1X = new List<int>();

                int Xstart = Convert.ToInt32(margin.Left);
                int Ystart = Convert.ToInt32(margin.Top);
                XY.Clear();

                for (int i = 0; i < Width/2; i++)
                {
                    Point p = new Point(Xstart, Ystart);
                    XY.Add(p);
                    Xstart+=2;
                }
                for (int i = 0; i < Height/2; i++)
                {
                    Point p = new Point(Xstart, Ystart);
                    XY.Add(p);
                    Ystart+=2;
                }
                for (int i = 0; i < Width/2; i++)
                {
                    Point p = new Point(Xstart, Ystart);
                    XY.Add(p);
                    Xstart-=2;
                }
                for (int i = 0; i < Height/2; i++)
                {
                    Point p = new Point(Xstart, Ystart);
                    XY.Add(p);
                    Ystart-=2;
                }
            }
       }

       public void Crash(double angular, double speed, int cr)
       {
            int ggg = cr;
            if (destroed != 1)
            {  if (IsCrashed == 0 && cr ==1)
                {
                    IsCrashed = 50;
                    CrashedAngular = angular;
                    CrSpeed = speed;
                    NowSpeed = NowSpeed*0.2;
                    double x = Math.Sin(rt.Angle * Math.PI / 180) * -speed*2;
                    double y = Math.Cos(rt.Angle * Math.PI / 180) * -speed*2;
                    margin = new Thickness(margin.Left - x, margin.Top + y, 0, 0);
                }
               if(cr == 2)
                {
                    NowSpeed = 0;
                }
            }
       }
      
        public void Portal(double Mapheight, double Mapwidth,Thickness MapMargin)
        {
            if (destroed != 1)
            {
                if (margin.Left < MapMargin.Left - Mapwidth)
                {
                    margin = new Thickness(Mapwidth , margin.Top, 0, 0);

                }
                if (margin.Left > Mapwidth )
                {
                    margin = new Thickness(MapMargin.Left - Mapwidth, margin.Top, 0, 0);

                }
                if (margin.Top < MapMargin.Top - Mapheight)
                {
                    margin = new Thickness(margin.Left, Mapheight - 5, 0, 0);
                }
                if (margin.Top > Mapheight )
                {
                    margin = new Thickness(margin.Left, MapMargin.Top - Mapheight, 0, 0);
                }
            }
        }

        public void Destroed()
        {
            destroed = 1;
            NowSpeed = 0;
        }

        public Point TopLeft(double left, double top, int width, int height, double angle) {

            double _Angle = 360 - angle + 90;
            if (_Angle > 360) { _Angle = _Angle - 360; }


            double centerX = (left - 2) + width / 2;
            double centerY = (top - 5) + height / 2;

            double D = Math.Sqrt(width * width + height * height);

            double a = Math.Asin(width / D) / (Math.PI / 180);

            double NewAngle = _Angle;
            if ((360 - _Angle) < a) { NewAngle = (NewAngle + a) - 360; } else { NewAngle += a; }

            //Console.WriteLine("NewAngle = {0}", NewAngle);
            //Console.WriteLine("aaa = {0}", a);
            //Console.WriteLine("DDD = {0}", D);

            double newX = ((Math.Cos(NewAngle * Math.PI / 180)) * (D / 2)) + centerX;
            double newY = -((Math.Sin(NewAngle * Math.PI / 180)) * (D / 2)) + centerY;



            return new Point(newX, newY);
        }

    }
}
