using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server.classes
{
    public class IsCrash
    {

        public void Crash(List<Car> car)
        {
            

            for (int i = 0; i < car.Count; i++)
            {

                for (int z = i + 1; z < car.Count; z++)
                {
                    if (i != z)
                    {
                       // Console.WriteLine("P1 = {0}  P2 = {1}",car[i].XY[0], car[z].XY[0]);  
                        for (int i2 = 0; i2 < car[i].XY.Count; i2++)
                        {
                            for (int z2 = 0; z2 < car[z].XY.Count; z2++)
                            {
                                // if (car[z].IsCrashed == 0 && car[i].IsCrashed == 0)
                                if (car[i].XY[i2].X == car[z].XY[z2].X && car[i].XY[i2].Y == car[z].XY[z2].Y)
                                {
                                    
                                    
                                    if (car[i].IsCrashed == 0)
                                        if (car[i].NowSpeed > car[z].NowSpeed)
                                        {


                                            car[z].Crash(car[i].Angle, car[i].NowSpeed);
                                           // car[i].NowSpeed = 0.1 * car[i].NowSpeed;
                                            car[z].AreCrashNow = 1;
                                            car[i].AreCrashNow = 2;

                                            //Console.WriteLine("P1");
                                        }
                                    if (car[z].IsCrashed == 0)
                                        if (car[i].NowSpeed < car[z].NowSpeed)
                                        {

                                            car[i].Crash(car[z].Angle, car[z].NowSpeed);
                                          //  car[z].NowSpeed = 0.1 * car[z].NowSpeed;
                                            car[z].AreCrashNow = 2;
                                            car[i].AreCrashNow = 1;

                                            //Console.WriteLine("P2");
                                        }

                                    break;
                                }
                                
                            }
                        }
                    }
                }
            }


        }

    }
}
