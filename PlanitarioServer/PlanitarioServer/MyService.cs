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
        public Player myself { set; get; } 

        public MyService() { }

        byte[] buildCommand(string textCommand)
        {
            byte[] command = Encoding.Default.GetBytes(textCommand);
            byte[] lcommand = BitConverter.GetBytes(command.Length);
            byte[] fullCommand = lcommand.Concat(command).ToArray();
            return fullCommand;
        }
        byte[] getByteName()
        {
            byte[] command = buildCommand("GETSOMENICK");
            // формируем ответ 
            byte[] answerMessage = Encoding.Default.GetBytes(myself.Nickname);
            byte[] lanswerMessage = BitConverter.GetBytes(answerMessage.Length);
            byte[] answer = command.Concat(lanswerMessage.Concat(answerMessage)).ToArray();
            return answer; 
        }
        
        public byte[] clientConnection(byte[] data)
        {                      
            // получаем данные отправленные пользователем 
            int sizeMessage = BitConverter.ToInt32(data, 0);
            string message = Encoding.Default.GetString(data, 4, sizeMessage);
            
            //UpdataPublisher.publisher.notify(answer);
            return getByteName();
        }
        public byte[] changeNickName(byte[] data)
        {
            int sizeMessage = BitConverter.ToInt32(data, 0);
            string newNick = Encoding.Default.GetString(data, 4, sizeMessage);
            myself.Nickname = newNick;
            return getByteName();  
        }
    }
}
