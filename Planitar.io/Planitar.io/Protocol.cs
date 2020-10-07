using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planitar.io
{
    delegate void Command(byte[] data);
    class Protocol
    {
        Dictionary<string, Command> protocol = new Dictionary<string, Command>();

        // формирование протокола для клиента 
        public static Protocol createProtocol(Canal c)
        {

            Protocol protocol = new Protocol();
            protocol.addCommand("GETSOMENICK", c.getMessage);
            protocol.addCommand("GET_CHANGED_DATA", c.newData); 
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
            try
            {
                int lenght = BitConverter.ToInt32(data, 0);
                string command = Encoding.Default.GetString(data, 4, lenght);
                return command;
            }
            catch { }
            return ""; 
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

        void error(byte[] data)
        {

        }

    }
}
