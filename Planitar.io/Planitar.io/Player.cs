using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Planitar.io
{
    class Player
    {
        public static List<Player> playerList = new List<Player>(); 
        // игрок текущего клиента 
        public static Player myseft { set; get; } 
        public static string oldName { set; get; }
        public int id { set; get; }             // Ид игрока
        public string Nickname { set; get; }         // Ник игрока
        public int Score = 0;          // Счёт игрока
        public long Record = 0;           // Рекорд игрока
        public bool isAlive = false;                // жив ли игрок 
        public bool itsMe = false; 
        public float MapScale = 0;         // Текущий масштаб карты
        public float Speed = 1;            // Текущая скорость игрока

        public bool BonusSpeed = false;        // Включено ли ускорение у игрока

        public uint LostScoresOnStep { set; get; }   // Количество очков которые мы теряем, например каждые 5 

        public Point Position { set; get; }       // Позиция игрока    
        public Point PlusCoordinates; // Координаты которые сдвигают карту для определенного пользователя, клиент

        public Rectangle Сollision = new Rectangle();
        public Color Color { set; get; }         // Цвет игрока

        // инициализация нас 
        public Player(int id, string Nickname, Color color)
        {
            this.id = id; 
            this.Nickname = Nickname;
            this.MapScale = 1f; 
            this.Speed = 0f;
            this.BonusSpeed = false; 
            this.LostScoresOnStep = 0;
            this.Color = color; 
            this.PlusCoordinates = new Point(0, 0);
            Сollision = new Rectangle(0, 0, 0, 0); 
        }

        // инициализания других игроков 
        public Player(Player player, Color color)
        {
            this.id = id;
            this.Nickname = Nickname;
        }
        
        public void ChangeSize(int x) 
        {
            Score += x;
            Сollision.Width += x; 
            Сollision.Height += x;
            Form1.thisForm.ChangeCenter(); 
        }
        public static Player getPlayer(int id)
        {
            foreach (Player player in Player.playerList)
            {
                if (player.id == id)
                {
                    return player;
                }
            }
            return null;
        }
    }
}
