using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;


namespace Planitar.io
{
    class Food
    {
        int bonus;    // Сколько получит пользователь за эту еду; Размер еды зависит от бонуса
        public Rectangle Сollision; // коллайдер еды
        public Color color;

        public Food() { }
        
        public Food(Point location, Rectangle MapRectangle)
        {
            Random rand = new Random(); 
            bonus = rand.Next(GameConst.FoodMinBonus, GameConst.FoodMaxBonus + 1);
            // размеры коллайдера еды
            Сollision = new Rectangle(new Point(location.X, location.Y), new Size(bonus * 3 + 10, bonus * 3 + 10));
            color = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
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
        }

        public void Draw(Graphics g)
        {
            g.FillEllipse(new SolidBrush(color), Сollision);
        }
    }
}
