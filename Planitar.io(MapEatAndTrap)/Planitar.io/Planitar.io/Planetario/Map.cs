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
        Random r = new Random();
        Size MapSize;
        List<Player> Players = new List<Player>();
        List<Food> Foods = new List<Food>();
        List<Trap> Traps = new List<Trap>();

        public Map()
        {
            MapSize = new Size(GameConst.MapWidth, GameConst.MapHeight);
            AddFood();
            AddTrap();
        }

        void AddTrap()
        {
            for (int i = 0; i < GameConst.TrapMaxOnMap; i++)
            {
                bool flag = true;
                Deceleration t = new Deceleration(r);

                while (flag)
                {
                    flag = false;

                    foreach (Deceleration dec in Traps)
                    {
                        if(t.Rect.IntersectsWith(dec.Rect))
                        {
                            t.ReRandom(r);
                            flag = true;
                        }
                    }
                }

                Traps.Add(t);
            }
        }

        void AddFood()
        {
            for (int i = 0; i < GameConst.FoodMaxOnMap; i++)
            {
                Food f = new Food(r);
                Foods.Add(f);
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

        public void Eat(MouseEventArgs e, Player p)
        {
            foreach (Food food in Foods)
            {
                if (food.Try_Eat(e)) // еда возвращает очки и меняет своё местоположение если мы попадаем в условие
                {
                    p.Score += (ulong)food.Bonus;
                    food.Destruction(r);
                    break;
                }
            }
        }

        public void DrawScore(Graphics g, Player p)
        {
            g.DrawString("Score: " + p.Score, new Font("Arial", 10), Brushes.White, 10, 10);
        }

        public void Paint_old(Graphics g)
        {
            foreach (Food food in Foods)
            {
                food.Draw(g);
            }
            foreach (Deceleration dec in Traps)
            {
                dec.Draw(g);
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
