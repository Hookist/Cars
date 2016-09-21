using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.classes
{
   public class Point
    {
        

       public double X { get; set; }
       public double Y { get; set; }
       

       public Point(int Xstart, int Ystart)
       {
           // TODO: Complete member initialization
           X = Xstart;
           Y = Ystart;
       }

       public Point()
       {
           // TODO: Complete member initialization
       }
      
    }
}
