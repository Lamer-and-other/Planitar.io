using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanitarioServer
{
    delegate byte[] Command(byte[] data); 
    class Protocol
    {
        Dictionary<string, Command> protocol = new Dictionary<string, Command>();

        // формирование протокола для клиента 
        public static Protocol createProtocol(MyService ms)
        {
            Protocol protocol = new Protocol();
            protocol.addCommand("CONNECT", ms.clientConnection);
            protocol.addCommand("EDITNICK", ms.changeNickName);
            protocol.addCommand("GETPLAYERS", ms.getPlayers);
            protocol.addCommand("DATANOTIFY", ms.notifyAboutChanges);
            protocol.addCommand("STARTGAME", ms.StartGame);
            //protocol.addCommand("CLOSE", ms.chekServer);
            return protocol;
        }
        
        public Command getMethod(string key)
        {
            try
            {
                return protocol[key];
            }
            catch
            {
                return error;
            }
        }

        public string parseCommand(byte[] data)
        {
            int lenght = BitConverter.ToInt32(data, 0);
            string command = Encoding.Default.GetString(data, 4, lenght);
            return command;
        }

        public byte[] parseData(byte[] data)
        {
            int size = data.Length;
            int lenght = BitConverter.ToInt32(data, 0);
            byte[] rez = new byte[size - 4 - lenght];
            for (int i = 0; i < rez.Length; i++)
            {
                rez[i] = data[i + lenght + 4];
            }
            return rez;
        }

        public void addCommand(string text, Command command)
        {
            protocol.Add(text, command);
        }

        byte[] error(byte[] data)
        {
            return Encoding.Default.GetBytes("Command not found");
        }

    }
}
