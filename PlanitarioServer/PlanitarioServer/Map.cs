using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanitarioServer
{
    class Map
    {
        Size         MapSize;
        public static List<Player> Players = new List<Player>(); 
        List<Food>   Foods;
        List<Trap>   Traps;
        // static потому что карта у нас будет одна и можно пока так оставить 
        public static Publisher globalPublisher = new Publisher();  
        
        public static void AddPlayer(Player player)
        {
            Players.Add(player); 
        }
        public static void RemovePlayer(Player player)
        {
            Players.Remove(player);
        }
    }
}
