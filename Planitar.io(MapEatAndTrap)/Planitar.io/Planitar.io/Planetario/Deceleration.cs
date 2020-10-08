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
        Rectangle rect;//Определяет расположение и размер 
        float coefficient = -0.5f;//коофициент скорости
        Color color = Color.FromArgb(40, 255, 255, 150);

        public Deceleration(Random rand)
        {
            rect = new Rectangle(new Point(rand.Next(1, GameConst.MapWidth), rand.Next(1, GameConst.MapHeight)), new Size(50, 50));
        }

        public Rectangle Rect { get { return rect; } }

        // перерандом позиции
        public void ReRandom(Random rand)
        {
            rect = new Rectangle(new Point(rand.Next(1, GameConst.MapWidth), rand.Next(1, GameConst.MapHeight)), new Size(50, 50));
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(new SolidBrush(color), rect);
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
        public void Action(CurrentPlayer player, int locationX, int locationY, int sizeX, int sizeY)
        {
            bool conteins = rect.IntersectsWith(new Rectangle(locationX, locationY, sizeX, sizeY));

            if (conteins)
            {
                player.Speed *= coefficient;
            }
        }
    }
}
