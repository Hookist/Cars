using Server.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {

        static byte[] ipserver = new byte[] { 10, 4, 7, 5 };
        public static List<Car> cars = new List<Car>();
        public static IsCrash crash = new IsCrash();
         public static   List<int> IP = new List<int>();
        static void Main(string[] args)
        {


                // Начинаем слушать соединения


            IPAddress ipadress = new IPAddress(ipserver);//95.67.77.242
            IPEndPoint ipendpoint = new IPEndPoint(ipadress, 11000);

            // СОЗДАЕМ СОКЕТ
            Socket MySoket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            MySoket.Bind(ipendpoint);// Связывание сокета с конечной точкой.
            MySoket.Listen(10);// 10 - Максимальная дленна ожидающих подключений.
            Thread Th = new Thread(crashM);
            Th.Start();
            
            while (true)
            {
                Console.WriteLine("--->Ждем подключения по порту {0}", ipendpoint);
                Socket Handler = MySoket.Accept();
                Console.WriteLine("------>Создаем отдельный поток для подключения.");
                Thread Thr = new Thread(new ParameterizedThreadStart(ThreadFunk));
                Thr.Start((object)Handler);
            }

        }

       
        public static void crashM()// Врезание <-------------------------------------------
        {   while (true)
            {
            if(deletCar>=0)
            {
                cars.Remove(cars[deletCar]);
                deletCar = -1;
            }
                for (int i = 0; i < cars.Count; i++)
                {
                    cars[i].GeneratingCarPoints();
                    
                }
                
                    crash.Crash(cars);
                    //Thread.Sleep(100);
                
                
            }
        }

        static int zik = 0;
        static int deletCar = -1;
        public static void ThreadFunk(object SocketObj)
        {
            // Console.WriteLine("Поток создан"); 
            
            Obrobotka ob = new Obrobotka();   
            SendString snds = new SendString();
            
            
            string lol = "*dd*";
            Car car = new Car();
            
            

            byte[] bytes = new byte[1024];
              Socket SocketMy = (Socket)SocketObj;
              while (SocketMy!=null)
            {
                try
                {

                
                //Console.Clear();
                int Yes = 0;
                int Id = -1;
                string data = null;
                // byte[] ss = new byte[1024];
                // SocketMy.Receive(ss);
                // string Var = Encoding.ASCII.GetString(ss);
                
                int bytesRec = SocketMy.Receive(bytes);

                data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
               // Console.WriteLine(data);
                car = ob.D(car, data);
                Id = car.CARID;


                //////////////////
                if (cars.Count >= 1)
                {
                    for (int i = 0; i < cars.Count; i++)
                    {

                        if (car.CARID == cars[i].CARID)
                            Yes++;
                    }
                    if (Yes == 0)
                    { cars.Add(car); Console.WriteLine("cw2"); }
                }
                if (zik == 0)
                {
                    Console.WriteLine("cw1");
                    cars.Add(car);
                    zik++;
                }


                if (cars.Count >= 2)
                {
                    // crash.Crash(cars); Console.WriteLine("cw3");
                }
                for (int i = 0; i < cars.Count; i++)
                {
                    if (Id == cars[i].CARID)
                    {
                        
                        lol = snds.DRet(cars[i],cars); Console.WriteLine("lol {0}", cars.Count);
                    }
                }
                /////////
                //Console.WriteLine(lol);
                byte[] msg = Encoding.UTF8.GetBytes(lol);
                SocketMy.Send(msg);
                for (int i = 0; i < cars.Count; i++)
                {
                    if (Id == cars[i].CARID)
                    {
                        cars[i].AreCrashNow = 0;
                    }
                }
                }
                catch (Exception)
                {
                    for (int i = 0; i < cars.Count; i++)
                    {
                        if (car.CARID == cars[i].CARID)
                        {
                            deletCar = i;
                            break;
                        }
                    }
                    SocketMy.Shutdown(SocketShutdown.Both);
                    SocketMy.Close();
            break;
                }
            }
            
            Console.WriteLine("Подключение закрыто");
        }
    }
}
