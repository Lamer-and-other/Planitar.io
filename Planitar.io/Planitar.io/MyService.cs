using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planitar.io
{
    class MyService
    {
        Canal canal { set; get; }
        public MyService(Canal canal)
        {
            this.canal = canal; 
        }
        public void SetDelegats(messages msg, reDrawing reDraw)
        {
            canal.getsomemessage = msg;
            canal.reDraw = reDraw; 
        }
        
        // формирование комманды 
        byte[] buildCommand(string textCommand)
        {
            byte[] command = Encoding.Default.GetBytes(textCommand);
            byte[] lcommand = BitConverter.GetBytes(command.Length);
            byte[] fullCommand = lcommand.Concat(command).ToArray();
            return fullCommand;
        }

        public void connectToServer()
        {
            byte[] command = buildCommand("CONNECT"); 
            byte[] message = Encoding.Default.GetBytes("I want connect to server! Please! I have to let me in"); 
            byte[] lmessage = BitConverter.GetBytes(message.Length); 
            byte[] request = command.Concat(lmessage.Concat(message)).ToArray(); 
            canal.sendCommand(request); 
        }

        public void changeNickName(string newName) 
        {
            byte[] command = buildCommand("EDITNICK"); 
            byte[] message = Encoding.Default.GetBytes(newName); 
            byte[] lmessage = BitConverter.GetBytes(message.Length);
            byte[] request = command.Concat(lmessage.Concat(message)).ToArray();
            canal.sendCommand(request);
        }

        public void chekNotify()
        {
            byte[] command = buildCommand("DATANOTIFY");
            byte[] request = command; 
            canal.sendCommand(request);
        }
        
        public void Disconnect()
        {
            byte[] command = buildCommand("CLOSE");
            canal.sendCommand(command); 
        }
    }
}
