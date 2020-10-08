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
        public byte[] clientConnection(byte[] data)
        {                      
            // получаем данные отправленные пользователем 
            int sizeMessage = BitConverter.ToInt32(data, 0);
            string message = Encoding.Default.GetString(data, 4, sizeMessage);

            //UpdataPublisher.publisher.notify(answer);
            byte[] command = buildCommand("GETSOMENICK");
            // формируем ответ 
            byte[] id = BitConverter.GetBytes(myself.id);
            byte[] answerMessage = Encoding.Default.GetBytes(myself.Nickname);
            byte[] lanswerMessage = BitConverter.GetBytes(answerMessage.Length);

            byte[] answer = command.Concat(lanswerMessage.Concat(id.Concat(answerMessage))).ToArray();
            return answer;
        }
        public byte[] changeNickName(byte[] data)
        {
            int sizeMessage = BitConverter.ToInt32(data, 0);
            string newNick = Encoding.Default.GetString(data, 4, sizeMessage);
            myself.Nickname = newNick;
            byte[] command = buildCommand("GETCHANGEDNAME"); 
            // формируем ответ              
            byte[] name = Encoding.Default.GetBytes(myself.Nickname);
            byte[] lname = BitConverter.GetBytes(name.Length);
            byte[] answer = command.Concat(lname.Concat(name)).ToArray();
            return answer; 
        }


        public byte[] getPlayers(byte[] data)
        {            
            byte[] command = buildCommand("GETPLAYERS");
            byte[] count = BitConverter.GetBytes(Map.Players.Count);
            byte[] answer = command.Concat(count).ToArray();  
            foreach (Player p in Map.Players)
            {
                byte[] id = BitConverter.GetBytes(p.id);
                byte[] name = Encoding.Default.GetBytes(p.Nickname); 
                byte[] lname = BitConverter.GetBytes(name.Length);
                answer = answer.Concat(id.Concat(lname.Concat(name))).ToArray(); 
            }
            Map.globalPublisher.notify(answer); 
            return null;
        }

        
        
        public byte[] notifyAboutChanges(byte[] data)
        {
            Random rand = new Random(); 
            int randomNumber = rand.Next(1, 1000);
            byte[] byteNumber = BitConverter.GetBytes(randomNumber);
            byte[] sizeByteNumber = BitConverter.GetBytes(4);
            
            byte[] command = buildCommand("GET_CHANGED_DATA"); 
            byte[] answer = command.Concat(byteNumber).ToArray();  
            Map.globalPublisher.notify(answer);  
            return null;  
        }
        
        public void notifySender(byte[] data) 
        {           
            byte[] size = BitConverter.GetBytes(data.Length);
            // отправляем ответ клиету                   
            stream.Write(size, 0, 4);  
            stream.Write(data, 0, data.Length);  
            
        }
    }
}
