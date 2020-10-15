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
        // подключение игрока  
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
        // переименовка игрока на сервере   
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

        // получение списка игроков 
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
            if(Map.Players.Count != 0)
                 Map.globalPublisher.notify(answer); 
            return null;
        }
        
        public byte[] StartGame(byte[] data)
        {
            int id = BitConverter.ToInt32(data, 0);
            Player player = Player.getPlayer(id); 
            Map.AddPlayer(player);
            
            /// записываекм данные о старте игры для игрока на сервер 
            //ID
            //разположение меня
            //размер меня 
            //разположение еды
            //размер еды
            //разположение ловушек 
            //размер ловушек
            byte[] command = buildCommand("DATASTART");  
            byte[] ID = BitConverter.GetBytes(id);
            // тут координаты получим из класса Map 
            byte[] pX = BitConverter.GetBytes(player.Position.X); 
            byte[] pY = BitConverter.GetBytes(player.Position.Y);
            byte[] mySize = BitConverter.GetBytes(player.Score);
            // получаем данные о еде 
            byte[] foodCount = BitConverter.GetBytes(Map.Foods.Count);
            byte[] foodData = foodCount; 
            for(int i = 0; i < Map.Foods.Count; i++)
            {
                byte[] foodPX = BitConverter.GetBytes(Map.Foods[i].Сollision.X); 
                byte[] foodPY = BitConverter.GetBytes(Map.Foods[i].Сollision.Y); 
                byte[] bonus = BitConverter.GetBytes(Map.Foods[i].Bonus);
                
                foodData = foodData.Concat(foodPX.Concat(foodPY.Concat(bonus))).ToArray();             
            }
            // получаем данные ^о ловушках  
            byte[] trapCount = BitConverter.GetBytes(Map.Traps.Count);
            byte[] trapData = trapCount; 
            for (int i = 0; i < Map.Traps.Count; i++)
            {
                byte[] trapPX = BitConverter.GetBytes(Map.Traps[i].Position.X);
                byte[] trapPY = BitConverter.GetBytes(Map.Traps[i].Position.Y);

                trapData = trapData.Concat(trapPX.Concat(trapPY)).ToArray();
            }
            
            byte[] answer = command.Concat(ID.Concat(pX.Concat(pY.Concat(
                mySize.Concat((foodData.Concat(trapData))))))).ToArray(); 
            return answer; 
        }

        public byte[] GetNewMove(byte[] data)
        {
            int id = BitConverter.ToInt32(data, 0);
            int x = BitConverter.ToInt32(data, 4);
            int y = BitConverter.ToInt32(data, 8);
            
            Player player = Player.getPlayer(id); 
            player.Сollision.X = x;
            player.Сollision.Y = y;

            int yum = 0;
            Food someFood = Map.Eat(player); 

            if (someFood != null)
            {               
                yum = 1;
            }

            byte[] command = buildCommand("NOTIFYNEWMOVE"); 
            byte[] boolByte = BitConverter.GetBytes(yum); 
            byte[] FX = BitConverter.GetBytes(someFood.Сollision.X);
            byte[] FY = BitConverter.GetBytes(someFood.Сollision.Y); 
            
            byte[] wholeAnswer = command.Concat(data.Concat(boolByte.Concat(FX.Concat(FY)))).ToArray(); 
            Map.globalPublisher.notify(wholeAnswer);
            return null; 
        }







        // тестовое уведомление об изменениях 
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
        
        // отправка даных всем "подписчикам"  
        public void notifySender(byte[] data) 
        {           
            byte[] size = BitConverter.GetBytes(data.Length);
            // отправляем ответ клиету                   
            stream.Write(size, 0, 4);  
            stream.Write(data, 0, data.Length);  
            
        }
    }
}
