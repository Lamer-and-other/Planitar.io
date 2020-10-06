using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Shapes;

namespace PlanitarioServer 
{
    // я изменил класс Паши потому что текущего игрока, мы будем определять по его id, 
    // который мы получим от клиента и сервер будет работать с этим клиетом в отдельном потоке (в функции exchange)   
    // я не вижу смысла создавать для этого отдельный класс CurrentPlayer 
      
    class Player
    {
        public static List<Player> playerList = new List<Player>();

        public uint id { set; get; }             // Ид игрока
        public string Nickname { set; get; }         // Ник игрока
        public ulong Score = 0;          // Счёт игрока
        public ulong Record = 0;           // Рекорд игрока
        public bool isAlive = false;                // жив ли игрок 

        public float MapScale = 0;         // Текущий масштаб карты
        public float Speed = 1;            // Текущая скорость игрока

        public bool BonusSpeed = false;        // Включено ли ускорение у игрока
        
        public uint LostScoresOnStep { set; get; }   // Количество очков которые мы теряем, например каждые 5 

        public Point Position { set; get; }       // Позиция игрока
        public Color Color { set; get; }         // Цвет игрока
        
        public MyService service = new MyService(); // класс для расшифровки-обработки полученый от клиента данных   
        //Ellipse Collise;

        public Player(string Nickname)
        {
            this.Nickname = Nickname;
        }
        
    }

}


