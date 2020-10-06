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
        
        public Food(Random r,Size mapSize)
        {
            Bonus = RandBonus(r);
            Position = RandPosition(r,mapSize);
            Color = RandColor(r);
        }

        int RandBonus(Random r)
        {
            int number = r.Next(GameConst.FoodMinBonus, GameConst.FoodMaxBonus + 1);
            return number;
        }

        Point RandPosition(Random r, Size mapSize)
        {
            Point pos = new Point(r.Next(0,mapSize.Width),r.Next(0, mapSize.Height));
            return pos;
        } 

        Color RandColor(Random r)
        {
            Color col = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
            return col;
        }
    }
}
