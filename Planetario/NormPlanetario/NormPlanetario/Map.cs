using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NormPlanetario
{
    class Map
    {
        Size MapSize;
        List<Player> Players;
        List<Food> Foods;
        List<Trap> Traps;
        Random rand = new Random();

        ulong score;  // счет очков пока в карте

        public Map()
        {
            MapSize = new Size(GameConst.MapWidth, GameConst.MapHeight);

            score = GameConst.PlayerMinScore;

            // первый разброс еды по карте
            Foods = new List<Food>();
            for (int i = 0; i < GameConst.FoodMaxOnMap; i++)
            {
                Food food = new Food(rand);
                Foods.Add(food);
            }
        }
        public ulong Score { get { return score; } }

        public void DrawFood(Graphics g)
        {
            foreach (Food food in Foods)
            {
                food.Draw(g);
            }
        }

        // пока нет player еду ест курсор на карте
        public void Eat(MouseEventArgs e)
        {
            foreach(Food food in Foods)
            {
                score += (ulong)food.Destruction(e, rand);  // еда возвращает очки и меняет своё местоположение
            }
        }

        void AddPlayer(Player player)
        {

        }
        void RemovePlayer(uint id)
        {

        }
    }
}
