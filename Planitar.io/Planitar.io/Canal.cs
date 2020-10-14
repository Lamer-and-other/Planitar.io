using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;


namespace Planitar.io
{
    class Canal
    {
        public TcpClient client = null;
        public NetworkStream stream = null;
        
        public string server = ""; 
        public int port = 0;
        Protocol protocol;
        
        public identification mySelfIndentity; 
        public reDrawing reDraw;
        public reName reSetName;
        public updataPlayerList updateplayerlist;
        public InitialGame initialGame; 

        public bool isClosed = false;
        public Map map { set; get; }

        public Canal() { }
        
        // создание канала 
        public Canal(string server, int port)
        {
            this.server = server;
            this.port = port;

            client = new TcpClient(server, port);
            stream = client.GetStream();

            protocol = Protocol.createProtocol(this);

            Thread th = new Thread(getAnswer);
            th.IsBackground = true;
            th.Start();
        }
        // функция отправки комманды с параметрами на сервер  
        public void sendCommand(byte[] data) 
        {
            byte[] banswer = new byte[4];
            try
            {
                stream.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                //message(ex.ToString());
            }
        }
        // поток получения информации из сервера 
        public void getAnswer()
        {
            while (true)
            {
                try
                {
                    byte[] banswer = new byte[4]; 
                    stream.Read(banswer, 0, banswer.Length);
                    int size = BitConverter.ToInt32(banswer, 0); 
                    banswer = new byte[size];
                    stream.Read(banswer, 0, banswer.Length); 
                    string answerCommand = protocol.parseCommand(banswer);
                    protocol.getMethod(answerCommand)(protocol.parseData(banswer));
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString()); 
                }
            }
        }
        // получаем расшифрованые данные о себе для самоидентификации 
        public void getMyself(byte[] data)
        {
            int size = BitConverter.ToInt32(data, 0);
            int id = BitConverter.ToInt32(data, 4);
            string name = Encoding.Default.GetString(data, 8, size);            
            mySelfIndentity(id, name);   

        }
        // новое имя 
        public void newName(byte[] data)
        {
            int size = BitConverter.ToInt32(data, 0);
            string name = Encoding.Default.GetString(data, 4, size);

            reSetName(name); 
        }
        
        // расшифровуем получиный из сервера байтный список игроков 
        public void getPlayers(byte[] data)
        {
            Player.playerList.Clear(); 
            int count = BitConverter.ToInt32(data, 0);
            int index = 4; 
            for(int i = 0; i < count; i++)
            {
                int id = BitConverter.ToInt32(data, index);
                int sizeName = BitConverter.ToInt32(data, index + 4); 
                string name = Encoding.Default.GetString(data, index + 8, sizeName);
                Random rand = new Random(); 
                Player newPlayer = new Player(id, name, Color.FromArgb(
                    rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255))); 
                
                Player.playerList.Add(newPlayer); 
                index += (8 + sizeName); 
            }
            updateplayerlist(); 
        }
        // получение стандартных данных старта игры для игрока   
        public void getStartGameData(byte[] data)
        {
            int id = BitConverter.ToInt32(data, 0);
            int positionX = BitConverter.ToInt32(data, 4);
            int positionY = BitConverter.ToInt32(data, 8);
            int size = BitConverter.ToInt32(data, 12);

            int index = 24; 
            
            int foodCount = BitConverter.ToInt32(data, 20);
            List<Point> foodPosition = new List<Point>(); 
           
            for(int i = 0; i < foodCount; i++)
            {
                int fX = BitConverter.ToInt32(data, index);
                int fY = BitConverter.ToInt32(data, index + 4);
                int bonus = BitConverter.ToInt32(data, index + 8);
                foodPosition.Add(new Point(fX, fY));
                map.AddFood(new Point(fX, fY), bonus); 
                index += 12;  
            }
            
            
            int trapCount = BitConverter.ToInt32(data, index); 
            List<Point> trapPosition = new List<Point>();
            index += 4; 
            for(int i = 0; i < trapCount; i++)
            {
                int tX = BitConverter.ToInt32(data, index);
                int tY = BitConverter.ToInt32(data, index + 4);
                trapPosition.Add(new Point(tX, tY));
                map.AddTrap(new Point(tX, tY));   
                index += 8;
            }
            initialGame(size, positionX, positionY); 
            
        }






        public void newData(byte[] data) 
        {
            int number = BitConverter.ToInt32(data, 0);
            reDraw(number);  
        }
    }
}
