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
        public void SetDelegats(messages msg)
        {
            canal.getsomemessage = msg; 
        }
        
        // формирование комманды 
        byte[] buildCommand(string textCommand)
        {
            byte[] command = Encoding.Default.GetBytes(textCommand);
            byte[] lcommand = BitConverter.GetBytes(command.Length);
            byte[] fullCommand = lcommand.Concat(command).ToArray();
            return fullCommand;
        }

        public void sendSomeMessage()
        {
            byte[] command = buildCommand("SENDTESTMESSAGE"); 
            byte[] message = Encoding.Default.GetBytes("I want connect to server! Please! I have to let me in"); 
            byte[] lmessage = BitConverter.GetBytes(message.Length); 
            byte[] request = command.Concat(lmessage.Concat(message)).ToArray();
            canal.sendCommand(request); 
        }

    }
}
