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
        public ulong Score = 0;          // Счёт игрока
        public ulong Record = 0;           // Рекорд игрока
        public bool isAlive = false;                // жив ли игрок 
        public bool itsMe = false; 
        public float MapScale = 0;         // Текущий масштаб карты
        public float Speed = 1;            // Текущая скорость игрока

        public bool BonusSpeed = false;        // Включено ли ускорение у игрока

        public uint LostScoresOnStep { set; get; }   // Количество очков которые мы теряем, например каждые 5 

        public Point Position { set; get; }       // Позиция игрока
        public Color Color { set; get; }         // Цвет игрока
        

        Rectangle ellipce = new Rectangle();

        public Player(int id, string Nickname)
        {
            this.id = id; 
            this.Nickname = Nickname;
        }

        
    }
}
