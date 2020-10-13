using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing; 

namespace Planitar.io
{
    class Deceleration : Trap
    {
        public Rectangle Сollision;//Определяет расположение и размер 
        float coefficient = -0.5f;//коофициент скорости
        public Color color = Color.FromArgb(40, 0, 0, 0);

        public Deceleration(Point location, Rectangle MapRectangle)
        {
            Сollision = new Rectangle(new Point(location.X, location.Y), new Size(50, 50));
        }
        
        public Rectangle Rect { get { return Сollision; } }

        // перерандом позиции
        public void ReRandom(Random rand, Rectangle MapRectangle)
        {
            Сollision = new Rectangle(new Point(rand.Next(MapRectangle.X, MapRectangle.Width + MapRectangle.X), rand.Next(MapRectangle.Y, MapRectangle.Height + MapRectangle.Y)), new Size(50, 50));
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(new SolidBrush(color), Сollision);
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
        public void Action(Player currentPlayer, int locationX, int locationY, int sizeX, int sizeY)
        {
            bool conteins = Сollision.IntersectsWith(new Rectangle(locationX, locationY, sizeX, sizeY));

            if (conteins)
            {
                currentPlayer.Speed *= coefficient;
            }
        }
    }
}
