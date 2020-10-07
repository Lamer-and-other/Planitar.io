using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planetario
{
    class Food
    {
        int   Bonus;    // Сколько получит пользователь за эту еду; Размер еды зависит от бонуса
        Point Position; // Позиция ловушки
        Color Color;    // Цвет ловушки
        Rectangle ellips;
        
        public Food(Random r,Size mapSize)
        {
            Bonus = r.Next(GameConst.FoodMinBonus, GameConst.FoodMaxBonus + 1);
            Position = new Point(r.Next(0, mapSize.Width), r.Next(0, mapSize.Height));
            Color = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
            ellips = new Rectangle(Position, new Size(Bonus + 10, Bonus + 10));
        }

        public void Paint(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color), ellips);
            g.DrawEllipse(new Pen(Color.Black), ellips);
        }
    }
}
