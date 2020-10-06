using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Drawing;
namespace PlanitarioServer
{
    class MyService
    {
        public NetworkStream stream = null;
        public MyService() { }


        public byte[] chekServer(byte[] data)
        {
            byte[] command = Encoding.Default.GetBytes("GETTESTMESSAGE"); 
            byte[] lcommand = BitConverter.GetBytes(command.Length);
            
            int sizeMessage = BitConverter.ToInt32(data, 0);
            string message = Encoding.Default.GetString(data, 4, sizeMessage);
            
            
            byte[] answerMessage = Encoding.Default.GetBytes("Your message has been recieve at Server");
            byte[] lanswerMessage = BitConverter.GetBytes(answerMessage.Length);
            byte[] answer = lcommand.Concat(command.Concat(lanswerMessage.Concat(answerMessage))).ToArray();
            //UpdataPublisher.publisher.notify(answer);
            return answer;
        }

    }
}
