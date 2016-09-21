using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using WpfApplication1.PX;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        byte[] ipserver = new byte[] { 10, 4, 7, 5 };
        List<Car> Players = new List<PX.Car>();
        Car Player = new Car();
        List<Image> im = new List<Image>();
        //IsCrash ic = new IsCrash();
        //Move m = new Move();
        //IsCrash crash = new IsCrash();
       // GameOver gm = new GameOver();
        /// <summary>
        /// /////////////////////////////////////////
        /// </summary>
        byte[] bytes = new byte[1024];

        IPAddress ipAddr;
        IPEndPoint ipEndPoint;
        Socket sender;
        string message;

        int idMy = 0;
        public MainWindow()
        {
            InitializeComponent();
            string pic = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.Length-11);           
            char s = pic[pic.Length-1];
            pic += "Pictures"+s+"car.png";
            //txt.Text = pic;
            
            
            message = null;
            ImageSourceConverter imgs = new ImageSourceConverter();
          Player.img.SetValue(Image.SourceProperty, imgs.ConvertFromString(@pic));//@"D:\DD\car.png"

            Player.margin = new Thickness(100, 150, 0, 0);
            grid.Children.Add(Player.img);

           // gameLoopTimer_Tick(this, new EventArgs());
          ////  Crash_Tick(this, new EventArgs());
          //  InitializeTimer();
          //  CrashTimer();

 
        }


        Random r = new Random();
        void gameLoopTimer_Tick(object sender, EventArgs e)
        {
            SendMessageFromSocket(idMy, Convert.ToInt32(Player.margin.Left), Convert.ToInt32(Player.margin.Top), Player.NowSpeed, Player.IsCrashed, Player.rt.Angle);
            Player.move();
            for (int i = 0; i < Players.Count; i++)
            {
                Players[i].move();
               // Players[i].Portal(this.Height, this.Width, this.Margin);

            }
            Player.Portal(this.Height, this.Width, this.Margin);
            // crash.Crash(Players);
            // gm.Over(Players);

        }

        DispatcherTimer dt = new DispatcherTimer();
        private void InitializeTimer()
        {


            dt.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dt.Tick += new EventHandler(gameLoopTimer_Tick);
            dt.Start();


        }
        private void CrashTimer()
        {

            //DispatcherTimer dt = new DispatcherTimer();
            //dt.Interval = new TimeSpan(0, 0, 0, 10, 10);
            //dt.Tick += new EventHandler(Crash_Tick);
            //dt.Start();


        }
        void Crash_Tick(object sender, EventArgs e)
        {


        }

        public void SendMessageFromSocket(int ID, int MarginX, int MarginY, double Speed, int Crash, double Angle)
        {


            //Console.Clear();
            //Console.Write("Введите сообщение: ");


            message = "*" + ID + "*" + MarginX + "*" + MarginY + "*" + Speed + "*" + Crash + "*" + Angle + "*";


            //Console.WriteLine("Сокет соединяется с {0} ", sender.RemoteEndPoint.ToString());
            byte[] msg = Encoding.UTF8.GetBytes(message);

            // Отправляем данные через сокет
            int bytesSent = sender.Send(msg);

            // Получаем ответ от сервера
            int bytesRec = sender.Receive(bytes);
            string data = Encoding.UTF8.GetString(bytes, 0, bytesRec);
            // string lol = null;

            //lol = GetData(data);
            //Console.WriteLine("\nОтвет от сервера: {0}\n\n", lol);
            //sender.Shutdown(SocketShutdown.Both);
            GetData(data);


        }
        public void GetData(string data)
        {
            //Console.WriteLine(data);
            //Console.WriteLine();
            
            int First = 0;
            string strCrashNow = null;
            string strCrashedAngular = null;
            string strCrashedSpeed = null;

            string Id = null;

            int IdDataStartString = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == '*' && First == 4)
                {
                    IdDataStartString = i;
                    break;
                }
                /////////////////////////////////////
                if (First == 4 && data[i] != '*')
                {
                    Id += data[i]; //Console.WriteLine(data[i]);
                }

                if (data[i] == '*' && First == 3)
                {
                    First++;
                }
                ////////////////////////////////////////////////////////////////////////////////////////////
                if (First == 3 && data[i] != '*')
                {
                    strCrashedSpeed += data[i]; //Console.WriteLine(data[i]);
                }

                if (data[i] == '*' && First == 2)
                {
                    First++;
                }
                ////////////////////////////////////////////////////////////////////////////////////////////

                if (First == 2 && data[i] != '*')
                {
                    strCrashedAngular += data[i]; //Console.WriteLine(data[i]);
                }

                if (data[i] == '*' && First == 1)
                {
                    First++;
                }
                ////////////////////////////////////////////////////////////////////////////////////////////

                if (First == 1 && data[i] != '*')
                {
                    strCrashNow += data[i];
                    // Console.WriteLine(data[i]);
                }

                if (data[i] == '*' && First == 0)
                {
                    First++;
                }


            }
            Console.WriteLine("{0}  {1}  {2}", strCrashedAngular, strCrashedSpeed, strCrashNow);
            Player.Crash(Convert.ToDouble(strCrashedAngular), Convert.ToDouble(strCrashedSpeed), Convert.ToInt32(strCrashNow));// сюда добавить Врезание  <-----------------
            
            First = 0;
            ///////////////////////////////////////////////////////////////////////////////////////////////
            string[] NowAnglePlayers = new string[Convert.ToInt32(Id)];
            string[] NowSpeedPlayers = new string[Convert.ToInt32(Id)];
            string[] NowXPlayers     = new string[Convert.ToInt32(Id)];
            string[] NowYPlayers     = new string[Convert.ToInt32(Id)];
            
            for (int i = 0; i < Convert.ToInt32(Id); i++)
            {
                NowAnglePlayers[i] = "";
                NowSpeedPlayers[i] = "";
                NowXPlayers    [i] = "";
                NowYPlayers    [i] = "";
            }
            
            for (int z = 0; z < Convert.ToInt32(Id); z++)
            {
                for (int i = IdDataStartString; i < data.Length; i++)
                {
                    if (data[i] == '*' && First == 4)
                    {
                        IdDataStartString = i;
                       // Console.WriteLine(data.Length);
                       // Console.WriteLine(IdDataStartString);
                        break;

                    }
                    /////////////////////////////////////
                    if (First == 4 && data[i] != '*')
                    {
                        NowYPlayers[z] += data[i]; //Console.WriteLine(data[i]);
                       // Console.WriteLine("Y={0}", NowYPlayers[z]);
                    }

                    if (data[i] == '*' && First == 3)
                    {
                        First++;
                    }
                    ////////////////////////////////////////////////////////////////////////////////////////////
                    if (First == 3 && data[i] != '*')
                    {
                        NowXPlayers[z] += data[i]; //Console.WriteLine(data[i]);
                      //  Console.WriteLine("X={0}", NowXPlayers[z]);
                    }

                    if (data[i] == '*' && First == 2)
                    {
                        First++;
                    }
                    ////////////////////////////////////////////////////////////////////////////////////////////

                    if (First == 2 && data[i] != '*')
                    {
                        NowSpeedPlayers[z] += data[i]; //Console.WriteLine(data[i]);
                       // Console.WriteLine("Sped={0}", NowSpeedPlayers[z]);
                    }

                    if (data[i] == '*' && First == 1)
                    {
                        First++;
                    }
                    ////////////////////////////////////////////////////////////////////////////////////////////

                    if (First == 1 && data[i] != '*')
                    {
                        NowAnglePlayers[z] += data[i];
                       //  Console.WriteLine("Angle={0}",NowAnglePlayers[z]);
                    }

                    if (data[i] == '*' && First == 0)
                    {
                        First++;
                    }
                }
                First = 0;
            }
            if (Players.Count < Convert.ToInt32(Id))
                for (int i = Players.Count; i < Convert.ToInt32(Id); i++)
                                                                                    /////////  <----Ошибка
                {                                                                      /////////  <----Ошибка
                    Car car = new Car();                                               /////////  <----Ошибка
                    Players.Add(car);                                                  /////////  <----Ошибка
                                                                                       /////////  <----Ошибка
                                                                                       /////////  <----Ошибка
                    grid.Children.Add(Players[i].img);               /////////  <----Ошибка
                }

            if (Players.Count > Convert.ToInt32(Id))
                
                {

                    grid.Children.Remove(Players[Players.Count-1].img);
                    //im.Remove(im.Last());
                    Players.Remove(Players[Players.Count-1]);
                    
                }

            for (int i = 0; i < Convert.ToInt32(Id); i++)
            {
                //Console.WriteLine("Player X ={0}",NowXPlayers[i]);
                //Console.WriteLine("Player Y ={0}",NowYPlayers[i]);
                //Console.WriteLine("Player Speed ={0}", NowSpeedPlayers[i]);
                //Console.WriteLine("Player Angle ={0}", NowAnglePlayers[i]);
                Players[i].NowSpeed = Convert.ToDouble(NowSpeedPlayers[i]);
                Players[i].rt.Angle = Convert.ToDouble(NowAnglePlayers[i]);
                Players[i].margin = new Thickness(Convert.ToDouble(NowXPlayers[i]), Convert.ToDouble(NowYPlayers[i]), 0, 0);
               // Console.WriteLine("Id={0} SP={1} ANG={2} X={3} Y{4}", i, Players[i].NowSpeed, Players[i].rt.Angle, Players[i].margin.Left, Players[i].margin.Top);
                
            }
        }

        private void Exit_Click(object send, RoutedEventArgs e)
        {
            sender.Close();
            this.Close();
        }

        private void Connect_Click(object sende, RoutedEventArgs e)
        {
            ipAddr = new IPAddress(ipserver);
            ipEndPoint = new IPEndPoint(ipAddr, 11000);
            sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sender.Connect(ipEndPoint);

            idMy = Convert.ToInt32(Text1.Text);

            gameLoopTimer_Tick(this, new EventArgs());
            InitializeTimer();
            Connec.IsEnabled = false;
        }
    }
}       
    

