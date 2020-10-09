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
            int[] mass = new int[10] { 5, 3, 7, 2, 9, 6, 3, 9, 3, 10 };
            Map.QuickSort(ref mass, 0, mass.Count() - 1); 
            for(int i = 0; i < mass.Count(); i++)
            {
                Console.Write(mass[i] + " "); 
            }
            Console.WriteLine(); 

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
