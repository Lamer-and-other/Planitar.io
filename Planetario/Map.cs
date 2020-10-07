using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Planetario
{
    class Map
    {
        Random       r = new Random();
        public Size         MapSize;
        List<Player> Players = new List<Player>();
        List<Food>   Foods = new List<Food>();
        List<Trap>   Traps = new List<Trap>();

        public Map()
        {
            MapSize = new Size(GameConst.MaxWidthMap,GameConst.MaxHeightMap);
            AddFood();
            //AddTrap();
        }

        /*void AddTrap()
        {
            if (Traps.Count == 0)
            {
                for (int i = 0; i < GameConst.FoodMaxOnMap; i++)
                {
                    Trap f = new Trap(r, MapSize);
                    Trap.Add(f);
                }
            }
        }*/

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

        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }

        public void RemovePlayer(Player player)
        {
            Players.Remove(player);
        }

        public void Paint_old(Graphics g)
        {
            for(int i = 0; i < Foods.Count; i++)
            {
                Foods[i].Paint(g);
            }
        }

        public void Scene(Graphics g, Panel panel)
        {
            Bitmap bmp = new Bitmap(panel.Width, panel.Height, g);
            Graphics g1 = Graphics.FromImage(bmp);

            g1.Clear(panel.BackColor);
            Paint_old(g1);

            g.DrawImage(bmp, 0, 0);
        }
    }
}
