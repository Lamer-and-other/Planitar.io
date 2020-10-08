using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Planetario
{
    class Food
    {
        int bonus;    // Сколько получит пользователь за эту еду; Размер еды зависит от бонуса
        Rectangle Collider; // коллайдер еды
        Color color;

        public Food(Random rand)
        {
            bonus = rand.Next(GameConst.FoodMinBonus, GameConst.FoodMaxBonus + 1);
            // размеры коллайдера еды
            Collider = new Rectangle(new Point(rand.Next(1, GameConst.MapWidth), rand.Next(1, GameConst.MapHeight)), new Size(bonus * 3 + 10, bonus * 3 + 10));
            color = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
        }

        public int Bonus { get { return bonus; } }

        public bool Try_Eat(MouseEventArgs e)
        {
            if (Collider.Contains(e.Location))
                return true;
            else
                return false;
        }

        public void Destruction(Random rand)// смена позиции, бонуса и цвета
        {
            bonus = rand.Next(GameConst.FoodMinBonus, GameConst.FoodMaxBonus + 1);
            Collider = new Rectangle(new Point(rand.Next(1, GameConst.MapWidth), rand.Next(1, GameConst.MapHeight)), new Size(bonus * 3 + 10, bonus * 3 + 10));
            color = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
        }

        public void Draw(Graphics g)
        {
            g.FillEllipse(new SolidBrush(color), Collider);
        }
    }
}
