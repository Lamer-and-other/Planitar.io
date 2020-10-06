using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planetario
{
    class Map
    {
        Random r = new Random();
        Size         MapSize;
        List<Player> Players = new List<Player>();
        List<Food>   Foods = new List<Food>();
        List<Trap>   Traps = new List<Trap>();

        public Map()
        {
            MapSize = new Size(2000,2000);
            AddFood();
            AddTrap();
        }

        void AddTrap()
        {
            if (Traps.Count == 0)
            {
                for (int i = 0; i < GameConst.FoodMaxOnMap; i++)
                {
                    Trap f = new Trap(r, MapSize);
                    Trap.Add(f);
                }
            }
        }

        void AddFood()
        {
            if(Foods.Count == 0)
            {
                for(int i = 0; i < GameConst.FoodMaxOnMap; i++) 
                {
                    Food f = new Food(r,MapSize);
                    Foods.Add(f);
                }
            }
        }

        void AddPlayer(Player player)
        {
            Players.Add(player);
        }

        void RemovePlayer(Player player)
        {
            Players.Remove(player);
        }
    }
}
