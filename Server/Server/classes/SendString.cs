using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.classes
{
   public class SendString
    {

        int First = 0;
        string data = null;

        public string DRet(Car car, List<Car>cars)
        {
            
                data = "*" + car.AreCrashNow + "*" + car.CrashedAngular + "*" + car.CrashedSpeed + "*"+(cars.Count-1);
            
            for (int i = 0; i < cars.Count; i++)
            {
                if(cars[i].CARID !=car.CARID)
                {
                    data += "*" + cars[i].Angle + "*" + cars[i].NowSpeed + "*" + cars[i].margin.X + "*" + cars[i].margin.Y;
                }
            }

            Console.WriteLine(data.Count());
            return data;
        }
    }
}
