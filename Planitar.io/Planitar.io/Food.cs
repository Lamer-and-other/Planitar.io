using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;


namespace Planitar.io
{
    class Food
    {
        int id = 0; 
        int bonus;    // Сколько получит пользователь за эту еду; Размер еды зависит от бонуса
        public Rectangle Сollision; // коллайдер еды
        public Color color;

        public Food() { }
        
        public Food(Point location, Random rand, int bonus, int id)
        {
            Сollision = new Rectangle(new Point(location.X, location.Y), new Size(bonus * 3 + 10, bonus * 3 + 10));
            color = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
            this.bonus = bonus;
            this.id = id; 
        }
        
        public int Bonus { get { return bonus; } }

        public bool Try_Eat(Player player, Point center_food)
        {
            Point centre_player = new Point(player.Сollision.X + player.Сollision.Width / 2, player.Сollision.Y + player.Сollision.Height / 2);

            if (Math.Pow((center_food.Y - centre_player.Y), 2) + Math.Pow((center_food.X - centre_player.X), 2) <= Math.Pow(player.Сollision.Width / 2, 2))
                return true; 
            else
                return false;
        }

        public void Destruction(Food food)// смена позиции, бонуса и цвета
        {
            //bonus = rand.Next(GameConst.FoodMinBonus, GameConst.FoodMaxBonus + 1);
            //Сollision = new Rectangle(new Point(rand.Next(MapRectangle.X, MapRectangle.Width + MapRectangle.X), rand.Next(MapRectangle.Y, MapRectangle.Height + MapRectangle.Y)), new Size(bonus * 3 + 10, bonus * 3 + 10));
            //color = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
            Сollision = new Rectangle(new Point(food.Сollision.X, food.Сollision.Y), new Size(food.bonus * 3 + 10, food.bonus * 3 + 10)); 
        }

        public void Draw(Graphics g)
        {
            g.FillEllipse(new SolidBrush(color), Сollision);
        }

        public static Food searchFood(int id, List<Food> foodList)
        {
            foreach(Food oneFood in foodList) 
            {
                if (oneFood.id == id)
                    return oneFood;
            }
            return null; 
        }
    }
}
