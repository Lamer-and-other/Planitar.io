using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planetario
{
    class Deceleration : Trap
    {
        public Deceleration(Random rand,Rectangle MapRectangle)
        {
            Collise = new Rectangle(new Point(rand.Next(MapRectangle.X, MapRectangle.Width + MapRectangle.X - 100), rand.Next(MapRectangle.Y, MapRectangle.Height + MapRectangle.Y - 100)), new Size(100, 100));
            Color = Color.FromArgb(40, 0, 0, 0);
        }

        public override Rectangle Rect { get { return Collise; } }

        // перерандом позиции
        public void ReRandom(Random rand, Rectangle MapRectangle)
        {
            Collise = new Rectangle(new Point(rand.Next(MapRectangle.X, MapRectangle.Width + MapRectangle.X), rand.Next(MapRectangle.Y, MapRectangle.Height + MapRectangle.Y)), new Size(50, 50));
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color), Collise);
        }

        /// <summary>
        /// Выполняет замедленеие игрока, если он попадает 
        /// в прямоугольник ловушки
        /// </summary>
        /// <param name="player">Игрок</param>
        /// <param name="locationX"></param>
        /// <param name="locationY"></param>
        /// <param name="sizeX"></param>
        /// <param name="sizeY"></param>
        public int Action(CurrentPlayer player, int speed)
        {
            return speed /= 3;
        }

        public bool Try_Action(CurrentPlayer player)
        {
            if (Collise.IntersectsWith(player.ThisPlayer.Сollision))
                return true;
            else
                return false;
        }
    }
}
