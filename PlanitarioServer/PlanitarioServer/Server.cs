using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Data;
using System.Threading;

namespace PlanitarioServer
{
   
    class Server
    {
        public messages servermeesage;
        static string getTime()
        {
            return DateTime.Now.TimeOfDay.Hours + "." + DateTime.Now.TimeOfDay.Minutes + "." + DateTime.Now.TimeOfDay.Seconds + ": ";
        }
        // запуск сервера по умолчанию в локальной сети 
        public void Start()
        {
            //Room.roomList.Add(new Room()); 
            TcpListener tcpl = null;
            try
            {
                IPAddress ipA = IPAddress.Parse("127.0.0.1");
                tcpl = new TcpListener(ipA, 2020);
                tcpl.Start();

                TcpClient client = null;
                servermeesage(getTime() + "Server start"); 
                while (true)
                {
                    client = tcpl.AcceptTcpClient();
                    servermeesage(getTime() + "New Client");
                    Thread exchangeThread = new Thread(exchange);
                    //exchangeThread.IsBackground = true;
                    exchangeThread.Start(client);
                }
            }
            catch (Exception ex)
            {
                servermeesage(ex.ToString());
            }
        }
        // запуск сервера по определённому аддресу и порту 
        public void Start(string address, int port)
        {
            ///Room.roomList.Add(new Room());

            TcpListener tcpl = null;
            try
            {
                IPAddress ipA = IPAddress.Parse(address);
                tcpl = new TcpListener(ipA, port);
                tcpl.Start();

                servermeesage(getTime() + "Server start");

                TcpClient client = null;
                while (true)
                {
                    client = tcpl.AcceptTcpClient();
                    servermeesage(getTime() + "New Client");
                    Thread exchangeThread = new Thread(exchange);
                    exchangeThread.IsBackground = true;
                    exchangeThread.Start(client);
                }
            }
            catch (Exception ex)
            {
                servermeesage(ex.ToString());
            }
        }

        // прослушивания новых клиентов 
        void Listening(object tcpL)
        {
            TcpClient client = null;
            while (true)
            {
                client = (tcpL as TcpListener).AcceptTcpClient();
                servermeesage(getTime() + "New Client"); 
                Thread exchangeThread = new Thread(exchange);
                exchangeThread.IsBackground = true;
                exchangeThread.Start(client);
            }
        }

        // обмен информацией между клиентом 
        void exchange(object tcpClient)
        {
            bool isStop = false;
            TcpClient client = (tcpClient as TcpClient);
            NetworkStream stream = client.GetStream(); 
            
            Player player = new Player(Map.globalPublisher); 
            player.service.myself = player; 
            player.service.stream = stream;
            Player.playerList.Add(player); 
        
            
            //player.service.Subscribe(UpdataPublisher.publisher);
            
            Protocol protocol = Protocol.createProtocol(player.service);
            
            servermeesage(getTime() + "Start exchange with client");
            do
            { 
                int size;
                byte[] bSize = new byte[4]; 
                byte[] buffer = null; 
                while (true)
                {
                    stream.Read(bSize, 0, bSize.Length);
                    size = BitConverter.ToInt32(bSize, 0);
                    buffer = new byte[size]; 
                    stream.Read(buffer, 0, buffer.Length); 
                    
                    // получаем комманду 
                    string command = protocol.parseCommand(buffer);
                    servermeesage(getTime() + $"Client send command: {command}"); 
                    if (command == "CLOSE")
                    {
                        isStop = true; 
                        player.service.stream.Close();
                        player.Unsubscribe(Map.globalPublisher);
                        Map.RemovePlayer(player); 
                        client.Close(); 
                        break;
                    }
                    
                    // выполняем действие с учётом парсинга заголовка полученого пакета
                    // и получаем ответный пакет   
                    byte[] answer = protocol.getMethod(command)(protocol.parseData(buffer));
                    if (answer != null)
                    {
                        byte[] sizeB = BitConverter.GetBytes(answer.Length);
                        // отправляем ответ клиету                   
                        stream.Write(sizeB, 0, sizeB.Length);
                        stream.Write(answer, 0, answer.Length); 
                    }
                }
            }
            while (isStop == false);
            //player.service.Unsubscribe(UpdataPublisher.publisher);
            servermeesage(getTime() + "Client is disconnected");
        }
    }
}
