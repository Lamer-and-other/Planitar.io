using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows;


namespace Planitar.io
{
    class Canal
    {
        public TcpClient client = null;
        public NetworkStream stream = null;

        public string server = "";
        public int port = 0;
        Protocol protocol;
        
        public messages getsomemessage;
        public reDrawing reDraw; 
        public bool isClosed = false;
        public Canal() { }
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
                    getsomemessage(ex.ToString()); 
                }
            }
        }
        
        public void getMessage(byte[] data)
        {
            int size = BitConverter.ToInt32(data, 0); 
            string answer = Encoding.Default.GetString(data, 4, size); 
        
            getsomemessage(answer);  
        }

        public void newData(byte[] data) 
        {
            int number = BitConverter.ToInt32(data, 0);
            reDraw(number);  
        }
    }
}
