using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanitarioServer
{
    delegate void messages(string messages);
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            server.servermeesage = printMessage;
            server.Start();
        }
        static void printMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
