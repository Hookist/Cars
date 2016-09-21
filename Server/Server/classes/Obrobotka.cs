using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server.classes
{
   public class Obrobotka
    {
        public string data { get; set; }
        
       
    public Car D(Car _car, string _data)
        {
        int First = 0;
        string strAngle = "";
        string strCrash = "";
        string strSpeed = "";
        string strY     = "";
        string strX     = "";
        string strId    = "";
        Car car;
            car = _car;
            data = _data;
            //Console.WriteLine(data.Length);
            //Console.WriteLine();
        for (int i = 0; i < data.Length; i++)
        {
////////////////////////////////////////////////////////////////////////////////////////////
           
           if (First == 6 && data[i] != '*')
           {
               strAngle += data[i];//Console.WriteLine(data[i]);
           }

           if (data[i] == '*' && First == 5 )
           {
               First++;
           }
////////////////////////////////////////////////////////////////////////////////////////////

           if (First == 5 && data[i] != '*')
           {
               strCrash += data[i];// Console.WriteLine(data[i]);
               
           }

           if (data[i] == '*' && First == 4)
           {
               First++;
           }
////////////////////////////////////////////////////////////////////////////////////////////

           if (First == 4 && data[i] != '*')
           {
               strSpeed += data[i];
              // Console.WriteLine("sp={0}", strSpeed);
           }

           if (data[i] == '*' && First == 3)
           {
               First++;
           }
////////////////////////////////////////////////////////////////////////////////////////////

           if (First == 3 && data[i] != '*')
           {
               strY += data[i];
              // Console.WriteLine("Y={0}", strY);
           }

           if (data[i] == '*' && First == 2)
           {
               First++;
           }
////////////////////////////////////////////////////////////////////////////////////////////

           if (First == 2 && data[i] != '*')
           {
               strX += data[i];
             //  Console.WriteLine("X={0}", strX);
           }

           if (data[i] == '*' && First == 1)
           {
               First++;
           }
////////////////////////////////////////////////////////////////////////////////////////////

           if (First == 1 )
           {
               strId += data[i];
             //  Console.WriteLine("Id={0}",strId);
           }

           if (data[i] == '*' && First == 0)
           {
               First++;
           }
////////////////////////////////////////////////////////////////////////////////////////////

        }


       // Thread.Sleep(1000);
        //Console.WriteLine(strId);
       // Console.WriteLine(strX);
       // Console.WriteLine(strY);
        //Console.WriteLine(strSpeed);
        //Console.WriteLine(strAngle);
        car.Angle= Convert.ToDouble(strAngle);
        car.IsCrashed  =Convert.ToInt32(strCrash);//int
        car.NowSpeed = Convert.ToDouble(strSpeed);//int
       
          Point p =  new Point(Convert.ToInt32(strX), Convert.ToInt32(strY));
          car.margin = p;
            
        car.CARID  =Convert.ToInt32(strId);//int
      //  Console.WriteLine("X={0} Y={1}",car.margin.X.ToString(),car.margin.Y.ToString());
        return car;
    }
    }
}
