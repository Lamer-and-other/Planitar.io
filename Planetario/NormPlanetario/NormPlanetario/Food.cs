using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NormPlanetario
{
    class Food
    {
        int Bonus;    // Сколько получит пользователь за эту еду; Размер еды зависит от бонуса
        Point Position; // Позиция еды
        Rectangle Collider; // коллайдер еды
        Color color;

        public Food(Random rand)
        {
            Bonus = rand.Next(GameConst.FoodMinBonus, GameConst.FoodMaxBonus);
            Position = new Point(rand.Next(1, GameConst.MapWidth), rand.Next(1, GameConst.MapHeight));

            // размеры коллайдера еды
            Collider = new Rectangle(Position, new Size(10, 10));

            color = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
        }

        // поедание
        public int Destruction(MouseEventArgs e, Random rand)
        {
            if (e.X > Collider.Left && e.X < Collider.Right && e.Y > Collider.Top && e.Y < Collider.Bottom)
            {
                Position.X = rand.Next(1, GameConst.MapWidth);
                Position.Y = rand.Next(1, GameConst.MapHeight);
                Collider.X = Position.X;
                Collider.Y = Position.Y;
                return Bonus;
            }
            else
            {
                return 0;
            }
        }

        public void Draw(Graphics g)
        {
            g.FillEllipse(new SolidBrush(color), Position.X, Position.Y, Collider.Width, Collider.Height);
        }
    }
}
