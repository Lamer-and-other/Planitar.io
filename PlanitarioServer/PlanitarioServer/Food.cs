using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanitarioServer
{
    class Food
    {
        public int   Bonus;    // Сколько получит пользователь за эту еду; Размер еды зависит от бонуса
        public Point Position; // Позиция ловушки
        public Color Color;    // Цвет ловушки
        public Food(Point p)
        {
            Position = p;
        }
    }
}
