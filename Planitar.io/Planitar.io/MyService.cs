using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing; 

namespace Planitar.io
{
    class MyService
    {
        Canal canal { set; get; }
        public MyService(Canal canal)
        {
            this.canal = canal; 
        }
        public void SetDelegats(identification idt, reName rnm,
            reDrawing reDraw, updataPlayerList upl, InitialGame ig, NewMove nm)
        {
            canal.mySelfIndentity = idt;
            canal.reSetName = rnm;
            canal.reDraw = reDraw; 
            canal.updateplayerlist = upl;
            canal.initialGame = ig;
            canal.newmove = nm; 
        }
        
        // формирование комманды 
        byte[] buildCommand(string textCommand)
        {
            byte[] command = Encoding.Default.GetBytes(textCommand);
            byte[] lcommand = BitConverter.GetBytes(command.Length);
            byte[] fullCommand = lcommand.Concat(command).ToArray();
            return fullCommand;
        }
        // формирование и отправа комманды подлючение 
        public void connectToServer()
        {
            byte[] command = buildCommand("CONNECT"); 
            byte[] message = Encoding.Default.GetBytes("I want connect to server! Please! I have to let me in"); 
            byte[] lmessage = BitConverter.GetBytes(message.Length); 
            byte[] request = command.Concat(lmessage.Concat(message)).ToArray(); 
            canal.sendCommand(request); 
        }
        // формирование и отправа комманды смены ника 

        public void changeNickName(string newName) 
        {
            byte[] command = buildCommand("EDITNICK"); 
            byte[] message = Encoding.Default.GetBytes(newName); 
            byte[] lmessage = BitConverter.GetBytes(message.Length); 
            byte[] request = command.Concat(lmessage.Concat(message)).ToArray();
            canal.sendCommand(request);
        }
        // формирование и отправа комманды получения игроков 
        public void getPlayers()
        {
            byte[] command = buildCommand("GETPLAYERS");
            byte[] request = command; 
            canal.sendCommand(request);
        }

        public void startGame(int id)
        {
            byte[] command = buildCommand("STARTGAME");
            byte[] ID = BitConverter.GetBytes(id);
            byte[] request = command.Concat(ID).ToArray(); 
            canal.sendCommand(request); 
        }

        public void NewMove(int id, Point location)
        {
            byte[] command = buildCommand("NEWMOVE"); 
            byte[] ID = BitConverter.GetBytes(id);
            byte[] X = BitConverter.GetBytes(location.X);
            byte[] Y = BitConverter.GetBytes(location.Y);
            byte[] request = command.Concat(ID.Concat(X.Concat(Y))).ToArray(); 
            canal.sendCommand(request);
        }

        public void chekNotify()
        {
            byte[] command = buildCommand("DATANOTIFY"); 
            byte[] request = command; 
            canal.sendCommand(request);
        }
        // формирование и отправа комманды отключения от сервера 
        public void Disconnect()
        {
            byte[] command = buildCommand("CLOSE");
            canal.sendCommand(command); 
        }
    }
}
