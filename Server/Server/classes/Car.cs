using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.classes
{
  public  class Car
    {       public   int Width = 50;
            public   int Height = 90;
        
            public   Point       margin          { get; set; }
            public   List<Point> XY              { get; set; }          
            public   double      NowSpeed         { get; set; }
            public   int         IsCrashed         { get; set; }
            public   double      Angle            { get; set; }
            public   int         AreCrashNow       { get; set; } //1 - ты врезался | 2 - ты врезал | 0- нет |
            public   double      CrashedAngular   { get; set; }
            public   double      CrashedSpeed     { get; set; }
            
      public Car()
            {
          
          
          NowSpeed        =0;
          IsCrashed       =0;
          Angle           =0;
          AreCrashNow     =0;
          CrashedAngular  =0;
          CrashedSpeed = 0;
            XY = new List<Point>();
            margin = new Point();
            }
        public int CARID=0;

        public void GeneratingCarPoints()
        {
           
                //  P1X = new List<int>();

                int Xstart = Convert.ToInt32(margin.X);
                int Ystart = Convert.ToInt32(margin.Y);
               if(XY!=null)
                XY.Clear();

                for (int i = 0; i < Width / 2; i++)
                {
                    Point p = new Point(Xstart, Ystart);
                    XY.Add(p);
                    Xstart += 2;
                }
                for (int i = 0; i < Height / 2; i++)
                {
                    Point p = new Point(Xstart, Ystart);
                    XY.Add(p);
                    Ystart += 2;
                }
                for (int i = 0; i < Width / 2; i++)
                {
                    Point p = new Point(Xstart, Ystart);
                    XY.Add(p);
                    Xstart -= 2;
                }
                for (int i = 0; i < Height / 2; i++)
                {
                    Point p = new Point(Xstart, Ystart);
                    XY.Add(p);
                    Ystart -= 2;
                }
            
        }

        public void Crash(double angular, double speed)
        {
            
                if (IsCrashed == 0)
                {
                    
                    CrashedAngular = angular;
                    CrashedSpeed = speed;
                }
            
        }
    }
}
